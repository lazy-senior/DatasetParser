using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class DRC_Dataset
{
    public string DRC_Dataset_ID { get; set; } = null!;

    public string DRC_Dataset_Obj { get; set; } = null!;

    public string DatasetDefinitionFile { get; set; } = null!;

    public bool MasterDataValidation { get; set; }

    public bool isReadOnly { get; set; }

    public bool isDynamic { get; set; }

    public bool getChanges { get; set; }

    public string ExportProperties { get; set; } = null!;

    public string DatasetVersion { get; set; } = null!;

    public string BEO_DRC_Instance_Obj { get; set; } = null!;

    public string DAO_DRC_Instance_Obj { get; set; } = null!;

    public string Proxy_DRC_Instance_Obj { get; set; } = null!;
}
