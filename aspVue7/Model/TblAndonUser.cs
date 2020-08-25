using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblAndonUser
    {
        public int 序号 { get; set; }
        public string 姓名 { get; set; }
        public string 工号 { get; set; }
        public string 账号 { get; set; }
        public string 部门名称 { get; set; }
        public string 密码 { get; set; }
        public int? 排序 { get; set; }
        public DateTime? 创建时间 { get; set; }
        public string 手机号 { get; set; }
        public string 职务 { get; set; }
        public string 职级 { get; set; }
        public string 所属车间 { get; set; }
        public string 安灯类别 { get; set; }
        public string 是否相关人员 { get; set; }
        public string 电子邮箱 { get; set; }
    }
}
