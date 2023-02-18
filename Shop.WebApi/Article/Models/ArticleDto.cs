using System;

namespace Shop.WebApi.Models
{
    public class ArticleDto
    {
        public string Name { get; set; }

        public int Price { get; set; }
        public bool IsSold { get; set; }

        public DateTime? SellingDate { get; set; }
    }
}
