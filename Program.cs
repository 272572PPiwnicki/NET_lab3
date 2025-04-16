using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 1000; // testy wykonano dla 250, 500, 750, 1000
            int[] threadCounts = { 1, 2, 4, 8 }; // liczba watkow, ograniczona przez zasoby procesora

            // tworzenie losowych macierzy o rozmiarze size
            var A = Matrix.GenerateRandom(size);
            var B = Matrix.GenerateRandom(size);

            Console.WriteLine("Testing on matrix size: " + size);

            // sequential
            var swSeq = Stopwatch.StartNew(); // uruchomienie stopera
            var resultSeq = MatrixMultiplier.MultiplySequential(A, B); // wywolanie metody mnozenia macierzy
            swSeq.Stop(); // zatrzymanie stopera
            Console.WriteLine($"Sequential: {swSeq.ElapsedMilliseconds} ms");

            // parallel
            foreach (var threads in threadCounts) // iteracja po liczbie watkow
            {
                var swPar = Stopwatch.StartNew();
                var resultPar = MatrixMultiplier.MultiplyParallel(A, B, threads);
                swPar.Stop();
                Console.WriteLine($"{threads} threads (Parallel): {swPar.ElapsedMilliseconds} ms");
            }

            // thread
            foreach (var threads in threadCounts)
            {
                var swThread = Stopwatch.StartNew();
                var resultThread = MatrixMultiplierThread.MultiplyWithThreads(A, B, threads);
                swThread.Stop();
                Console.WriteLine($"{threads} threads (Thread): {swThread.ElapsedMilliseconds} ms");
            }
        }
    }
}
