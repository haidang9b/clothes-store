using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Models
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int bill_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        [ForeignKey("bill_id")]
        [JsonIgnore]
        public virtual Bill bill { get; set; }
        [ForeignKey("product_id")]
        [JsonIgnore]
        public virtual Product product { get; set; }
    }
}
