using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : ClaseBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        
        Task<T> GetByIdWithSpec(ISpecification<T> spec);

        // Lista de Elementos, se especifican que relaciones y que condiciones se realizan a la consulta
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
    }
}
