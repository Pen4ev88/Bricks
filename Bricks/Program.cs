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
            try
            {
                int[,] matrix = ReadMatrixFromUi();
                int[,] matrixOut = MergeMatrix(matrix);

                PrintBricks(matrixOut);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int[,] ReadMatrixFromUi()
        {
            // rows    -> rol_col[0];
            // columns -> rol_col[1];
            int[] rows_cols = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();            

            if (rows_cols[0] < 2 || rows_cols[0] > 100 || rows_cols[1] < 2 || rows_cols[1] > 100)
            {
                throw new ArgumentOutOfRangeException(Messages.InvalidAreaSidesValues);
            }

            if (rows_cols[0] % 2 != 0 || rows_cols[1] % 2 != 0)
            {
                throw new ArgumentOutOfRangeException(Messages.InvalidAreaSidesValuesEven);
            }

            int[,] matrix = InitilizeMatrix(rows_cols[0], rows_cols[1]);

            Console.WriteLine();

            return matrix;
        }

        private static int[,] InitilizeMatrix(int rowsCount, int colsCount)
        {
            int[,] matrix = new int[rowsCount, colsCount];

            for (int i = 0; i < rowsCount; i++)
            {                
                int[] currentRowNumbers = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < currentRowNumbers.Count(); j++)
                {
                    matrix[i, j] = currentRowNumbers[j];
                }
            }

            return matrix;
        }

        static int[,] MergeMatrix(int[,] matrix)
        {
            int[,] matrixOut = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i += 2)
            {
                for (int j = 0; j < matrix.GetLength(1); j += 2)
                {
                    // the brick is horizontal
                    if (matrix[i, j] == matrix[i, j + 1])
                    {
                        int baseBrick = matrix[i, j];
                        int neighBrick = matrix[i + 1, j];

                        matrixOut.PutVerticalBrick(i, j, baseBrick);
                        matrixOut.PutVerticalBrick(i, j + 1, neighBrick);
                    }
                    else // the brick is vertical
                    {
                        int baseBrick = matrix[i, j];
                        int neighBrick = matrix[i + 1, j + 1];

                        matrixOut.PutHorizontalBrick(i, j, baseBrick);
                        matrixOut.PutHorizontalBrick(i + 1, j, neighBrick);
                    }
                }
            }
            return matrixOut;
        }

        static void PrintBricks(int[,] matrix)
        {            
            char wallBrick = '*';
            int lenghtBorder = matrix.GetLength(1) * 2 - 1; // length of char simbol of every even row
            string rowSpaceOrChar = "";  // string for whole row, describe the border of bricks, or empty space
            bool flagSpaceBrick = false; // flag for empty space if column pass between brick

            PrintEndBorderBricks(wallBrick, lenghtBorder);

            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                rowSpaceOrChar = "";
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    flagSpaceBrick = false;

                    if (colIndex == 0)
                        Console.Write("*");

                    if (colIndex == matrix.GetLength(1) - 1)    // last column 
                    {
                        Console.Write(matrix[rowIndex, colIndex] + "*");
                    }
                    else
                    {
                        if (matrix[rowIndex, colIndex] == matrix[rowIndex, colIndex + 1])
                            flagSpaceBrick = true;

                        if (rowIndex < matrix.GetLength(0) - 1)
                        {
                            if (matrix[rowIndex, colIndex] == matrix[rowIndex + 1, colIndex])
                                rowSpaceOrChar += " *";
                            else
                                rowSpaceOrChar += "**";
                        }

                        Console.Write(matrix[rowIndex, colIndex]);

                        if (flagSpaceBrick == true)
                            Console.Write(" ");
                        else
                            Console.Write("*");
                    }
                }

                Console.WriteLine();

                // Check if row is even
                if (rowIndex < matrix.GetLength(0) - 1 && rowIndex % 2 == 1)
                {
                    PrintEndBorderBricks(wallBrick, lenghtBorder);
                }
                else
                {
                    if (rowIndex < matrix.GetLength(0) - 1)
                        Console.Write("*");

                    if (rowSpaceOrChar.Count() > 0)
                    {
                        char last_char = rowSpaceOrChar.Last();
                        if (last_char == '*')
                            rowSpaceOrChar += " ";
                        else
                            rowSpaceOrChar += last_char;
                    }

                    Console.Write(rowSpaceOrChar);

                    if (rowIndex < matrix.GetLength(0) - 1)
                    {
                        Console.Write("*");
                        Console.WriteLine();
                    }
                }
            }
 
            PrintEndBorderBricks(wallBrick, lenghtBorder);

            Console.WriteLine();
        }

        static void PrintEndBorderBricks(char wallBrick, int rowLength)
        {
            Console.Write(wallBrick);

            while (true)
            {
                Console.Write(wallBrick);
                rowLength--;
                if (rowLength == 0)
                    Console.Write(wallBrick);
                if (rowLength == 0)
                    break;
            }

            Console.WriteLine();
        }
    }
}
