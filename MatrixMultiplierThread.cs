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
        // synchronizacja dostepu do danych
        // obiekt blokujacy uzywany do zabezpieczenia dostepu do wspoldzielonej macierzy wynikowej
        private static readonly object locker = new object();

        public static Matrix MultiplyWithThreads(Matrix A, Matrix B, int threadCount)
        {
            if (A.Cols != B.Rows)
                throw new ArgumentException("Invalid matrix dimensions");

            int size = A.Rows;
            var result = new Matrix(size, size); // tworzymy macierz wynikowa result
            Thread[] threads = new Thread[threadCount]; // tablica do ktorej bedziemy wrzuac wszystkie watki

            // podzial wierszy miedzy watki
            int rowsPerThread = size / threadCount;
            int remainingRows = size % threadCount;

            // wyznaczamy zakres wierszy dla kazdego watku
            int currentRow = 0;
            for (int t = 0; t < threadCount; t++)
            {
                int startRow = currentRow;
                int endRow = startRow + rowsPerThread + (t < remainingRows ? 1 : 0);
                currentRow = endRow;

                // tworzymy watki
                threads[t] = new Thread(() =>
                {
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            int sum = 0;
                            for (int k = 0; k < size; k++)
                                sum += A[i, k] * B[k, j];

                            // synchronizacja dostepu do macierzy wynikowej
                            lock (locker)
                            {
                                result[i, j] = sum;
                            }
                        }
                    }
                });
            }

            foreach (var thread in threads)
                thread.Start(); // uruchamiamy watki
            foreach (var thread in threads)
                thread.Join(); // czekamy na zakonczenie wszystkich watkow

            return result;
        }
    }
}