using System;

namespace Task_5 {
    public class Program {
        private static void Main(string[] args) {
            Console.Write("Порядок матрицы = ");
            int n = InputInteger();
            double[][] matr = new double[n][];

            matr = FillingMatrix(n, 1);

            PrintMatrix(matr, n);

            Console.WriteLine(SearchMaxElement(matr, n));

        }

        private static void PrintMatrix(double[][] matr, int n) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    Console.Write("{0,5}",matr[i][j]);
                }
                Console.WriteLine();
            }
        }

        
        /// <summary>
        /// Заполнение матрицы
        /// </summary>
        /// <param name="matr">Исходная матрица</param>
        /// <param name="n">Количество строк/столбцов</param>
        /// <param name="type">Тип ввода</param>
        private static double[][] FillingMatrix(int n, int type = 0) {
            double[][] fullMatrix = new double[n][];
            if (type == 0) { // рандом
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        Random rnd = new Random();
                        fullMatrix[i][j] = Math.Round(rnd.Next(1, 100) + rnd.NextDouble(), 2);
                    }
                }
            } else { // ручками
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        Console.Write("Введите a[{0}][{1}] = ",i+1,j+1);
                        fullMatrix[i][j] = InputDouble();
                    }
                }
            }

            return fullMatrix;
        }

        /// <summary>
        /// Поиск максимального элемента
        /// </summary>
        /// <param name="matr">Исходная матрица</param>
        /// <param name="n">Количество строк/столбцов</param>
        /// <returns></returns>
        private static string SearchMaxElement(double[][] matr, int n) {
            double max = 0;
            int k = 0;//номер строки
            int p = 0;//номер столбца
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (((j >= i) && (n - j + 1 >= i)) || ((i >= j) && (n - j + 1 <= i)) && (matr[i][j] > max)){
                        max = matr[i][j];
                        k = i;
                        p = j;
                    }
                }
            }
            return "Максимальный элемент: [" + k + "][" + p + "] = " + max;
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

   }
}
