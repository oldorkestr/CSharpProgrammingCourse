using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpProgrammingCourse.Lecture89
{
    // 1. Користувацький атрибут для визначення формату
    [AttributeUsage(AttributeTargets.Class)]
    public class SerializationFormatAttribute : Attribute
    {
        public string Format { get; } // "XML" або "Binary"
        public SerializationFormatAttribute(string format) => Format = format;
    }

    // 2. Клас даних (Метадані), які ми будемо обробляти
    [SerializationFormat("XML")]
    [Serializable]
    public class DataPayload
    {
        private string _status = "New";
        private DateTime _processedAt;

        public string Name { get; set; }
        public string Content { get; set; }

        // Приватний метод для зміни внутрішнього стану
        private void MarkAsProcessed()
        {
            _status = "Processed";
            _processedAt = DateTime.Now;
        }

        public override string ToString() => 
            $"[Name: {Name}, Status: {_status}, Date: {_processedAt}]";
    }

    public static class Lecture89
    {  
        public static void Demo()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            string inDir = Path.Combine(root, "In");
            string outDir = Path.Combine(root, "Out");
            string archiveDir = Path.Combine(root, "Archive");

            // Створення папок, якщо їх немає
            Directory.CreateDirectory(inDir);
            Directory.CreateDirectory(outDir);
            Directory.CreateDirectory(archiveDir);

            Console.WriteLine("Диспетчер запущено. Очікування файлів у папці In...");

            // Демонстрація: Створимо тестовий файл, якщо папка порожня
            PrepareTestFile(inDir);

            foreach (var filePath in Directory.GetFiles(inDir))
            {
                try
                {
                    ProcessFile(filePath, outDir, archiveDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка обробки {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }

        static void ProcessFile(string filePath, string outDir, string archiveDir)
        {
            string fileName = Path.GetFileName(filePath);
            Console.WriteLine($"--- Обробка: {fileName} ---");

            // 1. Десеріалізація (припустимо, що ми знаємо базовий тип для прикладу)
            DataPayload data;
            XmlSerializer serializer = new XmlSerializer(typeof(DataPayload));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                data = (DataPayload)serializer.Deserialize(fs);
            }
            Console.WriteLine($"Зчитано: {data}");

            // 2. Reflection: Дослідження типу та атрибутів
            Type type = data.GetType();
            var attr = type.GetCustomAttribute<SerializationFormatAttribute>();
            Console.WriteLine($"Тип: {type.Name}, Цільовий формат за атрибутом: {attr?.Format}");

            // 3. Reflection: Доступ до приватного методу та його динамічний виклик
            MethodInfo method = type.GetMethod("MarkAsProcessed", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(data, null);
            Console.WriteLine("Викликано приватний метод MarkAsProcessed через Reflection.");

            // 4. Маніпулювання файлами (Переміщення в архів)
            string archivePath = Path.Combine(archiveDir, fileName);
            if (File.Exists(archivePath)) File.Delete(archivePath);
            File.Move(filePath, archivePath);

            // 5. Серіалізація та запис у папку Out
            string outPath = Path.Combine(outDir, "Processed_" + fileName);
            using (FileStream fs = new FileStream(outPath, CreateMode(attr?.Format), FileAccess.Write))
            {
                serializer.Serialize(fs, data);
            }

            Console.WriteLine($"Результат збережено в Out. Оригінал переміщено в Archive.");
        }

        static FileMode CreateMode(string format) => FileMode.Create;

        static void PrepareTestFile(string path)
        {
            string testFile = Path.Combine(path, "test_data.xml");
            if (!File.Exists(testFile))
            {
                var testObj = new DataPayload { Name = "Завдання 20", Content = "Тестовий вміст" };
                XmlSerializer ser = new XmlSerializer(typeof(DataPayload));
                using (StreamWriter sw = new StreamWriter(testFile))
                {
                    ser.Serialize(sw, testObj);
                }
                Console.WriteLine("Створено тестовий XML файл.");
            }
        }
    }
}