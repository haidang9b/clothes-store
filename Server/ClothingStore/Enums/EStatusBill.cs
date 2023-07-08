using System.ComponentModel;

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
