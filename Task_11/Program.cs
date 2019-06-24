using System;
using System.IO;

namespace Task_11 {
    class Program {

        static string InputMessage = "";
        static string OutputMessage;

        static StreamWriter OutputFile;

        static int able = 0;

        static void Main(string[] args) {
            do {
                if (able == 0) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("МЕНЮ");
                    Console.ResetColor();
                    FirstMenu();
                } else if (able == 1) {
                    WorkWithMessage();
                }
            } while (true);
        }

        /// <summary>
        /// Ввод сообщения, с которым нужно работать
        /// </summary>
        /// <returns></returns>
        static string ConsoleInput() {
            string res;
            
            bool ok = true;
            do {
                Console.WriteLine("Введите сообщение, которое хотите обработать: ");
                res = Console.ReadLine();
                for (int i = 0; i < res.Length; i++) {
                    if (res[i] != '0' && res[i] != '1') {
                        ok = false;
                        break;
                    } else {
                        ok = true;
                    }
                }
                if (!ok)
                    Console.WriteLine("Сообщение должно содержать только 0 и 1.");
            } while (!ok);

            return res;
        }

        /// <summary>
        /// Ввод сообщения из файла
        /// </summary>
        /// <returns></returns>
        static string FileInput() {
            StreamReader InputFile;
            string Result;

            try {
                InputFile = new StreamReader("input.txt");
                Result = InputFile.ReadLine();
            } catch (FileNotFoundException e) {
                Console.WriteLine("Файла \"input.txt\" не обнаружено, введите выражение вручную: ");
                Result = ConsoleInput();
            }

            return Result;
        }

        /// <summary>
        /// Шифрование сообщения
        /// </summary>
        /// <param name="Input">Исходное сообщение</param>
        /// <returns></returns>
        static string EncryptMessage(string Input) {
            string Output = "";
            Output += Input[0];
            for (int i = 1; i < Input.Length; i++) {
                if (Input[i] == Input[i - 1])
                    Output += 1;
                else
                    Output += 0;
            }
            return Output;
        }

        /// <summary>
        /// Расшифровка сообщения
        /// </summary>
        /// <param name="Input">Зашифрованное сообщение</param>
        /// <returns></returns>
        static string DecryptMessage(string Input) {
            string Output = "";
            Output += Input[0];
            for (int i = 1; i < Input.Length; i++) {
                if (Input[i] == '1') {
                    Output += Output[i - 1];
                } else {
                    if (Input[i - 1] == '1')
                        Output += 0;
                    else
                        Output += 1;
                }
            }
            return Output;
        }

        static void WorkWithMessage() {
            Console.WriteLine("1. Зашифровать сообщение\n2. Расшифровать сообщение\n3. Назад\n4. Выйти");
            int cmd = getSettingInput(4);
            switch (cmd) {
                case 1: // зашифровать сообщение 
                    OutputMessage = EncryptMessage(InputMessage);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Зашифрованное сообщение: {0}", OutputMessage);
                    Console.ResetColor();
                    WriteToFile(OutputMessage);
                    break;
                case 2: // расшифровать сообщение
                    OutputMessage = DecryptMessage(InputMessage);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Расшифрованное сообщение: {0}", OutputMessage);
                    Console.ResetColor();
                    WriteToFile(OutputMessage);
                    break;
                case 3: // кнопка "Назад"
                    Console.Clear();
                    able = 0;
                    break;
                case 4: // выход из консольки
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Запись результата в файл
        /// </summary>
        /// <param name="Message"></param>
        static void WriteToFile(string Message) {
            Console.WriteLine("Записать в файл? ");
            Console.WriteLine("1. Да\n2. Нет");
            int cmd = getSettingInput(2);
            switch (cmd) {
                case 1: // запись в файл
                    try {
                        OutputFile = new StreamWriter("output.txt");
                        OutputFile.WriteLine(OutputMessage);
                        OutputFile.Close();
                    } catch (FileNotFoundException) {
                        File.Create("output.txt");
                        OutputFile = new StreamWriter("output.txt");
                        OutputFile.WriteLine(OutputMessage);
                        OutputFile.Close();
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Результат записан!");
                    Console.ResetColor();
                    break;
                case 2:
                    able = 0;
                    FirstMenu();
                    break;
            }
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        static void FirstMenu() {
            Console.WriteLine("1. Ввести выражение с клавиатуры\n2. Ввести выражение из файла\n3. Очистить консоль\n4. Выйти");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Команда: ");
            Console.ResetColor();
            int cmd = getSettingInput(4);

            switch (cmd) {
                case 1: // ввод с клавиатуры 
                    able = 1;
                    InputMessage = ConsoleInput();
                    WorkWithMessage();
                    break;
                case 2: // ввод из файла
                    able = 1;
                    InputMessage = FileInput();
                    break;
                case 3: // кнопка "Назад" 
                    Console.Clear();
                    able = 0;
                    break;
                case 4: // выход из консольки
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
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
                    Console.Write("Неверный воод! Введите целое число > 0 : ");
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
                    Console.Write("Неверный ввод! Введите корректные данные: ");
                    Console.ResetColor();
                }
            } while (!ok);
            return temp;
        }
    }
}
