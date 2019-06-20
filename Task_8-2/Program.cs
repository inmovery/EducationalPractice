using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_8_2 {
    class Program {
        static void Main(string[] args) {

            // Чтение графа из файла
            Graph Graph = Graph.ReadGraph();

            // Если удалось прочитать граф
            if (Graph != null) {
                // Выделение блоков
                Graph.Block();
                Console.WriteLine();
            }

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
