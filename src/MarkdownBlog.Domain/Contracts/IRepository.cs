using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Contracts
{
    public interface IRepository<T>
    {
        T Create(T entity);
        //void Delete(object id);
        //T Update(object id);
    }
}
