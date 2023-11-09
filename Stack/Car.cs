using Stack;
using System;
using Unit4.CollectionsLib;
public class Car
{
    public string model;
    public int year;
    public Car(string model, int year)
    {
        this.model = model;
        this.year = year;
    }
    public Car(Car car)
    {
        this.model = car.model;
        this.year = car.year;
    }
    public static bool operator >(Car c, Car cs) => c.year > cs.year;
    public static bool operator <(Car c, Car cs) => c.year < cs.year;

    public static bool operator >=(Car c, Car cs) => c.year >= cs.year;
    public static bool operator <=(Car c, Car cs) => c.year <= cs.year;

    public static bool operator ==(Car c, Car cs) => c.year == cs.year;
    public static bool operator !=(Car c, Car cs) => c.year != cs.year;

    public static void StackProgram()
    {
        //1
        Stack<Car> stack = new Stack<Car>();
        int Base = 2010;
        Random rand = new Random();
        rand.Next(0, 10);
        string[] models = Enum.GetNames(typeof(Models));
        for (int i = 0; i < 6; i++)
        {
            stack.Push(new Car(models[rand.Next(0, 2)], Base + rand.Next(0, 10)));
        }
        Console.WriteLine(stack);
        //2
        stack.Sort();
        Console.WriteLine(stack);
        //3
        stack.Push(new Car(models[rand.Next(0, 2)], Base));
        stack.Push(new Car(models[rand.Next(0, 2)],++Base));
        Console.WriteLine(stack);
        //4
        stack.Sort();
        Console.WriteLine(stack);
        //5
        stack.PopYear(2016);
        Console.WriteLine(stack);
        //6
        stack.StarModel(models[2]);
        Console.WriteLine(stack);
        //7
        Stack<Car> Toyota = new Stack<Car>();
        Toyota.Push(new Car(models[1], Base + rand.Next(0, 10)));
        Toyota.Push(new Car(models[1], Base + rand.Next(0, 10)));
        Toyota.Push(new Car(models[1], Base + rand.Next(0, 10)));
        Console.WriteLine(Toyota);
        //8
        Stack<Car> Unique = stack.UniqueModels(Toyota);
        Console.WriteLine(Unique);
    }
    enum Models
    {
        Jeep = 0,
        Toyota = 1,
        Subaru = 2
    }

    public static void QueueProgram()
    {

    }
}
