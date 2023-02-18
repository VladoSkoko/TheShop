using System;

namespace Shop.WebApi.Articles.Models
{
    public class ArticleDto
    {
        public string Name { get; set; }

        public int Price { get; set; }
        public bool IsSold { get; set; }
        public int? BuyerUserId { get; set; }

        public DateTime? SellingDate { get; set; }

        //public ArticleDto(string name, int price, bool isSold)
        //{
        //    this.Name = name;
        //    this.Price = price;
        //    this.IsSold = isSold;
        //}

        //public ArticleDto(Article article)
        //{
        //    this.Name = article.Name;
        //    this.Price = article.Price;
        //    this.IsSold = article.IsSold;
        //    this.SellingDate = article.SellingDate;
        //    this.BuyerUserId = article.BuyerUserId;
        //}
    }
}
