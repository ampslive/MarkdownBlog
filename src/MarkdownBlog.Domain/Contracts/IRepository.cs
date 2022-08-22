using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Contracts
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Get(string id);
        //void Delete(object id);
        //T Update(object id);
    }
}
