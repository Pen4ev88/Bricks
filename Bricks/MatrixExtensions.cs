using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks
{
    public static class MatrixExtensions
    {
        public static int[,] PutVerticalBrick(this int[,] matrix, int rowIndex, int colIndex, int brickSymbol)
        {
            matrix[rowIndex, colIndex] = brickSymbol;
            matrix[rowIndex + 1, colIndex] = brickSymbol;

            return matrix;
        }
      
        public static int[,] PutHorizontalBrick(this int[,] matrix, int rowIndex, int colIndex, int brickSymbol)
        {
            matrix[rowIndex, colIndex] = brickSymbol;
            matrix[rowIndex, colIndex + 1] = brickSymbol;

            return matrix;
        }
    }
}
