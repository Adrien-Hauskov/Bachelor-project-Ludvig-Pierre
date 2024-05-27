using System;
using System.IO;
using System.Linq;
using System.Globalization;
using BioLab.Biometrics.Mcc.Sdk;

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
    

    if (Directory.Exists(employeeDirectory))
    {
        var templateFiles = Directory.GetFiles(employeeDirectory, "*.txt");
        if (templateFiles.Length > 0)
        {
            
            Console.WriteLine("Here we run the matcher to see if the templates match");
            mr.matchTemplates(fingerInput, templateFiles.ToString());
            Console.WriteLine(templateFiles);


        }
        else
        {
            Console.WriteLine("No template found, creating a new fingerprint template...");
            // Create the template and get the combined matrix
            double[][] combinedMatrix = tp.CreateTemplate(employeeId);

            // Save the combined matrix to a file
            tp.SaveTemplateToFile(employeeId, combinedMatrix);

        }
    }
    else
    {
        Console.WriteLine("ID does not match employee");
        Console.WriteLine("Make sure correct ID is entered or contact administrator");
    }
}
