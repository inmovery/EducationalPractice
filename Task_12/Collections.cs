using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_12 {
    public class Collections {
        
        int[] SortedInc; // упорядоченный по возрастанию массив
        int[] SortedDec; // упорядоченный по убыванию массив
        int[] Unsorted; // неупорядоченный массив

        static Random rnd = new Random();

        public Collections(int Size) {
            SortedInc = GenerateSortedArray(Size, 1);
            SortedDec = GenerateSortedArray(Size, -1);
            Unsorted = GenerateArray(Size);
        }

        /// <summary>
        /// Получение массивов
        /// </summary>
        /// <param name="Order">Определитель порядка упорядочивания элементов (1 => по возрастанию, -1 => по убыванию, 0 => не упорядоченный)</param>
        /// <returns></returns>
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

        /// <summary>
        /// Генерация упорядоченного массива
        /// </summary>
        /// <param name="Size">Размерность массива</param>
        /// <param name="Order">Порядок упорядочивания (1 => по возрастанию, -1 => по убыванию)</param>
        /// <returns></returns>
        int[] GenerateSortedArray(int Size, int Order) {
            int[] Array = new int[Size];
            Array[0] = rnd.Next(10);
            for (ushort i = 1; i < Size; i++)
                Array[i] = Array[i - 1] + (rnd.Next(10) + 1) * Order;
            return Array;
        }

        /// <summary>
        /// Генерация неупорядоченного массива
        /// </summary>
        /// <param name="Size">Размерность массива</param>
        /// <returns></returns>
        int[] GenerateArray(int Size) {
            int[] Array = new int[Size];
            for (ushort i = 0; i < Size; i++)
                Array[i] = rnd.Next(-1000, 1000);
            return Array;
        }

        // Длины промежутков для сортировки
        // Использована последовательность Марцина Циура, считающаяся лучшим вариантом для массивов длиной менее 4000
        static int[] Increment = new int[] { 1, 4, 10, 23, 57, 132, 301, 701, 1750 };

        /// <summary>
        /// Сортировка Шелла
        /// </summary>
        /// <param name="Array">Результирующий массив</param>
        public static void SortShell(ref int[] Array) {
            // Счетчики сравнений и перестановок
            int ComparesCounter = 0, ReplacesCounter = 0;
            // Находим оптимальную длину шага
            int step = 0;
            while (Increment[step] < Array.Length)
                step++;

            while (step >= 0) {
                
                int increm = Increment[step];
                // Проход по циклу
                for (int i = 0; i < Array.Length - increm; i++) {
                    int j = i; // помощь при проходе по циклу
                    // Пока не дойдем до начала массива и текущий элемент больше находящегося на расстоянии шага
                    while (j >= 0 && Array[j] > Array[j + increm]) {
                        // Переставляем элемент местами
                        int temp = Array[j];
                        Array[j] = Array[j + increm];
                        Array[j + increm] = temp;
                        j--; // Переходим к следующему элементу
                        // Обновляем счетчики
                        ComparesCounter++;
                        ReplacesCounter++;
                    }
                    ComparesCounter++;
                }

                // Уменьшаем шаг
                step--;
            }

            // Выводим результаты сортировки
            Console.WriteLine("Отсортированный методом Шелла массив:");
            PrintArray(Array);
            Console.WriteLine("При сортировке массива было выполнено {0} сравнений(-я) и {1} перестановки(-вок).\n", ComparesCounter, ReplacesCounter);
        }

        // Для реализации было выбрано разбиение Хоара
        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="IndexFirst"></param>
        /// <param name="IndexLast"></param>
        /// <param name="ComparesCount"></param>
        /// <param name="ReplacesCount"></param>
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

        /// <summary>
        /// Вывод массива
        /// </summary>
        /// <param name="Array"></param>
        public static void PrintArray(int[] Array) {
            foreach (int Item in Array)
                Console.Write("{0} ", Item);
            Console.WriteLine("\n");
        }
    }
}
