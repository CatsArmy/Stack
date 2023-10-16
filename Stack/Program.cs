using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading;
using Unit4.CollectionsLib;

namespace Stack
{
    internal class Program
    {
        private static bool R = true;
        private static bool Debug
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }


        static void Main(string[] args)
        {
            int[] sarr = { 1, 2, 4, 8, 16 };
            int[] stackarr = { 1, 2, 2, 1 };
            Stack<int> s = Debug ? sarr.ToStack() : StackExtenstion.Create();
            Stack<int> stack = Debug ? stackarr.ToStack() : StackExtenstion.Create();
            Stack<int> copy;
            b();
            c();
            void Ex2Thro6()
            {
                Console.WriteLine($"S{s}");
                Console.WriteLine($"Length/Count: {s.Count(R)}");
                Console.WriteLine($"Sum: {s.Sum(R)}");
                Console.WriteLine($"Max: {s.Max(R)}");
            }
            void b()
            {
                Console.WriteLine("----------Ascending/Order/Descending----------");
                //5 + 6
                Console.WriteLine($"S{s}");
                Console.WriteLine($"S: {(s.IsUp(R) ? "Is In " : "Isn't ")}Up/Ascending Order");
                Console.WriteLine($"S:{(s.IsUpDown(R) ? "Is " : "Isn't ")} " +
                    $"In Up/Ascending Order Then Down/Descending Order");
                Console.Write("S"); s.PrintShit();
                Console.WriteLine($"Stack{stack}");
                Console.WriteLine($"Stack: {(stack.IsUp(R) ? "Is In " : "Isn't ")}Up/Ascending Order");

                Console.WriteLine($"Stack:{(stack.IsUpDown(R) ? "Is " : "Isn't ")} " +
                    $"In Up/Ascending Order Then Down/Descending Order");
                Console.Write("Stack");
                stack.PrintShit();
                Console.WriteLine();
            }
            void c()
            {
                //7 + 8
                Console.WriteLine("----------Copy----------");
                copy = stack.Copy(R);
                Console.WriteLine($"Copy{copy}");
                copy.Pop();
                Console.WriteLine("Copy.Pop");
                Console.WriteLine($"Copy{copy}");
                Console.WriteLine($"Copy:{(copy.IsUpDown(R) ? "Is " : "Isn't ")} " +
                    $"In Up/Ascending Order Then Down/Descending Order");
                Console.Write("Copy");
                copy.PrintShit();
                Console.WriteLine();
                Console.WriteLine($"Stack{stack}");
                Console.WriteLine($"Copy{copy}");
            }
            void d()
            {
                Console.WriteLine("----------Spill/Reverse----------");
                Console.WriteLine($"Copy{copy.Reverse(R)}");
                Console.WriteLine($"Stack{stack.Reverse(R)}");
            }
        }
    }
    public static class StackExtenstion
    {
        public static Stack<int> Create() => Create(new Stack<int>());

        //Nice lil challange for my self Code is safe clean and short recursive is fun
        private static Stack<int> Create(Stack<int> stack)
        {
            Console.WriteLine("Enter any int\nEnter \"S\" to stop");
            string input = Console.ReadLine();
            if (input == "s" || input == "S") return stack;
            if (int.TryParse(input, out int value)) stack.Push(value);

            Console.Clear();
            return Create(stack);
        }

        private static int Count(Stack<int> stack, Stack<int> s, int count = 0)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(s);
                return count;
            }

            s.Push(stack.Pop());
            count++;
            return Count(stack, s, count);
        }

        public static int Count(this Stack<int> stack, bool Recursive = false)
        {
            if (Recursive) return Count(stack, new Stack<int>());

            Stack<int> s = new Stack<int>();
            int count = 0;
            while (!stack.IsEmpty())
            {
                s.Push(stack.Pop());
                count++;
            }
            stack.Fix(s);
            return count;
        }

        private static int Sum(Stack<int> stack, Stack<int> s, int sum = 0)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(s);
                return sum;
            }

            int add = stack.Pop();
            s.Push(add);
            sum += add;

            return Sum(stack, s, sum);
        }

        public static int Sum(this Stack<int> stack, bool Recursive = false)
        {
            if (Recursive) return Sum(stack, new Stack<int>());

            Stack<int> s = new Stack<int>();
            int sum = 0;
            while (!stack.IsEmpty())
            {
                int add = stack.Pop();
                s.Push(add);
                sum += add;
            }
            stack.Fix(s);
            return sum;
        }

        private static int Max(Stack<int> stack, Stack<int> s, int max)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(s);
                return max;
            }

            int value = stack.Pop();
            if (max < value) { max = value; }
            s.Push(value);

            return Max(stack, s, max);
        }

        public static int Max(this Stack<int> stack, bool Recursive = false)
        {
            int max = stack.Top();
            if (Recursive) return Max(stack, new Stack<int>(), max);
            Stack<int> s = new Stack<int>();
            while (!stack.IsEmpty())
            {
                int value = stack.Pop();
                if (max < value)
                {
                    max = value;
                }
                s.Push(value);
            }
            stack.Fix(s);
            return max;
        }

        private static bool IsUp(Stack<int> stack, Stack<int> s, int up)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(s);
                return true;
            }

            int value = stack.Pop();
            s.Push(value);
            if (up > value)
            {
                stack.Fix(s);
                return false;
            }
            up = value;

            return IsUp(stack, s, up);
        }

        public static bool IsUp(this Stack<int> stack, bool Recursive = false)
        {
            Stack<int> s = new Stack<int>();
            int up = stack.Top();
            if (Recursive) return IsUp(stack, s, up);

            while (!stack.IsEmpty())
            {
                int value = stack.Pop();
                s.Push(value);
                if (up > value)
                {
                    stack.Fix(s);
                    return false;
                }
                up = value;
            }
            stack.Fix(s);
            return true;
        }

        private static bool IsUpDown(Stack<int> stack, Stack<int> s, int up, int count, int i = 0)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(s);
                return true;
            }
            int value = stack.Pop();
            s.Push(value);
            if (i < count / 2)
                if (up > value)
                {
                    stack.Fix(s);
                    return false;
                }
            if (i >= count / 2)
                if (value > up)
                {
                    stack.Fix(s);
                    return false;
                }
            up = value;

            return IsUpDown(stack, s, up, count, ++i);
        }

        public static bool IsUpDown(this Stack<int> stack, bool Recursive = false)
        {
            Stack<int> s = new Stack<int>();
            int count = stack.Count(Recursive);
            if (Recursive) return IsUpDown(stack, s, stack.Top(), count);

            int up = stack.Top();
            for (int i = 0; i < count; i++)
            {
                int value = stack.Pop();
                s.Push(value);
                if (i < count / 2)
                    if (up > value)
                    {
                        stack.Fix(s);
                        return false;
                    }
                if (i >= count / 2)
                    if (value > up)
                    {
                        stack.Fix(s);
                        return false;
                    }
                up = value;
            }
            stack.Fix(s);
            return true;
        }

        public static Stack<int> Copy(this Stack<int> stack, bool Recursive = false)
        {
            int[] arr = stack.ToArray(Recursive);
            return arr.ToStack();
        }

        //Spill If u want to call it that
        private static Stack<int> Reverse(Stack<int> stack, Stack<int> s, Stack<int> spill)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(s);
                return spill;
            }
            int value = stack.Pop();
            s.Push(value);
            spill.Push(value);

            return Reverse(stack, s, spill);
        }

        public static Stack<int> Reverse(this Stack<int> stack, bool Recursive = false)
        {
            Stack<int> s = new Stack<int>();
            Stack<int> spill = new Stack<int>();
            if (Recursive) return Reverse(stack, s, spill);
            while (!stack.IsEmpty())
            {
                int value = stack.Pop();
                s.Push(value);
                spill.Push(value);
            }
            stack.Fix(s);
            return spill;
        }

        internal static void Fix(this Stack<int> stack, Stack<int> s, Stack<int> other = null, bool Recursive = false)
        {
            int value;
            if (Recursive)
            {
                if (s.IsEmpty()) return;
                value = s.Pop();
                stack.Push(value);
                other?.Push(value);
                Fix(stack, s, other);
            }

            while (!s.IsEmpty())
            {
                value = s.Pop();
                stack.Push(value);
                other?.Push(value);
            }
        }

        public static void PrintShit(this Stack<int> stack)
        {
            int[] arr = stack.ToArray();
            int up = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                int value = arr[i];
                if (i < arr.Length / 2)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    if (up > value) Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{(i == 0 ? "[" : ",")}{arr[i]}");
                }
                if (i >= arr.Length / 2)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    if (value > up) Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{(i == arr.Length - 1 ? $",{arr[i]}]" : $",{arr[i]}")}");
                }
                up = value;
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public static Stack<int> ToStack(this int[] ints)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = ints.Length - 1; i >= 0; i--)
            {
                stack.Push(ints[i]);
            }
            return stack;
        }
        public static int[] ToArray(this Stack<int> stack, bool Recursive = false)
        {
            int[] arr = new int[stack.Count()];
            string[] strings = stack.ToString().Split(',');
            if (Recursive) return ToArray(arr, strings);

            for (int i = 0; i < strings.Length; i++)
            {
                arr[i] = int.Parse(GetDigits(strings[i]));
            }
            return arr;
        }
        public static int[] ToArray(int[] arr, string[] strings, int i = 0)
        { 
            if (!(i < strings.Length)) return arr;

            arr[i] = int.Parse(GetDigits(strings[i]));
            return ToArray(arr, strings, ++i);
        }
        public static string GetDigits(string str, int i = 0) 
        {
            if (!(i < str.Length)) return "";
            string result = char.IsDigit(str[i]) ? str[i].ToString() : "";
            
            return result + GetDigits(str, ++i);
        }

    }
}
