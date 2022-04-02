using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Contracts
{
    public abstract class BaseRepo<T> : IRepository<T>
    {
        public virtual T Create(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
