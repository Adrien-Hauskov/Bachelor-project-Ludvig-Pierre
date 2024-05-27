using System.Globalization;

namespace BioLab.Biometrics.Mcc.Sdk;

public class Matcher{
    //loads initial paramters for matching and enrolling
    //these improve accuracy when matching templates
    public void loadParams(){
        MccSdk.SetMccEnrollParameters(@"Biolab\Mcc\Sdk\MccPaperEnrollParameters.xml");
        MccSdk.SetMccMatchParameters(@"Biolab\Mcc\Sdk\MccPaperMatchParameters.xml");
    }

    public void matchTemplates(string fingerInput, string templateFiles)
    {
    var template1 = MccSdk.CreateMccTemplateFromTextTemplate(fingerInput);
    var template2 = MccSdk.CreateMccTemplateFromTextTemplate(templateFiles);
    var score = MccSdk.MatchMccTemplates(template1, template2);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
            "Match score: {0:R}",
            score));
    }
}