using System;
using System.Collections.Generic;
using System.ComponentModel;


class calculator
{
    List<string> history = new List<string>();
    public float add(float a, float b)
    {
        history.Add($"{a} + {b} = {a + b}");
        return a + b;

    }

    public float subtract(float a, float b)
    {
        history.Add($"{a} - {b} = {a - b}");
        return a - b;
    }

    public float multiply(float a, float b)
    {
        history.Add($"{a} * {b} = {a * b}");
        return a * b;
    }

    public float divide(float a, float b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }
        history.Add($"{a} / {b} = {a / b}");
        return a / b;
    }

    //history
    public void showHistory()
    {
        Console.WriteLine("==History==");
        foreach (var entry in history)
        {
            Console.WriteLine(entry);
        }
    }

    public void clearHistory()
    {
        history.Clear();
    }

}

    class Program
    {
    public static void Idk()
    {
        calculator calc = new calculator();

    lb: Console.WriteLine("Enter 2 nums: ");
        float.TryParse(Console.ReadLine(), out float a);
        float.TryParse(Console.ReadLine(), out float b);


        Console.WriteLine("Choose operation:\n1. Add\n2. Subtract\n3. Multiply\n4. Divide");
        int.TryParse(Console.ReadLine(), out int choice);

        float res = choice switch
        {
            1 => calc.add(a, b),
            2 => calc.subtract(a, b),
            3 => calc.multiply(a, b),
            4 => calc.divide(a, b),
            _ => throw new InvalidCastException("Invalid choice")
        };
        Console.WriteLine($"The result is: {res}");

        Console.WriteLine("Show history? (y/n): ");
        string y = Console.ReadLine();
        if (y == "y")
        {
            calc.showHistory();

        }
        Console.WriteLine("clear history? (y/n): ");
        string z = Console.ReadLine();
        if (z == "y")
        {
            calc.clearHistory();
            Console.WriteLine("History cleared.");
        }
        Console.WriteLine("Continue? (y/n): ");
        string c = Console.ReadLine();
        if (c == "y")
        {
            goto lb;
        }
        Console.WriteLine("Sayonara");
    }

}

    
