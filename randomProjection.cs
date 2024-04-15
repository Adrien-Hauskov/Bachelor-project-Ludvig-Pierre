using System;
using System.Linq;

public class RandomProjection
{
    private readonly int inputDimensions;
    private readonly int outputDimensions;
    private readonly double[,] projectionMatrix;

    public RandomProjection(int inputDimensions, int outputDimensions)
    {
        this.inputDimensions = inputDimensions;
        this.outputDimensions = outputDimensions;
        this.projectionMatrix = GenerateProjectionMatrix();
    }
    
    //generates the matrix and fills it with gaussian values
    private double[,] GenerateProjectionMatrix()
    {
        Random random = new Random();
        double[,] matrix = new double[inputDimensions, outputDimensions];
        for (int i = 0; i < inputDimensions; i++)
        {
            for (int j = 0; j < outputDimensions; j++)
            {
                matrix[i, j] = random.NextGaussian();
            }
        }
        return matrix;
    }

    // Project a data point from inputDimensions to outputDimensions
    public double[] Project(double[] dataPoint)
    {
        if (dataPoint.Length != inputDimensions)
        {
            throw new ArgumentException("Invalid input dimensions");
        }

        double[] projectedPoint = new double[outputDimensions];

        for (int j = 0; j < outputDimensions; j++)
        {
            double sum = 0;
            for (int i = 0; i < inputDimensions; i++)
            {
                sum += dataPoint[i] * projectionMatrix[i, j];
            }
            projectedPoint[j] = sum;
        }

        return projectedPoint;
    }
}

public static class RandomExtensions
{
    public static double NextGaussian(this Random rand, double mean = 0, double stdDev = 1)
    {
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);
        double randNormal = mean + stdDev * randStdNormal;
        return randNormal;
    }
}
