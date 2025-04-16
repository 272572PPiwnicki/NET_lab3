using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public static class MatrixMultiplier
    {
        public static Matrix MultiplyParallel(Matrix A, Matrix B, int threads)
        {
            if (A.Cols != B.Rows)
                throw new ArgumentException("Invalid matrix dimensions");

            int size = A.Rows;
            var result = new Matrix(size, size);

            var options = new ParallelOptions { MaxDegreeOfParallelism = threads };

            Parallel.For(0, size, options, i =>
            {
                for (int j = 0; j < size; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < size; k++)
                        sum += A[i, k] * B[k, j];
                    result[i, j] = sum;
                }
            });

            return result;
        }

        public static Matrix MultiplySequential(Matrix A, Matrix B)
        {
            if (A.Cols != B.Rows)
                throw new ArgumentException("Invalid matrix dimensions");

            int size = A.Rows;
            var result = new Matrix(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < size; k++)
                        sum += A[i, k] * B[k, j];
                    result[i, j] = sum;
                }
            }

            return result;
        }

    }
}
