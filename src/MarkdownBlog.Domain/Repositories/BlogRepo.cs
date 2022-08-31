using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Repositories
{
    public class BlogRepo : BaseRepo<Blog>
    {
        public override string TableName { get; set; } = "blog";

        private readonly IDatabaseHelper dbHelper;

        public BlogRepo(IDatabaseHelper helper) : base(helper)
        {
        }

        public Task<List<Blog>> GetBlogByTitle(string title)
        {
            string query = @"SELECT * FROM c where c.name Like @p0";

            string[] parameters = { $"{title}%" };

            return this.Search(query, parameters);
        }
    }
}
