using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Store
{
    public class BlogStore
    {
        private readonly BlogRepo blogRepo;
        public BlogStore(BlogRepo repo)
        {
            blogRepo = repo;
        }

        public async Task<Blog> Add(Blog blog)
        {
            return await blogRepo.Create(blog);
        }

        public async Task<Blog> Get(string id)
        {
            return await blogRepo.Get(id);
        }

        public async Task<List<Blog>> GetBlogByTitle(string text)
        {
            return await blogRepo.GetBlogByTitle(text);
        }
    }
}
