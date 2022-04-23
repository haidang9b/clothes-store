using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Dtos
{
    public class CartDto
    {
        public int[] products { get; set; }
        public string nameReceiver { get; set; }
        public string numberPhone { get; set; }
        public string address { get; set; }
    }
}
