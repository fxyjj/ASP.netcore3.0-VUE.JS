using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class QforOrderReportAs
    {
        public string 报工编号 { get; set; }
        public int? 定额件数 { get; set; }
        public DateTime? 作业完工时间 { get; set; }
        public double? 完工数量 { get; set; }
        public double? 不良数量 { get; set; }
        public double? 计划工作时间 { get; set; }
        public double? 操作时间 { get; set; }
        public double? 定额产量 { get; set; }
        public double? 合格率 { get; set; }
        public double? 员工效率 { get; set; }
        public double? 设备开动率 { get; set; }
        public int? 生产人数 { get; set; }
        public int? 定额人数 { get; set; }
        public bool? Oee { get; set; }
        public string 工作组 { get; set; }
        public bool 状态 { get; set; }
        public string 报工类别 { get; set; }
        public DateTime? 调试结束时间 { get; set; }
        public string 日 { get; set; }
        public int? 周 { get; set; }
        public int? 月 { get; set; }
        public DateTime? 班次日期 { get; set; }
        public double? 停机率 { get; set; }
        public double? 一次合格率 { get; set; }
        public double? 合格数量 { get; set; }
        public double? 返工合格数量 { get; set; }
        public double? 非计划停机时间 { get; set; }
        public double? OeeTarget { get; set; }
        public double? EffTarget { get; set; }
        public double? FttTarget { get; set; }
        public double? DtRTarget { get; set; }
        public double? EaTarget { get; set; }
        public double? UpphTarget { get; set; }
        public int? 工作小时数 { get; set; }
        public string 加工单元 { get; set; }
    }
}
