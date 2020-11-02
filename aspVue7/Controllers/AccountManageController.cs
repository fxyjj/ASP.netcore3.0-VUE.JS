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