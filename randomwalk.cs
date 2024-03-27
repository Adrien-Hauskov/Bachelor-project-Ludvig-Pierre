using System;
using System.Collections.Generic;

public class RandomWalkAlgorithm
{
    private static readonly Random random = new Random();

    public void ApplyAlgorithm(double[][] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                int[] directionsX = { -1, 0, 1, 0 };
                int[] directionsY = { 0, 1, 0, -1 };

                int directionIndex = random.Next(directionsX.Length);
                data[i][j] += directionsX[directionIndex];
            }
        }
    }
}