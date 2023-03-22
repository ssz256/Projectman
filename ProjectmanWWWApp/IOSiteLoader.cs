using System.Text;

namespace ProjectmanWWWApp;

public static class IOSiteLoader
{
    public static byte[] LoadFile(string path, bool ignoreTemplateSystem = false)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("File not found");

        string file = File.ReadAllText(path);

        if (ignoreTemplateSystem) goto EscapeIgnoring;

        file = file.Replace("@user-header", File.ReadAllText("www/user-header.html"));

EscapeIgnoring:

        return Encoding.UTF8.GetBytes(file);
    }
}
