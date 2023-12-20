using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class VT_DRC_DataProvider
{
    public string DAO_DRC_Instance_Obj { get; set; } = null!;

    public string DRC_DataProvider_Obj { get; set; } = null!;

    public string DRC_Dataset_Obj { get; set; } = null!;

    public string Owning_Obj { get; set; } = null!;

    public string Ref_DRC_DataProvider_Obj { get; set; } = null!;
}
