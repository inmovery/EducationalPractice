using System;

namespace Task_12 {
    class Program {
        static void Main(string[] args) {

            Console.Write("Какой размерностью будет массив: ");
            int size = getSimpleInput();// разрмерность массивов
            Collections col = new Collections(size);
            
            int[] ArrInc = col.GetArray(1); // упорядоченный по возрастанию массив
            int[] ArrDec = col.GetArray(-1); // упорядоченный по убыванию массив
            int[] ArrUns = col.GetArray(0); // неупорядоченный массив

            Console.WriteLine("Упорядоченный по возрастанию массив:");
            Collections.PrintArray(ArrInc);
            Console.WriteLine("Упорядоченный по убыванию массив:");
            Collections.PrintArray(ArrDec);
            Console.WriteLine("Неупорядоченный массив:");
            Collections.PrintArray(ArrUns);

            Console.WriteLine("Сортировка Шелла:");
            Console.WriteLine("Результат сортировки упорядоченного по возрастанию массива сортировкой Шелла:");
            Collections.SortShell(ref ArrInc);

            Console.WriteLine("Результат сортировки упорядоченного по убыванию массива сортировкой Шелла:");
            Collections.SortShell(ref ArrDec);

            Console.WriteLine("Результат сортировки неупорядоченного массива сортировкой Шелла:");
            Collections.SortShell(ref ArrUns);

            // Восстановление массивов
            ArrInc = col.GetArray(1);
            ArrDec = col.GetArray(-1);
            ArrUns = col.GetArray(0);

            Console.WriteLine("Быстрая сортировка:");

            // Счетчики сравнений и перестановок
            int ComparesCounter = 0, ReplacesCounter = 0;

            Console.WriteLine("Результат сортировки упорядоченного по возрастанию массива быстрой сортировкой:");
            Collections.QuickSort(ref ArrInc, 0, ArrInc.Length - 1, ref ComparesCounter, ref ReplacesCounter);
            Collections.PrintArray(ArrInc);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений(-я) и {1} перестановок(-вки).\n", ComparesCounter, ReplacesCounter);
            ComparesCounter = ReplacesCounter = 0;

            Console.WriteLine("Результат сортировки упорядоченного по убыванию массива быстрой сортировкой:");
            Collections.QuickSort(ref ArrDec, 0, ArrInc.Length - 1, ref ComparesCounter, ref ReplacesCounter);
            Collections.PrintArray(ArrDec);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений(-я) и {1} перестановок(-вки).\n", ComparesCounter, ReplacesCounter);
            ComparesCounter = ReplacesCounter = 0;

            Console.WriteLine("Результат сортировки неупорядоченного массива быстрой сортировкой:");
            Collections.QuickSort(ref ArrUns, 0, ArrInc.Length - 1, ref ComparesCounter, ref ReplacesCounter);
            Collections.PrintArray(ArrUns);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений(-я) и {1} перестановок(-вки).\n", ComparesCounter, ReplacesCounter);

            Console.ReadKey();
        }

        /// <summary>
        /// Ввод для менюшки с ограничением ввода
        /// </summary>
        /// <param name="n">Огрничение по вводу</param>
        /// <returns></returns>
        private static int getSettingInput(int n) {
            bool ok = true;
            int temp = 0;
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out temp);
                if (!ok || temp < 0 || temp > n) {
                    ok = false;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Неверный ввод! Введие корректные данные: ");
                    Console.ResetColor();
                }
            } while (!ok);
            return temp;
        }

        /// <summary>
        /// Обычный ввод для менюшки (> 0)
        /// </summary>
        /// <returns></returns>
        private static int getSimpleInput() {
            bool ok = true;
            int temp = 0;
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out temp);
                if (!ok || temp < 0) {
                    ok = false;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Неверный ввод! Введие корректные данные: ");
                    Console.ResetColor();
                }
            } while (!ok);
            return temp;
        }

    }
}
