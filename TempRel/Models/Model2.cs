namespace TempRel.Models
{
    public class Model2
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int BaseId { get; set; }
        public Model1 Model1 { get; set; }
    }
}
