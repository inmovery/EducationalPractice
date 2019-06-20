using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_12 {
    public class Collections {
        // Массивы - упорядоченный по возрастанию, по убыванию, не упорядоченный
        int[] SortedInc;
        int[] SortedDec;
        int[] Unsorted;

        #region Generation
        // ДСЧ, используемый при генерации массивов
        static Random rnd = new Random();

        // Конструктор
        public Collections(int Size = 100) {
            SortedInc = GenerateSortedArray(Size, 1);
            SortedDec = GenerateSortedArray(Size, -1);
            Unsorted = GenerateArray(Size);
        }

        // Метод для получения массивов
        // Переменная Order определяет порядок упорядочивания элементов массива: 
        // 1 - по возрастанию, -1 - по убыванию, 0 или любое другое число - не упорядоченный
        public int[] GetArray(int Order) {
            switch (Order) {
                case 1:
                    return SortedInc.ToArray();
                case -1:
                    return SortedDec.ToArray();
                default:
                    return Unsorted.ToArray();
            }
        }

        // Генерация упорядоченного массива
        // Переменная Order определяет порядок упорядочивания элементов массива: 1 - по возрастанию, -1 - по убыванию
        int[] GenerateSortedArray(int Size, int Order) {
            int[] Array = new int[Size];
            Array[0] = rnd.Next(10);
            for (ushort i = 1; i < Size; i++)
                Array[i] = Array[i - 1] + (rnd.Next(10) + 1) * Order;
            return Array;
        }

        // Генерация неупорядоченного массива
        int[] GenerateArray(int Size) {
            int[] Array = new int[Size];
            for (ushort i = 0; i < Size; i++)
                Array[i] = rnd.Next(-1000, 1000);
            return Array;
        }
        #endregion Generation

        #region Sort
        // Длины промежутков для сортировки
        // Использована последовательность Марцина Циура, считающаяся лучшим вариантом для массивов длиной менее 4000
        static int[] Increment = new int[] { 1, 4, 10, 23, 57, 132, 301, 701, 1750 };

        // Сортировка Шелла
        public static void SortShell(ref int[] Array) {
            // Счетчики сравнений и перестановок
            int ComparesCounter = 0, ReplacesCounter = 0;
            // Находим оптимальную длину шага
            int step = 0;
            while (Increment[step] < Array.Length)
                step++;

            // Начинаем сортировку
            while (step >= 0) {
                // Инкремент
                int increm = Increment[step];
                // Проход по циклу
                for (int i = 0; i < Array.Length - increm; i++) {
                    // Вспомогательная переменная для прохода по циклу
                    int j = i;
                    // Пока не дойдем до начала массива и текущий элемент больше находящегося на расстоянии шага
                    while (j >= 0 && Array[j] > Array[j + increm]) {
                        // Переставляем элемент местами
                        int temp = Array[j];
                        Array[j] = Array[j + increm];
                        Array[j + increm] = temp;
                        // Переходим к следующему элементу
                        j--;
                        // Обновляем счетчики
                        ComparesCounter++;
                        ReplacesCounter++;
                    }
                    ComparesCounter++;
                }

                // Уменьшаем шаг
                step--;
            }

            // Выводим результаты сортировки:
            Console.WriteLine("Отсортированный методом Шелла массив:");
            PrintArray(Array);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений и {1} перестановок.\n", ComparesCounter, ReplacesCounter);
        }

        // Быстрая сортировка
        // Для реализации было выбрано разбиение Хоара
        public static void QuickSort(ref int[] Array, int IndexFirst, int IndexLast, ref int ComparesCount, ref int ReplacesCount) {
            if (IndexFirst > IndexLast)
                return;
            // Опорный элемент, в данном случае - средний
            int Pivot = Array[(IndexLast - IndexFirst) / 2];
            // Вспомогательные переменные для прохода по массиву
            int i = IndexFirst, j = IndexLast;
            // Проход по массиву
            while (i < j) {
                // Поиск опорного элемента
                while (Array[i] < Pivot) {
                    ComparesCount++;
                    i++;
                }
                while (Array[j] > Pivot) {
                    ComparesCount++;
                    j--;
                }

                // Если не дошли до опорного элемента
                if (i < j) {
                    // Вспомогательная переменная для перестановки элементов
                    int Temp = Array[i];
                    Array[i++] = Array[j];
                    Array[j--] = Temp;
                    ReplacesCount++;
                }

                // Сортировка частей, на которые был разделен массив
                QuickSort(ref Array, IndexFirst, j, ref ComparesCount, ref ReplacesCount);
                QuickSort(ref Array, i + 1, IndexLast, ref ComparesCount, ref ReplacesCount);
            }
        }
        #endregion Sort

        // Печать массива
        public static void PrintArray(int[] Array) {
            foreach (int Item in Array)
                Console.Write("{0} ", Item);
            Console.WriteLine("\n");
        }
    }
}
