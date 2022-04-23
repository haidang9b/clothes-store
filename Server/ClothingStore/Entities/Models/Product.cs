using ClothingStore.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClothingStore.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string description { get;set;}
        public string image { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
