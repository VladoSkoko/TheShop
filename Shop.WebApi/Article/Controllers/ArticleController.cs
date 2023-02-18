using System;
using System.Web.Http;
using NLog;
using Shop.WebApi.Exceptions;
using Shop.WebApi.Localisation;
using Shop.WebApi.Models;
using Shop.WebApi.Resources;
using Shop.WebApi.Services;

namespace Shop.WebApi.Controllers
{
    [RoutePrefix("api/v1/article")]
    public class ArticleController : ApiController
    {
        private IArticleRepository articleRepository;
        private ILogger logger;

        private CachedSupplier CachedSupplier;
        private Warehouse Warehouse;
        private Dealer1 Dealer1;
        private Dealer2 Dealer2;

        public ArticleController(
            ILogger logger,
            IArticleRepository articleRepository
        )
        {
            this.articleRepository = articleRepository;
            this.logger = logger;
            CachedSupplier = new CachedSupplier();
            Warehouse = new Warehouse();
            Dealer1 = new Dealer1();
            Dealer2 = new Dealer2();
        }

        [Route("{id:int}/{maxExpectedPrice?}")]
        [HttpGet()]
        public Article GetArtice(int id, int maxExpectedPrice = 200)
        {
            Article article = null;
            Article tmp = null;
            var articleExists = CachedSupplier.ArticleInInventory(id);
            if (articleExists)
            {
                tmp = CachedSupplier.GetArticle(id);
                if (maxExpectedPrice < tmp.Price)
                {
                    articleExists = Warehouse.ArticleInInventory(id);
                    if (articleExists)
                    {
                        tmp = Warehouse.GetArticle(id);
                        if (maxExpectedPrice < tmp.Price)
                        {
                            articleExists = Dealer1.ArticleInInventory(id);
                            if (articleExists)
                            {
                                tmp = Dealer1.GetArticle(id);
                                if (maxExpectedPrice < tmp.Price)
                                {
                                    articleExists = Dealer2.ArticleInInventory(id);
                                    if (articleExists)
                                    {
                                        tmp = Dealer2.GetArticle(id);
                                        if (maxExpectedPrice < tmp.Price)
                                        {
                                            article = tmp;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (article != null)
                    {
                        CachedSupplier.SetArticle(article);
                    }
                }
            }

            return article;
        }

        [Route("{id:int}/buy/{buyerId:int}")]
        [HttpPost]
        public void BuyArticle(int id, int buyerId, ArticleDto articleDto)
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