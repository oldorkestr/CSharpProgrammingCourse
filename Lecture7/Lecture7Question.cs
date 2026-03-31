namespace CSharpProgrammingCourse.Lecture7;

public class Lecture7Question : IComparable<Lecture7Question>
{
    private int a;
    private int b;

    public int A
    {
        get { return a; }
        set { a = value; }
    }
    
    public int B
    {
        get { return b; }
        set { b = value; }
    }


    public Lecture7Question( int a, int b )
    {
        A = a;
        B = b;
    }

    public int CompareTo(Lecture7Question? other)
    {
        return A.CompareTo(other.A);
    }

    public override string ToString()
    {
        return "A=" + A + ", B=" + B;
    }

}

public class SortedSetTest
{
    private static SortedSet<Lecture7Question> set = new SortedSet<Lecture7Question>();

    public SortedSetTest(){}

    public void Add(Lecture7Question lecture7Question)
    {
        set.Add(lecture7Question);
    }

    public void Print()
    {
        foreach (Lecture7Question test in set)
        {
            Console.WriteLine(test.ToString());
        }
    }
}

public static class DemoLecture7
{
    public static void Demo()
    {
        SortedSetTest test = new SortedSetTest();
        Lecture7Question el1 = new Lecture7Question(7, 76);
        Lecture7Question el2 = new Lecture7Question(5, 87);
        test.Add(el1);
        test.Add(el2);
        
        test.Print();
    }
}