using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMVC.Models
{
    public class Context : DbContext
    {
        //Essa classe herda de uma classe Dbcontext

        public DbSet<Categoria> Categorias { get; set; } //Nesse caso estou adicionando uma tabela de categorias

        public DbSet<Produto> Produtos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Esse metodo vai configurar o EF, dentro dele que falo qual o banco de dados utilizar

            optionsBuilder.UseSqlServer(connectionString: @"Server=ALLAN-DEVELOPME\SQLEXPRESS;Initial Catalog=CursoMVC;Integrated Security=True");//Aqui aonde vai a string de conexão
        }

    }
}
