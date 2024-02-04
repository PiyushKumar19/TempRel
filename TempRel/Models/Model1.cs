using System.ComponentModel.DataAnnotations;

namespace TempRel.Models
{
    public class Model1
    {
        [Key]
        public int BaseId { get; set; }
        public string Name { get; set; }
        public List<Model2> Model2 { get; set; }
    }
}
