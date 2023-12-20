using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class DRC_DSEntityAlloc
{
    public int DRC_DSEntityAlloc_ID { get; set; }

    public string DRC_DSEntityAlloc_Obj { get; set; } = null!;

    public string DRC_Dataset_Obj { get; set; } = null!;

    public string DRC_Entity_Obj { get; set; } = null!;
}
