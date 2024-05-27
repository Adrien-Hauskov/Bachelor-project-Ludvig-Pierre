using System;
using System.IO;
using System.Linq;
using System.Globalization;

public class templateCreation
{
    public double[][] CreateTemplate(string employeeId)
    {
        /**
        *Ideally, this would mostly be replaced by the input from a fingerprint scanner, which would call the enrollment methods
        *From the MccSDK. The process below is for simulation a person scanner their finger. These are sample Minutiaes, fingerprints already scanned
        *and ready to be enrolled and matched. They are also used for transformation. 
        **/
        string filePath = @"SampleMinutiae\1_2.txt";
        string[] lines = File.ReadAllLines(filePath);

        string[] skippedLines = lines.Take(4).ToArray();

        double[][] matrix = lines.Skip(4).Select(line =>
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Select(part => double.Parse(part, CultureInfo.InvariantCulture)).ToArray();
        }).ToArray();

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

        return combinedMatrix;
    }

    // Method for saving the template to a file
    public void SaveTemplateToFile(string employeeId, double[][] combinedMatrix)
    {
        string employeeDirectory = "Employees";
        string filePath = Path.Combine(employeeDirectory, employeeId, $"{employeeId}_template.txt");

        // Create the employee directory if it doesn't exist
        if (!Directory.Exists(employeeDirectory))
        {
            Directory.CreateDirectory(employeeDirectory);
        }

        // Write the combined matrix to the template file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var row in combinedMatrix)
            {
                writer.WriteLine(string.Join(" ", row.Select(d => d.ToString(CultureInfo.InvariantCulture))));
            }
        }
    }

    // Method for saving the template to a temporary file
    public string SaveTemplateToTempFile(double[][] combinedMatrix)
    {
        // Create a temporary file
        string tempFilePath = Path.GetTempFileName();

        // Write the combined matrix to the temporary file
        using (StreamWriter writer = new StreamWriter(tempFilePath))
        {
            foreach (var row in combinedMatrix)
            {
                writer.WriteLine(string.Join(" ", row.Select(d => d.ToString(CultureInfo.InvariantCulture))));
            }
        }

        return tempFilePath;
    }
}
