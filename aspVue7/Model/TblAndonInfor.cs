using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblAndonInfor
    {
        public int 序号 { get; set; }
        public string 是否停线 { get; set; }
        public string 工作中心 { get; set; }
        public string 设备编号 { get; set; }
        public int? 问题类别 { get; set; }
        public string 问题描述 { get; set; }
        public string 按灯人 { get; set; }
        public DateTime? 按灯时间 { get; set; }
        public string 处理人 { get; set; }
        public DateTime? 处理时间 { get; set; }
        public bool? 临时解决 { get; set; }
        public string 不能解决原因 { get; set; }
        public string 行动计划 { get; set; }
        public DateTime? 计划完成日期 { get; set; }
        public string 退回人 { get; set; }
        public DateTime? 退回时间 { get; set; }
        public string 退回原因 { get; set; }
        public string 关闭人 { get; set; }
        public DateTime? 关闭时间 { get; set; }
        public string 确认人 { get; set; }
        public DateTime? 确认时间 { get; set; }
        public string 不合格品单号 { get; set; }
        public string 报修单号 { get; set; }
        public string 报工单号 { get; set; }
        public string 安灯编号 { get; set; }
        public int? 当前状态 { get; set; }
        public int? 是否推送 { get; set; }
        public int? 提醒阶段 { get; set; }
        public DateTime? 提醒时间 { get; set; }
    }
}
