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
    public class ChartdataController : Controller
    {

        // [HttpPost]
        [HttpGet("[action]")]
        public List<Testfalsk> chartdata()
        {
            
            var rng = new Random();
            var model = new BorgWarnerMisSQLContext();
            // var testData = model.Testfalsk.ToList<Testfalsk>();
            var testData = model.Database.SqlQuery<Testfalsk>("select * from dbo.testfalsk").ToList();
            return testData;
        }

        // [HttpPost("[action]")]
        // // public List<Testfalsk> queryDate([FromBody] seDate date)
        // public List<corssChartData> queryDate([FromBody] seDate date)
        // {
        //     // var tes = date;
        //     // string datestr = date.ToString();
        //     // var param = new{start = "", end = ""};
        //     // param = JsonConvert.DeserializeAnonymousType(datestr,param);
        //     var rng = new Random();
        //     var model = new BorgWarnerMisSQLContext();
        //     // Console.WriteLine("aaa  "+param);
        //     // var testData = model.Testfalsk.ToList<Testfalsk>();
        //     // var Sqlparam1 = new SqlParameter("@p0",SqlDbType.DateTime, 50);

        //     // Sqlparam1.Value = date.start;
        //     // var Sqlparam2 = new SqlParameter("@p1",SqlDbType.DateTime, 50);
        //     // Sqlparam2.Value = date.end;
        //     // var user = "Xiao";
        //     // var name = "Sea";
        //     // var begin = date.start;
        //     // var finish = date.end;
        //     // var testData = model.Database.SqlQuery<Testfalsk>($"EXECUTE dbo.qfortest @p0={user},@p1={name}").ToList();
        //     // var testData = model.Database.SqlQuery<corssChartData>($"EXECUTE dbo.qfortest").Where(te=>te.Birthdate>= date.start.Date && te.Birthdate <= date.end.Date ).ToList();
        //     // var testData = model.Testfalsk.Where(te => (te.Birthdate >= date.start.Date && te.Birthdate <= date.end.Date )).ToList();
        //     return testData;
        // }
        //  [HttpPost("[action]")]
        // public seDate queryDate([FromBody] seDate date)
        // {
        //     // var tes = date;
        //     // string datestr = date.ToString();
        //     // var param = new{start = "", end = ""};
        //     // param = JsonConvert.DeserializeAnonymousType(datestr,param);
        //     return date;
        // }
        
        [Route("api/[controller]")]
        // [EnableCors]
        [HttpPost("[action]")]
        public List<OEEset> OEEchartQuery([FromBody] queryParam prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.prodLine;
            var p2 = prm.dateUnit;
            var s = prm.start.Date;
            var e = prm.end.Date;
            var testData = model.Database.SqlQuery<OEEset>($"EXECUTE dbo.QforOEE @prodline='{p1}',@du='{p2}',@start='{s}',@end='{e}'")/*.Where(te=>te.日期>= prm.start && te.日期 <= prm.end )*/.ToList();
            return testData; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<EFFset> EFFchartQuery([FromBody] queryParam prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.prodLine;
            var p2 = prm.dateUnit;
            var s = prm.start.Date;
            var e = prm.end.Date;
            var testData = model.Database.SqlQuery<EFFset>($"EXECUTE dbo.QforEFF @prodline='{p1}',@du='{p2}',@start='{s}',@end='{e}'").ToList();
            return testData; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]

        public List<FTTset> FTTchartQuery([FromBody] queryParam prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.prodLine;
            var p2 = prm.dateUnit;
            var s = prm.start.Date;
            var e = prm.end.Date;
            var testData = model.Database.SqlQuery<FTTset>($"EXECUTE dbo.QforFTT @prodline='{p1}',@du='{p2}',@start='{s}',@end='{e}'").ToList();
            return testData; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]

        public List<TJLset> TJLchartQuery([FromBody] queryParam prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.prodLine;
            var p2 = prm.dateUnit;
            var s = prm.start.Date;
            var e = prm.end.Date;
            var testData = model.Database.SqlQuery<TJLset>($"EXECUTE dbo.QforTJL @prodline='{p1}',@du='{p2}',@start='{s}',@end='{e}'").ToList();
            return testData; 
        }
    }
    
    public class seDate{
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }



     //OEEset类用于存放数据库返回的数据集
    public class OEEset{
        public string dateunit { get; set; }
        public double OEE { get; set; }
        public double 目标 { get; set; }
    }

    public class EFFset{
        public string dateunit { get; set; }
        public double 员工效率 { get; set; }
        public double 目标 { get; set; }
    }

    public class FTTset{
        public string dateunit { get; set; }
        public double 一次合格率 { get; set; }
        public double 目标 { get; set; }
    }

    public class TJLset{

        public string dateunit { get; set; }
        public double 停机率 { get; set; }
        public double 目标 { get; set; }
    }
    
    
    public class queryParam{
        public DateTime start{get;set;}
        public DateTime end{get;set;}
        public string dateUnit{get;set;}
        public string prodLine{get;set;}
    }
}