#if UNITY_IOS
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

public class CalendarEventPostprocessBuild : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport Report)
    {
        var PlistPath = Report.summary.outputPath + "/Info.plist";
        var Plist = new PlistDocument();
        Plist.ReadFromString(File.ReadAllText(PlistPath));

        var PlistRoot = Plist.root;
        PlistRoot.SetString("NSCalendarsUsageDescription", "Add to calendar");

        File.WriteAllText(PlistPath, Plist.WriteToString());
    }
}
#endif