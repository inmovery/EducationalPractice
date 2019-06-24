using System;

namespace Task_5 {
    public class Program {

        private static int n; // размерность матрицы

        private static int Middle; // среднее значение (для расчёта)

        private static double[,] matr; // исходная матрица

        private static double Max; // максимальное значение среди элементов марицы,
                                   // которые попадают в заштрихованную область 

        // положение максимального элемента в матрице
        private static int x;
        private static int y;

        // то, какие элементы матрицы попадают в заштрихованную область
        private static int[,] mm;

        private static void Main(string[] args) {
            MainAction();
        }

        /// <summary>
        /// Основные действия программы
        /// </summary>
        private static void MainAction() {
            Console.Write("Размерность матрицы матрицы = ");
            n = InputInteger();

            matr = new double[n, n];

            FillingMatrix(ref matr, n); // сборка матрицы

            Middle = n / 2;

            Max = 0;
            x = 0;
            y = 0;

            mm = new int[n, n];

            // поиск максимального элемента в верхней половине матрицы
            for (int i = 0; i < Middle; i++) {
                for (int j = i; j < n - i; j++) {
                    mm[i, j] = 1;
                    if (matr[i, j] > Max) {
                        Max = matr[i, j];
                        x = i;
                        y = j;
                    }
                }
            }

            // поиск максимального элемента в нижней половине матрицы
            for (int i = Middle; i < n; i++) {
                for (int j = n - 1 - i; j <= i; j++) {
                    mm[i, j] = 1;
                    if (matr[i, j] > Max) {
                        Max = matr[i, j];
                        x = i;
                        y = j;
                    }
                }
            }

            PrintMatrix(matr, mm, n, x, y); // вывод матрицы

            Console.WriteLine("Максимальный элемент ({0}, {1}) = {2}", x, y, Max);

            // проверка на продолжение
            Console.WriteLine("—————————————————————————————————————");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Продолжить\n2. Очистить консоль и продолжить\n3. Закончить\n");
            Console.ResetColor();
            int input = 0;
            bool ok = true;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Команда: ");
            Console.ResetColor();
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out input);
                if (input > 3 || input <= 0)
                    ok = false;
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Такой команды не существует!");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Команда: ");
                    Console.ResetColor();
                }
            } while (!ok);

            switch (input) {
                case 1:
                    MainAction();
                    break;
                case 2:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Консоль очищена!");
                    Console.ResetColor();
                    MainAction();
                    break;
                case 3:
                    Environment.Exit(1);
                    break;
                default:
                    // additional feature
                    break;
            }

        }

        /// <summary>
        /// Вывод матрицы
        /// </summary>
        /// <param name="matr"></param>
        /// <param name="n"></param>
        private static void PrintMatrix(double[,] matr, int[,] mm, int n, int x, int y) {
            string pre = " ";
            for (int i = 0; i < n; i++) {
                pre += "————————";
            }

            Console.WriteLine(pre + "—");
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (mm[i,j] == 1) {
                        if (i == x && j == y) {
                            Console.Write("{0,2}", "|");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(String.Format("{0,6:0.00}", matr[i, j]));
                            Console.ResetColor();
                        } else {
                            Console.Write("{0,2}", "|");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(String.Format("{0,6:0.00}", matr[i, j]));
                            Console.ResetColor();
                        }
                    } else {
                        Console.Write(String.Format("{0,2}{1,6:0.00}", "|", matr[i, j]));
                    }
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
        private static void FillingMatrix(ref double[,] matr, int n, int type = 0) {
            Random rnd = new Random();
            if (type == 0) { // рандом
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        matr[i, j] = Math.Round(rnd.Next(10, 100) + rnd.NextDouble(), 2);
                    }
                }
            } else { // ручками
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        Console.Write("Введите ({0},{1}) = ", i + 1, j + 1);
                        matr[i, j] = InputDouble();
                    }
                }
            }
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
                if (!ok || result <= 0) {
                    ok = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Неверный ввод!");
                    Console.Write("Введите целое число > 0 : ");
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
                    ok = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Неверный ввод!");
                    Console.Write("Введите действительное число : ");
                    Console.ResetColor();
                }
            } while (!ok);
            return result;
        }

    }

}
