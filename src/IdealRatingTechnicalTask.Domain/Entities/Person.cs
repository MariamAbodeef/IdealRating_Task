

using System.ComponentModel.DataAnnotations.Schema;

namespace IdealRatingTechnicalTask.Domain.Entities
{
    [Table("Person_Details")]
    public class Person
    {
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("telephone Number")]
        public string TelephoneNumber { get; set; }
        [Column("country")]
        public string Country { get; set; }
    }
}
