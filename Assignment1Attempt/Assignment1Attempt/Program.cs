using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1Attempt
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vars
            Console.OutputEncoding = Encoding.UTF8;
            double vehicleValue = 0, premiumQuote;
            uint age, points;
            string gender;

            while (vehicleValue != -999)
            {
                //Input
                vehicleValue = DoubleInputCheck("Enter vehicle value: ");
                if (vehicleValue == -999)
                    continue;


                gender = GenderInput("Enter Gender: ");
                age = IntegerInputCheck("Enter Age: ");
                points = IntegerInputCheck("Enter penalty points: ");

                //Calculating Points
                premiumQuote = FullCostCalc(vehicleValue, points, age, gender);

                //Output
                Console.WriteLine("Your Premium Quote is: {0:c2}.", premiumQuote);
            }
        }

        /* INPUT */
        private static double DoubleInputCheck(string message)
        {
            //Vars
            double convertedValue = 0;
            bool valueCheck = false;

            while (!valueCheck)
            {
                //Asking the user to input a value
                Console.Write(message);
                valueCheck = double.TryParse(Console.ReadLine(), out convertedValue);

                //Checking if the value is of type double
                if (!valueCheck)
                    Console.WriteLine("Wrong value entered! Try Again!");
            }

            return convertedValue;
        }

        private static uint IntegerInputCheck(string message)
        {
            //Vars
            uint convertedValue = 0;
            bool valueCheck = false;

            while (!valueCheck)
            {
                //Asking the user to input a value
                Console.Write(message);
                valueCheck = uint.TryParse(Console.ReadLine(), out convertedValue);

                //Checking if the value is of type double
                if (!valueCheck)
                    Console.WriteLine("Wrong value entered! Try Again!");
            }

            return convertedValue;
        }

        private static string GenderInput(string message)
        {
            //Vars
            string gender = "";

            while (gender != "MALE" && gender != "FEMALE")
            {
                Console.Write(message);
                gender = Console.ReadLine().ToUpper().Trim();

                if (gender != "MALE" && gender != "FEMALE")
                    Console.WriteLine("Enter Male or Female!\n");
            }

            return gender;
        }

        /* Calculating premium */
        private static double PointsAdditionalCost(uint points)
        {
            //Vars
            double additionalCost;

            switch(points)
            {
                case 0:
                    additionalCost = 0;
                    break;
                case 1: case 2: case 3: case 4:
                    additionalCost = 100;
                    break;
                case 5: case 6: case 7:
                    additionalCost = 200;
                    break;
                case 8: case 9: case 10:
                    additionalCost = 300;
                    break;
                case 11: case 12:
                    additionalCost = 400;
                    break;
                default:
                    additionalCost = 0;
                    Console.WriteLine("No Quote Possible!");
                    break;
            }

            return additionalCost;
        }

        private static double FullCostCalc(double carValue, uint points, uint age, string gender)
        {
            //Vars
            const double BASIC_PREMIUM = 0.03, MALE_UNDER_25 = 1.10, FEMALE_UNDER_25 = 1.06;
            double pointsCost = PointsAdditionalCost(points);
            double fullCost;

            //Calculating full cost
            fullCost = carValue * BASIC_PREMIUM;

            if (gender == "MALE")
                fullCost *= MALE_UNDER_25;
            else
                fullCost *= FEMALE_UNDER_25;

            fullCost += pointsCost;

            return fullCost;
        }
    }
}
