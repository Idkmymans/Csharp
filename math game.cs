using System;

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
            int score = 0;
            int life = 3;

            while (life > 0)
            {
                int num1 = random.Next(1, 11);
                int num2 = random.Next(1, 11);
                int correctAnswer = num1 + num2;

                Console.Write($"What is {num1} + {num2}? ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int userAnswer))
                {
                    if (userAnswer == correctAnswer)
                    {
                        score++;
                        Console.WriteLine("Correct! Your score is now: " + score);
                    }
                    else
                    {
                        life--;
                        Console.WriteLine($"Wrong! The correct answer was {correctAnswer}. You have {life} lives left.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }
    }
