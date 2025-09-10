using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.IO;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        private SQLiteDatabaseHelper _dbHelper;

        public NovoProduto()
        {
            InitializeComponent();

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "produtos.db3");
            _dbHelper = new SQLiteDatabaseHelper(dbPath);
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescricaoEntry.Text))
            {
                await DisplayAlert("Erro", "Digite uma descrição", "OK");
                return;
            }

            var produto = new Produto
            {
                Descricao = DescricaoEntry.Text,
                Quantidade = double.TryParse(QuantidadeEntry.Text, out var quantidade) ? quantidade : 0,
                Preco = double.TryParse(PrecoEntry.Text, out var preco) ? preco : 0
            };

            _dbHelper.SaveProduto(produto);
            await Navigation.PopAsync();
        }
    }
}