﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_8 {
    public class Graph {

        public List<string> result = new List<string>();

        int Rows; // Количество вершин графа
        int Columns; // Количество ребер графа
        int[,] IncidenceMatrix; // Матрица инциденций
        int Blocks; // Количество блоков
        int CheckedCount; // Количество обследованных вершин
        int[] Checked; // Порядковые номера обхода вершин
        Stack<int> EdgesStack; // Стек с номерами ребер, составляющими блокы
        List<int> UsedEdges; // Список уже использованных ребер

        public Graph(int Rows, int Columns, int[,] Matrix) {
            this.Rows = Rows;
            this.Columns = Columns;
            IncidenceMatrix = Matrix;
            Blocks = 0;
            CheckedCount = 0;
            EdgesStack = new Stack<int>(Columns);
            Checked = new int[Rows];
            for (int i = 0; i < Rows; i++) {
                Checked[i] = 0;
            }
            UsedEdges = new List<int>(Columns);
        }

        /// <summary>
        /// Чтение графа из файла
        /// </summary>
        /// <returns></returns>
        public static Graph ReadGraph() {
            try {
                //FileStream File = new FileStream("C:/Users/clay.DESKTOP-JFB7SPD/Desktop/Учебная практика/EducationalPractice/Task_8-2/bin/Debug/input.txt", FileMode.Open);
                StreamReader sr = new StreamReader("input.txt");
                // Размер графа
                int Size = Int32.Parse(sr.ReadLine());
                // Количество ребер в графе
                int Edges = Int32.Parse(sr.ReadLine());
                // Матрица инциденций
                int[,] Matrix = new int[Size, Edges];
                for (int i = 0; i < Size; i++) {
                    string vals = sr.ReadLine();
                    if (vals.Length > Edges * 2 - 1)
                        vals = vals.Remove(Edges * 2 - 1);
                    // Чтение строки матрицы
                    int[] Row = vals.Split(' ').Select(n => int.Parse(n)).ToArray();
                    for (int j = 0; j < Edges; j++) {
                        if (Row[j] != 0 && Row[j] != 1) {
                            throw new Exception();
                        }
                        Matrix[i, j] = Row[j];
                    }
                }

                sr.Close();

                return new Graph(Size, Edges, Matrix);
            } catch (FileNotFoundException e) {
                Console.WriteLine("Не удается открыть файл, проверьте его наличие и правильность пути.");
                return null;
            } catch {
                Console.WriteLine("В файле содержатся некорректные данные.");
                return null;
            }
        }

        /// <summary>
        /// Выделение блоков графа методом поиска в глубину
        /// </summary>
        public void Block() {
            CheckedCount = 0;
            DFS(0, -1);
        }

        /// <summary>
        /// Поиск в глубину
        /// </summary>
        /// <param name="Pos">Позиция вершины</param>
        /// <param name="Parent">ТО, из какой вершины пришли</param>
        /// <returns></returns>
        int DFS(int Pos, int Parent) {
            Checked[Pos] = ++CheckedCount;

            int Minim = Checked[Pos]; // минимальное расстояние от Pos до входа

            // Перебор всех ребер, входящих или исходящих из вершины Pos
            for (int Edge = 0; Edge < Columns; Edge++) {
                if (IncidenceMatrix[Pos, Edge] == 1) {
                    int NextVerit = 0;
                    while (NextVerit < Rows && IncidenceMatrix[NextVerit, Edge] == 0 || NextVerit == Pos)
                        NextVerit++;

                    if (NextVerit != Parent) {

                        int t, cur_size = EdgesStack.Count;

                        // Если этого ребра еще нет в стеке
                        if (!UsedEdges.Contains(Edge)) {
                            UsedEdges.Add(Edge);
                            EdgesStack.Push(Edge);
                        }

                        //Если вершина еще не посещена
                        if (Checked[NextVerit] == 0) {
                            // Продолжаем обход из этой вершины
                            t = DFS(NextVerit, Pos);
                            if (t >= Checked[Pos]) {
                                string res = "Блоку " + ++Blocks + " принадлежат ребра: ";
                                while (EdgesStack.Count != cur_size) {
                                    res += ((char)('a' + EdgesStack.Pop()) + ", ");
                                }
                                result.Add(res);
                            }
                        } else
                            t = Checked[NextVerit];
                        Minim = Math.Min(Minim, t);
                    }
                }
            }
            return Minim;
        }
    }
}
