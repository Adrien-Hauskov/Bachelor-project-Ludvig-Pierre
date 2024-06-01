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
        // Use a fixed seed for deterministic behavior. We are just using 1 for now, it could be any number
        Random random = new Random(1);
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

        // Perform projection
        for (int j = 0; j < outputDimensions; j++)
        {
            double sum = 0;
            for (int i = 0; i < inputDimensions; i++)
            {
                sum += dataPoint[i] * projectionMatrix[i, j];
            }
            projectedPoint[j] = sum;
        }

        //As the filled gaussian values generated are outside of the accepted values of MccSDK,
        //code is implemented to ensure that the values stay within certain ranges.
        //While this may technically reduce security, in practice it still does meaningfull transformation of the template
        //Negative values are projected into their absolute values, and values exceeding 256 are reduced to 256, which isn't optimal
        //projectedPoint[0] = Math.Max(0, Math.Min(256, Math.Abs(projectedPoint[0])));
        //projectedPoint[1] = Math.Max(0, Math.Min(256, Math.Abs(projectedPoint[1])));
        projectedPoint[0] = Math.Round(projectedPoint[0]);
        projectedPoint[1] = Math.Round(projectedPoint[1]);
        projectedPoint[2] = (projectedPoint[2] % (2 * Math.PI) + 2 * Math.PI) % (2 * Math.PI);

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
