﻿namespace MarkdownBlog.API.Models;

public class AuthorRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ImageUri { get; set; }
    public string Bio { get; set; }
}