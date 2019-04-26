using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6 {
    class Program {

        // для проверки скорости выполнения операций
        private static Stopwatch one_case = new Stopwatch();
        private static Stopwatch two_case = new Stopwatch();


        private static int M, N; // ограничители по длине последовательности
        private static double L; // ограничитель для M

        private static double[] sequence; // итоговая последовательность

        static void Main(string[] args) {
            InputInteger(ref N, "Введите N: ");
            sequence = new double[N];
            InputDouble(ref sequence[0], "Введите a1: ");
            InputDouble(ref sequence[1], "Введите a2: ");
            InputDouble(ref sequence[2], "Введите a3: ");

            InputInteger(ref M, "Введите M: ");
            InputDouble(ref L, "Введите L: ");

            // подсчёт времени на N членов последовательности
            one_case.Start();
            for (int i = 3; i < N; i++) {
                sequence[i] = Math.Round(getSequenceValue(sequence, i),2);
            }
            one_case.Stop();
            
            // подсчёт времени на M членов последовательности
            two_case.Start();
            int c = 0;
            for (int i = 3; i < M; i++) {
                if (getSequenceValue(sequence, i) > L) {
                    if (c <= M) c++;
                    else break;
                }
            }
            two_case.Stop();

            // если поиск M членов последователности быстрее
            if (two_case.ElapsedMilliseconds < one_case.ElapsedMilliseconds) {
                sequence = SearchM();
            }

            // вывод последовательности
            for (int i = 0; i < sequence.Length; i++) {
                if (sequence[i] != 0)
                    Console.WriteLine("{0} ч.п. = {1}", i + 1, sequence[i]);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Поиск первых M членов последовательности, которые больше L
        /// </summary>
        /// <returns></returns>
        private static double[] SearchM() {
            double[] second = new double[3 + M];
            int c = 0;
            for (int i = 3; i < M + 3; i++) {
                double temp = getSequenceValue(sequence, i);
                if (temp > L) {
                    if (c <= M) {
                        second[i] = temp;
                        c++;
                    } else break;
                }
            }
            return second;
        }

        /// <summary>
        /// Вычисление последовательности рекурсией
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="input"></param>
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
                    Console.Write("Неаправильный ввод! Введите снова: ");
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
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Неаправильный ввод! Введите снова: ");
                    Console.ResetColor();
                }
            } while (!ok);
        }

    }
}
