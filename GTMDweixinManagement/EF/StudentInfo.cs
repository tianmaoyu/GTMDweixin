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
    using System;
    using System.Collections.Generic;
    
    public partial class StudentInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentInfo()
        {
            this.StudentProjectInfos = new HashSet<StudentProjectInfo>();
            this.StudentSignInfos = new HashSet<StudentSignInfo>();
        }
    
        public int ID { get; set; }
        public Nullable<int> SuccessfulTotalNumber { get; set; }
        public Nullable<bool> IsSupervisor { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> SupervisorUserID { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentProjectInfo> StudentProjectInfos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSignInfo> StudentSignInfos { get; set; }
    }
}
