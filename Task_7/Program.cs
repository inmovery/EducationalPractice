using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        
namespace Task_7 {
    public class Program {

        private static int print_num = 1; // для вывода номера сочетания
        private static int[] seq; // все возможные сочетания
        private static int n = 0; // алфавит от 1 до n
        private static int k = 0; // длина сочетаний

        private static void Main(string[] args) {
            MainAction();
        }

        /// <summary>
        /// Основные действия программы
        /// </summary>
        private static void MainAction() {
            print_num = 1;
            n = 0;
            k = 0;

            InputInteger(ref n, "Введите N (алфивит): ");
            InputInteger(ref k, "Введите K (длина каждого сочетания): ");

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

            // проверка на продолжение
            Console.WriteLine("————————————————————");
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
        /// Проверка того, подходит ли следующее сочетание
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
                    Console.Write("Неверный ввод!\nВведите целое число > 0 : ");
                    Console.ResetColor();
                }
            } while (!ok);
        }
    }

}
