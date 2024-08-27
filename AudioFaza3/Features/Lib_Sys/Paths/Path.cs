using System.Reflection;

namespace Streamstar.U;

public static class Path
{
    public static string ExeDir()
    {
        return System.IO.Path.GetFullPath(Assembly.GetExecutingAssembly().Location);
    }

    public static string Project()
    {
        return System.IO.Path.GetFullPath(System.IO.Path.Combine(ExeDir(), @"..\..\..\..\"));
    }

    public static string Assets()
    {
        return System.IO.Path.Combine(Project(), @"_assets");
    }

    public static string Full(string relative)
    {
        return System.IO.Path.Combine(Project(), relative);
    }
}