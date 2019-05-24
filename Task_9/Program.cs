using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_9 {
    class Program {

        private static int len; // длина двунаправленного списка 
        private static TwoList second; // двунаправленный список

        private static int input; // выбор команды в главном меню

        private static int count_items_list = 0; // искомое количество элементов

        private static Random rnd = new Random();
        private static Random rnd_for = new Random();

        static void Main(string[] args) {
            while (true) LinkedListTwo();
        }

        private static void LinkedListTwo() {
            bool ok;
            int input;
            Console.WriteLine("1. Создать список\n2. Подсчитать рекурсией\n3. Подсчитать БЕЗ рекурсии\n4. Очистить консоль\n5. Выход");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Команда: ");
            do {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out input);
                if (input <= 0 || input > 5)
                    ok = false;
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Такой команды не существует! Введите повторно:");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Команда: ");
                    Console.ResetColor();
                }
            } while (!ok);
            Console.ResetColor();
            switch (input) {
                case 1: // создание списка
                    Console.WriteLine("1. Создать список вручную\n2. Создать список рандомно(ДСЧ)");
                    int cho; // команда выбора
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Команда: ");
                    do {
                        string buf = Console.ReadLine();
                        ok = int.TryParse(buf, out cho);
                        if (cho < 1 || cho > 2)
                            ok = false;
                        if (!ok) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Такой команды не существует! Введите повторно:");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("Команда: ");
                            Console.ResetColor();
                        }
                    } while (!ok);
                    Console.ResetColor();

                    switch (cho) {
                        case 1: // ввод с клавы
                            Console.Write("Введите размер списка: ");
                            do {
                                string buf = Console.ReadLine();
                                ok = int.TryParse(buf, out len);
                                if (len == 0)
                                    ok = false;
                                if (!ok) {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write("Ошибка!");
                                    Console.ResetColor();
                                    Console.Write("Ведите правильныые данные: ");
                                }
                            } while (!ok);

                            second = MakeListTwo(len, 0);
                            ShowListTwo(second);
                            break;
                        case 2: // рандом
                            int len_for = rnd_for.Next(5, 25);
                            second = MakeListTwo(len_for);
                            ShowListTwo(second);
                            break;
                    }
                    break;
                case 2: // подсчёт количества элементов списка С рекурсией
                    FindCountItemsByRecurs(second); // подсёч
                    if (count_items_list == 0) Console.WriteLine("Список пустой!");
                    else Console.WriteLine("Количество элементов списка (Рекурсия) = " + count_items_list);
                    break;
                case 3: // подсчёт количество элементов списка БЕЗ рекурсии
                    int pre_value = FindCountItemsSimple(second);
                    if (pre_value == 0) Console.WriteLine("Список пустой!");
                    else Console.WriteLine("Количество элементов списка = " + pre_value);
                    break;
                case 4: // очистка консольки
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("ДВУНАПРАВЛЕННЫЙ СПИСОК");
                    Console.ResetColor();
                    LinkedListTwo();
                    break;
                case 5: // выход из консольки
                    Environment.Exit(0);
                    break;
            }
        }

        //————————————————————————————ДВУНАПРАВЛЕННЫЙ СПИСОК——————————————————————————————————————————————————————————

        /// <summary>
        /// Генерация нового элемента в двунаправленном списке
        /// </summary>
        /// <returns></returns>
        private static TwoList MakeTwoList() { return new TwoList(rnd.Next(-100, 100)); }

        /// <summary>
        /// Инициализация двунаправленного списка
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static TwoList MakeListTwo(int size, int choice = 1) {

            if (choice == 1) { // рандом
                TwoList sec = MakeTwoList();
                for (int i = 1; i < size; i++) {
                    TwoList p = MakeTwoList();
                    p.next = sec;
                    sec.prev = p;
                    sec = p;
                }
                return sec;
            } else { // ввод с клавы
                bool ok;
                int second;//первый элемент списка
                Console.Write("Введите 1 элемент: ");
                do {
                    string buf = Console.ReadLine();
                    ok = int.TryParse(buf, out second);
                    if (!ok) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Ошибка!");
                        Console.ResetColor();
                        Console.Write("Ведите правильныые данные: ");
                    }
                } while (!ok);
                TwoList sec = new TwoList(second); // создали список с первым элементом
                for (int i = 1; i < size; i++)
                    sec = AddToBegin(sec, 0, i + 1);
                return sec;
            }
        }

        /// <summary>
        /// Подсчёт количества элементов рекурсией
        /// </summary>
        /// <param name="beg">Текущий двунаправленный список</param>
        private static void FindCountItemsByRecurs(TwoList sec) {
            TwoList t = sec;
            if (t != null) {
                count_items_list++;
                FindCountItemsByRecurs(t.next);//переход к следующему элементу списка
            } 
        }

        /// <summary>
        /// Подсчёт количества элементов БЕЗ рекурсии
        /// </summary>
        /// <param name="beg">Текущий двунаправленный список</param>
        private static int FindCountItemsSimple(TwoList sec) {
            TwoList t = sec;
            int d = 0;
            while (t != null) {
                t = t.next; // переход к следующему элементу
                d++;
            }
            return d;
        }

        /// <summary>
        /// Добавление элементов в начало списка
        /// </summary>
        /// <param name="beg">Текущий двунаправленный список (Объект)</param>
        /// <returns>Двунаправленный список с адресом последнего элемента</returns>
        private static TwoList AddToBegin(TwoList sec, int type = 1, int index = 0) {

            if (type == 1) {//рандом
                TwoList list = MakeTwoList();
                if (sec == null)
                    return list;//если начало пустое, то возвращает список с одним элементом
                list.next = sec;//записыли адрес объекта в список
                sec.prev = list;
                sec = list;
                return sec;
            } else {//ввод с клавы
                bool ok;
                int n;//новый элемент в списке
                Console.Write("Введите {0} элемент: ", index);
                do {
                    string buf = Console.ReadLine();
                    ok = int.TryParse(buf, out n);
                    if (!ok) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Ошибка!");
                        Console.ResetColor();
                        Console.Write("Ведите правильныые данные: ");
                    }
                } while (!ok);
                TwoList list = new TwoList(n);
                list.next = sec;//записыли адрес объекта в список
                sec.prev = list;
                sec = list;
                return sec;
            }
        }

        /// <summary>
        /// Вывод двунаправленного списка
        /// </summary>
        /// <param name="beg">Текущий двунаправленный список</param>
        private static void ShowListTwo(TwoList sec) {
            TwoList t = sec;
            int i = 0;
            while (t != null) {
                Console.WriteLine($"{i + 1}: {t}");
                t = t.next;//переход к следующему элементу
                i++;
            }
            Console.WriteLine();
        }

    }

    /// <summary>
    /// Двунаправленный список
    /// </summary>
    class TwoList {
        public int data;
        public TwoList next;//следующее значение
        public TwoList prev;//предыдущее значение

        public TwoList() {
            data = 0;
            next = null;
            prev = null;
        }

        public TwoList(int d) {
            data = d;
            next = null;
            prev = null;
        }

        public override string ToString() { return data.ToString() + " "; }
    }

}
