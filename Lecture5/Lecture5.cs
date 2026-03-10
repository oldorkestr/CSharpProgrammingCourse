using System;
using System.Collections.Generic;

namespace CSharpProgrammingCourse.Lecture5
{
    // 1. Визначення типу делегата для кроку бізнес-процесу
    // Контекст (string) передається посиланню, щоб кожен крок міг його змінити
    public delegate void WorkflowStepHandler(ref string context);

    // Клас-аргумент для події
    public class StepEventArgs : EventArgs
    {
        public string CurrentStatus { get; set; }
        public StepEventArgs(string status) => CurrentStatus = status;
    }

    // 2. Клас Відправник (Sender) - Рушій процесів
    public class WorkflowProcessor
    {
        // Визначення події
        public event EventHandler<StepEventArgs> OnStepReached;

        // Масив делегатів для кроків процесу
        private WorkflowStepHandler[] _steps;

        public WorkflowProcessor(WorkflowStepHandler[] steps)
        {
            _steps = steps;
        }

        public void Run(string initialContext)
        {
            string context = initialContext;
            Console.WriteLine($"--- Запуск процесу для: {context} ---");

            for (int i = 0; i < _steps.Length; i++)
            {
                // Виклик конкретного кроку з масиву
                _steps[i]?.Invoke(ref context);

                // 6. Взаємодія об'єктів через подію
                OnStepReached?.Invoke(this, new StepEventArgs(context));
            }
        }
    }

    // 3. Клас Отримувач (Receiver) - Спостерігач
    public class AuditService
    {
        public void Subscribe(WorkflowProcessor processor)
        {
            // Використання лямбда-виразу для підписки
            processor.OnStepReached += (sender, e) => 
                Console.WriteLine($"[AUDIT]: Крок пройдено. Статус: {e.CurrentStatus}");
        }
    }

    public static class Lecture5
    {
        public static void Demo5()
        {
            // 4. Використання простих та групових делегатів через масив
            WorkflowStepHandler[] pipeline = new WorkflowStepHandler[3];

            // Крок 1: Лямбда-вираз (Простий делегат у масиві)
            pipeline[0] = (ref string ctx) => {
                ctx += " -> Перевірка складу";
            };

            // Крок 2: Анонімний метод
            pipeline[1] = delegate(ref string ctx) {
                ctx += " -> Оплата отримана";
            };

            // Крок 3: Груповий делегат (Multicast)
            // Поєднуємо декілька дій в одну комірці масиву
            WorkflowStepHandler notifyStep = (ref string ctx) => ctx += " -> Відправка SMS";
            notifyStep += (ref string ctx) => ctx += " -> Логування в БД"; 
            pipeline[2] = notifyStep;

            // Ініціалізація системи
            var engine = new WorkflowProcessor(pipeline);
            var audit = new AuditService();

            // Підписка на події
            audit.Subscribe(engine);

            // Додамо ще один обробник "на льоту" через лямбду (груповий виклик події)
            engine.OnStepReached += (s, e) => {
                if (e.CurrentStatus.Contains("Помилка")) 
                    Console.WriteLine("[ALERT]: Виявлено критичний статус!");
            };

            // Запуск
            engine.Run("Замовлення #404");

            Console.WriteLine("\n--- Процес завершено ---");
            Console.ReadLine();
        }
    }
}