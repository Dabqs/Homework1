using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            int intervalExtreme = 999999999;
        beginning:
            Console.Clear();
            Console.Write("Įveskite sveikąjį skaičių: ");
            string input = Console.ReadLine();

            if (!CheckIfIsInteger(input))
            {
                Console.WriteLine(input + " nėra sveikasis skaičius. Spauskite Enter jeigu norite tęsti.");
                Console.ReadLine();
                goto beginning;
            }

            int number = Convert.ToInt32(input);

            if (!CheckIfNumberIsInRange(intervalExtreme, number))
            {
                Console.WriteLine($"Skaicius {input} nėra intervale [-{intervalExtreme};{intervalExtreme}]. Spauskite Enter jeigu norite tęsti.");
                Console.ReadLine();
                goto beginning;
            }
            string answer = ChangeNumberToText(number).Trim();
            answer = char.ToUpper(answer[0]) + answer.Substring(1);
            Console.WriteLine(answer);
        }
        static bool CheckIfIsInteger(string input)
        {
            input = input.Trim();
            bool qualifiesToInteger = false;
            for (int i = 0; i < input.Length; i++)
            {
                char simbolis = input[i];
                if (char.IsDigit(simbolis))
                {
                    qualifiesToInteger = true;
                }
                else if (simbolis == '-' && input.IndexOf(simbolis) == 0) // jeigu prasideda minus zenklu
                {
                    qualifiesToInteger = true;
                }
                else
                {
                    qualifiesToInteger = false;
                }
            }
            return qualifiesToInteger;
        }
        static bool CheckIfNumberIsInRange(int intervalExtreme, int number)
        {
            bool digitIsInInterval = false;
            if (Math.Abs(number) <= intervalExtreme)
            {
                digitIsInInterval = true;
            }
            return digitIsInInterval;
        }
        static string ChangeNumberToText(int number)
        {
            Dictionary<int, string> dictNumbersToWords = new Dictionary<int, string>();
            dictNumbersToWords.Add(0, string.Empty);
            dictNumbersToWords.Add(1, "vienas");
            dictNumbersToWords.Add(2, "du");
            dictNumbersToWords.Add(3, "trys");
            dictNumbersToWords.Add(4, "keturi");
            dictNumbersToWords.Add(5, "penki");
            dictNumbersToWords.Add(6, "šeši");
            dictNumbersToWords.Add(7, "septyni");
            dictNumbersToWords.Add(8, "aštuoni");
            dictNumbersToWords.Add(9, "devyni");
            dictNumbersToWords.Add(10, "dešimt");
            dictNumbersToWords.Add(11, "vienuolika");
            dictNumbersToWords.Add(12, "dvylika");
            dictNumbersToWords.Add(13, "trylika");
            dictNumbersToWords.Add(14, "keturiolika");
            dictNumbersToWords.Add(15, "penkiolika");
            dictNumbersToWords.Add(16, "šešiolika");
            dictNumbersToWords.Add(17, "septyniolika");
            dictNumbersToWords.Add(18, "aštuoniolika");
            dictNumbersToWords.Add(19, "devyniolika");
            dictNumbersToWords.Add(20, "dvidešimt");
            dictNumbersToWords.Add(30, "trisdešimt");
            dictNumbersToWords.Add(40, "keturiasdešimt");
            dictNumbersToWords.Add(50, "penkiasdešimt");
            dictNumbersToWords.Add(60, "šešiasdešimt");
            dictNumbersToWords.Add(70, "septyniasdešimt");
            dictNumbersToWords.Add(80, "aštuoniasdešimt");
            dictNumbersToWords.Add(90, "devyniasdešimt");
            int firstDigit = 0;
            int secondDigit = 0;
            int thirdDigit = 0;
            string suffix = string.Empty;

            string numberToText = string.Empty;
            if (number < 0)
            {
                numberToText = "minus";
            }
            number = Math.Abs(number);
            if (number == 0)
            {
                return "Nulis";
            }

            if (number >= 100000000) //100 000 000 - 999 999 999
            {
                firstDigit = GetFirstDigit(number);
                if (firstDigit == 1)
                {
                    numberToText = numberToText + " simtas";
                }
                else
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[firstDigit]," šimtai");
                }
                if (GetFirstThreeNumbers(number).ToString().EndsWith("00"))
                {
                    numberToText = String.Concat(numberToText, " ", " milijonu");
                }
                CutFirstDigitFromNumber(ref number);
            }
            if (number >= 10000000) //10 000 000 - 99 999 999
            {
                string target = string.Empty;
                firstDigit = GetFirstDigit(number);
                secondDigit = GetSecondDigit(number);
                suffix = "milijonu";
                if (firstDigit == 1)
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[GetFirstTwoNumbers(number)], " ", suffix);
                }
                else 
                {
                    if (secondDigit == 1)
                    {
                        suffix = "milijonas";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                    }
                    else if(secondDigit ==0)
                    {
                        suffix = "milijonu";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)];
                    }
                    else
                    {
                        suffix = "milijonai";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                    }

                    numberToText = String.Concat(numberToText, " ", target, " ", suffix);
                }

                CutFirstDigitFromNumber(ref number);
                if (number != 0)
                {
                    CutFirstDigitFromNumber(ref number);
                }
            }
            if (number >= 1000000) //1 000 000 - 9 999 999
            {
                firstDigit = GetFirstDigit(number);
                if (firstDigit == 1)
                {
                    numberToText = String.Concat(numberToText, " milijonas");

                }
                else
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[firstDigit] + " milijonai");
                }
                CutFirstDigitFromNumber(ref number);

            }
            if (number >= 100000) //100 000 - 999 999
            {
                firstDigit = GetFirstDigit(number);
                if (firstDigit == 1)
                {
                    numberToText = numberToText + " simtas";
                }
                else
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[firstDigit], " šimtai");
                }
                if (GetFirstThreeNumbers(number).ToString().EndsWith("00"))
                {
                    numberToText = String.Concat(numberToText, " ", " tukstanciu");
                }
                CutFirstDigitFromNumber(ref number);
            }
            if (number >= 10000) //10 000 - 99 999
            {
                string target = string.Empty;
                firstDigit = GetFirstDigit(number);
                secondDigit = GetSecondDigit(number);
                suffix = "tukstanciu";
                if (firstDigit == 1)
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[GetFirstTwoNumbers(number)], " ", suffix);
                }
                else
                {
                    if (secondDigit == 1)
                    {
                        suffix = "tukstantis";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                    }
                    else if (secondDigit == 0)
                    {
                        suffix = "tukstanciu";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)];
                    }
                    else
                    {
                        suffix = "tukstanciai";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                    }

                    numberToText = String.Concat(numberToText, " ", target, " ", suffix);
                }

                CutFirstDigitFromNumber(ref number);
                if (number != 0)
                {
                    CutFirstDigitFromNumber(ref number);
                }
            }
            if (number >= 1000) //1000-9999
            {
                firstDigit = GetFirstDigit(number);
                if (firstDigit == 1)
                {
                    numberToText = String.Concat(numberToText, " tukstantis");

                }
                else
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[firstDigit] + " tukstanciai");
                }
                CutFirstDigitFromNumber(ref number);
            }
            if (number >= 100) // 100-999
            {
                firstDigit = GetFirstDigit(number);
                if (firstDigit == 1)
                {
                    numberToText = numberToText + " simtas";
                }
                else
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[firstDigit], " šimtai");
                }
                CutFirstDigitFromNumber(ref number);
            }
            if ( number >= 10)
            {
                string target = string.Empty;
                firstDigit = GetFirstDigit(number);
                secondDigit = GetSecondDigit(number);
                suffix = "";
                if (firstDigit == 1)
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[GetFirstTwoNumbers(number)], " ", suffix);
                }
                else
                {
                    if (secondDigit == 1)
                    {
                        suffix = "";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                    }
                    else if (secondDigit == 0)
                    {
                        suffix = "";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)];
                    }
                    else
                    {
                        suffix = "";
                        target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                    }

                    numberToText = String.Concat(numberToText, " ", target, " ", suffix);
                }

                CutFirstDigitFromNumber(ref number);
                if (number != 0)
                {
                    CutFirstDigitFromNumber(ref number);
                }

            }
            if (number < 10)
            {
                firstDigit = GetFirstDigit(number);
                if (firstDigit == 1)
                {
                    numberToText = String.Concat(numberToText, " vienas");

                }
                else
                {
                    numberToText = String.Concat(numberToText, " ", dictNumbersToWords[firstDigit]);
                }
            }

            return numberToText;
        }
        static int GetFirstDigit(int number)
        {
            return Convert.ToInt32(number.ToString()[0].ToString());
        }
        static int GetSecondDigit(int number)
        {
            return Convert.ToInt32(number.ToString()[1].ToString());
        }
        static int FloorBeginningToHundreds(int firstNumber) {
            return Convert.ToInt32(firstNumber.ToString() + "00");
        }
        static int FloorBeginningToTens(int firstNumber)
        {
            return Convert.ToInt32(firstNumber.ToString() + "0");
        }
        static int GetFirstTwoNumbers(int number)
        {
            return Convert.ToInt32(number.ToString().Substring(0,2));
        }
        static int GetFirstThreeNumbers(int number)
        {
            return Convert.ToInt32(number.ToString().Substring(0, 3));
        }
        static void CutFirstDigitFromNumber(ref int number)
        {
            string numberString = number.ToString();
            if (numberString.Length == 1)
            {
                number = 0;
            }
            else
            {
                number = Convert.ToInt32(numberString.Substring(1));
            }
        }
    }
}
