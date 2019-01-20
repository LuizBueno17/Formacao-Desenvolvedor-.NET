using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        //Funcionalidade #1.13 - A classe ProdutoRepository (\Repositories\ProdutoRepository.cs) possui um campo privado ICategoriaRepository?
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task <IList<Produto>> GetProdutos()
        {
            return await dbSet.ToListAsync();
        }

        //Funcionalidade #3.1 - A classe ProdutoRepository possui um overload do método GetProdutos, para aceitar uma string de pesquisa?
        public async Task<IList<Produto>> GetProdutos(string pesquisa)
        {
            if (string.IsNullOrEmpty(pesquisa))
            {
                return await GetProdutos();
            }
            //Funcionalidade #3.2 - Esse novo overload do método GetProdutos efetua uma consulta LINQ parametrizada com a string de pesquisa?
            //Funcionalidade #3.3 - Essa consulta retorna os produtos com nome ou categorias contendo a string de pesquisa?
            return await dbSet.Where(p => p.Nome.Contains(pesquisa) || p.Categoria.Nome.Contains(pesquisa)).ToListAsync();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    await categoriaRepository.SaveCategoria(livro.Categoria);
                    //Após a categoria ser criada ainda preciso usar um objeto do tipo Categoria no construtor do produto.
                    Categoria categoria = await categoriaRepository.GetCategoria(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
