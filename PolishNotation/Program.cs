using System;
using System.Collections;

namespace PolishNotation
{
    internal class Program
    {
        // AHMED YILMAZ 171213038
        public enum Operators
        {
            ADD = 0,
            SUB = 1,
            MUL = 2,
            DIV = 3
        }
        
        private static bool OperatorCheck(string ch)
        {
            return ((ch == "+") | (ch ==  "-") | (ch == "*") | (ch == "/"));
        }

        private static int OperatorType(string ch)
        {
            switch (ch)
            {
                case "+":
                    return (int)Operators.ADD;
                case "-":
                    return (int)Operators.SUB;
                case "*":
                    return (int)Operators.MUL;
                case "/":
                    return (int)Operators.DIV;
                default:
                    return -1;
            }
        }

        private static double Calculate(string ch, double value, double value2)
        {
            var Operator = OperatorType(ch);
            switch (Operator)
            {
                case 0:
                    return value + value2;
                case 1:
                    return value - value2;
                case 2:
                    return value * value2;
                case 3:
                    return value / value2;
                default:
                    Console.WriteLine("Hatalı giriş yaptınız!");
                    return -1;
            }
        }
        public static void Main(string[] args)
        {
            Stack stack = new Stack();
            Console.Write("Polish Notasyonu giriniz:");
            var notation = Console.ReadLine()?.Split(' ');
            
            for (int i = notation.Length -1; i>=0; i--)
            {
                //OPERATOR MU KONTROL ET
                if (OperatorCheck(notation[i]))
                {
                    // EGER GELEN DEGER OPERATORSE HESAPLAMA ISLEMINI YAP
                    stack.Push(Calculate(notation[i],double.Parse((string) stack.Pop()), double.Parse((string) stack.Pop())).ToString());
                }
                else
                {
                    // GELEN DEGER OPERATOR DEGILSE DIREKT EKLE
                    stack.Push(notation[i]);
                }
            }

            while (stack.Count != 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}