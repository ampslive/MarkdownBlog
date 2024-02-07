namespace MarkdownBlog.Domain.Contracts;

public enum PostStatus
{
    Draft = 0,
    Preview = 1,
    Published = 2,
    Archive = 3,
}

public enum ContentType
{
    EmbTxt,
    LocalMD,
    ExtMD
}

public enum SocialMediaProvider
{
    Twitter = 0,
    LinkedIn = 1,
    YouTube = 2,
    Threads = 3,
    Mastodon = 4,
    Facebook = 5,
    Email = 6
}
