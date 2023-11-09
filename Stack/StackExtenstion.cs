using Microsoft.Win32;
using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Unit4.CollectionsLib;

namespace Stack
{
    public static class StackExtenstion
    {
        #region Stack<int>
        public static Stack<int> Create() => Create(new Stack<int>());

        //Nice lil challange for my self Code is safe clean and short recursive is fun
        private static Stack<int> Create(Stack<int> stack)
        {
            Console.WriteLine("Enter any int\nEnter \"S\" to stop");
            string input = Console.ReadLine();
            if (input.ToLower() == "s") return stack;
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
        internal static void Fix(this Stack<string> stack, Stack<string> s)
        {
            string value;
                if (s.IsEmpty()) return;
                value = s.Pop();
                stack.Push(value);
//                other?.Push(value);
                Fix(stack, s);

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
        
        internal static void Fix(this Stack<Car> stack, Stack<Car> s, Stack<Car> other = null, bool Recursive = false)
        {
            Car value;
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
        public static void Sort(this Stack<int> stack)
        {
            if (stack.IsEmpty()) return;
            int value = stack.Pop();
            Sort(stack);
            SortedInsert(stack, value);
        }

        public static void SortedInsert(this Stack<int> stack, int value)
        {
            if (stack.IsEmpty() || value > stack.Top())
            {
                stack.Push(value);
                return;
            }
            int val = stack.Pop();
            SortedInsert(stack, value);
            stack.Push(val);
        }
        /*
        public static void SortedInsert(this Stack<int> stack, int value, int j = 0, bool NoArr = true)
        {
            if (NoArr)
            {
                SortedInsert(stack, value, new Stack<int>());
                return;
            }
            int[] s = stack.ToArray();
            int[] arr = new int[s.Length + 1];
            if (value < s[j])
            {
                for (int i = ++j; i < arr.Length; i++)
                {
                    arr[i] = s[--i];
                }
                stack = arr.ToStack();
                return;
            }
            SortedInsert(stack, value, ++j);
        }
        public static void SortedInsert(Stack<int> stack, int value, Stack<int> s)
        {
            if (stack.IsEmpty())
            {
                stack.Push(value);
                stack.Fix(s);
                return;
            }
            int val = stack.Pop();
            s.Push(val);
            if (value < val)
            {
                stack.Push(value);
                stack.Fix(s);
                return;
            }
            SortedInsert(stack, value, s);
        }*/
        /*public static void Sort(this Stack<int> stack, bool NoArr = true)
        {
            if (NoArr)
            {
                Sort(stack, new Stack<int>());
                return;
            }
            stack = BubbleSort(stack.ToArray()).ToStack();
        }
        public static void Sort(Stack<int> stack,Stack<int> s, int i = 0)
        {
            int count = stack.Count();
            if (i == --count)
            {
                return;
            }
            int value = stack.Pop();
            int next = stack.Top();
            if (next < value)
            {
                stack.Push(next);
            }//need help
        }*/
        public static Stack<int> SortedStack(this Stack<int> stack)
        {
            Stack<int> copy = stack.Copy();
            copy.Sort();
            return copy;
        }
        static int[] BubbleSort(int[] bubble)
        {
            int TempSort;
            int tempIndx;
            for (int i = 0; i < bubble.Length - 1; i++)
            {
                for (int j = 0; j < bubble.Length - 1; j++)
                {//brute force go brrrrrrrrrrr
                    tempIndx = j + 1;
                    TempSort = bubble[j];
                    if (bubble[tempIndx] < bubble[j])
                    {
                        bubble[j] = bubble[tempIndx];
                        bubble[tempIndx] = TempSort;
                    }
                }
            }
            return bubble;
        }
#endregion
        #region Stack<Car>
        public static void PopYear(this Stack<Car> stack, int year)
        {
            PopYear(stack, new Stack<Car>(), year);
        }
        private static void PopYear(Stack<Car> stack, Stack<Car> stash, int year)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(stash);
                return;
            }
            Car car = stack.Pop();
            if (car.year < year)
            {
                PopYear(stack, stack, year);
                return;
            }
            stash.Push(car);
        }
        public static void SortedPush(this Stack<Car> stack, Car value)
        {
            if (stack.IsEmpty() || value > stack.Top())
            {
                stack.Push(value);
                return;
            }
            Car val = stack.Pop();
            SortedPush(stack, value);
            stack.Push(val);
        }
        public static void Sort(this Stack<Car> stack)
        {
            if (stack.IsEmpty()) return;
            Car value = stack.Pop();
            Sort(stack);
            SortedPush(stack, value);
        }
        public static void StarModel(this Stack<Car> stack, string model)
        {
            if (stack.IsEmpty()) return;
            StarModel(stack, stack.Pop(), model);
        }
        private static void StarModel(this Stack<Car> stack, Car value, string model)
        {
            if (stack.IsEmpty() || value.model == model)
            {
                stack.Push(value);
                return;
            }
            Car val = stack.Pop();
            StarModel(stack, value, model);
            stack.Push(val);
        }
        public static Stack<Car> UniqueModels(this Stack<Car> stack, Stack<Car> s)
        {
            return UniqueModels(stack.Copy(), s.Copy(), new Stack<Car>());
        }
        private static Stack<Car> UniqueModels(Stack<Car> stack, Stack<Car> s, Stack<Car> result)
        {
            if (stack.IsEmpty())
            {
                return result;
            }
            Car value = stack.Pop();
            if (s.Contains(value.model))
            {
                stack.Pop(value.model);
                s.Pop(value.model);
                return UniqueModels(stack, s, result);
            }
            result.Push(value);
            return UniqueModels(stack, s, result);
        }
        public static bool Contains(this Stack<Car> stack, string model)
        {
            return Contains(stack, new Stack<Car>(), model);
        }
        private static bool Contains(this Stack<Car> stack,  Stack<Car> stash, string model)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(stash);
                return false;
            }
            Car value = stack.Pop();
            stash.Push(value);
            if (value.model == model)
            {
                stack.Fix(stash);
                return true;
            }
            return Contains(stack, stash, model);
        }
        public static void Pop(this Stack<Car> stack, string model)
        {
            Pop(stack, new Stack<Car>(), model);
        }
        private static int Pop(this Stack<Car> stack, Stack<Car> stash, string model)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(stash);
                return 0;
            }
            Car value = stack.Pop();
            if (value.model == model)
            {
                return Pop(stack, stash, model);
            }
            stash.Push(value);
            return Pop(stack, stack, model);
        }
        public static Stack<Car> Copy(this Stack<Car> queue)
        {
            return Copy(queue, new Stack<Car>(), new Stack<Car>());
        }
        private static Stack<Car> Copy(Stack<Car> stack, Stack<Car> stash, Stack<Car> copy)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(stash, copy);
                return copy;
            }
            Car value = stack.Pop();
            stash.Push(value);
            return Copy(stack, stash, copy);
        }
        #endregion
    }
}
