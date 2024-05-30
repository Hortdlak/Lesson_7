using System.Reflection;
using System.Text;

namespace Lesson_7
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TestClass testClass = new TestClass(10, new char[] { 'A', 'B', 'C' }, "Hello", 10.01m, "Hi");


            string objectStr = ObjectToString(testClass);
            Console.WriteLine("Объект в строку:");
            Console.WriteLine(objectStr);


            Console.WriteLine("\nВосстанавливаем объект:");
            TestClass restoredClass = (TestClass)StringToObject(objectStr);
            Console.WriteLine($"I: {restoredClass.I}");
            Console.WriteLine($"C: {new string(restoredClass.C ?? Array.Empty<char>())}");
            Console.WriteLine($"S: {restoredClass.S}");
            Console.WriteLine($"D: {restoredClass.D}");
            Console.WriteLine($"F: {restoredClass.F}");
            Console.ReadLine();

        }

        static string ObjectToString(object obj)
        {
            StringBuilder sb = new StringBuilder();
            Type type = obj.GetType();

            sb.AppendLine(type.Assembly.ToString());
            sb.AppendLine(type.FullName);

            foreach (var prop in type.GetProperties())
            {
                if (ShouldSkipProperty(prop)) continue;

                string propName = GetCustomNameOrDefault(prop);
                sb.Append($"{propName}:");
                sb.AppendLine(ConvertPropertyValueToString(prop.GetValue(obj)));
            }

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (ShouldSkipField(field)) continue;

                string fieldName = GetCustomNameOrDefault(field);
                sb.Append($"{fieldName}:");
                sb.AppendLine(ConvertFieldValueToString(field.GetValue(obj)));
            }

            return sb.ToString();
        }

        static object StringToObject(string s)
        {
            string[] lines = s.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2) throw new ArgumentException("Invalid input string format.");

            string assemblyPath = Assembly.GetExecutingAssembly().Location; // Путь к исполняемой сборке
            string typeName = lines[1].Trim(); // Имя типа

            Assembly assembly = Assembly.LoadFrom(assemblyPath); // Загружаем сборку
            if (assembly == null) throw new ArgumentException("Assembly not found.");

            Type type = assembly.GetType(typeName, true); // Получаем тип из сборки
            if (type == null) throw new ArgumentException("Type not found.");

            object obj = Activator.CreateInstance(type) ?? throw new InvalidOperationException("Unable to create instance of type.");

            // Остальной код остается без изменений

            return obj;
        }

        static bool ShouldSkipProperty(PropertyInfo prop)
        {
            return prop.GetCustomAttribute<DontSaveAttribute>() != null ||
                   prop.GetCustomAttribute<CustomNameAttribute>()?.SkipSave == true;
        }

        static bool ShouldSkipField(FieldInfo field)
        {
            return field.GetCustomAttribute<CustomNameAttribute>()?.SkipSave == true;
        }

        static string GetCustomNameOrDefault(MemberInfo member)
        {
            return member.GetCustomAttribute<CustomNameAttribute>()?.CustomName ?? member.Name;
        }

        static string ConvertPropertyValueToString(object value)
        {
            if (value is char[] charArray) return new string(charArray);
            return value?.ToString() ?? string.Empty;
        }

        static string ConvertFieldValueToString(object value)
        {
            if (value is char[] charArray) return new string(charArray);
            return value?.ToString() ?? string.Empty;
        }
    }
}
