using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab3
{
    public static class MatrixMultiplierThread
    {
        private static readonly object locker = new object();

        public static Matrix MultiplyWithThreads(Matrix A, Matrix B, int threadCount)
        {
            if (A.Cols != B.Rows)
                throw new ArgumentException("Invalid matrix dimensions");

            int size = A.Rows;
            var result = new Matrix(size, size);
            Thread[] threads = new Thread[threadCount];

            int rowsPerThread = size / threadCount;
            int remainingRows = size % threadCount;

            int currentRow = 0;
            for (int t = 0; t < threadCount; t++)
            {
                int startRow = currentRow;
                int endRow = startRow + rowsPerThread + (t < remainingRows ? 1 : 0);
                currentRow = endRow;

                threads[t] = new Thread(() =>
                {
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            int sum = 0;
                            for (int k = 0; k < size; k++)
                                sum += A[i, k] * B[k, j];

                            // Synchronizacja dostępu do współdzielonej macierzy wynikowej
                            lock (locker)
                            {
                                result[i, j] = sum;
                            }
                        }
                    }
                });
            }

            foreach (var thread in threads)
                thread.Start();
            foreach (var thread in threads)
                thread.Join();

            return result;
        }
    }
}