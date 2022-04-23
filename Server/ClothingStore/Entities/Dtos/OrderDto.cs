using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Dtos
{
    public class OrderDto
    {
        public int[] productIDs { get; set; }
        public int idCustomer { get; set; }
    }
}
