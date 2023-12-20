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
