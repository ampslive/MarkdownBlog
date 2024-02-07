using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Utils;

public static class ExtensionMethods
{
    public static Post UpdatePostStatus(this Post post, PostStatus targetPostStatus)
    {
        var postStatusGrid = new Dictionary<PostStatus, PostStatus[]>();

        postStatusGrid.Add(PostStatus.Draft, new PostStatus[] { PostStatus.Preview });
        postStatusGrid.Add(PostStatus.Preview, new PostStatus[] { PostStatus.Draft, PostStatus.Published });
        postStatusGrid.Add(PostStatus.Published, new PostStatus[] { PostStatus.Preview, PostStatus.Archive });
        postStatusGrid.Add(PostStatus.Archive, new PostStatus[] { PostStatus.Draft, PostStatus.Preview, PostStatus.Published });

        if (postStatusGrid[post.Status].Contains(targetPostStatus))
            post.Status = targetPostStatus;

        if (targetPostStatus == PostStatus.Published)
            post.DatePublished = DateTime.UtcNow;
        else
            post.DatePublished = null;

        return post;
    }
}
