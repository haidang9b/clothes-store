using ClothingStore.Entities.Models;
using ClothingStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Dtos
{
    public class TrackingDto
    {
        public int id { get; set; }
        public string user { get; set; }
        public double totalPrice { get; set; }
        public DateTimeOffset createdDate { get; set; }
        public DateTimeOffset updateDate { get; set; }
        public string nameReceiver { get; set; }
        public string numberPhone { get; set; }
        public string address { get; set; }
        public EStatusBill status { get; set; }
        public List<BillDetail> billDetails { get; set; }

    }
}
