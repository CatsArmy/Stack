using System;
using System.Net.Http.Headers;
using Unit4.CollectionsLib;

namespace Stack
{
    internal class MyIntStack
    {
        private Stack<int> stack;
        public MyIntStack(int[] arr) 
        {
            stack = arr.ToStack();
        }
        public MyIntStack(MyIntStack stack)
        {
            this.stack = stack.stack.Copy();
        }
        public Stack<int> GetStack()
        {
            return stack.Copy();
        }
        public int GetSize()
        {
            return stack.Count();
        }
        public Stack<int> GetReverseStack()
        {
            return stack.Reverse();
        }
        public override string ToString()
        {
            Stack<int> stack = this.stack.Copy();
            return $"Size:{GetSize()}\n[{stack.Pop()}" + ToString(stack);
        }
        public string ToString(Stack<int> stack)
        {
            if (stack.IsEmpty()) return "]";

            return $", {stack.Pop()}" + ToString(stack);
        }
        public void ReversePrint()
        {
            MyIntStack stack = new MyIntStack(this.stack.Reverse().ToArray());
            Console.WriteLine(stack);
        }
        public void Add(int s)
        {
            stack.Push(s);
        }
        public void Remove(int s)
        {
            Remove(s, new Stack<int>());
        }
        private void Remove(int s, Stack<int> fix)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(fix);
                return;
            }
            int value = stack.Pop();
            if (value != s)
            {
                fix.Push(value);
            }
            Remove(s, fix);
        }
        public void Empty()
        {
            if (stack.IsEmpty()) return;
            stack.Pop();
            Empty();
            
            //or just do this:
            //stack = new Stack<int>();
        }
        public void RemoveDigit(int digit)
        {
            if (digit.ToString().Length > 1) return;
            RemoveDigit(digit, new Stack<int>());
        }
        private void RemoveDigit(int s, Stack<int> fix)
        {
            if (stack.IsEmpty())
            {
                stack.Fix(fix);
                return;
            }
            int value = stack.Pop();
            if (!value.ToString().Contains(s.ToString()))
            {
                fix.Push(value);
            }
            Remove(s, fix);
        }
        public int GetBottom()
        {
            return GetReverseStack().Top();
        }
    }
}
