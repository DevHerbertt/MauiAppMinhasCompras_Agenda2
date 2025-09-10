using SQLite;
using System.IO;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        private readonly SQLiteConnection _connection;

        public SQLiteDatabaseHelper(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Produto>(); // Cria a tabela Produto
        }

        // Obter todos os produtos
        public List<Produto> GetAllProdutos()
        {
            return _connection.Table<Produto>().ToList();
        }

        // Obter um produto específico pelo Id
        public Produto GetProduto(int id)
        {
            return _connection.Table<Produto>().FirstOrDefault(p => p.Id == id);
        }

        // Salvar um novo produto ou atualizar se já existir
        public int SaveProduto(Produto produto)
        {
            if (produto.Id != 0)
                return _connection.Update(produto); // Atualiza se o Id não for zero
            else
                return _connection.Insert(produto); // Insere um novo produto
        }

        // Deletar um produto pelo Id
        public int DeleteProduto(int id)
        {
            return _connection.Delete<Produto>(id); // Deleta o produto com o Id especificado
        }
    }
}
