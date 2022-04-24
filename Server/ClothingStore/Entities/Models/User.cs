using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int role_id { get; set; }
        [ForeignKey("role_id")]
        [JsonIgnore]
        public Role role { get; set; }
        [JsonIgnore]

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
