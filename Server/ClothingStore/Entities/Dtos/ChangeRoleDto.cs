using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Entities.Dtos
{
    public class ChangeRoleDto
    {
        public int user_id { get; set; }
        public int role_id { get; set; }
    }
}
