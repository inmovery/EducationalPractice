using System;
using System.Linq;
using System.IO;

namespace Task_10 {
    public class Graph {
        
        int Size; // Количество вершин графа
        byte[,] AdjacencyMatrix; // Матрица смежности
        int[] Values; // Массив значений, записанных в вершинах

        public Graph(int Size, int[] Val, byte[,] Matrix) {
            this.Size = Size;
            AdjacencyMatrix = Matrix;
            Values = Val;
        }

        public Graph() {
            this.Size = 0;
            AdjacencyMatrix = new byte[0,0];
            Values = new int[0];
        }

        /// <summary>
        /// Чтение графа из файла
        /// </summary>
        /// <returns></returns>
        public static Graph ReadGraph() {
            Graph g = new Graph();
            try {
                int Size;
                int[] Val;
                byte[,] Matrix;
                using (FileStream file = new FileStream(@"C:\Users\clay.DESKTOP-JFB7SPD\Desktop\Учебная практика\EducationalPractice\Task_10\bin\Debug\input.txt", FileMode.Open)) {
            
                    StreamReader reader = new StreamReader(file);
                        
                        bool ok = int.TryParse(reader.ReadLine(), out Size);
                        // Массив значений
                        string vals = reader.ReadLine();
                        Val = new int[Size];
                        if (vals.Length > Size * 2 - 1)
                            vals = vals.Remove(Size * 2 - 1);
                        Val = vals.Split(' ').Select(n => int.Parse(n)).ToArray();
                        // Матрица смежности
                        Matrix = new byte[Size, Size];
                        for (int i = 0; i < Size; i++) {
                            vals = reader.ReadLine();
                            if (vals.Length > Size * 2 - 1)
                                vals = vals.Remove(Size * 2 - 1);
                            // Чтение строки матрицы
                            byte[] Row = vals.Split(' ').Select(n => Byte.Parse(n)).ToArray();
                            for (int j = 0; j < Size; j++) {
                                if (Row[j] != 0 && Row[j] != 1) {
                                    Console.WriteLine("В файле содержатся некорректные данные.");
                                    return null;
                                }
                                Matrix[i, j] = Row[j];
                            }
                        }
                
                    reader.Close();
                }
                g = new Graph(Size, Val, Matrix);
            } catch (FormatException) {
                Console.WriteLine("Заданные данные некорректны!");
            }
            return g;
        }

        /// <summary>
        /// Запись графа в файл
        /// </summary>
        public void WriteGraph() {
            using (FileStream file = new FileStream(@"C:\Users\clay.DESKTOP-JFB7SPD\Desktop\Учебная практика\EducationalPractice\Task_10\bin\Debug\output.txt", FileMode.OpenOrCreate)) {

                StreamWriter sw = new StreamWriter(file);

                sw.WriteLine(Size); // Размер графа

                // Массив значений
                foreach (int item in Values)
                    sw.Write(item + " ");
                sw.WriteLine();

                // Матрица смежности
                for (int i = 0; i < Size; i++) {
                    for (int j = 0; j < Size; j++)
                        sw.Write(AdjacencyMatrix[i, j] + " ");
                    sw.WriteLine();
                }

                Console.WriteLine("Информация об обработанном графе записана в файл output.txt");

                sw.Close();
            }
        }

        /// <summary>
        /// Стягиевание графа
        /// </summary>
        /// <param name="Val"></param>
        public void Contraction(int Val) {
            bool ok = false;
            for (int j = 0; j < AdjacencyMatrix.Length / Size; j++) {
                for (int k = 0; k < AdjacencyMatrix.Length / Size; k++) {
                    if (AdjacencyMatrix[j, k] == 1) {
                        ok = true;
                        break;
                    }
                }
            }
            if (ok) {
                // Если искомое значение отсутствует в графе
                if (!Values.Contains(Val)) {
                    Console.WriteLine("В графе нет вершины с указанным значением.");
                    return;
                }
                // Номер вершины, где впервые встречается искомое значение
                int FirstVertex = Array.IndexOf(Values, Val);

                int i = FirstVertex + 1;
                // Проходим по оставшимся вершинам графа
                while (i < Size) {
                    // Если нашлась еще одна вершина с искомым значением
                    if (Values[i] == Val) {
                        // Переносим ребра
                        // Перенос ребер, исходящих из этой вершины
                        for (int col = 0; col < Size; col++)
                            // Если из данной вершины исходит ребро (но не в точку,
                            // в которую стягиваем, чтобы не было петлей)
                            if (AdjacencyMatrix[i, col] == 1 && col != FirstVertex)
                                // Переносим начало ребра в вершину, куда стягиваем
                                AdjacencyMatrix[FirstVertex, col] = 1;

                        // Перенос ребер, входящих в эту вершину
                        for (int row = 0; row < Size; row++)
                            // Если в данную вершину входит ребро (но не из точки,
                            // в которую стягиваем, чтобы не было петлей)
                            if (AdjacencyMatrix[row, i] == 1 && row != FirstVertex)
                                // Переносим конец ребра в вершину, куда стягиваем
                                AdjacencyMatrix[row, FirstVertex] = 1;

                        // Удаление вершины из графа
                        RemoveVertex(i);
                    } else
                        i++;
                }
            } else {
                Console.WriteLine("Нечего удалять!");
            }
        }

        /// <summary>
        /// Удаление вершины графа
        /// </summary>
        /// <param name="index"></param>
        void RemoveVertex(int index) {
            // Удаление значения
            int[] NewValues = new int[Size - 1];
            for (int i = 0; i < index; i++)
                NewValues[i] = Values[i];
            for (int i = index + 1; i < Size; i++)
                NewValues[i - 1] = Values[i];
            Values = NewValues;

            // Удаление вершины из матрицы
            byte[,] NewMatrix = new byte[Size - 1, Size - 1];

            // Копирование незатронутой части
            for (int i = 0; i < index; i++)
                for (int j = 0; j < index; j++)
                    NewMatrix[i, j] = AdjacencyMatrix[i, j];
            // Удаление столбца
            for (int i = 0; i < NewMatrix.GetLength(0); i++)
                for (int j = index; j < NewMatrix.GetLength(1); j++)
                    NewMatrix[i, j] = AdjacencyMatrix[i, j + 1];
            // Удаление строки
            for (int i = index; i < NewMatrix.GetLength(0); i++)
                for (int j = 0; j < NewMatrix.GetLength(1); j++)
                    NewMatrix[i, j] = AdjacencyMatrix[i + 1, j];
            AdjacencyMatrix = NewMatrix;

            Size--;
        }
    }
}
