using System.ComponentModel;

namespace ClothingStore.Enums
{
    public enum EUploadFileType
    {
        [Description("Upload image")]
        Image = 1,
        [Description("Upload pdf")]
        PDF,
        [Description("Upload word")]
        Docx,
        [Description("Upload excel")]
        Excel
    }
}
