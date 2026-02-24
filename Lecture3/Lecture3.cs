using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpProgrammingCourse.Lecture3
{
    // 1. Базовий клас команди
    public abstract class Command
    {
        public string Name { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public abstract void Execute();
    }

    // Похідні класи для демонстрації 'is' та 'as'
    public class TextCommand : Command 
    { 
        public string Content { get; set; }
        public override void Execute() => Console.WriteLine($"[Text] Executing: {Content}");
    }

    public class MathCommand : Command 
    { 
        public int Value { get; set; }
        public override void Execute() => Console.WriteLine($"[Math] Adding: {Value}");
    }

    // 2. Головний клас - Командний Стек
    public class CommandStack
    {
        private List<Command> _history = new List<Command>();

        // --- ІНДЕКСАТОР ---
        public Command this[int index]
        {
            get => index >= 0 && index < _history.Count ? _history[index] : null;
        }

        public void Push(Command cmd)
        {
            // --- NULL-COALESCING ASSIGNMENT (??=) ---
            cmd.Name ??= "Unnamed Command";
            
            _history.Add(cmd);
            cmd.Execute();
        }

        // --- ПЕРЕВАНТАЖЕННЯ ОПЕРАТОРА (--) для Undo ---
        public static CommandStack operator --(CommandStack stack)
        {
            if (stack._history.Count > 0)
            {
                var last = stack._history[^1];
                Console.WriteLine($"[Undo] Removing: {last.Name}");
                stack._history.RemoveAt(stack._history.Count - 1);
            }
            return stack;
        }

        // --- ПЕРЕВІРКА РІВНОСТІ ---
        public override bool Equals(object obj)
        {
            // --- ОПЕРАТОР AS ---
            var other = obj as CommandStack;
            if (other == null) return false;
            return this._history.Count == other._history.Count;
        }

        public static bool operator ==(CommandStack s1, CommandStack s2) => s1?.Equals(s2) ?? ReferenceEquals(s2, null);
        public static bool operator !=(CommandStack s1, CommandStack s2) => !(s1 == s2);

        // --- ЯВНЕ ПЕРЕТВОРЕННЯ ТИПІВ (Explicit) ---
        // Перетворюємо стек на масив назв команд
        public static explicit operator string[](CommandStack stack)
        {
            return stack._history.Select(c => c.Name).ToArray();
        }

        public void AnalyzeHistory()
        {
            Console.WriteLine("\n--- History Analysis ---");
            foreach (var cmd in _history)
            {
                // --- ОПЕРАТОР IS (Pattern Matching) ---
                if (cmd is TextCommand txt)
                {
                    Console.WriteLine($"Found text: {txt.Content?.ToUpper() ?? "EMPTY"}");
                }
                else if (cmd is MathCommand math)
                {
                    Console.WriteLine($"Found math value: {math.Value}");
                }

                // --- NULL-CONDITIONAL (?.) ---
                Console.WriteLine($"Command Timestamp: {cmd?.Timestamp.ToShortTimeString()}");
            }
        }
    }

    // 3. Тестування
    public static class Lecture3
    {
        public static void Test()
        {
            CommandStack myStack = new CommandStack();

            // Додаємо команди
            myStack.Push(new TextCommand { Content = "Hello C#", Name = "WriteText" });
            myStack.Push(new MathCommand { Value = 42, Name = "CalcSum" });
            myStack.Push(new TextCommand { Content = null }); // Тест на null

            // Використання індексатора та null-coalescing
            string firstCmdName = myStack[0]?.Name ?? "Unknown";
            Console.WriteLine($"\nFirst command in history: {firstCmdName}");

            // Використання перевантаженого оператора -- (Undo)
            myStack--; 

            // Аналіз за допомогою is/as
            myStack.AnalyzeHistory();

            // Явне перетворення
            string[] names = (string[])myStack;
            Console.WriteLine($"\nCommands in array: {string.Join(", ", names)}");
            
            Console.ReadKey();
        }
    }
}