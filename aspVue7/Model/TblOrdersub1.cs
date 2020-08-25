using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblOrdersub1
    {
        public string 报工编号 { get; set; }
        public string 创建人 { get; set; }
        public DateTime? 创建日期 { get; set; }
        public string 所属车间 { get; set; }
        public string 加工单元 { get; set; }
        public string 工作组 { get; set; }
        public string 加工人员 { get; set; }
        public string 所属班组 { get; set; }
        public DateTime? 生产日期 { get; set; }
        public string 作业单号 { get; set; }
        public string 物料编号 { get; set; }
        public string 物料描述 { get; set; }
        public int? 工序号 { get; set; }
        public string 工序名称 { get; set; }
        public string 报工类别 { get; set; }
        public int? 定额件数 { get; set; }
        public DateTime? 调试开始时间 { get; set; }
        public double? 调试时间 { get; set; }
        public double? 调试暂停 { get; set; }
        public DateTime? 调试结束时间 { get; set; }
        public DateTime? 作业开始时间 { get; set; }
        public DateTime? 作业完工时间 { get; set; }
        public double? 计划停机时间 { get; set; }
        public double? 非计划停机时间 { get; set; }
        public double? 完工数量 { get; set; }
        public double? 合格数量 { get; set; }
        public double? 料废 { get; set; }
        public double? 机废 { get; set; }
        public double? 调废 { get; set; }
        public double? 工废 { get; set; }
        public double? 待处理 { get; set; }
        public double? 不良数量 { get; set; }
        public double? 返工合格数量 { get; set; }
        public double? 回用数量 { get; set; }
        public string 回用批次号 { get; set; }
        public double? 溢出数量 { get; set; }
        public string 不合格单编号 { get; set; }
        public bool 状态 { get; set; }
        public int? 生产人数 { get; set; }
        public int? 定额人数 { get; set; }
        public DateTime? 班次日期 { get; set; }
        public decimal? 补贴金额 { get; set; }
        public double? 等价系数 { get; set; }
    }
}
