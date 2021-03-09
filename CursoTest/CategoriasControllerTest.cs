using CursoMVC.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CursoAPI.Controllers;
using System.Threading.Tasks;
using Xunit;
using System.Threading;

namespace CursoTest
{
    public class CategoriasControllerTest
    {
        //Moq do context
        private readonly Mock<DbSet<Categoria>> _mockSet; //Informa que é um objeot de moq
        private readonly Mock<Context> _mockContext;
        private readonly Categoria _categoria;

        public CategoriasControllerTest()
        {
            //Esses teste são para ver se eles foram executados um de cada vez cada 1
            //Inicializando minhas variaveis
            _mockSet = new Mock<DbSet<Categoria>>();
            _mockContext = new Mock<Context>();
            _categoria = new Categoria { Id = 1, Descricao = "Teste categoria" };

            
            //Estou mostrando o que o FindAsync deve fazer
            _mockContext.Setup(m => m.Categorias).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.Categorias.FindAsync(1)).ReturnsAsync(_categoria);

            _mockContext.Setup(m => m.SetModified(_categoria)); //Ele vai setar o metodo categoria modificado, se não, não é possivel realizar o PUT
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));

        }
        [Fact] //Xunit

        public async Task Get_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object); //Feito isso eu vou ter todos os metodos da minha API Controller categorias

            //Verifiamos se o findAsync foi executado uma vez só
            await service.GetCategoria(id:1); //Dentro do parametro utilizar a controller da API,  vou dar um get passando o ID =1
            _mockSet.Verify(expression: m => m.FindAsync(1), Times.Once()); //Vou verificar se ele executou meu FindAsync

        }

        [Fact]
        public async Task Put_Categoria()
        {
            //Esses teste são para ver se eles foram executados um de cada vez cada 1
            var service = new CategoriasController(_mockContext.Object);

            await service.PutCategoria(1, _categoria);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);// Vai testar se vai executar o saveChanges
        }

        [Fact]
        public async Task Post_Categoria()
        {
            //Esses teste são para ver se eles foram executados um de cada vez cada 1
            var service = new CategoriasController(_mockContext.Object);

            await service.PostCategoria(_categoria);

            _mockSet.Verify(m => m.Add(_categoria), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            await service.DeleteCategoria(1);
            //Esses teste são para ver se eles foram executados um de cada vez cada 1
            _mockSet.Verify(m => m.FindAsync(1), Times.Once);
            _mockSet.Verify(m => m.Remove(_categoria), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

   