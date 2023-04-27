namespace MarkdownBlog.Domain.Contracts;

public enum PostStatus
{
    Draft = 0,
    Published = 1
}

public enum ContentType
{
    EmbTxt,
    LocalMD,
    ExtMD
}
