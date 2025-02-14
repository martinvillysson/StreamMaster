using System.ComponentModel.DataAnnotations.Schema;

namespace StreamMaster.Domain.Models
{
    [TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
    public class M3UGroup : BaseEntity
    {
        public static string APIName => "M3UGroups";

        [Column(TypeName = "citext")]
        public string Name { get; set; } = "";

        public bool IsIncluded { get; set; } = true;

        public int TotalCount { get; set; }

        public bool IsUser { get; set; }

        public bool IsPPV { get; set; }

        public bool IsVOD { get; set; }

        public virtual int M3UFileId { get; set; }
    }
}