using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Magic_8_Ball
{
    // Entry point for nmagic 8 ball app
    class Program
    {
        static void Main(string[] args)
        {
            // save defualt console color
            ConsoleColor oldColor = Console.ForegroundColor;

            TellPeopleWhatProgramThisIs();

            // Create a randomizer object
            Random randomObject = new Random();


            // loop fireever
            while (true)
            {
                string questionString = GetQuestionFromUser();

                int numberOfSecondsToSleep = randomObject.Next(5) + 1;
                Console.WriteLine("Thinking about your answer, stand by.....");
                Thread.Sleep(numberOfSecondsToSleep * 1000);

                if (questionString.Length == 0)
                {
                    Console.WriteLine("You need to ask a question fool!");
                    continue;
                }

                // See if the user typed 'quit' as the question
                if (questionString.ToLower() == "quit")
                {
                    break;
                }

                // get a random number
                int randomNumber = randomObject.Next(4);

                Console.ForegroundColor = (ConsoleColor)randomObject.Next(15);

                // Use random number to determain response
                switch (randomNumber)
                {
                    case 0:
                        {
                            Console.WriteLine("Yes!");
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("NO!");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("HELL NO!");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("OMG YES!");
                            break;
                        }
                }
            }

            // Cleaning up
            Console.ForegroundColor = oldColor;
        }
        // This will print the name of the program and who created it.
        static void TellPeopleWhatProgramThisIs()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("M");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("agic 8 Ball (by: Timothy)");
        }
        static string GetQuestionFromUser()
        {
            // this block of code will ask a user for a question 
            // and store thenquestion text in questionString variable

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ask a question?: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string questionString = Console.ReadLine();

            return questionString;

        }
    }
}
