using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblOrderReportTarget
    {
        public int Id { get; set; }
        public string 所属车间 { get; set; }
        public string 生产单元 { get; set; }
        public double? OeeTarget { get; set; }
        public double? EffTarget { get; set; }
        public double? FttTarget { get; set; }
        public double? DtRTarget { get; set; }
        public double? EaTarget { get; set; }
        public double? UpphTarget { get; set; }
    }
}
