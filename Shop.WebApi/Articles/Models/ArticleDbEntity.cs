using System;

namespace Shop.WebApi.Articles.Models
{
    public class ArticleDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }
        public bool IsSold { get; set; }

        public DateTime? SellingDate { get; set; }
        public int? BuyerUserId { get; set; }

        public ArticleDbEntity() { }
        public ArticleDbEntity(Article article)
        {
            this.Id = article.Id;
            this.Name = article.Name;
            this.Price = article.Price;
            this.IsSold = article.IsSold;
            this.SellingDate = article.SellingDate;
            this.BuyerUserId = article.BuyerUserId;
        }
    }
}
