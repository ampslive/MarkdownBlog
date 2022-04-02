using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Contracts
{
    public interface IPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Permalink { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public PostStatus Status { get; set; }
        public bool IsActive { get; set; }
    }
}
