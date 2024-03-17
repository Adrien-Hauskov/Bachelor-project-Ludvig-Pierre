using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = @"Sampleminutiae/1_1.txt";
        string[] lines = File.ReadAllLines(filePath);

        // Assuming the matrix is of type double
        double[][] matrix = lines.Skip(4).Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(double.Parse)
                                                      .ToArray())
                                 .ToArray();


        foreach (var row in matrix)
        {
            Console.WriteLine(string.Join(" ", row));
        }
        Console.WriteLine("modified");
        RandomWalkAlgorithm rw = new RandomWalkAlgorithm();
        rw.ApplyAlgorithm(matrix);

        foreach (var row in matrix)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }
}