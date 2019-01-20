using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    //Funcionalidade #3.4 - Existe uma ViewModel para busca de produtos na pasta \Models\ViewModels do projeto?

    public class BuscaViewModel
    {
        public BuscaViewModel(IList<Produto> produtos, IList<Categoria> categorias)
        {
            Produtos = produtos;
            Categorias = categorias;
        }

        //Funcionalidade #3.5 - Essa ViewModel contém uma propriedade para o texto de pesquisa e outra para a lista de produtos a serem exibidos na view?
        public IList<Produto> Produtos { get; }
        public IList<Categoria> Categorias { get; }
        public string Pesquisa { get; set; }
    }
}