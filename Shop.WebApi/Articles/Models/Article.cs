using System;

namespace Shop.WebApi.Articles.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public bool IsSold { get; private set; }

        public DateTime? SellingDate { get; private set; }
        public int? BuyerUserId { get; set; }

        public Article(int id, string name, int price, bool isSold)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.IsSold = isSold;
        }

        public Article(int Id, ArticleDto articleDto, int? buyerUserId = null)
        {
            this.Id = Id;
            this.Name = articleDto.Name;
            this.Price = articleDto.Price;
            this.IsSold = articleDto.IsSold;
            this.SellingDate = articleDto.SellingDate;
            this.BuyerUserId = buyerUserId;
        }

        public void Sell(int buyerUserId)
        {
            if (this.IsSold)
            {
                return;
            }

            this.BuyerUserId = buyerUserId;
            this.IsSold = true;
            this.SellingDate = DateTime.Now;
        }

        public ArticleDto ToDto()
        {
            return new ArticleDto()
            {
                Name = this.Name,
                Price = this.Price,
                IsSold = this.IsSold,
                SellingDate = this.SellingDate,
                BuyerUserId = this.BuyerUserId
            };
        }
    }
}