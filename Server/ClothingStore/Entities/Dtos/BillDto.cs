using ClothingStore.Entities.Models;
using ClothingStore.Enums;
using System;

namespace ClothingStore.Entities.Dtos
{
    public class BillDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public double TotalPrice { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string NameReceiver { get; set; }
        public string NumberPhone { get; set; }
        public string Address { get; set; }
        public EStatusBill Status { get; set; }
    }
}
