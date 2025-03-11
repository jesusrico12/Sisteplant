namespace Sisteplant.Domain.Exercises.Exercise3
{
    public class Order
    {
        public int OrderId { get; set; }
        public int DistributorId { get; set; }
        // FK
        public int ItemOrdered { get; set; }
        public int ItemQuantity { get; set; }

        // Navigation property (for related Item entity)
        public Item Item { get; set; }
    }
}
