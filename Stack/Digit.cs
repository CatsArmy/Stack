using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

public struct Digit
{
    int digit;
    public static bool operator ==(Digit left, Digit right) => left.digit == right.digit;
    public static bool operator !=(Digit left, Digit right) => left.digit != right.digit;

    public static bool operator ==(int left, Digit right) => left == right.digit;
    public static bool operator ==(Digit left, int right) => left.digit == right;
    public static bool operator !=(int left, Digit right) => left != right.digit;
    public static bool operator !=(Digit left, int right) => left.digit != right;

    public Digit(Digits digit)
    {
        this.digit = (int)digit;
    }
    private Digit(int digit)
    {
        this.digit = digit;
    }
    public static Digit[] GetDigits(int value) 
    {
        string str = value.ToString();
        Digit[] digits = new Digit[str.Length];
        for (int i = 0; i < digits.Length; i++)
        {
            digits[i] = new Digit(int.Parse(str[i].ToString()));
        }
        return digits;
    }
    private static Digit[] AllDigits()
    {
        Digit[] arr = new Digit[10];
        var digits = Enum.GetValues(typeof(Digits));
        int index = 0;
        foreach (Digits item in digits)
        {
            arr[index] = new Digit(item);
            index++;
        }
        return arr;
    }
    public static Digit[] Digits = AllDigits();
    
}

public enum Digits
{
    Zero,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine
}