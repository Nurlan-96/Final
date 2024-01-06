using CompanyApplication.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.DataContext.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T View(Predicate<T> filter);
        List<T> ViewAll(Predicate<T> filter = null);
    }
}
