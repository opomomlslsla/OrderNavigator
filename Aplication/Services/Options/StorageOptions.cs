using System.Runtime;
namespace Aplication.Services.Options;

public class StorageOptions
{
    public bool UseDatabase { get; set; }
    public bool UseFile { get; set; }
    public string FilePath { get; set; }

    private string IsValidPath(string path, bool allowRelativePaths = false)
    {
        bool isValid = true;
        string fullPath = Path.GetFullPath(path);
        string root = Path.GetPathRoot(path);
        isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
        return isValid ? path : throw new ArgumentException("ResultFilePath is incorrect");
    }
}
