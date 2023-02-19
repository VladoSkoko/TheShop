using System;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using Shop.WebApi.Exceptions;
using Shop.WebApi.Exceptions.Types;
using Shop.WebApi.Articles.Models;
using Shop.WebApi.Suppliers.Managers;
using Shop.WebApi.Articles.Repositories;

namespace Shop.WebApi.Articles.Controllers
{
    [RoutePrefix("api/v1/article")]
    public class ArticleController : ApiController
    {
        private readonly IArticleRepository articleRepository;
        private readonly ILogger logger;
        private readonly ISupplierManager supplierManager;

        public ArticleController(
            ILogger logger,
            IArticleRepository articleRepository,
            ISupplierManager supplierManager
        )
        {
            this.articleRepository = articleRepository;
            this.logger = logger;
            this.supplierManager = supplierManager;
        }

        [Route("{id:int}")]
        [HttpGet()]
        public async Task<ArticleDto> GetArticle(int id, int maxExpectedPrice = 200)
        {
            ArticleDto articleDto = await this.supplierManager.GetArticleAsync(id);

            if (articleDto == null)
            {
                throw new NotFoundException(ExceptionMessage.ArticleNotFound);
            }

            return articleDto;
        }

        [Route("{id:int}/buy/{buyerId:int}")]
        [HttpPost]
        public void BuyArticle(int id, int buyerId, [FromBody] ArticleDto articleDto)
        {
            if (articleDto == null)
            {
                throw new BadRequestException(ExceptionMessage.CouldNotOrderArticle);
            }

            logger.Debug("Trying to sell article with id=" + id);

            var article = new Article(id, articleDto);

            article.Sell(buyerId);

            try
            {
                articleRepository.Insert(new ArticleDbEntity(article));
                logger.Info("Article with id " + article.Id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id " + article.Id);
                throw ex;
            }
        }
    }
}