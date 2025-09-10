using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        private SQLiteDatabaseHelper _dbHelper;
        private int _produtoId;

        public EditarProduto(int produtoId)
        {
            InitializeComponent();
            _produtoId = produtoId;

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "produtos.db3");
            _dbHelper = new SQLiteDatabaseHelper(dbPath);

            CarregarProduto();
        }

        private void CarregarProduto()
        {
            var produto = _dbHelper.GetProduto(_produtoId);

            if (produto != null)
            {
                DescricaoEntry.Text = produto.Descricao;
                QuantidadeEntry.Text = produto.Quantidade.ToString();
                PrecoEntry.Text = produto.Preco.ToString();
            }
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
                Id = _produtoId,
                Descricao = DescricaoEntry.Text,
                Quantidade = double.TryParse(QuantidadeEntry.Text, out var quantidade) ? quantidade : 0,
                Preco = double.TryParse(PrecoEntry.Text, out var preco) ? preco : 0
            };

            _dbHelper.SaveProduto(produto);
            await Navigation.PopAsync();
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            var confirmar = await DisplayAlert("Confirmar", "Deseja excluir este produto?", "Sim", "Não");

            if (confirmar)
            {
                _dbHelper.DeleteProduto(_produtoId);
                await Navigation.PopAsync();
            }
        }
    }
}