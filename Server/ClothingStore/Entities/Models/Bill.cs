using ClothingStore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Models
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public DateTimeOffset createdDate { get; set; }
        public DateTimeOffset updateDate { get; set; }
        public string nameReceiver { get; set; }
        public string numberPhone { get; set; }
        public string address { get; set; }
        public EStatusBill status { get; set; }
        public virtual ICollection<BillDetail> billDetails { get; set; }
    }
}
