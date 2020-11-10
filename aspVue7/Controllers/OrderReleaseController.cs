using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspVue7.Model;
using System.Data;
using System.Data.SqlClient;//reference from SqlParameter!!!
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Microsoft.AspNetCore.NodeServices;
using System.Security.Cryptography;
using NPOI.SS.UserModel;
using Microsoft.AspNetCore.Hosting;
using System.Collections;


namespace aspVue7.Controllers
{
    
    [Route("api/[controller]")]
    public class OrderReleaseController : Controller
    {
        [HttpPost("[action]")]
        public List<orderRes> getOrder([FromBody] srhPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<orderRes>($"EXECUTE dbo.QforWebOrderRelease @prodLine='{prm.pline}',@srh='{prm.srhCont}' ").ToList();
            return testData;
        }
    }

    public class srhPrm{
        public string pline{get;set;}
        public string srhCont{get;set;}
    }

    public class orderRes{
        public string 作业单号{get;set;}
        public string 物料号{get;set;}
        public string 物料描述{get;set;}
        public int 订单数量{get;set;}
        public DateTime 计划开始日期{get;set;}
        public DateTime 计划完工日期{get;set;}
        public DateTime 生产下达日期{get;set;}
        public DateTime 生产开始日期{get;set;}
        public DateTime 生产完工日期{get;set;}
        public int 合格数量{get;set;}
        public int 不合格数量{get;set;}
        public string 来源车间{get;set;}
        public string 状态{get;set;}
        public string 订单类型{get;set;}
        public string 备注{get;set;}

    }
}