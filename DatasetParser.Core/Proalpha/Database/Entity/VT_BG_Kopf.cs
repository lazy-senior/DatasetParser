using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class VT_BG_Kopf
{
    public string Formular { get; set; } = null!;

    public string GeneratorTypen { get; set; } = null!;

    public string SuccessorForms { get; set; } = null!;

    public string BG_Kopf_Obj { get; set; } = null!;

    public string Print_DRC_Instance_Obj { get; set; } = null!;

    public string Dialog_DRC_Instance_Obj { get; set; } = null!;

    public string Systemoption { get; set; } = null!;

    public bool PreviewData { get; set; }

    public string LayoutTypen { get; set; } = null!;
}
