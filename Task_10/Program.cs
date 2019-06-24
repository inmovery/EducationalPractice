using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_10 {
    class Program {
        static void Main(string[] args) {

            Graph Graph = Graph.ReadGraph(); // Чтение графа из файла

            // Если удалось прочитать граф
            if (Graph != null) {
                int Value; // Значение, записанное в вершинах, которые надо стянуть
                
                bool ok;

                // Ввод значения, которое хотим стянуть
                do {
                    Console.Write("Введите значение, записанное в вершинах, которые надо стянуть: ");
                    ok = Int32.TryParse(Console.ReadLine(), out Value);
                    if (!ok)
                        Console.WriteLine("В вершинах записаны целые числа! Повторите ввод: ");
                } while (!ok);

                // Стягиваем вершины с указанным значением
                Graph.Contraction(Value);

                //// Запись полученного графа в файл
                Graph.WriteGraph();
            }

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
