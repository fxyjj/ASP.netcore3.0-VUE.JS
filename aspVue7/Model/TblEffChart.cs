using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblEffChart
    {
        public string 日 { get; set; }
        public string 周 { get; set; }
        public string 月 { get; set; }
        public double? 员工效率 { get; set; }
        public double? EffTarget { get; set; }
    }
}
