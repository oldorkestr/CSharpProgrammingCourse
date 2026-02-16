namespace CSharpProgrammingCourse.Lecture2;

// Клас – посилальний тип
class Person
{
    private string _name;
    private int _age;
    private int _experiance;
    private const int _rate = 5;
    private const int _startingSalary = 1000;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    public int Experiance
    {
        get { return _experiance; }
        set { _experiance = value; }
    }

    public Person(string name, int age, int experiance)
    {
        Name = name;
        Age = age;
        Experiance = experiance;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Class Person: Name = {Name}, Age = {Age}, Salary = {CalculateSalaryByExperience(Experiance).ToString()}");
    }

    public int CalculateSalaryByExperience(int experiances)
    {
        return (_startingSalary * _rate) * experiances;
    }
}

// Структура – значимий тип
struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Display()
    {
        Console.WriteLine($"Struct Point: X = {X}, Y = {Y}");
    }
}


static class StringExtensions
{
    public static bool IsCapitalized(this string str)
    {
        if (string.IsNullOrEmpty(str)) return false;
        return char.IsUpper(str[0]);
    }
}

public static class Lecture2
{
    public static void DemoClassesAndStructs()
    {
        // Робота з класом
        Person person1 = new Person("Taras", 25, 5);
        Person person2 = person1; // Копіювання посилання
        person2.Age = 30;

        person1.DisplayInfo(); // Age буде 30, бо клас – посилальний тип
        person2.DisplayInfo();

        // Робота зі структурою
        Point point1 = new Point(10, 20);
        Point point2 = point1; // Поверхневе копіювання значення
        point2.X = 50;

        point1.Display(); // X залишиться 10, бо структура – значимий тип
        point2.Display();
        
        bool result = "Hello".IsCapitalized();
        Console.WriteLine(result);
    }
}