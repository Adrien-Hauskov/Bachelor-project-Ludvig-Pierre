using System;
using System.IO;
using System.Linq;
using System.Globalization;
using BioLab.Biometrics.Mcc.Sdk;


const string Employees = "Employees";
templateCreation tp = new templateCreation();
Matcher mr = new Matcher();

bool running = true;
while (running)
{
    Console.WriteLine("Enter ID:");
    string employeeId = Console.ReadLine();
    string employeeDirectory = Path.Combine(Employees, employeeId);
    Directory.CreateDirectory(Path.Combine(employeeDirectory));

    // Create the template and get the combined matrix
    double[][] combinedMatrix = tp.CreateTemplate(employeeId);

    if (Directory.Exists(employeeDirectory))
    {
        var templateFiles = Directory.GetFiles(employeeDirectory, "*.txt");
        if (templateFiles.Length > 0)
        {

            // Save the template to temp file for matching
            string tempFilePath = tp.SaveTemplateToTempFile(combinedMatrix);

            string templateFile = templateFiles[0];
            mr.matchTemplates(tempFilePath, templateFile);
            /*clean up temp file after match for security
            *ideally, this would be encrypted like much other stuff here
            *I ran out of time so it will be for a future iteration once a real fingerprint scanner is implemented
            */

            File.Delete(tempFilePath);

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
    if (employeeId.ToLower() == "quit")
    {
        Console.WriteLine("Quitting");
        running = false;
    }
}
