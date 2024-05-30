namespace Lesson_7
{
    internal class TestClass
    {
        [DontSave]
        public int I { get; set; }

        [CustomName("CustomCharArray", true)]
        public char[]? C { get; set; }

        public string? S { get; set; }
        public decimal D { get; set; }

        [CustomName("CustomFieldName")]
        public string F = "Hello";

        private Type type;
        private object[] objects;

        public TestClass(int i)
        {
            I = i;
        }

        public TestClass()
        {
        }

        public TestClass(int i, char[]? c, string? s, decimal d, string f) : this(i)
        {
            C = c;
            S = s;
            D = d;
            F = f;
        }

        public TestClass(Type type, object[] objects)
        {
            this.type = type;
            this.objects = objects;
        }
    }
}
