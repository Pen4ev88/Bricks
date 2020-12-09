using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bricks
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = InputMatrix();

            int[,] matrixOut = MergeMatrix(matrix);

            PrintBricks(matrixOut);
        }
        static int[,] InputMatrix()
        {
            string[] rol_col = Console.ReadLine().Split(' ');
            int m_number = int.Parse(rol_col[0]);
            int n_number = int.Parse(rol_col[1]);


            if ((n_number < 2 || n_number > 100) || (m_number < 2 || m_number > 100))
                Console.WriteLine("Input data must be at least 2 and can not exceed 100!");
            if (n_number % 2 != 0 || n_number % 2 != 0)
                Console.WriteLine("Input data was not even!");

            int[,] matrix = new int[m_number, n_number];

            for (int i = 0; i < m_number; i++)
            {
                string[] string_n = Console.ReadLine().Split(' ');

                for (int j = 0; j < string_n.Count(); j++)
                {
                    matrix[i, j] = int.Parse(string_n[j]);
                }
            }
            Console.WriteLine();

            return matrix;
        }

        static int[,] MergeMatrix(int[,] matrix)
        {
            int[,] matrixOut = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i += 2)
            {
                for (int j = 0; j < matrix.GetLength(1); j += 2)
                {
                    int base_brick = 0;
                    int second_brick = 0;

                    if (matrix[i, j] == matrix[i, j + 1])
                    {
                        base_brick = matrix[i, j];
                        second_brick = matrix[i + 1, j];

                        matrixOut[i, j] = base_brick;
                        matrixOut[i + 1, j] = base_brick;

                        matrixOut[i, j + 1] = second_brick;
                        matrixOut[i + 1, j + 1] = second_brick;
                    }
                    else
                    {
                        base_brick = matrix[i, j];
                        second_brick = matrix[i + 1, j + 1];

                        matrixOut[i, j] = base_brick;
                        matrixOut[i, j + 1] = base_brick;

                        matrixOut[i + 1, j] = second_brick;
                        matrixOut[i + 1, j + 1] = second_brick;
                    }
                }
            }
            return matrixOut;
        }

        static void PrintBricks(int[,] matrix)
        {
            // Border H -> |, BorderL -> 
            char wallBrickH = '*';
            char wallBrickL = '-';
            char wallBrickInternal = '|';
            int lenghtBorder = matrix.GetLength(1) * 2 - 1;
            string row_space = "";
            bool flag_space_row = false;

            // ######## border Top #############             
            BorderBricks(wallBrickH, wallBrickL, lenghtBorder);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                row_space = "";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    flag_space_row = false;

                    if (j == 0)
                        Console.Write("|");

                    if (j == matrix.GetLength(1) - 1)
                    {
                        Console.Write(matrix[i, j] + "|");
                    }
                    else
                    {
                        if (matrix[i, j] == matrix[i, j + 1])
                            flag_space_row = true;

                        if (i < matrix.GetLength(0) - 1)
                        {
                            if (matrix[i, j] != matrix[i + 1, j])
                                row_space += "--";
                            else
                                row_space += " |";
                        }

                        Console.Write(matrix[i, j]);

                        if (flag_space_row == true)
                            Console.Write(" ");
                        else
                            Console.Write("|");
                    }
                }

                Console.WriteLine();

                if (i < matrix.GetLength(0) - 1 && i % 2 == 1)
                {
                    BorderBricks(wallBrickInternal, wallBrickL, lenghtBorder);
                }
                else
                {
                    if (i < matrix.GetLength(0) - 1)
                        Console.Write("|");
                    if (row_space.Count() > 0)
                    {
                        char last_char = row_space.Last();
                        if (last_char == '|')
                            row_space += " ";
                        else
                            row_space += last_char;
                    }

                    Console.Write(row_space);

                    if (i < matrix.GetLength(0) - 1)
                    {
                        Console.Write("|");
                        Console.WriteLine();
                    }
                }
            }

            // ######## border Bottom ############# 
            BorderBricks(wallBrickH, wallBrickL, lenghtBorder);

            Console.WriteLine();
        }

        static void BorderBricks(char wallBrickH, char wallBrickL, int rowLength)
        {
            Console.Write(wallBrickH);

            while (true)
            {
                Console.Write(wallBrickL);
                rowLength--;
                if (rowLength == 0)
                    Console.Write(wallBrickH);
                if (rowLength == 0)
                    break;
            }
            Console.WriteLine();
        }
    }
}
