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
    }
}
