using System;
using System.IO;
using System.Linq;
using System.Globalization;
using BioLab.Biometrics.Mcc.Sdk;

class Program
{
    static void Main()
    {
        const string Employees = "Employees";
        templateCreation tp = new templateCreation();
        Matcher mr = new Matcher();
        string fingerInput = @"SampleMinutiae\1_1.txt";
        bool running = true;
        mr.loadParams();

        while (running)
        {
            Console.WriteLine("Enter ID:");
            string employeeId = Console.ReadLine();
            string employeeDirectory = Path.Combine(Employees, employeeId);

            // Create the template and get the combined matrix
            double[][] combinedMatrix = tp.CreateTemplate(employeeId);

            if (Directory.Exists(employeeDirectory))
            {
                var templateFiles = Directory.GetFiles(employeeDirectory, "*.txt");
                if (templateFiles.Length > 0)
                {

                    // Save the combined matrix to a temporary file
                    string tempFilePath = tp.SaveTemplateToTempFile(combinedMatrix);

                    // Assuming there's only one template file per employee
                    string templateFile = templateFiles[0];

                    // Pass the file path to the matchTemplates method
                    mr.matchTemplates(tempFilePath, templateFile);

                    // Clean up the temporary file after matching
                    File.Delete(tempFilePath);

                }
                else if (employeeId.ToLower() == "quit")
                {
                    Console.WriteLine("quitting");
                    running = false;
                }
                else
                {

                    // Save the combined matrix to a file
                    tp.SaveTemplateToFile(employeeId, combinedMatrix);

                    Console.WriteLine("New template created, enter id again and rescan finger");


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