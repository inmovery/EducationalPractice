using System;

namespace Task_4 {
    public class Program {
        private static void Main(string[] args) {
            // ввод
            Console.Write("Введите n:");
            int n = InputInteger(); // степень и нижний индекс

            Console.Write("Введите x:");
            double x = InputDouble(); // действительная часть комплексного числа
            Console.Write("Введите y:");
            double y = InputDouble(); // коэффициент при мнемой части комплексного числа

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("———————————————————————————————————————————————————————————");
            Console.WriteLine("| Введите коэфициенты для i многочлена [ a(n) + b(n)i ] |");
            Console.ResetColor();

            double[] a = new double[n+1];
            double[] b = new double[n+1];
            for (int i = n; i >= 0; i--) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("———————————————————————————————————————————————————————————");
                Console.ResetColor();
                Console.Write("Введите a({0}) = ", i);
                a[i] = InputDouble();
                Console.Write("Введите b({0}) = ", i);
                b[i] = InputDouble();
            }
            Complex result = new Complex(a[0],b[0]);
            for (int i = n; i > 0; i--) {
                Complex temp = (new Complex(a[i], b[i])) * Complex.Pow(new Complex(x, y), i);
                result += temp;
            }

            Console.ForegroundColor = ConsoleColor.Green;

            // проверки и усовершенствование вывода результата
            if (result.ToString() == "0 + 0i") {
                Console.WriteLine("Ответ: 0");
            }
            if (result.Imaginary > 0) {
                if (result.ToString().Split('+')[0].Trim() == "0" && result.ToString().Split('+')[1].Trim() != "0i") {
                    Console.WriteLine("Ответ: " + result.Imaginary + "i");
                } else if (result.ToString().Split('+')[1].Trim() == "0i" && result.ToString().Split('+')[0].Trim() != "0") {
                    Console.WriteLine("Ответ: " + result.Real);
                } else {
                    Console.WriteLine("Ответ: {0}", result.ToString());
                }
            } else {
                if (result.ToString().Split('-')[0].Trim() == "0" && result.ToString().Split('-')[1].Trim() != "0i") {
                    Console.WriteLine("Ответ: " + result.Imaginary + "i");
                } else if (result.ToString().Split('-')[1].Trim() == "0i" && result.ToString().Split('-')[0].Trim() != "0") {
                    Console.WriteLine("Ответ: " + result.Real);
                } else {
                    Console.WriteLine("Ответ: {0}", result.ToString());
                }
            }
            Console.ResetColor();
            Console.ReadKey();
            
        }

        /// <summary>
        /// Ввод действительных чисел
        /// </summary>
        /// <returns></returns>
        private static double InputDouble() {
            bool ok = true;
            double result;
            do {
                string text = Console.ReadLine();
                ok = double.TryParse(text, out result);
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ошибка. Неверный ввод!");
                    Console.Write("Введите повторно : ");
                    Console.ResetColor();
                }
            } while (!ok);
            return result;
        }

        /// <summary>
        /// Ввод натуральных, целых чисел
        /// </summary>
        /// <returns></returns>
        private static int InputInteger() {
            bool ok = true;
            int result;
            do {
                string text = Console.ReadLine();
                ok = int.TryParse(text, out result);
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ошибка. Неверный ввод!");
                    Console.Write("Введите повторно : ");
                    Console.ResetColor();
                }
            } while (!ok);
            return result;
        }

    }

    public class Complex {

        public double Real { get; }
        public double Imaginary { get; }

        public Complex(double real, double imaginary) {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        /// <summary>
        /// Сложение комплексных чисел
        /// </summary>
        /// <param name="c1">Первое слагаемое</param>
        /// <param name="c2">Второе слагаемое</param>
        public static Complex operator +(Complex c1, Complex c2) {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        /// <summary>
        /// Разность комплексных чисел
        /// </summary>
        /// <param name="c1">Первое слагаемое</param>
        /// <param name="c2">Второе слагаемое</param>
        public static Complex operator -(Complex c1, Complex c2) {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        /// <summary>
        /// Произведение комплексных чисел
        /// </summary>
        /// <param name="c1">Первое слагаемое</param>
        /// <param name="c2">Второе слагаемое</param>
        public static Complex operator *(Complex c1, Complex c2) {
            return new Complex(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary, c2.Real * c1.Imaginary + c1.Real * c2.Imaginary);
        }

        /// <summary>
        /// Произведение действительного и комплексного числа
        /// </summary>
        /// <param name="d"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex operator *(double d, Complex a) { return new Complex(d * a.Real, d * a.Imaginary); }

        /// <summary>
        /// Деление комплексных чисел
        /// </summary>
        /// <param name="c1">Первое слагаемое</param>
        /// <param name="c2">Второе слагаемое</param>
        public static Complex operator /(Complex c1, Complex c2) {
            if ((c2.Real == 0.0f) && (c2.Imaginary == 0.0f)) throw new DivideByZeroException("На ноль невозможно делить!");

            double newReal =
            (c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) /
            (c2.Real * c2.Real + c2.Imaginary * c2.Imaginary);

            double newImaginary =
            (c2.Real * c1.Imaginary - c1.Real * c2.Imaginary) /
            (c2.Real * c2.Real + c2.Imaginary * c2.Imaginary);

            return (new Complex(newReal, newImaginary));
        }

        /// <summary>
        /// Возведение комплексного числа в степень
        /// </summary>
        /// <param name="a">Исходное комплексное число</param>
        /// <param name="b">Возводимая степень</param>
        /// <returns></returns>
        public static Complex Pow(Complex a, double b) { return Exp(b * Log(a)); }

        /// <summary>
        /// Модуль комплексного числа
        /// </summary>
        /// <param name="a">Исходное комплексное число</param>
        /// <returns></returns>
        public static double Abs(Complex a) { return Math.Sqrt(a.Imaginary * a.Imaginary + a.Real * a.Real); }

        /// <summary>
        /// Экспонента комплексного числа
        /// </summary>
        /// <param name="a">Исходное комплексное число</param>
        /// <returns></returns>
        public static Complex Exp(Complex a) { return new Complex(Math.Exp(a.Real) * Math.Cos(a.Imaginary), Math.Exp(a.Real) * Math.Sin(a.Imaginary)); }

        /// <summary>
        /// Логорифм комплексного исла
        /// </summary>
        /// <param name="a">Исходное комплексное число</param>
        /// <returns></returns>
        public static Complex Log(Complex a) { return new Complex(Math.Log(Abs(a)), Arg(a)); }

        /// <summary>
        /// Аргумент для подсчёта логарифма комплексного числа
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Arg(Complex a) {
            if (a.Real < 0) {
                if (a.Imaginary < 0) return Math.Atan(a.Imaginary / a.Real) - Math.PI;
                else return Math.PI - Math.Atan(-a.Imaginary / a.Real);
            } else return Math.Atan(a.Imaginary / a.Real);
        }

        /// <summary>
        /// Вывод комплекных чисел
        /// </summary>
        public override string ToString() {
            if(Imaginary < 0)
                return string.Format("{0} - {1}i", Real, (-1)*Imaginary);
            else
                return string.Format("{0} + {1}i", Real, Imaginary);
        }

    }

}
