using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public static class MatrixMultiplier // klasa statyczna bo nie tworzymy jej obiektów
    {
        // mnozenie macierzy A i B przy pomocy Parallel.for
        public static Matrix MultiplyParallel(Matrix A, Matrix B, int threads)
        {
            // walidacja wymiarow (niepotrzebna)
            if (A.Cols != B.Rows)
                throw new ArgumentException("Invalid matrix dimensions");

            int size = A.Rows;
            var result = new Matrix(size, size); // tworzenie pustej macierzy wynikowej

            var options = new ParallelOptions { MaxDegreeOfParallelism = threads };

            // wykonuje kazda iteracje i w osobnym watku, dla kazdego i przelatujemy po kolumnach j i wewnetrznie po k
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

            // zwracamy przemnozonej macierzy wynikowej
            return result;
        }

        // sekwencyjne mnozenie macierzy - dodane dla porownania wydajnosci mnozenia przy uzyciu 1 watku
        // i - iteruje po wierszach A, j - iteruje po kolumnach B, k - przemieszcza sie wzdluz danego wiersza A i kolumny B
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
