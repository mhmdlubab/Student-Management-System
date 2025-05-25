using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.Json;

namespace assignment_c__1._1
{
    public static class StudentDataManager
    {
        private static string filePath = "students.json";

        public static void Save(List<Student> students)
        {
            string json = JsonSerializer.Serialize(students);
            File.WriteAllText(filePath, json);
        }

        public static List<Student> Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Student>>(json);
            }
            return new List<Student>();
        }
    }
}

