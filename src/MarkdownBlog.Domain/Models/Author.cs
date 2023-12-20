using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Author : BaseModel
{
    public Author()
    {
        
    }

    public Author(string name, string imageUri, string bio) : base()
    {
        Name = name;
        ImageUri = imageUri;
        Bio = bio;
    }

    public string Name { get; set; }
    public string ImageUri { get; set; }
    public string Bio { get; set; }
    public List<Social> Socials { get; set; }
}
