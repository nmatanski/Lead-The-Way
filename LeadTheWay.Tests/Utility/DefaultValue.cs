using System;

namespace LeadTheWay.Tests.Utility
{
    public static class DefaultValue
    {
        public static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }
    }
}
