using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_8 {
    /// <summary>
    /// Вершина
    /// </summary>
    public class Vertex {

        public int x, y; // координаты

        public int dfsNum; // порядковый номер посещения глубину
        public int low; // минимальный номер среди всех вершин, смежных с V и с теми вершинами, в которые мы пришли по пути, проходящем через V
        public int parent; // номер вершины, из который попали в текущую вершину
        public bool removed; // посетили вершину?

        public Vertex(int x, int y) {
            this.x = x;
            this.y = y;
            dfsNum = -1;
            low = -1;
            parent = -1;
            removed = false;
        }

        public void ClearValues() {
            this.dfsNum = -1;
            this.low = -1;
            this.parent = -1;
            this.removed = false;
        }

    }

    /// <summary>
    /// Ребро
    /// </summary>
    class Edge {

        public int v1, v2; // смежные вершины

        public Edge(int v1, int v2) {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    class DrawGraph {
        Bitmap bitmap;
        Pen backPen;
        Pen checkedPen;
        Pen edgeLine; // чем рисуются рёбра (перо)
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 20; //радиус окружности вершины

        public DrawGraph(int width, int height) {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);

            //сглаживание
            gr.SmoothingMode = SmoothingMode.AntiAlias;//HighQuality
            //настройка поля для графа
            clearSheet();
            //настройка крагу-границы вершин
            backPen = new Pen(Color.White);
            backPen.Width = 5;
            //настройка выбранной вершины
            checkedPen = new Pen(System.Drawing.ColorTranslator.FromHtml("#686fe3"));
            checkedPen.Width = 3;
            //настройка рёбер
            edgeLine = new Pen(System.Drawing.ColorTranslator.FromHtml("#686fe3"));
            edgeLine.Width = 3;
            //настройка номеров вершин и названий рёбер
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap() { return bitmap; }

        /// <summary>
        /// Очистка поля графа + установка фона
        /// </summary>
        public void clearSheet() { gr.Clear(Color.FromArgb(91, 100, 110)); }

        /// <summary>
        /// Рисование круглишка вершины
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="number"></param>
        public void drawVertex(int x, int y, string number) {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);//заполнение круга белым цветом
            gr.DrawEllipse(backPen, (x - R), (y - R), 2 * R, 2 * R);//создание круга–границы
            point = new PointF(x - 9, y - 9);//координаты номера вершины
            gr.DrawString(number, fo, br, point);//создание номера вершины
        }

        /// <summary>
        /// Рисование выбора круглишка вершины
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void drawSelectedVertex(int x, int y) { gr.DrawEllipse(checkedPen, (x - R), (y - R), 2 * R, 2 * R); }

        /// <summary>
        /// Рисование ребра
        /// </summary>
        /// <param name="V1">Первая вершина</param>
        /// <param name="V2">Вторая вершина</param>
        /// <param name="edge">Ребро</param>
        /// <param name="numberE">Номер ребра</param>
        public void drawEdge(Vertex V1, Vertex V2, Edge edge, int numberE) {
            if (edge.v1 == edge.v2) {
                gr.DrawArc(edgeLine, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (edge.v1 + 1).ToString());
            } else {
                gr.DrawLine(edgeLine, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (edge.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (edge.v2 + 1).ToString());
            }
        }

        /// <summary>
        /// Перерисовывание ребра
        /// </summary>
        /// <param name="V1">Первая вершина</param>
        /// <param name="V2">Вторая вершина</param>
        /// <param name="edge">Ребро</param>
        /// <param name="numberE">Номер ребра</param>
        public void RedrawEdge(Vertex V1, Vertex V2, Edge edge, int numberE, Pen edgeBlock) {

            gr.SmoothingMode = SmoothingMode.AntiAlias;

            edgeBlock.Width = 3;

            if (edge.v1 == edge.v2) {
                gr.DrawArc(edgeBlock, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                drawVertex(V1.x, V1.y, (edge.v1 + 1).ToString());
            } else {
                gr.DrawLine(edgeBlock, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                drawVertex(V1.x, V1.y, (edge.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (edge.v2 + 1).ToString());
            }
        }

        public void drawALLGraph(List<Vertex> vertices, List<Edge> edges) {
            // создание рёбер
            for (int i = 0; i < edges.Count; i++) {
                if (edges[i].v1 == edges[i].v2) {
                    gr.DrawArc(edgeLine, (vertices[edges[i].v1].x - 2 * R), (vertices[edges[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(vertices[edges[i].v1].x - (int)(2.75 * R), vertices[edges[i].v1].y - (int)(2.75 * R));
                    gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                } else {
                    gr.DrawLine(edgeLine, vertices[edges[i].v1].x, vertices[edges[i].v1].y, vertices[edges[i].v2].x, vertices[edges[i].v2].y);
                    point = new PointF((vertices[edges[i].v1].x + vertices[edges[i].v2].x) / 2, (vertices[edges[i].v1].y + vertices[edges[i].v2].y) / 2);
                    gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
            }
            // создание вершин
            for (int i = 0; i < vertices.Count; i++) {
                drawVertex(vertices[i].x, vertices[i].y, (i + 1).ToString());
            }
        }


        //public void RedrawALLGraph(List<Vertex> vertices, List<Edge> edges, List<string> blocks) {
        //    // создание рёбер
        //    for (int i = 0; i < edges.Count; i++) {
        //        if (edges[i].v1 == edges[i].v2) {
        //            gr.DrawArc(edgeLine, (vertices[edges[i].v1].x - 2 * R), (vertices[edges[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
        //            point = new PointF(vertices[edges[i].v1].x - (int)(2.75 * R), vertices[edges[i].v1].y - (int)(2.75 * R));
        //            gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
        //        } else {
        //            gr.DrawLine(edgeLine, vertices[edges[i].v1].x, vertices[edges[i].v1].y, vertices[edges[i].v2].x, vertices[edges[i].v2].y);
        //            point = new PointF((vertices[edges[i].v1].x + vertices[edges[i].v2].x) / 2, (vertices[edges[i].v1].y + vertices[edges[i].v2].y) / 2);
        //            gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
        //        }
        //    }
        //    // создание вершин
        //    for (int i = 0; i < vertices.Count; i++) {
        //        drawVertex(vertices[i].x, vertices[i].y, (i + 1).ToString());
        //    }
        //}


        /// <summary>
        /// Заполнение матрицы смежности
        /// </summary>
        /// <param name="numberV">Номер вершины</param>
        /// <param name="edges">Список рёбер</param>
        /// <param name="matrix">Исходная матрица</param>
        public void fillAdjacencyMatrix(int numberV, List<Edge> edges, int[,] matrix) {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < edges.Count; i++) {
                matrix[edges[i].v1, edges[i].v2] = 1;
                matrix[edges[i].v2, edges[i].v1] = 1;
            }
        }

        /// <summary>
        /// Заполнение матрицы инциденций
        /// </summary>
        /// <param name="numberV">Номер вершины</param>
        /// <param name="E">Список рёбер</param>
        /// <param name="matrix">Исходная матрица</param>
        public void fillIncidenceMatrix(int numberV, List<Edge> edges, int[,] matrix) {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < edges.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < edges.Count; i++) {
                matrix[edges[i].v1, i] = 1;
                matrix[edges[i].v2, i] = 1;
            }
        }

    }

    public static class BiconnectedComponentsFinder {

        public static List<List<string>> blocks = new List<List<string>>();

        public static List<List<int>> edgeList = new List<List<int>>();

        public static List<Vertex> vertices = new List<Vertex>();  // список вершин
        public static List<string> components = new List<string>(); // список двусвязных компонент

        public static int numEdges = 0;

        public static int dfscounter, compcounter, numPoints;
        public static string comp, points;

        // находит точки сочленения
        public static void findArticulationPoints(Vertex v) {

            v.dfsNum = ++dfscounter; // у каждой вершины свой порядковый номер посещения в обходе в глубину
            v.low = v.dfsNum; //low - минимальный номер среди всех вершин, смежных с v и с теми вершинами, в которые мы пришли по пути, проходящем через v.

            // Depth First Search - поиск в глубину
            int vNum = vertices.IndexOf(v);
            List<int> vList = edgeList[vNum];

            //поиск смежных вершин
            for (int i = 0; i < vList.Count; i++) {
                Vertex x = vertices[vList[i]];

                if (x.dfsNum == -1) {
                    x.parent = vNum;
                    findArticulationPoints(x);
                    v.low = Math.Min(v.low, x.low);

                    if (x.low >= v.dfsNum) {

                        // для вывода компонент
                        points += "  " + vNum;
                        numPoints++;
                        comp = "Компоненты " + (compcounter + 1) + ": {";
                        blocks.Add(new List<string>());
                        // находит двусвязные компоненты
                        biconnectedComponent(v, x);

                        comp += "}";

                        // для вывода компонент
                        components.Add(comp);
                        comp = "";
                        compcounter++;
                    }
                } else if (v.parent != vList[i]) {
                    v.low = Math.Min(v.low, x.dfsNum);
                }
            }
        }


        // поиск двусвязных компонент графа по точкам сочленения
        private static void biconnectedComponent(Vertex v, Vertex x) {

            int xNum = vertices.IndexOf(x); // порядковый номер первой вершины
            if (xNum == vertices.IndexOf(v) || x.removed) {
                return;
            }

            List<int> xList = edgeList[xNum]; // получаем список(массив) номеров вершин с которыми вершина х имеет ребро (смежные вершины для х)

            for (int i = 0; i < xList.Count; i++) {
                int x1 = xList[i]; // номер i-той смежной вершины
                if (!vertices[x1].removed) {
                    blocks[compcounter].Add((xNum + 1).ToString() + "," + (xList[i] + 1).ToString());
                    comp += "{" + (xNum+1).ToString() + "," + (xList[i]+1).ToString() + "}";
                    x.removed = true;
                    biconnectedComponent(v, vertices[x1]);
                }
            }
        }


    }

}
