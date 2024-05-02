using System;
using System.IO;
using System.Linq;
using System.Globalization;

class Program
{

    static void Main()
    {
        const string Employees = "Employees";
        templateCreation tp = new templateCreation();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Enter ID:");
            string employeeId = Console.ReadLine();
            string employeeDirectory = Path.Combine(Employees, employeeId);

            if (Directory.Exists(employeeDirectory))
            {
                if (Directory.GetFiles(employeeDirectory, "*.txt").Length > 0)
                {
                    Console.WriteLine("Here we run the matcher to see if the templates match");
                    //TODO implement matching from MccSDK
                }
                else
                {
                    Console.WriteLine("No template found, creating a new fingerprint template...");
                    // Create the template and get the combined matrix
                    double[][] combinedMatrix = tp.CreateTemplate(employeeId);

                    // Save the combined matrix to a file
                    tp.SaveTemplateToFile(employeeId, combinedMatrix);

                    //TODO: Implement matching from MccSDK
                }
            }
            else
            {
                Console.WriteLine("ID does not match employee");
                Console.WriteLine("Make sure correct ID is entered or contact administrator");
            }
        }
    }
}
