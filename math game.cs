using System;
using System.Collections;

class Mathgame
{
    static void Main(string[] args)
    {
        Console.WriteLine("===Math Game===");

        Console.WriteLine("You have 3 lives. For each correct answer, you earn a point. For each wrong answer, you lose a life.");
        Console.WriteLine("choose your difficulty: \n 1. easy \n 2. medium \n 3. hard");

        int diff;
        while (!int.TryParse(Console.ReadLine(), out diff) || diff < 1 || diff > 3)
        {
            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
        }

        Start(diff);
    }

    static void Start(int diff)
    {
        Random random = new Random();

        int life = diff == 1 ? 5 : diff == 2 ? 3 : 1;//if(diff==1){life=5}else if(diff==2){life=3}else{life=1}
        int maxnum = diff == 1 ? 15 : diff == 2 ? 50 : 100;

        int score = 0;

        while (life > 0)
        {
            int num1 = random.Next(1, maxnum + 1);
            int num2 = random.Next(1, maxnum + 1);
            int correctanswer = 0;
            string op = "";
            switch (random.Next(1, 5))
            {
                case 1:
                    op = "+";
                    correctanswer = num1 + num2;
                    break;

                case 2:
                    op = "-";
                    correctanswer = num1 - num2;
                    break;

                case 3:
                    op = "*";
                    correctanswer = num1 * num2;
                    break;

                case 4:
                    op = "/";
                    while (num2 == 0 || num1 % num2 != 0) // Ensure no division by zero and result is an integer
                    {
                        num1 = random.Next(1, maxnum + 1);
                        num2 = random.Next(1, maxnum + 1);
                    }
                    correctanswer = num1 / num2;
                    break;
            }
            ;


            Console.Write($"What is {num1} {op} {num2}? ");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int userAnswer))
            {
                if (userAnswer == correctanswer)
                {
                    score++;
                    Console.WriteLine("Correct! Your score is now: " + score);
                }
                else
                {
                    life--;
                    Console.WriteLine($"Wrong! The correct answer was {correctanswer}. You have {life} lives left.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }

        Console.WriteLine($"game over! Final Score: {score}");
    }
}