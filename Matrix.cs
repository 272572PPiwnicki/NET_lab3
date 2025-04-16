using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Matrix
    {
        public int[,] Data { get; }

        public int Rows => Data.GetLength(0);
        public int Cols => Data.GetLength(1);

        public Matrix(int rows, int cols)
        {
            Data = new int[rows, cols];
        }

        public static Matrix GenerateRandom(int size)
        {
            var rand = new Random();
            var matrix = new Matrix(size, size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix.Data[i, j] = rand.Next(1, 100);
            return matrix;
        }

        public int this[int row, int col]
        {
            get => Data[row, col];
            set => Data[row, col] = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    sb.Append(Data[i, j] + "\t");
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}
