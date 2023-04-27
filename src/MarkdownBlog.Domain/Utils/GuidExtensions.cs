namespace MarkdownBlog.Domain.Utils;

public static class GuidExtensions
{
    public static string ToShortString(this Guid guid)
    {
        string base64Guid = Convert.ToBase64String(guid.ToByteArray());
        base64Guid = base64Guid.Replace('+', '-').Replace('/', '_');
        base64Guid = base64Guid.Substring(0, base64Guid.Length - 2);
        return base64Guid; 
    }
}
