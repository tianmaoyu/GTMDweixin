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
    
    public partial class Flow_State :ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flow_State()
        {
            this.Flow_FlowInstance = new HashSet<Flow_FlowInstance>();
            this.Flow_FlowTask = new HashSet<Flow_FlowTask>();
        }
    
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Remark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Flow_FlowInstance> Flow_FlowInstance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Flow_FlowTask> Flow_FlowTask { get; set; }
    }
}
