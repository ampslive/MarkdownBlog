using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Contracts
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity, string? partitionKey = null);
        Task<T> Get(string id, string? partitionKey = null);
        Task<List<T>> Search(string inputQuery, string[] parameters);
        //void Delete(object id);
        //T Update(object id);
    }
}
