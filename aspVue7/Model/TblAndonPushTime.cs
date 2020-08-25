using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblAndonPushTime
    {
        public int 序号 { get; set; }
        public string 状态 { get; set; }
        public int? 提醒阶段 { get; set; }
        public string 操作流程 { get; set; }
        public int? 时间间隔 { get; set; }
        public string 是否邮件通知 { get; set; }
        public string 推送人员职级 { get; set; }
    }
}
