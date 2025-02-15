using System.ComponentModel.DataAnnotations.Schema;

namespace StreamMaster.Domain.Models
{
    [TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
    public class UserGroup : BaseEntity
    {
        public UserGroup()
        {
        }

        public UserGroup(string name) => this.Name = name;

        public UserGroup(string name, int totalCount)
        {
            this.Name = name;
            this.TotalCount = totalCount;
        }

        public int TotalCount { get; set; }

        public static string APIName => nameof(UserGroup);

        [Column(TypeName = "citext")]
        public string Name { get; set; } = "";
    }
}