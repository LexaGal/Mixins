using System;
using Mixing.Messages;

namespace Mixing.TargetTypeAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    class TargetTypeAttribute : Attribute
    {
        public Type TargetClass { get; private set; }

        public TargetTypeAttribute(Type targetClass, Type baseType)
        {
            if (targetClass != baseType && !targetClass.IsSubclassOf(baseType))
            {
                throw new ApplicationException(string.Format("Target class must be subclass of {0}", baseType.Name));
            }
            TargetClass = targetClass;
        }
    }
}