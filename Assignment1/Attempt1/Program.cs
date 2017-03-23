/*
 *  #######################################################
 *  ## Assignment1 - Employee Pay Calculator             ##
 *  ##   The app uses:                                   ##
 *  ##   1)Two methods to calculate hourly rate and pay  ##
 *  ##   2)Switch statement to apply basic hourly rate   ##
 *  ##   3)Checks for user input (no way to enter the    ##
 *  ##      information incorrectly)                     ##
 *  ##                                                   ##
 *  ## Copyright (c) Alan Jachimczak                     ##
 *  #######################################################
 */

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
            uint age = 0, hoursWorked = 0;
            double pay;
            string qualification = "", repeat = "Y", ageCheckRequest = "", inputFormat = "{0, -49}{1,-2}";
            bool hoursWorkedCheck = true, ageCheck = true, repeatCheck = true;
            Console.OutputEncoding = Encoding.UTF8; //Applying UTF-8 encoding so that the euro sign is displayed in console

            /* OUTER LOOP THAT MAKES SURE THAT THE USER ENTERS CORRECT DETAILS */
            while (hoursWorkedCheck == true && ageCheck == true && repeatCheck == true)
            {
                ///////////// 
                //  INPUT  // 
                /////////////

                //Clearing the console window for better presentation
                Console.Clear();

                //Getting value for the amount of hours and making sure it is entered right
                Console.Write(inputFormat, "Enter the amount of hours worked",":");
                hoursWorkedCheck = uint.TryParse(Console.ReadLine(), out hoursWorked);
                if (hoursWorkedCheck == false || hoursWorked < 1 || hoursWorked > 24)
                {
                    Console.WriteLine("Wrong value entered for hours!");
                    hoursWorkedCheck = true;        //Setting the value for the loop so that it doesn't terminate
                    continue;
                }

                //Getting the value for the age and making sure it's correct
                Console.Write(inputFormat, "Enter employee age", ":");
                ageCheck = uint.TryParse(Console.ReadLine(), out age);
                if (ageCheck == false || age < 1)
                {
                    Console.WriteLine("Wrong value entered for age!");
                    ageCheck = true;
                    continue;
                }
                else if (age < 15)
                {
                    //When an age of under 15 is entered, the user is prompted to know his entered age
                    while (ageCheckRequest != "YES" && ageCheckRequest != "NO")
                    {
                        Console.Write("Are you sure your that the employees age is {0}? (yes/no): ", age);
                        ageCheckRequest = Console.ReadLine().ToUpper().Trim();

                        if (ageCheckRequest != "YES" && ageCheckRequest != "NO")
                        {
                            Console.WriteLine("Enter yes or no for an answer!");
                            continue;
                        }
                    }
                    if (ageCheckRequest == "NO")
                        continue;
                }

                //Checking for qualifications and making sure the app recognies it
                Console.Write(inputFormat, "Does the employee have qualifications? (yes/no)",":");
                qualification = Console.ReadLine().ToUpper().Trim();
                if (qualification != "YES" && qualification != "NO")
                {
                    Console.WriteLine("Wrong value entered for qualifications!");
                    continue;
                }

                ///////////////////////////
                // OUTPUT AND PROCESSING //
                ///////////////////////////
                pay = PayCalculation(age, hoursWorked, qualification);

                Console.WriteLine(inputFormat + "{2,-5}", "Your pay is", ":", pay.ToString("c2"));

                //Checking that user input is correct
                while (repeatCheck == true)
                {
                    Console.Write(inputFormat, "Do you wish to calculate again? (yes/no)", ":");
                    repeat = Console.ReadLine().ToUpper().Trim();

                    if (repeat != "YES" && repeat != "NO")
                    {
                        Console.WriteLine("Type yes or no as an answer!");
                        continue;
                    }
                    else if (repeat == "YES")
                        break;
                    else
                        repeatCheck = false;
                }
            }
        }

        /*
         *   ########################
         *   # SUPLEMENTARY METHODS #
         *   ########################
         */

        static private double PayCalculation(uint age, uint hoursWorked, string qualification)
        {
            //Vars
            double pay = 0, rateOfPay = 0;

            //Calculating the rate of pay for employee
            switch (hoursWorked)
            {
                case 1: case 2: case 3: case 4: case 5:
                    rateOfPay = RateOfPayCalc(5.00, qualification, age);
                    break;
                case 6: case 7:
                    rateOfPay = RateOfPayCalc(6.00, qualification, age);
                    break;
                case 8: case 9: case 10:
                    rateOfPay = RateOfPayCalc(7.00, qualification, age);
                    break;
                case 11: case 12: case 13: case 14: case 15:
                    rateOfPay = RateOfPayCalc(9.00, qualification, age);
                    break;
                case 16: case 17: case 18: case 19: case 20: case 21: case 22: case 23: case 24:
                    rateOfPay = RateOfPayCalc(10.00, qualification, age);
                    break;
            }

            //Calculating pay
            pay = hoursWorked * rateOfPay;
            if (pay > 50)
                pay = 50;

            //Returning the pay amount to the Main() method
            return pay;
        }

        static private double RateOfPayCalc(double hourlyRate, string qualification, uint age)
        {
            //Checking for gain in hourly rate due to age and qualifications
            if (age < 21)
            {
                if (qualification == "NO")
                    hourlyRate /= 1.1;  //10 % loss to hourly rate
            }
            else
            {
                if (qualification == "YES")
                    hourlyRate *= 1.2;  //20% gain to hourly rate
                else
                    hourlyRate *= 1.15; //15% gain to hourly rate
            }
                
            //Returning the hourly rate
            return hourlyRate;
        }
    }
}