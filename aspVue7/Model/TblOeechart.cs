using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblOeechart
    {
        public int Id { get; set; }
        public string 日 { get; set; }
        public string 周 { get; set; }
        public string 月 { get; set; }
        public double? 完工数量 { get; set; }
        public double? 不良数量 { get; set; }
        public int? 定额人数 { get; set; }
        public int? 生产人数 { get; set; }
        public double? 定额产量 { get; set; }
        public double? 操作时间 { get; set; }
        public double? 计划工作时间 { get; set; }
        public double? OeeTarget { get; set; }
        public DateTime? 日期 { get; set; }
    }
}
