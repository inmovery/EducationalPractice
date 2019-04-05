using System;

namespace Task_5
{
    public class Program
    {

        private static void Main(string[] args)
        {
            Console.Write("Порядок матрицы = ");
            int n = InputInteger();

            double[,] matr = new double[n, n];

            FillingMatrix(ref matr, n);//сборка матрицы

            PrintMatrix(matr, n);//вывод матрицы

            Console.WriteLine(SearchMaxElement(matr, n));//вывод максимального элемента в матрице

            Console.ReadKey();

        }

        private static void PrintMatrix(double[,] matr, int n)
        {
            string pre = " ";
            for (int i = 0; i < n; i++)
            {
                pre += "————————";
            }
            Console.WriteLine(pre + "—");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(String.Format("{0,2}{1,6:0.00}", "|", matr[i, j]));
                }
                Console.WriteLine(String.Format("{0,2}", "|"));
            }
            Console.WriteLine(pre + "—");
        }


        /// <summary>
        /// Заполнение матрицы
        /// </summary>
        /// <param name="matr">Исходная матрица</param>
        /// <param name="n">Количество строк/столбцов</param>
        /// <param name="type">Тип ввода</param>
        private static void FillingMatrix(ref double[,] matr, int n, int type = 0)
        {
            Random rnd = new Random();
            if (type == 0)
            { // рандом
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matr[i, j] = Math.Round(rnd.Next(10, 100) + rnd.NextDouble(), 2);
                    }
                }
            }
            else
            { // ручками
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("Введите ({0},{1}) = ", i + 1, j + 1);
                        matr[i, j] = InputDouble();
                    }
                }
            }
        }

        /// <summary>
        /// Поиск максимального элемента
        /// </summary>
        /// <param name="matr">Исходная матрица</param>
        /// <param name="n">Количество строк/столбцов</param>
        /// <returns></returns>
        private static string SearchMaxElement(double[,] matr, int n)
        {
            double max = 0;
            int k = 0;//номер строки
            int p = 0;//номер столбца
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (((j >= i) && (n - j + 1 >= i)) || ((i >= j) && (n - j + 1 <= i)) && (matr[i, j] > max))
                    {
                        max = matr[i, j];
                        k = i;
                        p = j;
                    }
                }
            }
            return "Максимальный элемент: (" + (k + 1) + "," + (p + 1) + ") = " + max;
        }

        /// <summary>
        /// Ввод натуральных, целых чисел
        /// </summary>
        /// <returns></returns>
        private static int InputInteger()
        {
            bool ok = true;
            int result;
            do
            {
                string text = Console.ReadLine();
                ok = int.TryParse(text, out result);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ошибка. Неверный ввод!");
                    Console.Write("Введите повторно : ");
                    Console.ResetColor();
                }
            } while (!ok);
            return result;
        }

        /// <summary>
        /// Ввод действительных чисел
        /// </summary>
        /// <returns></returns>
        private static double InputDouble()
        {
            bool ok = true;
            double result;
            do
            {
                string text = Console.ReadLine();
                ok = double.TryParse(text, out result);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ошибка. Неверный ввод!");
                    Console.Write("Введите повторно : ");
                    Console.ResetColor();
                }
            } while (!ok);
            return result;
        }

    }
}
