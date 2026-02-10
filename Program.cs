using CSharpProgrammingCourse.Lecture1;

class Program
{
    static void Main(string[] args)
    {
        #region Lecture1
        //Lecture1.TypesDemo();
        //Lecture1.ArraysDemo();
        //Lecture1.AlgorithmsDemo();
        #endregion

        #region Practice1

        Console.Write("Please, enter a days number:");
        int days = int.Parse(Console.ReadLine());
        
        Practice1 practice1Object = new Practice1(days);

        #endregion
    }
}