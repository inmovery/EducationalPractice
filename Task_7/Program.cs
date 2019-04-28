using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7 {
    public class Program {

        private static int print_num = 1;// для вывода номера сочетания

        private static void Main(string[] args) {
            int[] seq;// все возможные сочетания
            int n = 0;// алфавит от 1 до n
            int k = 0;// длина сочетаний

            InputInteger(ref n, "Введите N: ");
            InputInteger(ref k, "Введите K: ");

            // начальное заполнение массива от 1 до n
            seq = new int[n];
            for (int i = 0; i < n; i++) {
                seq[i] = i + 1;
            }

            if (n >= k) {
                Print(seq, k);// + если n = k
                while (NextSet(seq, n, k))
                    Print(seq, k);
            } else {
                Console.WriteLine("Таких сочетаний нет!");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Проверка того, подходит ли следующее осчетание
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static bool NextSet(int[] a, int n, int k) {
            for (int i = k - 1; i >= 0; --i) {
                if (a[i] < n - k + i + 1) {
                    ++a[i];
                    for (int j = i + 1; j < k; ++j) {
                        a[j] = a[j - 1] + 1;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Вывод сочетаний
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        private static void Print(int[] a, int k) {
            Console.Write("{0,-2}", print_num++);
            Console.Write("{0,1}", ": ");
            for (int i = 0; i < k; i++)  Console.Write(a[i] + " ");
            Console.Write("\n");
        }


        /// <summary>
        /// Воод целых данных
        /// </summary>
        /// <param name="init">В какую переменную должно занесить число</param>
        /// <param name="splash">Вступительный текст</param>
        private static void InputInteger(ref int init, string splash) {
            bool ok = true;
            Console.Write(splash);
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out init);
                if (init <= 0)
                    ok = false;
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Неаправильный ввод! Введите снова: ");
                    Console.ResetColor();
                }
            } while (!ok);
        }
    }

}
