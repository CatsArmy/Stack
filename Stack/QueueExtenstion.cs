using Stack;
using System;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
using Unit4.CollectionsLib;

public static class QueueExtenstion
{
    public static int Count(this Queue<int> queue, bool Recursive = true)
    {
        Queue<int> stash = new Queue<int>();
        if (Recursive)
        {
            return Count(queue, stash);
        }
        int i = 0;
        while (!queue.IsEmpty())
        {
            i++;
            stash.Insert(queue.Remove());
        }
        queue.Fix(stash);
        return i;
    }
    private static int Count(Queue<int> queue, Queue<int> stash, int i = 0)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return i;
        }
        stash.Insert(queue.Remove());
        return Count(queue, stash, ++i);

    }
    public static int CountNum(this Queue<int> queue, int num, bool Recursive = true)
    {
        Queue<int> stash = new Queue<int>();
        if (Recursive)
        {
            return Count(queue, stash);
        }
        int i = 0;
        while (!queue.IsEmpty())
        {
            int value = queue.Remove();
            stash.Insert(value);
            if (value == num)
            {
                i++;
            }
        }
        queue.Fix(stash);
        return i;
    }
    private static int CountNum(Queue<int> queue, Queue<int> stash, int num, int i = 0)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return i;
        }
        int value = queue.Remove();
        stash.Insert(value);
        if (value == num)
        {
            return CountNum(queue, stash, ++i);
        }
        return CountNum(queue, stash, i);
    }
    public static int CountDigit(this Queue<int> queue, Digit digit, bool Recursive = true)
    {
        Queue<int> stash = new Queue<int>();
        if (Recursive)
        {
            return CountDigit(queue, stash, digit);
        }
        int i = 0;
        while (!queue.IsEmpty())
        {
            int value = queue.Remove();
            stash.Insert(value);
            foreach (Digit val in Digit.GetDigits(value))
            {
                if (digit == val)
                {
                    i++;
                }
            }
        }
        queue.Fix(stash);
        return i;
    }
    private static int CountDigit(Queue<int> queue, Queue<int> stash, Digit digit, int i = 0)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return i;
        }
        int value = queue.Remove();
        stash.Insert(value);

        foreach (Digit val in Digit.GetDigits(value))
        {
            if (digit == val)
            {
                i++;
            }
        }
        return CountDigit(queue, stash, digit, i);
    }
    public static int Sum(this Queue<int> queue, bool Recursive = true)
    {
        Queue<int> stash = new Queue<int>();
        if (Recursive)
        {
            return Sum(queue, stash);
        }
        int sum = 0;
        while (!queue.IsEmpty())
        {
            int value = queue.Remove();
            sum += value;
            stash.Insert(value);
        }
        queue.Fix(stash);
        return sum;
    }
    private static int Sum(Queue<int> queue, Queue<int> stash, int sum = 0)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return sum;
        }
        int value = queue.Remove();
        stash.Insert(value);
        return Sum(queue, stash, sum += value);

    }
    public static int Min(this Queue<int> queue, bool Recursive = true)
    {
        Queue<int> stash = new Queue<int>();
        int min = queue.Head();
        if (Recursive)
        {
            return Min(queue, stash, min);
        }
        while (!queue.IsEmpty())
        {
            int value = queue.Remove();
            if (min < value)
            {
                min = value;
            }
            stash.Insert(value);
        }
        queue.Fix(stash);
        return min;
    }
    private static int Min(Queue<int> queue, Queue<int> stash, int min)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return min;
        }
        int value = queue.Remove();
        if (value < min)
        {
            min = value;
        }
        stash.Insert(value);
        return Min(queue, stash, min);
    }
    internal static void Fix(this Queue<int> queue, Queue<int> stash, Queue<int> other = null, bool Recursive = false)
    {
        int value;
        if (Recursive)
        {
            if (stash.IsEmpty()) return;
            value = stash.Remove();
            queue.Insert(value);
            other?.Insert(value);
            Fix(queue, stash, other);
        }

        while (!stash.IsEmpty())
        {
            value = stash.Remove();
            queue.Insert(value);
            other?.Insert(value);
        }
    }
    internal static void Fix(this Queue<char> queue, Queue<char> stash, Queue<char> other = null, bool Recursive = false)
    {
        char value;
        if (Recursive)
        {
            if (stash.IsEmpty()) return;
            value = stash.Remove();
            queue.Insert(value);
            other?.Insert(value);
            Fix(queue, stash, other);
        }

        while (!stash.IsEmpty())
        {
            value = stash.Remove();
            queue.Insert(value);
            other?.Insert(value);
        }
    }
    public static int RemoveNegetiveNumbers(this Queue<int> queue) => RemoveNegetiveNumbers(queue, new Queue<int>());
    private static int RemoveNegetiveNumbers(this Queue<int> queue, Queue<int> stash)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return 0;
        }
        int value = queue.Remove();
        if (value.IsPositive())
        {
            stash.Insert(value);
        }
        return RemoveNegetiveNumbers(queue, stash);
    }
    public static Queue<int> ToQueue(this int[] arr)
    {
        return ToQueue(arr, new Queue<int>());
    }
    private static Queue<int> ToQueue(this int[] arr, Queue<int> queue, int i = 0)
    {
        if (!(i < arr.Length))
        {
            return queue;
        }
        queue.Insert(arr[i]);
        return ToQueue(arr, queue, ++i);
    }
    public static Queue<int> Create() => Create(new Queue<int>());

    //Nice lil challange for my self Code is safe clean and short recursive is fun
    private static Queue<int> Create(Queue<int> queue)
    {
        Console.WriteLine("Enter any int\nEnter \"S\" to stop");
        string input = Console.ReadLine();
        if (input.ToLower() == "s") return queue;
        if (int.TryParse(input, out int value)) queue.Insert(value);

        Console.Clear();
        return Create(queue);
    }

    public static void SortedInsert(this Queue<int> queue, int value)
    {
        if (queue.IsEmpty())
        {
            queue.Insert(value);
            return;
        }
        int val = queue.Remove();
        if (value > val)
        {
            queue.Insert(value);
            return;
        }
        SortedInsert(queue, value);
        queue.Insert(val);
    }
    public static void SortedInsert(this Queue<char> queue, char value)
    {
        if (queue.IsEmpty())
        {
            queue.Insert(value);
            return;
        }
        char val = queue.Remove();
        if (char.IsLower(value))
        {
            if (char.IsUpper(val) || val >= 'z')
            {
                queue.Insert(value);
                return;
            }
        }
        if (char.IsUpper(value))
        {
            if (char.IsLower(val) || val >= 'Z')
            {
                queue.Insert(value);
                return;
            }
        }
        SortedInsert(queue, value);
        queue.Insert(val);
    }
    private static void SortedInsertBySmallToBig(this Queue<char> queue, char value)
    {
        if (queue.IsEmpty())
        {
            queue.Insert(value);
            return;
        }
        char val = queue.Remove();
        if (char.IsUpper(value))
        {
            if (char.IsLower(val) || val >= 'Z')
            {
                queue.Insert(value);
                return;
            }
        }
        if (char.IsLower(value))
        {
            if (char.IsUpper(val) || val >= 'z')
            {
                queue.Insert(value);
                return;
            }
        }
        SortedInsertBySmallToBig(queue, value);
        queue.Insert(val);
    }
    public static void Sort(this Queue<int> queue)
    {
        if (queue.IsEmpty()) return;
        int value = queue.Remove();
        Sort(queue);
        SortedInsert(queue, value);
    }
    public static Queue<char> SortQueue(this Queue<char> queue)
    {
        Queue<char> copy = queue.Copy();
        copy.Sort();
        return copy;
    }
    public static Queue<int> SortQueue(this Queue<int> queue)
    {
        Queue<int> copy = queue.Copy();
        copy.Sort();
        return copy;
    }
    public static Queue<int> Copy(this Queue<int> queue)
    {
        return Copy(queue, new Queue<int>());
    }
    private static Queue<int> Copy(this Queue<int> queue, Queue<int> copy)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(copy, copy);
            return copy;
        }
        int value = queue.Remove();
        copy.Insert(value);
        return Copy(queue, copy);
    }
    public static Queue<char> Copy(this Queue<char> queue)
    {
        return Copy(queue, new Queue<char>(), new Queue<char>());
    }
    private static Queue<char> Copy(Queue<char> queue, Queue<char> stash, Queue<char> copy)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash, copy);
            return copy;
        }
        char value = queue.Remove();
        stash.Insert(value);
        copy.Insert(value);
        return Copy(queue, stash, copy);
    }
    public static double Avarage(this Queue<int> queue)
    {
        return queue.Sum() / queue.Count();
    }
    public static void Mult(this Queue<int> queue, int mult) => Mult(queue, new Queue<int>(), mult);
    private static void Mult(this Queue<int> queue, Queue<int> q, int mult)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(q);
            return;
        }
        q.Insert(queue.Remove() * mult);
    }

    public static Queue<char> Create(Queue<char> queue)
    {
        Console.WriteLine("Enter any char\nEnter \"S\" to stop");
        ConsoleKeyInfo input = Console.ReadKey();
        if (input.Key == ConsoleKey.Escape) return queue;
        queue.Insert(input.KeyChar);
        Console.Clear();
        return Create(queue);
    }
    public static Queue<char> Create(this string str) => Create(str, new Queue<char>());
    private static Queue<char> Create(string str, Queue<char> queue, int i = 0)
    {
        if (i >= str.Length)
        {
            return queue;
        }
        queue.Insert(str[i]);
        return Create(str, queue, ++i);
    }
    public static int Count(this Queue<char> queue) => Count(queue, new Queue<char>());
    private static int Count(Queue<char> queue, Queue<char> fix, int i = 0)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(fix);
            return i;
        }
        fix.Insert(queue.Remove());
        return Count(queue, fix, ++i);
    }
    public static bool Contains(this Queue<char> queue, char c) => Contains(queue, new Queue<char>(), c);
    private static bool Contains(Queue<char> queue, Queue<char> fix, char c)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(fix);
            return false;
        }
        char value = queue.Remove();
        fix.Insert(value);
        if (value == c)
        {
            queue.EmptyToFix(fix);
            queue.Fix(fix);
            return true;
        }
        return Contains(queue, fix, c);
    }
    public static void Remove(this Queue<char> queue, char c) => Remove(queue, new Queue<char>(), c);
    private static int Remove(Queue<char> queue, Queue<char> fix, char c)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(fix);
            return 0;
        }
        char value = queue.Remove();
        if (value == c)
        {
            return Remove(queue, fix, c);
        }
        fix.Insert(value);
        return Remove(queue, fix, c);
    }
    public static void Sort(this Queue<char> queue)
    {
        if (queue.IsEmpty()) return;
        char value = queue.Remove();
        Sort(queue);
        SortedInsert(queue, value);
    }
    public static void SortBySmallToBig(this Queue<char> queue)
    {
        if (queue.IsEmpty()) return;
        char value = queue.Remove();
        SortBySmallToBig(queue);
        SortedInsertBySmallToBig(queue, value);
    }
    internal static int EmptyToFix(this Queue<char> queue, Queue<char> fix)
    {
        if (queue.IsEmpty())
        {
            return 0;
        }
        fix.Insert(queue.Remove());
        return EmptyToFix(queue, fix);
    }
    public static void RemoveTail(this Queue<char> queue) => RemoveTail(queue, new Queue<char>());
    private static void RemoveTail(this Queue<char> queue, Queue<char> fix)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(fix);
            return;
        }
        char next = queue.Remove();
        if (queue.IsEmpty())
        {
            RemoveTail(queue, fix);
            return;
        }
        fix.Insert(next);
        RemoveTail(queue, fix);
    }
    public static Queue<char> Cut(this Queue<char> queue, Queue<char> q)
    {
        return Cut(queue.Copy().RemoveDuplicates(), q.Copy().RemoveDuplicates(), new Queue<char>());
    }
    
    private static Queue<char> Cut(this Queue<char> queue, Queue<char> q,Queue<char> cut)
    {
        if (queue.IsEmpty() && q.IsEmpty())
        {
            return cut;
        }
        if (queue.IsEmpty())
        {
            return Cut(q, queue, cut);
        }
        char c = queue.Remove();
        if (q.Contains(c))
        {
            q.Remove(c);
            cut.Insert(c);
        }
        return Cut(queue, q, cut);
    }
    public static Queue<char> SortedCut(this Queue<char> queue, Queue<char> q)
    {
        return SortedCut(queue.Copy().RemoveDuplicates(), q.Copy().RemoveDuplicates(), new Queue<char>());
    }

    private static Queue<char> SortedCut(this Queue<char> queue, Queue<char> q, Queue<char> cut)
    {
        if (queue.IsEmpty() && q.IsEmpty())
        {
            cut.SortBySmallToBig();
            return cut;
        }
        if (queue.IsEmpty())
        {
            return SortedCut(q, queue, cut);
        }
        char c = queue.Remove();
        if (q.Contains(c))
        {
            q.Remove(c);
            cut.Insert(c);
        }
        return SortedCut(queue, q, cut);
    }
    private static Queue<char> RemoveDuplicates(this Queue<char> queue)
    {
        return RemoveDuplicates(queue, new Queue<char>(), new Queue<char>());
    }
    private static Queue<char> RemoveDuplicates(this Queue<char> queue, Queue<char> stash, Queue<char> clean)
    {
        if (queue.IsEmpty())
        {
            queue.Fix(stash);
            return clean;
        }
        char c = queue.Remove();
        stash.Insert(c);
        if (clean.Contains(c))
        {
            return RemoveDuplicates(queue, stash, clean);
        }
        clean.Insert(c);
        return RemoveDuplicates(queue, stash, clean);
    }
}
