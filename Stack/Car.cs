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
}
