using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace Task_8 {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        DrawGraph field; //поле для графа
        List<Vertex> vertices; //список вершин
        List<Edge> edges; //список рёбер
        int[,] AMatrix; //матрица смежности
        int[,] IMatrix; //матрица инцидентности

        //для создания ребра
        int selected1; //выбрана перая вершина
        int selected2; //выбрана вторая вершина 

        private Point PointStart;
        private Point PointEnd;

        private delegate void DrawHandler(MouseEventArgs e);

        private bool OffOn = false;

        /// <summary>
        /// Инициалзация всякого
        /// </summary>
        public Form1() {
            InitializeComponent();

            BiconnectedComponentsFinder.edgeList = new List<List<int>>();
            BiconnectedComponentsFinder.vertices = new List<Vertex>();

            BiconnectedComponentsFinder.dfscounter = 0; // Счетчик
            BiconnectedComponentsFinder.compcounter = 0; // Число двусвязных комонент
            BiconnectedComponentsFinder.numPoints = 0; // Число точек сочленения

            BiconnectedComponentsFinder.points = "Точки сочленения:";

            vertices = new List<Vertex>();
            field = new DrawGraph(sheet.Width, sheet.Height);
            edges = new List<Edge>();

            sheet.Image = field.GetBitmap();
            sheet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseDown);
            sheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseMove);
            sheet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseUp);

            sheet.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.sheet_PreviewKeyDown);

        }

        /// <summary>
        /// Кнопка "ВЫБРАТЬ ВЕРШИНУ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectButton_Click(object sender, EventArgs e) {
            //анимация выбора кнопки
            selectButton.Enabled = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;

            field.clearSheet();
            field.drawALLGraph(vertices, edges);
            sheet.Image = field.GetBitmap();
            selected1 = -1;//выбрана первая вершина (для создания ребра)
        }

        /// <summary>
        /// Кнопка "СОЗДАТЬ ВЕРШИНУ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawVertexButton_Click(object sender, EventArgs e) {
            //анимация выбора кнопки
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;

            field.clearSheet();
            field.drawALLGraph(vertices, edges);
            sheet.Image = field.GetBitmap();
        }

        /// <summary>
        /// Кнопка "СОЗДАТЬ РЕБРО"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawEdgeButton_Click(object sender, EventArgs e) {
            //анимация выбора кнопки
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;

            field.clearSheet();
            field.drawALLGraph(vertices, edges);
            sheet.Image = field.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }

        /// <summary>
        /// Кнопка "УДАЛИТЬ ЭЛЕМЕНТ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e) {
            //анимация выбора кнопки
            deleteButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;

            field.clearSheet();
            field.drawALLGraph(vertices, edges);
            sheet.Image = field.GetBitmap();
        }

        /// <summary>
        /// Кнопка "УДАЛИТЬ ГРАФ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteALLButton_Click(object sender, EventArgs e) {
            //анимация выбора кнопки
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;

            // процесс удаления
            BiconnectedComponentsFinder.edgeList.Clear();
            vertices.Clear();
            edges.Clear();
            field.clearSheet();
            sheet.Image = field.GetBitmap();
        }
        
        /// <summary>
        /// Кнопка "МАТРИЦА ИНЦИДЕНЦИЙ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>         
        private void buttonInc_Click(object sender, EventArgs e) {
            createIncAndOut();
        }

        private void sheet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {

        }

        /// <summary>
        /// Нажатие на вершину (+ перетаскивание != клик)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sheet_MouseDown(object sender, MouseEventArgs e) {
            //e.X, e.Y - координаты нажатия
            if (drawEdgeButton.Enabled == false) {
                // обработка левой кнопки мыши

                if (e.Button == MouseButtons.Left) {

                    PointStart = e.Location;
                    OffOn = true;// включение отрисовки линии

                    for (int i = 0; i < vertices.Count; i++) {
                        if (Math.Pow((vertices[i].x - e.X), 2) + Math.Pow((vertices[i].y - e.Y), 2) <= field.R * field.R) {
                            //если первая вершина выбрана
                            if (selected1 == -1) {
                                field.drawSelectedVertex(vertices[i].x, vertices[i].y);//добавляем маркер выбранности
                                selected1 = i;//записываем номер выбранной вершины
                                sheet.Image = field.GetBitmap();// обноавляем поле
                                break;
                            } else {
                                field.clearSheet();
                                field.drawALLGraph(vertices, edges);
                                field.drawSelectedVertex(vertices[i].x, vertices[i].y);
                                sheet.Image = field.GetBitmap();
                                break;
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Отслеживание перемещение курсора мыши при захвате вершины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sheet_MouseMove(object sender, MouseEventArgs e) {
            Pen style = new Pen(Color.White, 2);
            style.DashStyle = DashStyle.Dash;
            if (Button.MouseButtons == MouseButtons.Left) {
                if (drawEdgeButton.Enabled == false) {
                    if (OffOn == true) {
                        ((PictureBox)sender).Refresh();
                        Graphics g = ((PictureBox)sender).CreateGraphics();
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        PointEnd = e.Location;

                        g.DrawLine(style, PointStart, PointEnd);
                    }
                }
            }

            style.Dispose();
        }
        
        /// <summary>
        /// Событие, когда я отпуская левую кнопки мыши (для создания ребра)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sheet_MouseUp(object sender, MouseEventArgs e) {
            OffOn = false;
            ((PictureBox)sender).Refresh();
            bool ok = true;
            if (Math.Pow((PointStart.X - e.X), 2) + Math.Pow((PointStart.Y - e.Y), 2) <= Math.Pow((PointEnd.X - e.X), 2) + Math.Pow((PointEnd.Y - e.Y), 2)) {
                ok = false;
                ((PictureBox)sender).Refresh();
            }
            for (int i = 0; i < vertices.Count; i++) {
                if (Math.Pow((vertices[i].x - e.X), 2) + Math.Pow((vertices[i].y - e.Y), 2) <= field.R * field.R && ok) {

                    //MessageBox.Show(PointStart.ToString()+"\n"+PointEnd.ToString());

                    //если первая вершина выбрана и нажата вторая вершина
                    if (selected2 == -1 && selected1 != -1) {
                        field.drawSelectedVertex(vertices[i].x, vertices[i].y);
                        selected2 = i;
                        edges.Add(new Edge(selected1, selected2));
                        
                        addEdge(selected1, selected2);
                        addEdge(selected2, selected1);

                        field.drawEdge(vertices[selected1], vertices[selected2], edges[edges.Count - 1], edges.Count - 1);
                        selected1 = -1;
                        selected2 = -1;
                        sheet.Image = field.GetBitmap();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Обработка нажатий на поле для графа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sheet_MouseClick(object sender, MouseEventArgs e) {
            // Нажата кнопка "Выбрать вершину" + поиск степени вершины
            if (selectButton.Enabled == false) {
                //e.X, e.Y – координаты точки в месте щелчка мыши
                for (int i = 0; i < vertices.Count; i++) {
                    if (Math.Pow((vertices[i].x - e.X), 2) + Math.Pow((vertices[i].y - e.Y), 2) <= field.R * field.R) {
                        //очистка
                        if (selected1 != -1) {
                            selected1 = -1;
                            field.clearSheet();
                            field.drawALLGraph(vertices, edges);
                            sheet.Image = field.GetBitmap();
                        }
                        if (selected1 == -1) {
                            field.drawSelectedVertex(vertices[i].x, vertices[i].y);//задание фактора выбранности
                            selected1 = i;//выбрана такая-то вершина
                            sheet.Image = field.GetBitmap();
                            createAdjAndOut();
                            listBoxMatrix.Items.Clear();
                            int degree = 0;
                            for (int j = 0; j < vertices.Count; j++)
                                degree += AMatrix[selected1, j];
                            listBoxMatrix.Items.Add("Степень вершины №" + (selected1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
            // Нажата кнопка "Создать вершину"
            if (drawVertexButton.Enabled == false) {
                vertices.Add(new Vertex(e.X, e.Y));
                BiconnectedComponentsFinder.vertices.Add(new Vertex(e.X, e.Y));
                BiconnectedComponentsFinder.edgeList.Add(new List<int>());//для создание списка смежности
                field.drawVertex(e.X, e.Y, vertices.Count.ToString());
                sheet.Image = field.GetBitmap();
            }
            // Нажата кнопка "Удалить вершину ( + рёбро(-а), если есть)"
            if (deleteButton.Enabled == false) {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                for (int i = 0; i < vertices.Count; i++) {
                    if (Math.Pow((vertices[i].x - e.X), 2) + Math.Pow((vertices[i].y - e.Y), 2) <= field.R * field.R) {
                        for (int j = 0; j < edges.Count; j++) {
                            if ((edges[j].v1 == i) || (edges[j].v2 == i)) {
                                edges.RemoveAt(j);
                                j--;
                            } else {
                                if (edges[j].v1 > i)
                                    edges[j].v1--;
                                if (edges[j].v2 > i)
                                    edges[j].v2--;
                            }
                        }
                        vertices.RemoveAt(i);
                        BiconnectedComponentsFinder.edgeList.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag) {
                    for (int i = 0; i < edges.Count; i++) {
                        if (edges[i].v1 == edges[i].v2) { // Если это петля
                            if ((Math.Pow((vertices[edges[i].v1].x - field.R - e.X), 2) + Math.Pow((vertices[edges[i].v1].y - field.R - e.Y), 2) <= ((field.R + 2) * (field.R + 2))) &&
                                (Math.Pow((vertices[edges[i].v1].x - field.R - e.X), 2) + Math.Pow((vertices[edges[i].v1].y - field.R - e.Y), 2) >= ((field.R - 2) * (field.R - 2)))) {
                                edges.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        } else { // Если не петля
                            if (((e.X - vertices[edges[i].v1].x) * (vertices[edges[i].v2].y - vertices[edges[i].v1].y) / (vertices[edges[i].v2].x - vertices[edges[i].v1].x) + vertices[edges[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - vertices[edges[i].v1].x) * (vertices[edges[i].v2].y - vertices[edges[i].v1].y) / (vertices[edges[i].v2].x - vertices[edges[i].v1].x) + vertices[edges[i].v1].y) >= (e.Y - 4)) {
                                if ((vertices[edges[i].v1].x <= vertices[edges[i].v2].x && vertices[edges[i].v1].x <= e.X && e.X <= vertices[edges[i].v2].x) ||
                                    (vertices[edges[i].v1].x >= vertices[edges[i].v2].x && vertices[edges[i].v1].x >= e.X && e.X >= vertices[edges[i].v2].x)) {
                                    edges.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                // Обновление графа на экране
                if (flag) {
                    field.clearSheet();
                    field.drawALLGraph(vertices, edges);
                    sheet.Image = field.GetBitmap();
                }
            }
        }


        // добавление ребра в список смежности
        private void addEdge(int n1, int n2) {
            BiconnectedComponentsFinder.edgeList[n1].Add(n2);
            BiconnectedComponentsFinder.numEdges++;
        }

        /// <summary>
        /// Cоздание матрицы смежности + ВЫВОД 
        /// </summary>
        private void createAdjAndOut() {
            AMatrix = new int[vertices.Count, vertices.Count];
            field.fillAdjacencyMatrix(vertices.Count, edges, AMatrix);
            listBoxMatrix.Items.Clear();
            string sOut = "    ";
            for (int i = 0; i < vertices.Count; i++)
                sOut += (i + 1) + " ";
            listBoxMatrix.Items.Add(sOut);
            for (int i = 0; i < vertices.Count; i++) {
                sOut = (i + 1) + " | ";
                for (int j = 0; j < vertices.Count; j++)
                    sOut += AMatrix[i, j] + " ";
                listBoxMatrix.Items.Add(sOut);
            }
        }

        /// <summary>
        /// Создание матрицы инцидентности + ВЫВОД
        /// </summary>
        private void createIncAndOut() {
            if (edges.Count > 0) {
                IMatrix = new int[vertices.Count, edges.Count];
                field.fillIncidenceMatrix(vertices.Count, edges, IMatrix);
                listBoxMatrix.Items.Clear();
                string sOut = "*   ";
                for (int i = 0; i < edges.Count; i++)
                    sOut += (char)('a' + i) + " ";
                listBoxMatrix.Items.Add(sOut);
                for (int i = 0; i < vertices.Count; i++) {
                    sOut = (i + 1) + " | ";
                    for (int j = 0; j < edges.Count; j++)
                        sOut += IMatrix[i, j] + " ";
                    listBoxMatrix.Items.Add(sOut);
                }
            } else
                listBoxMatrix.Items.Clear();
        }

        /// <summary>
        /// Сохранение графа в качестве изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e) {
            if (sheet.Image != null) {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) {
                    try {
                        sheet.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    } catch {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка "ПОСТРОИТЬ МАТРИЦУ СМЕЖНОСТЕЙ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e) {
            AMatrix = new int[vertices.Count, vertices.Count];
            field.fillAdjacencyMatrix(vertices.Count, edges, AMatrix);
            listBoxMatrix.Items.Clear();
            string sOut = "    ";
            for (int i = 0; i < vertices.Count; i++)
                sOut += (i + 1) + " ";
            listBoxMatrix.Items.Add(sOut);
            for (int i = 0; i < vertices.Count; i++) {
                sOut = (i + 1) + " | ";
                for (int j = 0; j < vertices.Count; j++)
                    sOut += AMatrix[i, j] + " ";
                listBoxMatrix.Items.Add(sOut);
            }
        }

        /// <summary>
        /// Поиск блоков по рёбрам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e) {

            IMatrix = new int[vertices.Count, edges.Count];
            field.fillIncidenceMatrix(vertices.Count, edges, IMatrix);

            listBoxMatrix.Items.Clear();
            // Чтение графа из файла
            Graph graph = new Graph(vertices.Count, edges.Count, IMatrix);

            // Если удалось прочитать граф
            if (graph != null) {
                // Выделение блоков
                graph.Block();
            }

            for (int i = 0; i < graph.result.Count; i++) {
                listBoxMatrix.Items.Add(graph.result[i]);
            }

        }

        /// <summary>
        /// Поиск блоков в вершинам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {

            BiconnectedComponentsFinder.dfscounter = 0; // Счетчик
            BiconnectedComponentsFinder.compcounter = 0; // Число двусвязных комонент
            BiconnectedComponentsFinder.numPoints = 0; // Число точек сочленения

            BiconnectedComponentsFinder.components = new List<string>(); // Список компонент для вывода

            for (int i = 0; i < BiconnectedComponentsFinder.vertices.Count; i++) {
                BiconnectedComponentsFinder.vertices[i].ClearValues();
            }

            listBoxMatrix.Items.Clear();
            BiconnectedComponentsFinder.findArticulationPoints(BiconnectedComponentsFinder.vertices[0]);
            listBoxMatrix.Items.Add("Количество блоков: " + BiconnectedComponentsFinder.compcounter);
            listBoxMatrix.Items.Add("Количество точек сочленения: " + BiconnectedComponentsFinder.compcounter);
            foreach (string s in BiconnectedComponentsFinder.components) {
                listBoxMatrix.Items.Add(s);
            }
        }

        /// <summary>
        /// Тест (Составление списка смежности)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e) {
            listBoxMatrix.Items.Clear();

            for (int i = 0; i < BiconnectedComponentsFinder.edgeList.Count; i++) {
                BiconnectedComponentsFinder.edgeList[i].Sort();
            }

            for (int i = 0; i < BiconnectedComponentsFinder.edgeList.Count; i++) {
                string temp = "";
                for (int j = 0; j < BiconnectedComponentsFinder.edgeList[i].Count; j++) {
                    temp += ((BiconnectedComponentsFinder.edgeList[i][j] + 1) + " ");
                }
                listBoxMatrix.Items.Add((i + 1) + ": " + temp);
            }
        }

        /// <summary>
        /// Выделение блоков (перекрас рёбер)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e) {

            List<Color> colors = new List<Color>(10);
            colors.Add(System.Drawing.ColorTranslator.FromHtml("#686fe3"));
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Magenta);
            colors.Add(Color.Yellow);
            colors.Add(System.Drawing.ColorTranslator.FromHtml("#686fe3"));
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Magenta);
            colors.Add(Color.Yellow);
            
            field.clearSheet();
            Pen edgeBlocks;
            for (int i = 0; i < BiconnectedComponentsFinder.blocks.Count; i++) {
                edgeBlocks = new Pen(colors[i]);
                for (int j = 0; j < BiconnectedComponentsFinder.blocks[i].Count; j++) { // взяли номера рёб
                    int[] h = BiconnectedComponentsFinder.blocks[i][j].Split(',').Select(x => int.Parse(x)).ToArray();
                    //((PictureBox)sender).Refresh();
                    //Graphics g = ((PictureBox)sender).CreateGraphics();
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.DrawLine(edgeBlocks, vertices[h[0] - 1].x, vertices[h[0] - 1].y, vertices[h[1] - 1].x, vertices[h[1] - 1].y);
                    field.RedrawEdge(vertices[h[0]-1], vertices[h[1]-1], edges[edges.Count - 1], i, edgeBlocks);
                    sheet.Image = field.GetBitmap();
                }
            }

            //field.RedrawALLGraph(vertices, edges, BiconnectedComponentsFinder.blocks[0]); // перекрас рёбер
            sheet.Image = field.GetBitmap();
        }
    }
}
