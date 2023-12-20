using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class DRC_Class
{
    public string Parent_DRC_Class_Obj { get; set; } = null!;

    public string DRC_Class_ID { get; set; } = null!;

    public string DRC_Class_Obj { get; set; } = null!;

    public string Rend_DRC_Instance_Obj { get; set; } = null!;
}
