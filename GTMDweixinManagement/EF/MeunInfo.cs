//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GTMDweixinManagement.EF
{
    using Newtonsoft.Json;
    using ModelBase;
    using System;
    using System.Collections.Generic;
    
    public partial class MeunInfo :ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeunInfo()
        {
            this.MeunRoleInfos = new HashSet<MeunRoleInfo>();
        }
    
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public Nullable<int> TreeParentID { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string TreePath { get; set; }
        public string URL { get; set; }
        public string Remark { get; set; }
    
        [JsonIgnore]
        public virtual MeunInfo TreeParentMeunInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<MeunRoleInfo> MeunRoleInfos { get; set; }
    }
}
