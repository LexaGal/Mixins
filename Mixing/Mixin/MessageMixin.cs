using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mixing.Messages;
using Mixing.MessagesSenders;
using Mixing.TargetTypeAttributes;

namespace Mixing.Mixin
{
    public static class Mixin
    {
        private static readonly Dictionary<Type, Func<object>> SendersDictionary;

        private static readonly Exception InitException;

        static Mixin()
        {
            try
            {
                SendersDictionary = new Dictionary<Type, Func<object>>();

                var typesWithTargetTypeAttributes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Select(
                        t => new
                        {
                            Type = t,
                            Attributes =
                                t.GetCustomAttributes(typeof (TargetTypeAttribute), false)
                                    .ToList()
                                    .ConvertAll(o => o as TargetTypeAttribute)
                        })
                    .Where(obj => obj.Attributes.Count != 0).ToList();


                List<Type> targetClasses = new List<Type>();

                foreach (var type in typesWithTargetTypeAttributes)
                {
                    type.Attributes.ForEach(a => targetClasses.Add(a.TargetClass));
                }

                targetClasses = targetClasses.Distinct().ToList();

                foreach (var tc in targetClasses)
                {
                    int count = 0;
                    foreach (var twtta in typesWithTargetTypeAttributes)
                    {
                        if (twtta.Attributes.Any(a => a.TargetClass == tc)) count++;
                    }
                    if (count > 1)
                    {
                        throw new ApplicationException(string.Format("{0} has more then one binding", tc.FullName));
                    }
                }

                foreach (var type in typesWithTargetTypeAttributes)
                {
                    var t = type;
                    foreach (var tc in targetClasses.Where(tc => t.Attributes.Any(a => a.TargetClass == tc)))
                    {
                        SendersDictionary.Add(tc, () => Activator.CreateInstance(t.Type));
                    }
                }
            }
            catch (Exception ex)
            {
                InitException = ex;
            }
        }

        private static ISender<T> GetSenderFor<T>(Type mesType) where T: class 
        {
            if (InitException != null)
            {
                throw new ApplicationException(
                    string.Format("Mixin exception: {0}", InitException.Message), InitException);
            }

            if (!SendersDictionary.ContainsKey(mesType))
            {
                throw new ApplicationException(
                    string.Format("Mixin exception: {0} has no binding", mesType.FullName));
            }
            return SendersDictionary[mesType]() as ISender<T>;
        }

        public static void Send<T>(this T t) where T: class 
        {
            GetSenderFor<T>(t.GetType()).Send(t);
        }
    }
}