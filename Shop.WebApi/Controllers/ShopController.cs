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
    public class ShopController : ApiController
    {
        private Db Db;
        private ILogger logger;

        private CachedSupplier CachedSupplier;
        private Warehouse Warehouse;
        private Dealer1 Dealer1;
        private Dealer2 Dealer2;

        public ShopController(
            ILogger logger
        )
        {
            Db = new Db();
            this.logger = logger;
            CachedSupplier = new CachedSupplier();
            Warehouse = new Warehouse();
            Dealer1 = new Dealer1();
            Dealer2 = new Dealer2();
        }

        [HttpGet()]
        public Article GetArtice(int id, int maxExpectedPrice = 200)
        {
            Article article = null;
            Article tmp = null;
            var articleExists = CachedSupplier.ArticleInInventory(id);
            if (articleExists)
            {
                tmp = CachedSupplier.GetArticle(id);
                if (maxExpectedPrice < tmp.ArticlePrice)
                {
                    articleExists = Warehouse.ArticleInInventory(id);
                    if (articleExists)
                    {
                        tmp = Warehouse.GetArticle(id);
                        if (maxExpectedPrice < tmp.ArticlePrice)
                        {
                            articleExists = Dealer1.ArticleInInventory(id);
                            if (articleExists)
                            {
                                tmp = Dealer1.GetArticle(id);
                                if (maxExpectedPrice < tmp.ArticlePrice)
                                {
                                    articleExists = Dealer2.ArticleInInventory(id);
                                    if (articleExists)
                                    {
                                        tmp = Dealer2.GetArticle(id);
                                        if (maxExpectedPrice < tmp.ArticlePrice)
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

        [HttpPost]
        public void BuyArticle(Article article, int buyerId)
        {
            if (article == null)
            {
                throw new BadRequestException(ExceptionMessage.CouldNotOrderArticle);
            }

            logger.Debug("Trying to sell article with id=" + article.ID);

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;

            try
            {
                Db.Save(article);
                logger.Info("Article with id " + article.ID + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id " + article.ID);
                throw ex;
            }
        }
    }
}