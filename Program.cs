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
            int size = 1000;
            int[] threadCounts = { 1, 2, 4, 8 };

            var A = Matrix.GenerateRandom(size);
            var B = Matrix.GenerateRandom(size);

            Console.WriteLine("Testing on matrix size: " + size);

            // Sekwencyjnie
            var swSeq = Stopwatch.StartNew();
            var resultSeq = MatrixMultiplier.MultiplySequential(A, B);
            swSeq.Stop();
            Console.WriteLine($"Sequential: {swSeq.ElapsedMilliseconds} ms");

            foreach (var threads in threadCounts)
            {
                var swPar = Stopwatch.StartNew();
                var resultPar = MatrixMultiplier.MultiplyParallel(A, B, threads);
                swPar.Stop();
                Console.WriteLine($"{threads} threads (Parallel): {swPar.ElapsedMilliseconds} ms");
            }

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
