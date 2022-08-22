using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Domain.Contracts;

public interface IDatabaseHelper
{
    Task<T> CreateAsync<T>(T obj, string tableName);
    void GetAsync<T>();
    void QueryAsync<T>();
}
