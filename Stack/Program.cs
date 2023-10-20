using System;
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
