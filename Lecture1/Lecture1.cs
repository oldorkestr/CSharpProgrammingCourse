namespace CSharpProgrammingCourse.Lecture1;

public static class Lecture1
{
    #region Types
    public static void TypesDemo()
    {
        // ===== 1. Типи-значення =====
        int a = 10;
        int b = a;   // копіюється значення
        Console.WriteLine("Value types first entry:");
        Console.WriteLine($"a = {a}, b = {b}\n");
        b = 20;

        Console.WriteLine("Value types second entry:");
        Console.WriteLine($"a = {a}, b = {b}\n");

        // ===== 2. Типи-посилання =====
        int[] arr1 = { 1, 2, 3 };
        int[] arr2 = arr1;   // копіюється посилання
        arr2[0] = 100;

        Console.WriteLine("Reference types:");
        Console.WriteLine($"arr1[0] = {arr1[0]}, arr2[0] = {arr2[0]}\n");

        // ===== 3. Enum =====
        Day today = Day.Monday;
        Console.WriteLine("Enum:");
        Console.WriteLine($"Today is {today}, numeric value = {(int)today}\n");

        // ===== 4. Object =====
        object obj = 42;
        Console.WriteLine("System.Object:");
        Console.WriteLine($"Value: {obj}");
        Console.WriteLine($"Type: {obj.GetType()}\n");

        // ===== 5. Рядок (reference type, але поводиться як value) =====
        string s1 = "Hello";
        string s2 = s1;
        s2 += " C#";

        Console.WriteLine("String behavior:");
        Console.WriteLine($"s1 = {s1}");
        Console.WriteLine($"s2 = {s2}\n");

        // ===== 6. Багатовимірний масив =====
        int[,] matrix = { { 1, 2 }, { 3, 4 } };
        Console.WriteLine("Multidimensional array:");
        Console.WriteLine($"matrix[1,0] = {matrix[1, 0]}");

        Console.WriteLine("\nTypes demo finished.");
    }

    // Допоміжний enum
    enum Day
    {
        Monday,
        Tuesday,
        Wednesday
    }
    #endregion

    #region Arrays
    public static void ArraysDemo()
    {
        // ===== 1. Одновимірний масив =====
        int[] oneDimensional = { 10, 20, 30, 40 };

        Console.WriteLine("One-dimensional array:");
        for (int i = 0; i < oneDimensional.Length; i++)
        {
            Console.WriteLine($"oneDimensional[{i}] = {oneDimensional[i]}");
        }
        Console.WriteLine();

        // ===== 2. Багатовимірний масив (rectangular) =====
        int[,] twoDimensional =
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

        Console.WriteLine("Two-dimensional array:");
        for (int i = 0; i < twoDimensional.GetLength(0); i++)
        {
            for (int j = 0; j < twoDimensional.GetLength(1); j++)
            {
                Console.Write($"{twoDimensional[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        // ===== 3. Тривимірний масив =====
        int[,,] threeDimensional = new int[2, 2, 2]
        {
            { { 1, 2 }, { 3, 4 } },
            { { 5, 6 }, { 7, 8 } }
        };

        Console.WriteLine("Three-dimensional array:");
        for (int i = 0; i < threeDimensional.GetLength(0); i++)
        {
            for (int j = 0; j < threeDimensional.GetLength(1); j++)
            {
                for (int k = 0; k < threeDimensional.GetLength(2); k++)
                {
                    Console.WriteLine($"[{i},{j},{k}] = {threeDimensional[i, j, k]}");
                }
            }
        }
        Console.WriteLine();

        // ===== 4. Зубчастий масив (jagged array) =====
        int[][] jagged = new int[3][];

        jagged[0] = new int[] { 1, 2 };
        jagged[1] = new int[] { 3, 4, 5 };
        jagged[2] = new int[] { 6 };

        Console.WriteLine("Jagged array:");
        for (int i = 0; i < jagged.Length; i++)
        {
            Console.Write($"Row {i}: ");
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.Write($"{jagged[i][j]} ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nArrays demo finished.");
    }

    #endregion
    
    #region Alghoritms
    public static void AlgorithmsDemo()
    {
        Console.WriteLine("=== SEQUENTIAL ALGORITHM ===");

        // 1. ПОСЛІДОВНИЙ АЛГОРИТМ
        int a = 10;
        int b = 3;

        int sum = a + b;
        int diff = a - b;
        int product = a * b;
        double division = (double)a / b;

        Console.WriteLine($"a = {a}, b = {b}");
        Console.WriteLine($"Sum = {sum}");
        Console.WriteLine($"Difference = {diff}");
        Console.WriteLine($"Product = {product}");
        Console.WriteLine($"Division = {division:F2}");

        Console.WriteLine("\n=== BRANCHING ALGORITHM ===");

        // 2. ГАЛУЖЕНИЙ АЛГОРИТМ
        if (a > b)
        {
            Console.WriteLine("a is greater than b");
        }
        else if (a < b)
        {
            Console.WriteLine("a is less than b");
        }
        else
        {
            Console.WriteLine("a is equal to b");
        }

        Console.WriteLine("\n=== CYCLIC ALGORITHMS ===");

        // 3.1 Цикл for
        Console.WriteLine("For loop:");
        for (int i = 1; i <= 5; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        // 3.2 Цикл while
        Console.WriteLine("While loop:");
        int counter = 5;
        while (counter > 0)
        {
            Console.Write(counter + " ");
            counter--;
        }
        Console.WriteLine();

        // 3.3 Цикл do-while
        Console.WriteLine("Do-while loop:");
        int number = 0;
        do
        {
            Console.WriteLine("This will execute at least once");
            number++;
        }
        while (number < 1);

        // 3.4 Цикл foreach
        Console.WriteLine("\nForeach loop:");
        int[] numbers = { 2, 4, 6, 8 };
        foreach (int n in numbers)
        {
            Console.Write(n + " ");
        }

        Console.WriteLine("\n\nAlgorithms demo finished.");
    }
    #endregion
}