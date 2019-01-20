using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    //Funcionalidade #1.9 - Existe um novo repositório para categoria, no caminho \Repositories\CategoriaRepository.cs?

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<IList<Categoria>> GetCategorias()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IList<Categoria>> GetCategorias(IList<Produto> produtos)
        {
            var categorias = await GetCategorias();
            foreach (var categoria in dbSet)
            {
                if (!produtos.Where(p => p.Categoria.Id == categoria.Id).Any())
                {
                    categorias.Remove(categoria);
                }
            }
            return categorias.ToList();
        }

        public async Task <Categoria> GetCategoria(string nome)
        {
            return await dbSet.Where(c => c.Nome == nome).SingleOrDefaultAsync();
        }

        //Funcionalidade #1.10 - A classe CategoriaRepository possui um método para salvar categoria?
        //Funcionalidade #1.14 - Os métodos de acesso a dados são assíncronos?
        public async Task SaveCategoria(string nome)
        {
            if (!dbSet.Where(p => p.Nome == nome).Any())
            {
                dbSet.Add(new Categoria(nome));
            }
            await contexto.SaveChangesAsync();
        }
    }
}
