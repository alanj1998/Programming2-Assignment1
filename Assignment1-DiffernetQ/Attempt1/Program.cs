using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attempt1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vars
            Console.OutputEncoding = Encoding.UTF8;
            uint days, age;
            double retailValue, fine;
            string dvdType;
            bool repeat = true;

            //Assigning all possible values for dvd type
            string[] dvdTypes = new string[2];
            dvdTypes[0] = "NEW RELEASE";
            dvdTypes[1] = "OLD RELEASE";

            while (repeat)
            {
                Console.Clear();
                //Input
                days = UIntInputConvert("Enter days late: ");
                dvdType = StringInput("Enter DVD type: ", dvdTypes);
                retailValue = DoubleInputConvert("Enter DVD retail value: ");
                age = UIntInputConvert("Enter memebers age: ");

                //Calc
                fine = FineCalc(days, age, retailValue, dvdType);

                //Output
                Console.WriteLine("Your Fine is: {0:c2}.", fine);
                repeat = RepeatCheck();
            }
        }

        private static uint UIntInputConvert(string message)
        {
            //Vars
            uint valueToBeConverted = 0;
            bool convertCheck = false;

            //Loop until you get the right answer
            while (!convertCheck)
            {
                //Input
                Console.Write(message);
                convertCheck = uint.TryParse(Console.ReadLine(), out valueToBeConverted);

                //Check if the value is correct
                if (!convertCheck)
                    Console.WriteLine("Wrong value entered! Try again!\n");
            }

            //Return Value
            return valueToBeConverted;
        }

        private static double DoubleInputConvert(string message)
        {
            //Vars
            double valueToBeConverted = 0;
            bool convertCheck = false;

            //Loop until you get the right answer
            while (!convertCheck)
            {
                //Input
                Console.Write(message);
                convertCheck = double.TryParse(Console.ReadLine(), out valueToBeConverted);

                //Check if the value is correct
                if (!convertCheck)
                    Console.WriteLine("Wrong value entered! Try again!\n");
            }

            //Return Value
            return valueToBeConverted;
        }

        private static string StringInput(string message, string[] validOptions)
        {
            //Vars
            string input = "";

            while (!validOptions.Contains(input))
            {
                //Input
                Console.Write(message);
                input = Console.ReadLine().ToUpper().Trim();

                if (!validOptions.Contains(input))
                {
                    Console.WriteLine("Please enter {0} or {1} for input!", validOptions[0], validOptions[1]);
                }
            }

            return input;
        }

        private static double FineCalc(uint days, uint age, double retailValue, string releaseType)
        {
            //Vars
            double fullCharge;
            const double newUnder18 = 0.10, newOver18 = 1.12, oldUnder18 = 1.05, oldOver18 = 1.07;

            //Getting day charge
            double dayCharge = DayPrice(days);

            //Calculating full cost
            if (releaseType == "NEW RELEASE")
            {
                if (age < 18)
                    fullCharge = dayCharge + (retailValue * newUnder18);
                else
                    fullCharge = dayCharge * newOver18;
            }
            else
            {
                if (age < 18)
                    fullCharge = dayCharge * oldUnder18;
                else
                    fullCharge = dayCharge * oldOver18;
            }

            //Checking if fine exceedes full charge
            if (retailValue < fullCharge)
                fullCharge = retailValue;

            //Returning the charge
            return fullCharge;
        }

        private static double DayPrice(uint days)
        {
            //Vars
            double dayFine;

            //Assigning values
            switch (days)
            {
                case 0:
                    dayFine = 0;
                    break;
                case 1: case 2: case 3: case 4:
                    dayFine = days * 0.5;
                    break;
                case 5: case 6: case 7:
                    dayFine = days * 0.75;
                    break;
                case 8: case 9: case 10:
                    dayFine = days * 1.00;
                    break;
                case 11: case 12: case 13: case 14: case 15:
                    dayFine = days * 2.00;
                    break;
                default:
                    dayFine = days * 2.50;
                    break;
            }

            //Return day Fine
            return dayFine;
        }

        private static bool RepeatCheck()
        {
            //Vars
            bool repeat, tempCheck = false;
            char temp = 'Y';

            while (!tempCheck)
            {
                //Getting user to do something
                Console.Write("Would you like to calculate again?(y/n): ");
                tempCheck = char.TryParse(Console.ReadLine().Trim().ToUpper(), out temp);

                if (!tempCheck)
                    Console.WriteLine("Enter y or n as an answer only!");
            }

            if (temp == 'Y')
               return repeat = true;
            else
               return repeat = false;
        }
    }
}