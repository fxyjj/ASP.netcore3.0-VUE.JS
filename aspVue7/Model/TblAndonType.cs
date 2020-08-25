using System;
using System.Collections.Generic;

namespace aspVue7.Model
{
    public partial class TblAndonType
    {
        public int 优先级 { get; set; }
        public string 类别描述 { get; set; }
        public byte[] 安灯图片a { get; set; }
        public byte[] 安灯图片m { get; set; }
        public byte[] 安灯图片p { get; set; }
        public int? 安灯时长 { get; set; }
        public string 类别英文描述 { get; set; }
    }
}
