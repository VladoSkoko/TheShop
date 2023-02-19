using Moq;
using Shop.WebApi.Articles.Models;
using Shop.WebApi.Suppliers.Managers;
using Shop.WebApi.Suppliers.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Test.Suppliers.Managers
{
    public class SupplierManagerTest : IDisposable
    {
        private readonly Mock<ICachedSupplierService> cachedSupplierService;
        private readonly Mock<ISupplierService> supplier1;
        private readonly Mock<ISupplierService> supplier2;

        public SupplierManagerTest()
        {
            this.cachedSupplierService = new Mock<ICachedSupplierService>();
            this.supplier1 = new Mock<ISupplierService>();
            this.supplier2 = new Mock<ISupplierService>();
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public async void GetArticleAsync_FromCachedSupplier()
        {
            int articleId = 123;
            var article = new ArticleDto()
            {
                Name = "Test Article",
                Price = 100,
                IsSold = false,
            };

            this.cachedSupplierService.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(true));
            this.cachedSupplierService.Setup(x => x.GetArticleAsync(articleId)).Returns(Task.FromResult(article));

            var supplierManager = new SupplierManager(
                this.cachedSupplierService.Object,
                new List<ISupplierService>() { supplier1.Object, supplier2.Object }
            );

            ArticleDto result = await supplierManager.GetArticleAsync(articleId);

            Assert.Same(article, result);

            this.cachedSupplierService.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.cachedSupplierService.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Once());
            this.cachedSupplierService.Verify(x => x.SetArticle(It.IsAny<Article>()), Times.Never());

            this.supplier1.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Never());
            this.supplier1.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());

            this.supplier2.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Never());
            this.supplier2.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async void GetArticleAsync_FromFirstExternalSupplier()
        {
            int articleId = 123;
            var article = new ArticleDto()
            {
                Name = "Test Article",
                Price = 100,
                IsSold = false,
            };

            this.cachedSupplierService.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(false));

            this.supplier1.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(true));
            this.supplier1.Setup(x => x.GetArticleAsync(articleId)).Returns(Task.FromResult(article));

            this.supplier2.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(true));
            this.supplier2.Setup(x => x.GetArticleAsync(articleId)).Returns(Task.FromResult(article));

            var supplierManager = new SupplierManager(
                this.cachedSupplierService.Object,
                new List<ISupplierService>() { supplier1.Object, supplier2.Object }
            );

            ArticleDto result = await supplierManager.GetArticleAsync(articleId);

            Assert.Same(article, result);

            this.cachedSupplierService.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.cachedSupplierService.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());
            this.cachedSupplierService.Verify(x => x.SetArticle(It.IsAny<Article>()), Times.Once());

            this.supplier1.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.supplier1.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Once());

            this.supplier2.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.supplier2.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async void GetArticleAsync_FromSecondExternalSupplier()
        {
            int articleId = 123;
            var article = new ArticleDto()
            {
                Name = "Test Article",
                Price = 100,
                IsSold = false,
            };

            this.cachedSupplierService.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(false));

            this.supplier1.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(false));

            this.supplier2.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(true));
            this.supplier2.Setup(x => x.GetArticleAsync(articleId)).Returns(Task.FromResult(article));

            var supplierManager = new SupplierManager(
                this.cachedSupplierService.Object,
                new List<ISupplierService>() { supplier1.Object, supplier2.Object }
            );

            ArticleDto result = await supplierManager.GetArticleAsync(articleId);

            Assert.Same(article, result);

            this.cachedSupplierService.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.cachedSupplierService.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());
            this.cachedSupplierService.Verify(x => x.SetArticle(It.IsAny<Article>()), Times.Once());

            this.supplier1.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.supplier1.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());

            this.supplier2.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.supplier2.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void GetArticleAsync_NoMatch()
        {
            int articleId = 123;
            var article = new ArticleDto()
            {
                Name = "Test Article",
                Price = 100,
                IsSold = false,
            };

            this.cachedSupplierService.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(false));

            this.supplier1.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(false));

            this.supplier2.Setup(x => x.ArticleInInventoryAsync(articleId)).Returns(Task.FromResult(false));

            var supplierManager = new SupplierManager(
                this.cachedSupplierService.Object,
                new List<ISupplierService>() { supplier1.Object, supplier2.Object }
            );

            ArticleDto result = await supplierManager.GetArticleAsync(articleId);

            Assert.Null(result);

            this.cachedSupplierService.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.cachedSupplierService.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());
            this.cachedSupplierService.Verify(x => x.SetArticle(It.IsAny<Article>()), Times.Never());

            this.supplier1.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.supplier1.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());

            this.supplier2.Verify(x => x.ArticleInInventoryAsync(It.IsAny<int>()), Times.Once());
            this.supplier2.Verify(x => x.GetArticleAsync(It.IsAny<int>()), Times.Never());
        }
    }
}
