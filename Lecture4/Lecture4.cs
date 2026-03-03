namespace CSharpProgrammingCourse.Lecture4;

public interface IHarvestable
{
    void Harvest();
}

// 2. Абстрактний клас, що описує загальні риси всіх рослин
public abstract class Plant
{
    #region Public methods
    public string Species { get; set; }
    public int AgeInMonths { get; set; }
    
    #endregion
    protected Plant(string species, int age)
    {
        Species = species;
        AgeInMonths = age;
    }

    // Загальний метод для всіх рослин
    public void Water()
    {
        Console.WriteLine($"[Догляд]: Рослину {Species} полито. Вологість ґрунту в нормі.");
    }

    // Абстрактний метод (кожна рослина росте по-своєму)
    public abstract void Grow();
}

// 3. Клас-спадкоємець, що підтримує інтерфейс (Яблуня)
public class AppleTree : Plant, IHarvestable
{
    public AppleTree(int age) : base("Яблуня", age) { }

    public override void Grow()
    {
        Console.WriteLine($"Яблуня виросла на 10 см за цей сезон.");
    }

    public void Harvest()
    {
        Console.WriteLine("🍏 Успіх: Зібрано кошик стиглих яблук!");
    }
}

// 4. Клас-спадкоємець без підтримки інтерфейсу (Папороть)
public class Fern : Plant
{
    public Fern(int age) : base("Папороть", age) { }

    public override void Grow()
    {
        Console.WriteLine("Папороть розпустила нове листя в тіні.");
    }
}

public static class Lecture4
{
    public static void Lecture4Demo()
    {
        // Створюємо список базового типу
        List<Plant> garden = new List<Plant>
        {
            new AppleTree(24),
            new Fern(12),
            new AppleTree(36)
        };

        Console.WriteLine("--- Робота в саду ---");

        foreach (var plant in garden)
        {
            Console.WriteLine($"\nОб'єкт: {plant.Species}");
                
            // Виклик методів абстрактного класу
            plant.Water();
            plant.Grow();

            // Перевірка на наявність інтерфейсу (is-перевірка)
            if (plant is IHarvestable harvestablePlant)
            {
                harvestablePlant.Harvest();
            }
            else
            {
                Console.WriteLine("Ця рослина декоративна, врожаю не буде.");
            }
        }
    }
}
