using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    [Table(name:"User")]
    public class User
    {
        [Column(name:"Id")]
        public int Id { get; set; }
        [Column(name:"Name")]
        public string Name { get; set; }
        [Column(name:"Email")]
        public string Email { get; set; }
    }

}
