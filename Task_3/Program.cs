using System;

namespace Task_3 {
    public class Program {
        private static void Main(string[] args) { MainAction(); }

        /// <summary>
        /// Основная задача программы
        /// </summary>
        private static void MainAction() {

            // ввод координат
            Console.Write("Введите координату X:");
            double x = checkInput();
            Console.Write("Введите координату Y:");
            double y = checkInput();

            // ограничения
            bool y1 = y <= Math.Abs(x);
            bool y2 = x >= -1;
            bool y3 = x <= 1;
            bool y4 = y >= -2;

            // вычисление результата
            if (y1 && y2 && y3 && y4) {
                Console.Write("Точка ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("входит");
                Console.ResetColor();
                Console.WriteLine(" в заштрихованную область");
            } else {
                Console.Write("Точка ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("не входит");
                Console.ResetColor();
                Console.WriteLine(" в заштрихованную область");
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
        /// Проверка на ввод координат
        /// </summary>
        /// <returns></returns>
        private static double checkInput() {
            bool ok = true;
            double result;
            do {
                string text = Console.ReadLine();
                ok = double.TryParse(text, out result);
                if (!ok) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Вы ввели некорректные данные!");
                    Console.Write("Введите действительное число : ");
                    Console.ResetColor();
                }
            } while (!ok);
            return result;
        }

    }
}
