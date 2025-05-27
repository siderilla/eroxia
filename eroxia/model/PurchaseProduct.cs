using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class PurchaseProduct
    {
        public Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public PurchaseProduct(Purchase purchase, int productId, int quantity)
        {
            Purchase = purchase;
            ProductId = productId;
            Quantity = quantity;
        }

    }
}

