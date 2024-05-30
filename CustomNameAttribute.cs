using System;

namespace Lesson_7
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CustomNameAttribute : Attribute
    {
        public string CustomName { get; }
        public bool SkipSave { get; }

        public CustomNameAttribute(string customName, bool skipSave = false)
        {
            CustomName = customName;
            SkipSave = skipSave;
        }
    }
}

