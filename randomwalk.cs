using System;
using System.Collections.Generic;

public class RandomWalkAlgorithm
{
    private static readonly Random random = new Random();

    public void ApplyAlgorithm(List<(double X, double Y, double Theta)> data)
    {
        foreach (var item in data)
        {

            int[] directionsX = { -1, 0, 1, 0 };
            int[] directionsY = { 0, 1, 0, -1 };


            int directionIndex = random.Next(directionsX.Length);
            double newX = item.X + directionsX[directionIndex];
            double newY = item.Y + directionsY[directionIndex];


            Console.WriteLine($"New X: {newX}, New Y: {newY}, New Theta: {item.Theta}");
        }
    }
}