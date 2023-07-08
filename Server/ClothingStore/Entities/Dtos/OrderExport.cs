using ClothingStore.Enums;
using System;

namespace ClothingStore.Entities.Dtos
{
    public class OrderExport
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string NameReceiver { get; set; }
        public string NumberPhone { get; set; }
        public string Address { get; set; }
        public EStatusBill Status { get; set; }
        public double TotalCost { get; set; }
    }
}
