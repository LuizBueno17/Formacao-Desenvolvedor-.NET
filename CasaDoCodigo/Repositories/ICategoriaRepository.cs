using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IList<Categoria>> GetCategorias();
        Task<IList<Categoria>> GetCategorias(IList<Produto> produtos);
        Task SaveCategoria(string nome);
        Task<Categoria> GetCategoria(string nome);
    }
}