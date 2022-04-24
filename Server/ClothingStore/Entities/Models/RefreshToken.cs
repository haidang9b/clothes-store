using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string refreshToken { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsExpired => CreatedDate.AddDays(5) <= DateTime.Now;

        public int user_id;
        [ForeignKey("user_id")]
        public virtual User user { get; set; }
    }
}
