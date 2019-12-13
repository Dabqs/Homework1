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

            if (!IsInteger(input))
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
        static bool IsInteger(string input)
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
            Dictionary<int, string> dictNumbersToWords = CreateDictionaryNumberToWords();

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
                numberToText = HundredsToText(number, numberToText, "milijonu", dictNumbersToWords);
                CutFirstDigitFromNumber(ref number);
            }

            if (number >= 10000000) //10 000 000 - 99 999 999
            {
                numberToText = TensToText(number, numberToText, dictNumbersToWords, "milijonu", "milijonas", "milijonai");
                CutFirstDigitFromNumber(ref number);
                if (number != 0)
                {
                    CutFirstDigitFromNumber(ref number);
                }
            }

            if (number >= 1000000) //1 000 000 - 9 999 999
            {
                numberToText = UnitsToText(number, numberToText, dictNumbersToWords, "milijonas", "milijonai");
                CutFirstDigitFromNumber(ref number);
            }

            if (number >= 100000) //100 000 - 999 999
            {
                numberToText = HundredsToText(number, numberToText, "tukstanciu", dictNumbersToWords);
                CutFirstDigitFromNumber(ref number);
            }

            if (number >= 10000) //10 000 - 99 999
            {
                numberToText = TensToText(number, numberToText, dictNumbersToWords, "tukstanciu", "tukstantis", "tukstanciai");
                CutFirstDigitFromNumber(ref number);
                if (number != 0)
                {
                    CutFirstDigitFromNumber(ref number);
                }
            }

            if (number >= 1000) //1000-9999
            {
                numberToText = UnitsToText(number, numberToText, dictNumbersToWords, "tukstantis", "tukstanciai");

                CutFirstDigitFromNumber(ref number);
            }

            if (number >= 100) // 100-999
            {
                numberToText = HundredsToText(number, numberToText, string.Empty, dictNumbersToWords);
                CutFirstDigitFromNumber(ref number);
            }

            if (number >= 10)
            {
                numberToText = TensToText(number, numberToText, dictNumbersToWords, string.Empty, string.Empty, string.Empty);
                CutFirstDigitFromNumber(ref number);
                if (number != 0)
                {
                    CutFirstDigitFromNumber(ref number);
                }
            }

            if (number < 10)
            {
                numberToText = UnitsToText(number, numberToText, dictNumbersToWords, "vienas", string.Empty);
            }

            return numberToText;
        }
        static Dictionary<int, string> CreateDictionaryNumberToWords()
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
            return dictNumbersToWords;
        }
        static string HundredsToText(int number, string numberToText, string suffix, Dictionary<int, string> dictNumbersToWords)
        {
            int firstDigit = GetFirstDigit(number);
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
                numberToText = String.Concat(numberToText, " ", suffix);
            }
            return numberToText;
        }
        static string TensToText(int number, string numberToText, Dictionary<int, string> dictNumbersToWords, string suffix0, string suffix1, string suffixOther)
        {
            string target = string.Empty;
            string suffix = string.Empty;
            int firstDigit = GetFirstDigit(number);
            int secondDigit = GetSecondDigit(number);
            if (firstDigit == 1)
            {
                suffix = suffix0;
                return String.Concat(numberToText, " ", dictNumbersToWords[GetFirstTwoNumbers(number)], " ", suffix);
            }
            else
            {
                if (secondDigit == 1)
                {
                    suffix = suffix1;
                    target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                }
                else if (secondDigit == 0)
                {
                    suffix = suffix0;
                    target = dictNumbersToWords[FloorBeginningToTens(firstDigit)];
                }
                else
                {
                    suffix = suffixOther;
                    target = dictNumbersToWords[FloorBeginningToTens(firstDigit)] + " " + dictNumbersToWords[secondDigit];
                }

                return String.Concat(numberToText, " ", target, " ", suffix);
            }
        }
        static string UnitsToText(int number, string numberToText, Dictionary<int, string> dictNumbersToWords, string suffix1, string suffixOther)
        {
            int firstDigit = GetFirstDigit(number);
            if (!String.IsNullOrWhiteSpace(suffixOther))
            {
                suffixOther = " " + suffixOther;
            }

            if (firstDigit == 1)
            {
                return String.Concat(numberToText, " " + suffix1);
            }
            else
            {
                return String.Concat(numberToText, " ", dictNumbersToWords[firstDigit] + suffixOther);
            }
        }
        static int GetFirstDigit(int number)
        {
            return Convert.ToInt32(number.ToString()[0].ToString());
        }
        static int GetSecondDigit(int number)
        {
            return Convert.ToInt32(number.ToString()[1].ToString());
        }
        static int FloorBeginningToTens(int firstNumber)
        {
            return Convert.ToInt32(firstNumber.ToString() + "0");
        }
        static int GetFirstTwoNumbers(int number)
        {
            return Convert.ToInt32(number.ToString().Substring(0, 2));
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
