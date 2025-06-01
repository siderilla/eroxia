using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class PurchaseProduct
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Purchase? Purchase { get; set; }
        public Product? Product { get; set; }

        public PurchaseProduct(int purchaseId, int productId, int quantity)
        {
            PurchaseId = purchaseId;
            ProductId = productId;
            Quantity = quantity;
        }

        public override string ToString()
        {
            var purchaseInfo = Purchase != null
                ? $"Cliente: {Purchase.Client.Name} {Purchase.Client.Surname}"
                : $"PurchaseID: {PurchaseId}";

            var productInfo = Product != null
                ? $"{Product.Name} - {Quantity} pezzo/i - {Product.Price:C} cad."
                : $"ProductID: {ProductId}, Quantità: {Quantity}";

            return $"{purchaseInfo} ha acquistato {productInfo}";
        }

    }
}

