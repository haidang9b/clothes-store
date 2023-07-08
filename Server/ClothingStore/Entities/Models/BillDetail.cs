using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClothingStore.Entities.Models
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(BillId))]
        [JsonIgnore]
        public virtual Bill Bill { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
