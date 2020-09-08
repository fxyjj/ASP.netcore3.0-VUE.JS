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
    public class AndonController : Controller
    {
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<AndonHourRate> hourRate([FromBody] queryPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var testData = model.Database.SqlQuery<AndonHourRate>($"EXECUTE dbo.QforHourRate @dateunit='{p1}',@starttime='{s}',@endtime='{e}'").ToList();
            return testData; 
        }

        public class queryPrm{
            public string dateunit{get;set;}
            public DateTime start{get;set;}
            public DateTime end{get;set;}
        }

        public class AndonHourRate{
            public string dateunit{get;set;}
            public double procssTime{get;set;}
            public double planTime{get;set;}
            public int andonNum{get;set;}
            public double repairTime{get;set;}
            public double AvgRpsTime{get;set;}
        }
    }
}