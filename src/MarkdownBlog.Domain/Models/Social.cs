using MarkdownBlog.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Models;

public class Social
{
    public SocialMediaProvider Provider { get; set; }
    public string Uri { get; set; }
}
