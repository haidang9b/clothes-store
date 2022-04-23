using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Enums
{
    public enum EStatusBill
    {
        [Description("Pending")]
        Confirm = 1,
        [Description("Processing")]
        InProgress,
        [Description("Delivered")]
        Done,
        [Description("Cancelled")]
        Cancel,
    }
}
