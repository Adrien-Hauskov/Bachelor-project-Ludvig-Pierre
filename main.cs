using System;
using System.IO;
using System.Linq;
using System.Globalization;

class Program
{
    static void Main()
    {
        string filePath = @"SampleMinutiae\1_1.txt";
        string[] lines = File.ReadAllLines(filePath);


        string[] skippedLines = lines.Take(4).ToArray();

        double[][] matrix = lines.Skip(4).Select(line =>
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Select(part => double.Parse(part, CultureInfo.InvariantCulture)).ToArray();
        }).ToArray();

        Console.WriteLine("Original Matrix:");
        PrintMatrix(matrix);

        // Define input and output dimensions for random projection, we choose the output dimension of 3, to keep the template format valid
        int inputDimensions = matrix[0].Length;
        int outputDimensions = 3;


        RandomProjection rp = new RandomProjection(inputDimensions, outputDimensions);

        // Project each row of the matrix using the random projection algorithm
        for (int i = 0; i < matrix.Length; i++)
        {
            matrix[i] = rp.Project(matrix[i]);
        }

        // Combine the skipped lines with the projected matrix for MccSDK to be able to read it
        double[][] combinedMatrix = new double[skippedLines.Length + matrix.Length][];
        for (int i = 0; i < skippedLines.Length; i++)
        {
            combinedMatrix[i] = new double[] { double.Parse(skippedLines[i], CultureInfo.InvariantCulture) };
        }
        for (int i = 0; i < matrix.Length; i++)
        {
            combinedMatrix[skippedLines.Length + i] = matrix[i];
        }

        Console.WriteLine("\nCombined Matrix:");
        PrintMatrix(combinedMatrix);
    }

    static void PrintMatrix(double[][] matrix)
    {
        foreach (var row in matrix)
        {
            Console.WriteLine(string.Join(" ", row.Select(d => d.ToString(CultureInfo.InvariantCulture))));
        }
    }
}
