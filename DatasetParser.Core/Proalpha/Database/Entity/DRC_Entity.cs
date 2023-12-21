using System;
using System.Collections.Generic;

namespace DatasetParser.Core.Proalpa.Database.Entity;

public partial class DRC_Entity
{
    public string DRC_Entity_ID { get; set; } = null!;

    public string DRC_Entity_Obj { get; set; } = null!;

    public string EntityDefinitionFile { get; set; } = null!;

    public bool hasBeforeTable { get; set; }

    public bool isNoUndoEntity { get; set; }

    public string DRC_Table_Obj { get; set; } = null!;

    public int EntityType { get; set; }

    public bool isTranslatable { get; set; }

    public ICollection<DRC_DSEntityAlloc> DRC_DSEntityAllocations {get;set;} = null!;
}
