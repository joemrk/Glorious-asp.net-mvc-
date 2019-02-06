using Glorius.Models.Data;
using Glorius.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glorius.Models
{
    public class Cart
    {
        List<CartLine> cart = new List<CartLine>();
        CartLine cartLine = new CartLine();


        public void Add(ProductDTO product, int quantity)
        {
            CartLine line = cart
                .Where(e => e.Product.Id == product.Id)
                .FirstOrDefault();

            if (line == null)
            {
                cart.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void Del(int id)
        {
            cart.RemoveAll(l => l.Product.Id == id);
        }

        public void Edit(ProductDTO product, int quantity)
        {
            CartLine line = cart
                .Where(e => e.Product.Id == product.Id)
                .FirstOrDefault();

            line.Quantity = quantity;
        }

        public int TotalSum()
        {
            foreach (var item in cart)
            {
                if (item.Product.Discount != 0)
                    item.Product.Price = item.Product.Discount;
            }

            return cart.Sum(c => (int)c.Product.Price * c.Quantity);
        }

        public void Clear()
        {
            cart.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return cart; }
        }

    }
    public class CartLine
    {
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}