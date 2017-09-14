using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Задание2
{
    class Program
    {
        static public void Quicksort(List<int> a)
        {
            if (a.Count > 1) Quicksort(a, 0, a.Count - 1);
        }

        static private void Quicksort(List<int> a, int left, int right)
        {
            if (left == right) return;
            int i = left + 1;
            int j = right;
            int pivot = a[left];

            while (i < j)
            {
                if (a[i] <= pivot) i++;
                else if (a[j] > pivot) j--;
                else
                { 
                    int m = a[i];
                    a[i] = a[j];
                    a[j] = m;
                }
            }

            if (a[j] <= pivot)
            {
                int m = a[left];
                a[left] = a[right];
                a[right] = m;
                Quicksort(a, left, right - 1);
            }
            else
            {
                Quicksort(a, left, i - 1);
                Quicksort(a, i, right);
            }
        }

        static public void Output(List<int> list)//Вывод результатов
        {
            int i = 0;

            for (i = 0; i < list.Count; i++)
                if (list[i] != 0) break;

            using (StreamWriter file = new StreamWriter("OUTPUT.TXT"))
            {
                for (int j = i; j < list.Count; j++)
                {
                    Console.Write(list[j]);
                    file.Write(list[j]);
                }
            }
        }

        static void Main(string[] args)
        {
            FileStream f = new FileStream("INPUT.TXT", FileMode.Create);//Создание файла с числами
            f.Close();

            using (StreamWriter file = new StreamWriter("INPUT.TXT"))
            {
                file.WriteLine("101");
                file.WriteLine("1");
            }

            string str;
            List<int> first = new List<int>();
            List<int> second = new List<int>();

            using (StreamReader file = new StreamReader("INPUT.TXT"))//Считывание информации из файла
            {
                str = file.ReadLine();
                for (int i = 0; i < str.Length; i++)
                    first.Add(int.Parse(str[i].ToString()));
                str = file.ReadLine();
                for (int i = 0; i < str.Length; i++)
                    second.Add(int.Parse(str[i].ToString()));
            }

            Quicksort(first);//Сортировка значений по возрастанию
            Quicksort(second);//получение минимальным чисел

            List<int> firstMax = new List<int>();
            List<int> secondMax = new List<int>();

            for (int i = first.Count - 1; i > -1; i--)//Получение масимальных значений
                firstMax.Add(first[i]);
            
            for (int i = second.Count - 1; i > -1; i--)
                secondMax.Add(second[i]);

            if (firstMax.Count > second.Count)//Если первое число длиннее второго
            {
                for (int i = 1; i <= second.Count; i++)//Вычитание
                {
                    if (firstMax[firstMax.Count - i] >= second[second.Count - i])//Если первая цифра больше второй
                        firstMax[firstMax.Count - i] = firstMax[firstMax.Count - i] - second[second.Count - i];
                    else//Если вторая цифра больше первой
                    {
                        firstMax[firstMax.Count - i] = firstMax[firstMax.Count - i] + 10 - second[second.Count - i];
                        --firstMax[firstMax.Count - i - 1];
                    }
                }

                for (int i = 1; i <= firstMax.Count; i++)//Если первая цифра второго числа была больше соответствующей первого
                    if (firstMax[firstMax.Count - i] < 0)
                    {
                        firstMax[firstMax.Count - i] = firstMax[firstMax.Count - i] + 10;
                        --firstMax[firstMax.Count - i - 1];
                    }

                Output(firstMax);

            }
            else
            if (secondMax.Count > first.Count)//Если второе число длиннее первого
            {
                for (int i = 1; i <= first.Count; i++)
                {
                    if (secondMax[secondMax.Count - i] >= first[first.Count - i])//Если вторая цифра больше первой
                        secondMax[secondMax.Count - i] = secondMax[secondMax.Count - i] - first[first.Count - i];
                    else//Если первая цифра больше второй
                    {
                        secondMax[secondMax.Count - i] = secondMax[secondMax.Count - i] + 10 - first[first.Count - i];
                        secondMax[secondMax.Count - i - 1]--;
                    }
                }

                for (int i = 1; i <= secondMax.Count; i++)//Если последняя цифра первого числа была больше соответствующей второго
                    if (secondMax[secondMax.Count - i] < 0)
                    {
                        secondMax[secondMax.Count - i] = secondMax[secondMax.Count - i] + 10;
                        --secondMax[secondMax.Count - i - 1];
                    }

                Output(secondMax);
            }
            else         
            if (first.Count == second.Count)//Если числа равной длинны
            {
                for (int i = 1; i < first.Count; i++)
                {
                    if (firstMax[firstMax.Count - i] >= second[second.Count - i])//Если первая цифра больше второй
                        firstMax[firstMax.Count - i] = firstMax[firstMax.Count - i] - second[second.Count - i];
                    else//Если вторая цифра больше первой
                    {
                        firstMax[firstMax.Count - i] = firstMax[firstMax.Count - i] + 10 - second[second.Count - i];
                        --firstMax[firstMax.Count - i - 1];
                    }
                }
                firstMax[0] = firstMax[0] - second[0];

                if (firstMax[0] < 0)//Если второе число было больше первого
                {
                    for (int i = 1; i < first.Count; i++)
                    {
                        if (secondMax[secondMax.Count - i] >= first[first.Count - i])//Если вторая цифра больше первой
                            secondMax[secondMax.Count - i] = secondMax[secondMax.Count - i] - first[first.Count - i];
                        else//Если первая цифра больше второй
                        {
                            secondMax[secondMax.Count - i] = secondMax[secondMax.Count - i] + 10 - first[first.Count - i];
                            secondMax[secondMax.Count - i - 1]--;
                        }
                    }
                    secondMax[0] = secondMax[0] - first[0];

                    Output(secondMax);
                }
                else Output(firstMax);
            }

            Console.ReadLine();
        }
    }
}
