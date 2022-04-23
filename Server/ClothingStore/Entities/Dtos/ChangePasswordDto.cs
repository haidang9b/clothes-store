using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Dtos
{
    public class ChangePasswordDto
    {
        public string username { get; set; }
        public string passwordOld { get; set; }
        public string passwordNew { get; set; }
    }
}
