using System;
using UnityEngine;

namespace PurpleFlowerCore.Utility
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ConfigurableAttribute : Attribute
    {
        public string MenuName { get; }
        public string[] Tags { get; }
        
        public ConfigurableAttribute(string menuName = "", params string[] tags)
        {
            MenuName = menuName;
            Tags = tags;
        }
    }
}