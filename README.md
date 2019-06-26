# EducationalPractice

Учебная практика в НИУ ВШЭ (в Перми) студента Пискунова Романа Андреевича

## Оглавление:
1. [Задача 193 на ACMP](#Задание-1)
2. [Задача 896 на ACMP](#Задание-2)
3. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-3)
4. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-4)
5. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-5)
6. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-6)
7. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-7)
8. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-8)
9. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-9)
10. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-10)
11. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-11)
12. [59, рисунок з (Из сборника задач по программированию авторов)](#Задание-12)

## Задание 1:

### Задача:
Определить и вычислить положение и размеры заданных прямоугольников, количество и номера которых задаются пользователем в таблице.

### Входные данные:
* 3 целых числа > 0 : N, M и K.
* N строк по M чисел, каждое из которых соответствует номеру прямоугольника (таблица соответствующая полю N×M клеток).

### Выходные данные:
K строк, каждая из которых описывает соответствующий её номеру прямоугольник, а именно, состоит из координат левого нижнего и правого верхнего углов найденных прямоугольников.

<details>
    <summary><u><b>Программный код 1 задания</b></u></summary>
    
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

[:arrow_up:Оглавление](#Оглавление)

## Задание 2:

### Задача:
Известно, что на дне рождения может быть либо M, либо N человек, включая самого именинника. На какое минимальное количество частей ему нужно разрезать торт (не обязательно всех равных), чтобы при любом из указанных количеств собравшихся, все съели торт поровну?

### Входные данные:
* 2 натуральных числа : M, N (безразницы в каком порядке).

### Выходные данные:
Искомое минимальное количество кусочков торта (целое число > 0).

<details>
    <summary><u><b>Программный код 2 задания</b></u></summary>
    
```C#
using System;
using System.IO;

namespace Task_2 {
    public class Program {
        private static void Main(string[] args) {
            StreamReader reader = new StreamReader("INPUT.txt");
            StreamWriter writer = new StreamWriter("OUTPUT.txt");
            
            string[] init = reader.ReadToEnd().Split();
            
            int m = Convert.ToInt32(init[0]);
            int n = Convert.ToInt32(init[1]);
            int r = m + n;
            int p = 0;
            while (n > 0) {
                p = m % n;
                m = n;
                n = p;
            }

            writer.WriteLine(r - m);
            reader.Close();
            writer.Close();
        }
    }
}
```
</details>

[:arrow_up:Оглавление](#Оглавление)

## Задание 3:

### Задача:
Определить, принадлежит ли точка вводимыми пользователем координатами x и y заштрихованной области, представленного на рисунке ниже.

![Рисунок с заштрихованной областью](images_for_git/task_3.jpg)

### Входные данные:
* 2 вещественных числа: координаты X и Y.

### Выходные данные:
Информация о попадании: либо "Попадает", либо "Не попадает".

<details>
    <summary><u><b>Главное из программного кода к 3 заданию:</b></u></summary>
    
```C#
using System;
using System.IO;

namespace Task_2 {
    public class Program {
        private static void Main(string[] args) {
            StreamReader reader = new StreamReader("INPUT.txt");
            StreamWriter writer = new StreamWriter("OUTPUT.txt");
            
            string[] init = reader.ReadToEnd().Split();
            
            int m = Convert.ToInt32(init[0]);
            int n = Convert.ToInt32(init[1]);
            int r = m + n;
            int p = 0;
            while (n > 0) {
                p = m % n;
                m = n;
                n = p;
            }

            writer.WriteLine(r - m);
            reader.Close();
            writer.Close();
        }
    }
}
```

</details>

[:arrow_up:Оглавление](#Оглавление)
