using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
