using System;
using System.IO;
using System.Linq;
using System.Globalization;
using BioLab.Biometrics.Mcc.Sdk;

namespace BioLab.Biometrics.Mcc.Sdk;
class Program
{

    static void Main()
    {
        const string Employees = "Employees";
        MccSdk.SetMccEnrollParameters(@"Biolab\Mcc\Sdk\MccPaperEnrollParameters.xml");
        MccSdk.SetMccMatchParameters(@"Biolab\Mcc\Sdk\MccPaperMatchParameters.xml");
        var template1 = MccSdk.LoadMccTemplateFromTextFile(@"SampleMinutiae\1_1.txt");
        templateCreation tp = new templateCreation();
        bool running = true;
        //var template1 = @"SampleMinutiae\1_1.txt";
        while (running)
        {
            Console.WriteLine("Enter ID:");
            string employeeId = Console.ReadLine();
            string employeeDirectory = Path.Combine(Employees, employeeId);

            if (Directory.Exists(employeeDirectory))
            {
                string[] templateFiles = Directory.GetFiles(employeeDirectory, "*.txt");
                if (templateFiles.Length > 0)
                {   
                    var template2 = MccSdk.LoadMccTemplateFromTextFile(@"Employees\1\1_template.txt"); 
                    Console.WriteLine("Here we run the matcher to see if the templates match");
                    var score = MccSdk.MatchMccTemplates(template1, template2);
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "Match score: {0:R}",
                    score));


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
