namespace Pharmacy_back.Models
{
    
        public class ProductInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        
    
    public class OrderGroup
    {
        public DateTime OrderDate { get; set; }
        public List<ProductInfo> Products { get; set; }
    }
}
