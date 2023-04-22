using System;

namespace AB_Utility.LeoEcsLiteEntityConverter.Generator.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentConverterAttribute : Attribute
    {
        public Type ComponentType { get; }

        public ComponentConverterAttribute(Type componentType)
        {
            ComponentType = componentType;
        }
    }
}