using System;
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


namespace aspVue7.Controllers
{
    
    [Route("api/[controller]")]
    public class AccountManageController : Controller
    {
         [HttpPost("[action]")]
        public List<res> getUsr([FromBody] reqInfo qInfor)
        {
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<res>($"EXECUTE dbo.QforUsrDisplay @qstring='{qInfor.queryInfo}' ").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<milinRes> crtKey([FromBody] milinPrm prm)
        {
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<milinRes>($"EXECUTE dbo.QforKeyCreate @kStr='{prm.kstr}' ").ToList();
            return testData;
        }

        [HttpGet("[action]")]
        public List<kTabRes> keyTab()
        {
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<kTabRes>("select * from tblWebToken where validate = 1 ").ToList();
            return testData;
        }

        //密令表格结果
        public class kTabRes{
            public string token{get;set;}
            public DateTime cdate{get;set;}
            public int validate{get;set;}
        }

        //密令参数
        public class milinPrm{
            public string kstr{get;set;}
        }
        //密令结果
        public class milinRes{
            public bool resSign{get;set;}
        }

        public class reqInfo{
            public string queryInfo{get;set;}
        }

        public class res{
            public string username{get;set;}
            public string uname{get;set;}
            public string crtficate{get;set;}
            public DateTime cdate{get;set;}
        }
    }
}