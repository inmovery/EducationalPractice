using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_6 {
    public class Program {

        // для проверки скорости выполнения операций
        private static Stopwatch one_case = new Stopwatch();
        private static Stopwatch two_case = new Stopwatch();
        
        private static int M, N; // ограничители по длине последовательности
        private static double L; // ограничитель для M

        private static double[] sequence; // итоговая последовательность

        private static void Main(string[] args) {
            InitVars();
            if (!MainAction()) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Лимит времени выполнения превышен (2 сек.)\nЗадайте не такие большие числа!");
                Console.ResetColor();
                Console.WriteLine("————————————————————");
                InitVars();
            }

            // проверка на продолжение
            Console.WriteLine("————————————————————");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Продолжить\n2. Закончить\n");
            Console.ResetColor();
            int input = 0;
            bool ok = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Команда: ");
            Console.ResetColor();
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out input);
                if (input > 2 || input <= 0)
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
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Консоль очищена!");
                    Console.ResetColor();
                    InitVars();
                    MainAction();
                    break;
                case 2:
                    Environment.Exit(1);
                    break;
                default:
                    // additional feature
                    break;
            }

            Console.ReadKey();
        }

        private static void InitVars() {
            InputInteger(ref N, "Введите количество элементов N : ");
            sequence = new double[N];
            InputDouble(ref sequence[0], "Введите a1: ");
            InputDouble(ref sequence[1], "Введите a2: ");
            InputDouble(ref sequence[2], "Введите a3: ");

            InputInteger(ref M, "Введите количество элементов M : ");
            InputDouble(ref L, "Введите значение L (минимум для M): ");
        }

        /// <summary>
        /// Основные действия в программе
        /// </summary>
        private static bool MainAction() {

            bool result = true;

            // подсчёт времени на N членов последовательности
            one_case.Start();
            DateTime startTime_n = DateTime.Now; // для ограничения по времени выполнения
            for (int i = 3; i < N; i++) {
                if (DateTime.Now.Subtract(startTime_n) >= new TimeSpan(0, 0, 0, 0, 2000)) {
                    result = false;
                    break;
                }
                sequence[i] = Math.Round(getSequenceValue(sequence, i), 2);
            }
            one_case.Stop();

            // подсчёт времени на M членов последовательности
            two_case.Start();
            DateTime startTime_m = DateTime.Now; // для ограничения по времени выполнения
            int c = 0;
            int d = 0;
            while (c != M) {
                if (DateTime.Now.Subtract(startTime_n) >= new TimeSpan(0, 0, 0, 0, 2000)) {
                    result = false;
                    break;
                }
                if (getSequenceValue(sequence, d) > L) {
                    if (c <= M)
                        c++;
                    else
                        break;
                }
                d++;
            }
            two_case.Stop();

            if(result == true) {
                // если поиск M членов последователности быстрее
                if (two_case.ElapsedMilliseconds < one_case.ElapsedMilliseconds) {
                    sequence = SearchM();
                    Console.WriteLine("Поиск M элем. послед. быстрее на {0} миллисекунд(-у)", one_case.ElapsedMilliseconds - two_case.ElapsedMilliseconds);
                } else { // если подсчёт N членов последовательности быстрее
                    Console.WriteLine("Вычисление N элем. послед. быстрее на {0} миллисекунд(-у)", two_case.ElapsedMilliseconds - one_case.ElapsedMilliseconds);
                }

                // вывод последовательности
                for (int i = 0; i < sequence.Length; i++)
                    Console.WriteLine("{0} эл. послед. = {1}", i + 1, sequence[i]);
            }
            
            return result;
        }

        /// <summary>
        /// Поиск первых M членов последовательности, которые больше L
        /// </summary>
        /// <returns></returns>
        private static double[] SearchM() {
            double[] second = new double[3 + M];
            second[0] = sequence[0];
            second[1] = sequence[1];
            second[2] = sequence[2];
            int c = 0; // проверка для M
            int d = 0; // счётчик 
            while (c != M) {
                double temp = getSequenceValue(sequence, d);
                if (temp > L) {
                    if (c <= M) {
                        second[c + 3] = temp;
                        c++;
                    } else break;
                }
                d++;
            }
            return second;
        }

        /// <summary>
        /// Вычисление последовательности рекурсией
        /// </summary>
        /// <param name="seq">последовательность</param>
        /// <param name="input">номер элемента в последовательности</param>
        /// <returns></returns>
        private static double getSequenceValue(double[] seq, int input) {
            if (input == 0)
                return seq[0];
            else if (input == 1)
                return seq[1];
            else if (input == 2)
                return seq[2];
            else
                return ((7/3)*getSequenceValue(seq, input-1) + getSequenceValue(seq, input - 2))/(2* getSequenceValue(seq, input - 3));
        }
        
        /// <summary>
        /// Воод не целых данных
        /// </summary>
        /// <param name="init"></param>
        /// <param name="splash"></param>
        private static void InputDouble(ref double init, string splash) {
            bool ok = true;
            Console.Write(splash);
            do {
                string buf = Console.ReadLine();
                ok = double.TryParse(buf, out init);
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Неверный ввод!\nВведите вещественное число : ");
                    Console.ResetColor();
                }
            } while (!ok);
        }

        /// <summary>
        /// Воод целых данных
        /// </summary>
        /// <param name="init"></param>
        /// <param name="splash"></param>
        private static void InputInteger(ref int init, string splash) {
            bool ok = true;
            Console.Write(splash);
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out init);
                if (init < 3) ok = false;
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Неверный ввод!\nВведите целое число > 2 : ");
                    Console.ResetColor();
                }
            } while (!ok);
        }

    }
}
