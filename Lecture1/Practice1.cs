namespace CSharpProgrammingCourse.Lecture1;

// Умова:
// Створити програму для аналізу енергоспоживання.
//     Вимоги:
// 1. Ввести кількість днів і споживання енергії (double).
// 2. Зберегти дані в одновимірний масив.
// 3. Створити двовимірний масив: день – споживання – різниця.
// 4. Створити зубчастий масив споживання по приладах.
// 5. Обчислити загальне та середнє споживання.
// 6. Визначити дні перевищення норми.
// 7. Вивести звіт

public class Practice1
{
    private double[] _oneDimentionalData;
    private double[,] _twoDimentionalData;
    
    public double[] OneDimentionalData
    {
        get { return _oneDimentionalData; }
        set { _oneDimentionalData = value; }
    }

    public double[,] TwoDimentionalData
    {
        get { return _twoDimentionalData; }
        set { _twoDimentionalData = value; }
    }

    public Practice1(int daysCount)
    {
        OneDimentionalData = DataEntry(daysCount);

        foreach (double element in OneDimentionalData)
        {
            Console.WriteLine(element);
        }
    }

    public double[] DataEntry(int daysCount)
    {
        double[] data = new double[daysCount];
        for (int i = 0; i < daysCount; i++)
        {
            data[i] = InputDouble();
        }
        
        return data;
    }

    static double InputDouble()
    {
        double value = -1;
        while (value == -1)
        {
            string stringValue = Console.ReadLine();
            double.TryParse(stringValue, out value);
            if (value == 0 && stringValue != "0")
            {
                Console.WriteLine("Please input double");
            }
            else if(stringValue == "0")
            {
                value = 0;
            }
        }
        return value;
    }
    
    void EnergyConsuptionSummary()
    {
        
    }
}