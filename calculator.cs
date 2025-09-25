using System;
using System.Collections.Generic;

class Idk
{
    public static void Idks()
    {
        float a, b, r = 0;
        int c;
        string e = "";
        string op = "";

        List<string> h = new List<string>();

        do {
            Console.WriteLine("Enter 2 nums: ");
            float.TryParse(Console.ReadLine(), out a);
            float.TryParse(Console.ReadLine(), out b);

            Console.WriteLine("Choose operation:\n1. Add\n2. Subtract\n3. Multiply\n4. Divide");
            c = int.Parse(Console.ReadLine());

            switch (c) {
                case 1:
                    r = a + b;
                    op = "+";
                    Console.WriteLine($"The sum of {a} and {b} is {r}");
                    break;

                case 2:
                    r = a - b;
                    op = "-";
                    Console.WriteLine($"The difference of {a} and {b} is {r}");
                    break;

                case 3:
                    r = a * b;
                    op = "*";
                    Console.WriteLine($"The product of {a} and {b} is {r}");
                    break;

                case 4:
                    if (b == 0)
                        Console.WriteLine("Division by zero is not allowed.");
                    else
                    {
                        r = a / b;
                        Console.WriteLine($"The quotient of {a} and {b} is {r}");
                    }
                    op = "/";
                    break;
                
                default:
                    Console.WriteLine("Invalid choice");
                    continue;
            }
            if (c >= 1 && c <= 4 && !(c == 4 && b == 0))
            {
                h.Add($"{a} {op} {b} = {r}");
            }
            Console.WriteLine("Show history? (y/n): ");
            string u = Console.ReadLine();
            if (u == "y") {
                Console.WriteLine("== History ==");
                for (int i = 0; i < h.Count; i++) {
                    Console.WriteLine(h[i]);
                }
            }

            Console.WriteLine("Continue with operations? (type 'exit' to stop)");
            e = Console.ReadLine();

        } while (e != "exit");
    }
}
