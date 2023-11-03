using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Unit4.CollectionsLib;

namespace Stack
{
    internal class Program
    {
        private readonly static bool R = true;
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
            Queue<char> queue = "abcdefG".Create();
            Queue<char> q = "gfedcba".Create();
            Queue<char> cut = QueueExtenstion.Cut(queue, q);
        }
        static void QueueIntTest()
        {
            Queue<int> queue = new Queue<int>();
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                queue.Insert(rand.Next(80, 100));
            }
            Console.WriteLine(queue);
            int[] arr = { 1, 2, 3, 4, 5 };
            Queue<int> queue2 = arr.ToQueue();
            Console.WriteLine(queue2);
            Console.WriteLine($"Queue Avarage:{queue.Avarage()}");
            Console.WriteLine($"Queue2 Avarage: {queue2.Avarage()}");
            Console.WriteLine($"Queue Min:{queue.Min()}");
            Console.WriteLine($"Queue2 Min: {queue2.Min()}");
            int head = queue.Head();
            Console.WriteLine($"Queue {head} Was Counted :{queue.CountNum(head)} Times");
            head = queue2.Head();
            Console.WriteLine($"Queue2 {head} Was Counted :{queue2.CountNum(head)} Times");
            int[] count = new int[10];
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = queue.CountDigit(Digit.Digits[i]);
            }
            int max = count[0];
            int maxIndex = 0;
            max = count.Max();
            for (int i = 0; i < count.Length; i++)
            {
                if (max == count[i])
                {
                    maxIndex = i;
                }
            }
            Console.WriteLine($"Most Common Digit:{maxIndex} Showed up: {max}");
            Console.WriteLine(queue);
            Console.WriteLine(queue2);
            Console.WriteLine("Multiplying all of queue numbers by -1");
            queue.Mult(-1);
            Console.WriteLine("Removing Negetive numbers");
            queue.RemoveNegetiveNumbers();
            Console.WriteLine(queue);
        }
        static void MyIntStackTest()
        {
            //Todo Add Test Program
        }
        static void StackTest()
        {
            int[] sarr = { 1, 2, 4, 8, 16 };
            int[] stackarr = { 1, 2, 2, 1 };
            Stack<int> s = Debug ? sarr.ToStack() : StackExtenstion.Create();
            Stack<int> stack = Debug ? stackarr.ToStack() : StackExtenstion.Create();
            Stack<int> copy;
            Ex2Thro6();
            Ex5And6();
            Ex7();
            Ex8();
            void Ex2Thro6()
            {
                Console.WriteLine($"S{s}");
                Console.WriteLine($"Length/Count: {s.Count(R)}");
                Console.WriteLine($"Sum: {s.Sum(R)}");
                Console.WriteLine($"Max: {s.Max(R)}");
            }
            void Ex5And6()
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
            void Ex7()
            {
                //7
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
            void Ex8()
            {
                Console.WriteLine("----------Spill/Reverse----------");
                Console.WriteLine($"Copy{copy.Reverse(R)}");
                Console.WriteLine($"Stack{stack.Reverse(R)}");
            }
        }
    }
}
