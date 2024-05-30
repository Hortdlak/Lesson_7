using System;

namespace Lesson_7
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DontSaveAttribute : Attribute
    {
    }
}
