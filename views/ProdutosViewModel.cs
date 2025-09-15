using BuscaProdutos.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BuscaProdutos.Models;

namespace BuscaProdutos.ViewModels
{
    public class ProdutosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Produto> ProdutosOriginais { get; set; }
        public ObservableCollection<Produto> ProdutosFiltrados { get; set; }

        public ProdutosViewModel()
        {
            // Inicializando a lista de produtos
            ProdutosOriginais = new ObservableCollection<Produto>
            {
                new Produto { Nome = "Camiseta" },
                new Produto { Nome = "Calça" },
                new Produto { Nome = "Tênis" },
                new Produto { Nome = "Boné" },
                new Produto { Nome = "Jaqueta" },
            };

            // Inicializando a coleção filtrada (começa com todos os produtos)
            ProdutosFiltrados = new ObservableCollection<Produto>(ProdutosOriginais);
        }

        // Método para filtrar os produtos com base no termo digitado no SearchBar
        public void FiltrarProdutos(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                ProdutosFiltrados.Clear();
                foreach (var produto in ProdutosOriginais)
                    ProdutosFiltrados.Add(produto);
            }
            else
            {
                var resultado = ProdutosOriginais
                    .Where(p => p.Nome.ToLower().Contains(termo.ToLower()))
                    .ToList();

                ProdutosFiltrados.Clear();
                foreach (var produto in resultado)
                    ProdutosFiltrados.Add(produto);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nome = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
