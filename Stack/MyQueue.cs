using System;
using Unit4.CollectionsLib;

public class MyQueue
{
    private Queue<string> queue;
    private int size = 0;
    public MyQueue()
    {
        Initilaize();
    }
    public MyQueue(string[] strings)
    {
        Initilaize();
        foreach (string str in strings)
        {
            this.queue.Insert(str);
            this.size++;
        }
    }
    public MyQueue(MyQueue myQueue)
    {
        this.queue = myQueue.queue.Copy();
        this.size = myQueue.size;
    }
    private void Initilaize()
    {
        this.queue = new Queue<string>();
        this.size = 0;
    }
    public Queue<string> GetQueue()
    {
        return this.queue.Copy();
    }
    public int GetSize()
    {
        return this.size;
    }
    public override string ToString()
    {
        string str = $"Size:{this.size} [";
        return ToString(str, new Queue<string>());
    }
    public string ToString(string str, Queue<string> queue)
    {
        if (this.queue.IsEmpty())
        {
            this.queue.Fix(queue);
            return $"{str}]";
        }
        string s = this.queue.Remove();
        queue.Insert(s);
        s = $"{s},";
        str += s;
        return ToString(str, queue);
    }
    public void Add(string str)
    {
        this.queue.Insert(str);
        this.size++;
    }
    public string Remove()
    {
        string str = this.queue.Remove();
        this.size--;
        return str;
    }
    public void Remove(string str)
    {
        Remove(str, new Queue<string>());
    }
    private void Remove(string str, Queue<string> queue)
    {
        if (this.queue.IsEmpty())
        {
            this.queue.Fix(queue);
            return;
        }
        string s = this.queue.Remove();
        if (s.Contains(str))
        {
            this.size--;
            Remove(str, queue);
            return;
        }
        queue.Insert(s);
        Remove(str, queue);
    }
    public void Remove(char c)
    {
        Remove(c.ToString());
    }
    public void Empty()
    {
        this.queue = new Queue<string>();
        this.size = 0;
    }
    public void Reverse()
    {
        Reverse(new Stack<string>());
    }
    private void Reverse(Stack<string> stack)
    {
        if (queue.IsEmpty())
        {
            queue = stack.ToQueue();
            return;
        }
        stack.Push(queue.Remove());
        Reverse(stack);
    }
    public void ReversePrint()
    {
        MyQueue myQueue = new MyQueue(this);
        myQueue.Reverse();
        Console.WriteLine(myQueue);
    }
    public string RemoveTail()
    {
        if (this.queue.IsEmpty())
        {
            return string.Empty;
        }
        return RemoveTail(new Queue<string>(), this.queue.Remove());
    }
    private string RemoveTail(Queue<string> queue, string str)
    {
        if (this.queue.IsEmpty())
        {
            this.queue.Fix(queue);
            this.size--;
            return str;
        }
        queue.Insert(str);
        return RemoveTail(queue, this.queue.Remove());
    }
    public Queue<string> GetReverseQueue()
    {
        MyQueue myQueue = new MyQueue(this);
        myQueue.Reverse();
        return myQueue.queue;
    }

    public static void Program()
    {
        //1,2
        string[] strings = { "I", "love", "my", "Queue", " ", " Queue", "is", "mush" };
        MyQueue myQueue = new MyQueue(strings);
        //3,4
        myQueue.ReversePrint();
        //5
        MyQueue copy = new MyQueue(myQueue);
        //6
        Console.WriteLine($"Removed[{myQueue.Remove()}], MyQueue:{myQueue}");
        //7
        myQueue.Empty();
        Console.WriteLine($"MyQueue.Empty(); {myQueue}");
        //8 
        Console.WriteLine($"Copy: {copy}, IsCopyEmpty {copy.queue.IsEmpty()}");
        //9
        copy.Remove("Queue");
        Console.WriteLine(copy);
        //10
        copy.Remove('m');
        Console.WriteLine(copy);
    }
}
