# EducationalPractice

Учебная практика в НИУ ВШЭ (в Перми) студента Пискунова Романа Андреевича

## Задание № 1:

### Задача:
Определить и вычислить положение и размеры заданных прямоугольников, количество и номера которых задаются пользователем.

### Входные данные:
* 3 целых числа > 0 : N, M и K.
* N строк по M чисел, каждое из которых соответствует номеру прямоугольника (таблица соответствующая полю N×M клеток).

### Выходные данные:
K строк, каждая из которых описывает соответствующий её номеру прямоугольник, а именно, состоит из координат левого нижнего и правого верхнего углов найденных прямоугольников.

<details>
    <summary><b>Программный код 1 задания</b></summary>
    
```C#
using System;
using System.IO;

namespace Task_1 {
    public class Program {
        private static void Main(string[] args) {

            StreamWriter writer = new StreamWriter("OUTPUT.txt");
            StreamReader reader = new StreamReader("INPUT.txt");

            string init = "";
            string[] sett = reader.ReadLine().TrimStart(' ').Split(' ');//первая строка
            while (!reader.EndOfStream) init += (reader.ReadLine() + " ");

            init = init.TrimStart(' ');

            string[] pre = init.Split(' ');

            int n = Convert.ToInt32(sett[0]); // количество строк
            int m = Convert.ToInt32(sett[1]); // количество столбцов
            int k = Convert.ToInt32(sett[2]); // количество прямоугольников

            int[] xmin = new int[256]; // координата x левых нижних углов
            int[] ymin = new int[256]; // координата y левых нижних углов

            int[] xmax = new int[256]; // координата x правых верхних углов
            int[] ymax = new int[256]; // координата y правых верхних углов
            
            for (int i = 1; i <= k; i++){
                xmin[i] = m;
                ymin[i] = n;
            }

            int c = 0;
            int d = 0; // для проверки покрытых прямоугольников
            int count = 0; // количество занятых клеток
            for (int y = n; y >= 1; y--) {
                for (int x = 1; x <= m; x++) {
                    int j = Convert.ToInt32(pre[c]); // значение ячейки в матрице
                    
                    if (j > 0) {
                        d = j;
                        count++;
                        if (x < xmin[j]) xmin[j] = x;
                        if (y < ymin[j]) ymin[j] = y;

                        if (x > xmax[j]) xmax[j] = x;
                        if (y > ymax[j]) ymax[j] = y;
                    }
                    c++;
                }
            }

            for (int i = 1; i <= k; i++) {
                // если это единичная клетка, которая покрывает другую
                if (xmin[i] == m && ymin[i] == n && xmax[i] == 0 && ymax[i] == 0 && count == 1) {
                    writer.WriteLine((xmin[d] - 1) + " " + (ymin[d] - 1) + " " + xmax[d] + " " + ymax[d]);
                } else {
                    writer.WriteLine((xmin[i] - 1) + " " + (ymin[i] - 1) + " " + xmax[i] + " " + ymax[i]);
                }
            }

            writer.Close();
            reader.Close();
        }
    }
}
```
</details>
