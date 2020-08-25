using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblAndonPermissions
    {
        public int 序号 { get; set; }
        public string 权限编号 { get; set; }
        public string 权限名称 { get; set; }
        public int? 排序 { get; set; }
    }
}
