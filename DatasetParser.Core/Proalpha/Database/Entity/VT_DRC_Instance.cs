using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class VT_DRC_Instance
{
    public string DRC_Instance_ID { get; set; } = null!;

    public string DRC_Instance_Obj { get; set; } = null!;

    public string DRC_Class_Obj { get; set; } = null!;

    public string Directory { get; set; } = null!;
}
