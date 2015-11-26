using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Программа работает с 2 клаассами матриц. Второй класс наследник первого и размера только 2х2.");
            Console.WriteLine("Работаем с TMatrix или с TMatrix2? 1/2");
            string s2 = Console.ReadLine();
            if (s2 == "1")
            {
                TMatrix matrix = new TMatrix();
                Console.WriteLine("Будем читать из файла или пишем так? F/W");
                TMatrix matrix5 = new TMatrix();
                string s1 = Console.ReadLine();
                if (s1 == "F")
                {
                    matrix.input2();
                    matrix.output();
                    matrix5.input2();
                    matrix5.output();
                }
                else
                {
                    matrix.size1();
                    matrix.input1();
                    matrix.output();
                    matrix5.size1();
                    matrix5.input1();
                    matrix5.output();
                }
                TMatrix matrix6 = new TMatrix();
                Console.WriteLine("Сумма - ");
                matrix6 = matrix + matrix5;
                matrix6.output();
                Console.WriteLine("Разность - ");
                matrix6 = matrix - matrix5;
                matrix6.output();

                Console.WriteLine("END");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Заполните матрицу 'A' 2x2 через enter");
                double[] a = new double[4];
                zap(a);

                TMatrix2 matrix = new TMatrix2();
                matrix.Create(a[0], a[1], a[2], a[3]);
                matrix.output();

                Console.WriteLine("Заполните матрицу 'B' 2x2 через enter");
                zap(a);
                TMatrix2 matrix1 = new TMatrix2();
                matrix1.Create(a[0], a[1], a[2], a[3]);
                matrix1.output();
                
                TMatrix2 matrix4 = new TMatrix2();
                //Console.WriteLine("Результат перемножения матриц :");
                //matrix4 = matrix;
                //matrix4.Umn(matrix1);
                //matrix4.vivod();
                Console.WriteLine("Выполняем isRoot. Определяем A^n=B ?");
                Console.WriteLine("Введите n:");
                int n;
                string s = Console.ReadLine();
                try
                {
                    n = int.Parse(s);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                    return;
                }
                bool b = isRoot(matrix, matrix1, n);
                Console.WriteLine("Ответ - " + b);
                Console.WriteLine("B^(-1):  ");
                matrix1.Inverse();
                matrix1.output();
                Console.WriteLine("END");
                Console.ReadLine();
            }
        }
        public static bool isRoot(TMatrix2 matrix, TMatrix2 matrix1, int n)
        {
            TMatrix2 matrix2 = new TMatrix2();
            matrix2.m[1, 1] = matrix.m[1, 1];
            matrix2.m[1, 2] = matrix.m[1, 2];
            matrix2.m[2, 1] = matrix.m[2, 1];
            matrix2.m[2, 2] = matrix.m[2, 2];
            int i = 2;
            while (i <= n)
            {
                matrix.Umn(matrix2);
                i++;
            }
            Console.WriteLine("A^n=");
            matrix.output();
            if (((matrix.m[1, 1] == matrix1.m[1, 1]) && (matrix.m[1, 2] == matrix1.m[1, 2])) && ((matrix.m[2, 1] == matrix1.m[2, 1]) && (matrix.m[2, 2] == matrix1.m[2, 2])))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void zap(double[] a)
        {
            int j = 0;
            while (j <= 3)
            {
                string s = Console.ReadLine();
                try
                {
                    a[j] = double.Parse(s);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                    return;
                }
                j++;
            }
        }
    }

    public class TMatrix2 : TMatrix
    {
        public void Create(double a11, double a12, double a21, double a22)
        {
            m[1, 1] = a11;
            m[1, 2] = a12;
            m[2, 1] = a21;
            m[2, 2] = a22;
        }
        public override void output()
        {
            Console.WriteLine("Матрица 2x2: ");
            Console.Write(m[1, 1] + " ");
            Console.Write(m[1, 2]);
            Console.WriteLine();
            Console.Write(m[2, 1] + " ");
            Console.WriteLine(m[2, 2]);
        }
        public double Det()
        {
            double h = m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1];
            return h;
        }
        public void Inverse()
        {
            double h = 1 / (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]);
            double a, b, c, d;
            a = m[1, 1];
            b = m[1, 2];
            c = m[2, 1];
            d = m[2, 2];
            m[1, 2] = -c;
            m[2, 1] = -b;
            m[1, 1] = d;
            m[2, 2] = a;
            c = m[1, 2];
            m[1, 1] = m[1, 1] * h;
            m[1, 2] = m[2, 1] * h;
            m[2, 1] = c * h;
            m[2, 2] = m[2, 2] * h;

        }
        public void Umn(TMatrix2 matrix1)
        {
            double[,] n = new double[3, 3];
            double a11 = matrix1.m[1, 1];
            double a12 = matrix1.m[1, 2];
            double a21 = matrix1.m[2, 1];
            double a22 = matrix1.m[2, 2];
            n[1, 1] = m[1, 1] * a11 + m[1, 2] * a21;
            n[1, 2] = m[1, 1] * a12 + m[1, 2] * a22;
            n[2, 1] = m[2, 1] * a11 + m[2, 2] * a21;
            n[2, 2] = m[2, 1] * a12 + m[2, 2] * a22;
            m[1, 1] = n[1, 1];
            m[1, 2] = n[1, 2];
            m[2, 1] = n[2, 1];
            m[2, 2] = n[2, 2];
        }
        public void Del(TMatrix2 matrix1)
        {
            double a11 = matrix1.m[1, 1];
            double a12 = matrix1.m[1, 2];
            double a21 = matrix1.m[2, 1];
            double a22 = matrix1.m[2, 2];

            double h = 1 / (a11 * a22 - a12 * a21);
            double c;
            c = a12 * h;
            a12 = a21 * h;
            a21 = c;
            a11 = a11 * h;
            a22 = a22 * h;
            double[,] n = new double[2, 2];
            n[1, 1] = m[1, 1] * a11 + m[1, 2] * a21;
            n[1, 2] = m[1, 1] * a21 + m[1, 2] * a22;
            n[2, 1] = m[2, 1] * a11 + m[2, 2] * a21;
            n[2, 2] = m[2, 1] * a21 + m[2, 2] * a22;
            m[1, 1] = n[1, 1];
            m[1, 2] = n[1, 2];
            m[2, 1] = n[2, 1];
            m[2, 2] = n[2, 2];
        }
    }

    public class TMatrix
    {
        public int b = 0;
        public int a = 0;

        public static TMatrix operator * (TMatrix a, TMatrix b)
        {
            TMatrix c = new TMatrix();
            double[,] n = new double[3, 3];
            double a11 = a.m[1, 1];
            double a12 = a.m[1, 2];
            double a21 = a.m[2, 1];
            double a22 = a.m[2, 2];
            c.m[1, 1] = b.m[1, 1] * a11 + b.m[1, 2] * a21;
            c.m[1, 2] = b.m[1, 1] * a12 + b.m[1, 2] * a22;
            c.m[2, 1] = b.m[2, 1] * a11 + b.m[2, 2] * a21;
            c.m[2, 2] = b.m[2, 1] * a12 + b.m[2, 2] * a22;
            return c;
        }

        public static TMatrix operator -(TMatrix a, TMatrix b) //перегрузка оператора -
        {
            TMatrix c = new TMatrix();
            c.a = a.a;
            c.b = a.b;
            if (!((a.a == b.a) && (a.b == b.b)))
            {
                Console.WriteLine("Матрицы не равны, ответ будет не верен!!!");
                return c;
            }
            else
            {
                for (int i = 1; i <= c.a;i++ )
                {
                    for (int j = 1; j <= c.b; j++)
                    {
                        c.m[i, j] = a.m[i, j] - b.m[i, j];
                    }
                }
                return c;
            }
        }
        public static TMatrix operator +(TMatrix a, TMatrix b) //перегрузка оператора +
        {
            TMatrix c = new TMatrix();
            c.a = a.a;
            c.b = a.b;
            if (!((a.a == b.a) && (a.b == b.b)))
            {
                Console.WriteLine("Матрицы не равны, ответ будет не верен!!!");
                return c;
            }
            else
            {
                for (int i = 1; i <= c.a; i++)
                {
                    for (int j = 1; j <= c.b; j++)
                    {
                        c.m[i, j] = a.m[i, j] + b.m[i, j];
                    }
                }
                return c;
            }
        }
        public void input2()
        {
            string s;
            Console.WriteLine("Введите расположение файла");
            Console.WriteLine("P.S. Файл должен быть формата - каждая цифра на новой строке, сначала идет цифра обозначающая количество строк, потом количество столбцов, далее сама матрица");
            string s1 = Console.ReadLine();
            try
            {
                FileStream file = new FileStream(s1, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);

                s = reader.ReadLine();
                try
                {
                    a = int.Parse(s);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                    return;
                }
                s = reader.ReadLine();
                try
                {
                    b = int.Parse(s);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                    return;
                }
                double k = 0;
                for (int i = 1; i <= a; i++)
                {
                    for (int j = 1; j <= b; j++)
                    {
                        s = reader.ReadLine();
                        try
                        {
                            k = double.Parse(s);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                            return;
                        }
                        m[i, j] = k;
                    }
                }
                reader.Close();
            }
            catch (Exception error1)
            {
                Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error1.Message);
                return;
            }
        }

        public TMatrix()
        {
        }
        public void size1()
        {
            Console.WriteLine("Введите количество строк матрицы");
            string s = Console.ReadLine();
            try
            {
                a = int.Parse(s);
            }
            catch (Exception error)
            {
                Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                Console.WriteLine("END");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Введите количество столбцов матрицы");
            s = Console.ReadLine();
            try
            {
                b = int.Parse(s);
            }
            catch (Exception error)
            {
                Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                Console.WriteLine("END");
                Console.ReadLine();
                return;
            }
        }

        public double[,] m = new double[50, 50];

        public virtual void output()
        {
            int i = 1;
            int j = 1;
            while (i <= a)
            {
                while (j <= b)
                {
                    Console.Write(m[i, j] + " ");
                    j++;
                }
                Console.WriteLine();
                j = 1;
                i++;
            }
        }

        public void input1()
        {
            Console.WriteLine("Заполните матрицу через enter");
            int i = 1;
            int j = 1;
            double c = 0;
            while (i <= a)
            {
                while (j <= b)
                {
                    string s = Console.ReadLine();
                    try
                    {
                        c = double.Parse(s);
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine("Произошла ошибка! Информация об ошибке - " + error.Message);
                        return;
                    }
                    m[i, j] = c;
                    j++;
                }
                j = 1;
                i++;
            }
        }
    }
}