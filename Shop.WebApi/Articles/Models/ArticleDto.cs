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
    }
}
