namespace Sisteplant.Domain.Exercises.Exercise3
{
    public class Item
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        //Relation
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
