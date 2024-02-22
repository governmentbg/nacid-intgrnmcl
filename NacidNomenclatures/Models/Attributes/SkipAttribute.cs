using System;
using System.Reflection;

namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class SkipAttribute : Attribute
    {
        public static bool IsDeclared(PropertyInfo propertyInfo)
            => propertyInfo.GetCustomAttribute(typeof(SkipAttribute)) != null;
    }
}
