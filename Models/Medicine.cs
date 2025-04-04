namespace Pharmacy_back.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public string Dosage { get; set; }
        public string Form { get; set; }
        public string Type { get; set; }
        public string Active_Ingredient { get; set; }
        public string img {  get; set; }
    }
}
