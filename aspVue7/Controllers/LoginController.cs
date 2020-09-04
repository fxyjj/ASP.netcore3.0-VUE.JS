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

namespace aspVue7.Controllers
{
    
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        // [HttpPost]
        [HttpPost("[action]")]
        public List<usrDBO> validate([FromBody] usrInfo usrname)
        {
            
            var rng = new Random();
            var model = new BorgWarnerMisSQLContext();
            var usr = usrname.username;
            // var testData = model.Testfalsk.ToList<Testfalsk>();
            // var testData = model.Database.SqlQuery($"select txtPassword from dbo.tblUsers where txtUsername='{usr}'").ToString();
            var testData = model.Database.SqlQuery<usrDBO>($"select txtPassword from dbo.tblUsers where txtUsername='{usr}' ").ToList();
            return testData;
        }

        public class usrInfo{
            public string username{get;set;}
        }

        public class usrDBO{
            public string txtPassword {get;set;}
        }
    }
}