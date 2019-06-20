using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_12 {
    class Program {
        static void Main(string[] args) {
            // Создание трех массивов длиной 100
            Collections col = new Collections();
            // Вспомогательные переменные для работы с массивами
            int[] ArrInc = col.GetArray(1);
            int[] ArrDec = col.GetArray(-1);
            int[] ArrUns = col.GetArray(0);

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
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений и {1} перестановок.\n", ComparesCounter, ReplacesCounter);
            ComparesCounter = ReplacesCounter = 0;

            Console.WriteLine("Результат сортировки упорядоченного по убыванию массива быстрой сортировкой:");
            Collections.QuickSort(ref ArrDec, 0, ArrInc.Length - 1, ref ComparesCounter, ref ReplacesCounter);
            Collections.PrintArray(ArrDec);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений и {1} перестановок.\n", ComparesCounter, ReplacesCounter);
            ComparesCounter = ReplacesCounter = 0;

            Console.WriteLine("Результат сортировки неупорядоченного массива быстрой сортировкой:");
            Collections.QuickSort(ref ArrUns, 0, ArrInc.Length - 1, ref ComparesCounter, ref ReplacesCounter);
            Collections.PrintArray(ArrUns);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений и {1} перестановок.\n", ComparesCounter, ReplacesCounter);

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
