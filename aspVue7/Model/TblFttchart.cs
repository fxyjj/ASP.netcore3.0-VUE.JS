using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblFttchart
    {
        public string 日 { get; set; }
        public string 周 { get; set; }
        public string 月 { get; set; }
        public double? 合格数量 { get; set; }
        public double? 返工合格数量 { get; set; }
        public double? 完工数量 { get; set; }
        public double? 一次合格率 { get; set; }
        public double? FttTarget { get; set; }
    }
}
