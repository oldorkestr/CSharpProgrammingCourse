using System;
using System.Collections.Generic;

namespace CSharpProgrammingCourse.Lecture6
{
    // 1. Узагальнений інтерфейс
    public interface IReport<T> where T : struct
    {
        void AddData(T item);
        void Clear();
        void PrintTable();
    }

    // 2. Узагальнений клас із обмеженням (Constraint)
    // where T : struct — гарантує, що ми працюємо лише з числами, датами, bool тощо.
    public class ReportGenerator<T> : IReport<T> where T : struct
    {
        private List<T> _data = new List<T>();
        private readonly int _fixedRows;

        public ReportGenerator(int fixedRows = 5)
        {
            _fixedRows = fixedRows;
        }

        public void AddData(T item)
        {
            _data.Add(item);
            Console.WriteLine($"Додано запис типу {typeof(T).Name}: {item}");
        }

        public void Clear()
        {
            _data.Clear();
        }

        // 3. Засоби узагальнених класів (typeof, default)
        public void PrintTable()
        {
            Console.WriteLine(new string('-', 30));
            // Використання typeof(T) для отримання імені типу під час виконання
            Console.WriteLine($"ЗВІТ ПО ТИПУ: {typeof(T).Name.ToUpper()}");
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("| Індекс | Значення           |");
            Console.WriteLine(new string('-', 30));

            for (int i = 0; i < _fixedRows; i++)
            {
                T value;
                if (i < _data.Count)
                {
                    value = _data[i];
                }
                else
                {
                    // Використання default(T) для заповнення порожніх клітинок
                    // Для int це буде 0, для double 0.0, для DateTime 01.01.0001 тощо.
                    value = default(T); 
                }

                Console.WriteLine($"| {i + 1,-6} | {value,-18} |");
            }

            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"Всього реальних записів: {_data.Count}\n");
        }
    }

    public static class Lecture6
    {
        public static void Demo6()
        {
            // Робота з цілими числами (int - struct)
            var intReport = new ReportGenerator<int>(3);
            intReport.AddData(150);
            intReport.AddData(42);
            intReport.PrintTable();

            // Робота з датами (DateTime - struct)
            var dateReport = new ReportGenerator<DateTime>(4);
            dateReport.AddData(DateTime.Now);
            dateReport.AddData(new DateTime(2026, 1, 1));
            dateReport.PrintTable();

            // Спроба використати посилальний тип призведе до помилки компіляції:
            // var stringReport = new ReportGenerator<string>(); // Помилка! string не є struct
        }
    }
}