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
        //按灯信息汇总
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<AndonHourRate> hourRate([FromBody] queryPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var stats = prm.stus;
            var testData = model.Database.SqlQuery<AndonHourRate>($"EXECUTE dbo.QforHourRate @dateunit='{p1}',@starttime='{s}',@endtime='{e}',@status='{stats}'").ToList();
            return testData; 
        }

        //安灯数量
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<EGRNum> EGRdata([FromBody] AndonNumPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var testData = model.Database.SqlQuery<EGRNum>($"EXECUTE dbo.QforAndonNumEGR @dateunit='{p1}',@starttime='{s}',@endtime='{e}'").ToList();
            return testData; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<CMNum> CMdata([FromBody] AndonNumPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var testData = model.Database.SqlQuery<CMNum>($"EXECUTE dbo.QforAndonNumCM @dateunit='{p1}',@starttime='{s}',@endtime='{e}'").ToList();
            return testData; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<ICNum> ICdata([FromBody] AndonNumPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var testData = model.Database.SqlQuery<ICNum>($"EXECUTE dbo.QforAndonNumIC @dateunit='{p1}',@starttime='{s}',@endtime='{e}'").ToList();
            return testData; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<PTCTHNum> PTCTHdata([FromBody] AndonNumPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var testData = model.Database.SqlQuery<PTCTHNum>($"EXECUTE dbo.QforAndonNumPTCTH @dateunit='{p1}',@starttime='{s}',@endtime='{e}'").ToList();
            return testData; 
        }

        //按灯类别
         [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<catePrm> category([FromBody] queryPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var stats = prm.stus;
            var testData = model.Database.SqlQuery<catePrm>($"EXECUTE dbo.QforAndonCategoryPie @dateunit='{p1}',@start='{s}',@end='{e}',@status='{stats}'").ToList();
            return testData; 
        }
        public class queryPrm{
            public string dateunit{get;set;}
            public DateTime start{get;set;}
            public DateTime end{get;set;}
            public string stus{get;set;}
        }

        public class AndonNumPrm{
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

        public class EGRNum{
            public string 日期单位{get;set;}
            public int BPVNum{get;set;}
            public int VFNum{get;set;}
            public int 柔性线Num{get;set;}
            public int GDINum{get;set;}
            public int AMNum{get;set;}
            public int ETVNum{get;set;}
            public int LPNum{get;set;}
        }

        public class CMNum{
            public string 日期单位{get;set;}
            public int CM1Num{get;set;}
            public int CM2Num{get;set;}
            public int CM3Num{get;set;}
            public int MixNum{get;set;}
            public int Mod1Num{get;set;}
            public int Mod2Num{get;set;}
        }

        public class ICNum{
            public string 日期单位{get;set;}
            public int Hb1Num{get;set;}
            public int Hb2Num{get;set;}
            public int IPTE1Num{get;set;}
            public int IPTE2Num{get;set;}
            public int Msl1Num{get;set;}
            public int Msl2Num{get;set;}
        }

        public class PTCTHNum{
            public string 日期单位{get;set;}
            public int ecrvNum{get;set;}
            public int hvNum{get;set;}
            public int lvNum{get;set;}
            public int fordNum{get;set;}
            public int gacNum{get;set;}
            public int hkmcNum{get;set;}
            public int jlrNum{get;set;}
            public int sgmeNum{get;set;}
            public int laNum{get;set;}
        }

        public class catePrm{
            public string 日期单位{get;set;}
            public double gProcessT{get;set;}
            public double gAndonN{get;set;}
            public double gRespT{get;set;}
            public double gRepairT{get;set;}
            public double gWorkRate{get;set;}
             public double sProcessT{get;set;}
            public double sAndonN{get;set;}
            public double sRespT{get;set;}
            public double sRepairT{get;set;}
            public double sWorkRate{get;set;}
             public double cProcessT{get;set;}
            public double cAndonN{get;set;}
            public double cRespT{get;set;}
            public double cRepairT{get;set;}
            public double cWorkRate{get;set;}
             public double zProcessT{get;set;}
            public double zAndonN{get;set;}
            public double zRespT{get;set;}
            public double zRepairT{get;set;}
            public double zWorkRate{get;set;}
            public double plantimeSum{get;set;}

        }
    }
}