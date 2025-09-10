using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.IO;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        private SQLiteDatabaseHelper _dbHelper;

        public ListaProduto()
        {
            InitializeComponent();

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "produtos.db3");
            _dbHelper = new SQLiteDatabaseHelper(dbPath);
            CarregarProdutos();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            ProdutosListView.ItemsSource = _dbHelper.GetAllProdutos();
        }

        private async void OnAdicionarProdutoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        private async void OnProdutoTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Produto produto)
            {
                await Navigation.PushAsync(new EditarProduto(produto.Id));
                ProdutosListView.SelectedItem = null;
            }
        }
    }
}