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

        //按灯TopIssue
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<TIQuryPrm> topissue([FromBody] topissuePrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.dateunit;
            var s = prm.start;
            var e = prm.end;
            var testData = model.Database.SqlQuery<TIQuryPrm>($"EXECUTE dbo.QforTopIssueDU @dateunit='{p1}',@start='{s}',@end='{e}'").ToList();
            return testData; 
        }
        //设备安灯工时分布
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<equipQueryRes> equipChart([FromBody] equipPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.date;
            var p2 = prm.dateunit;
            var p3 = prm.status;
            var testData = model.Database.SqlQuery<equipQueryRes>($"EXECUTE dbo.QforTopIssueEquip @date='{p1}',@dateunit='{p2}',@status='{p3}'").ToList();
            return testData; 
        }
        //产线安灯工时分布
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<prodQueryRes> prodChart([FromBody] prodPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var p1 = prm.date;
            var p2 = prm.dateunit;
            var p3 = prm.status;
            var testData = model.Database.SqlQuery<prodQueryRes>($"EXECUTE dbo.QforTopIssueProd @date='{p1}',@dateunit='{p2}',@status='{p3}'").ToList();
            return testData; 
        }
         //产线安灯工时分布
        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<andonSts> andonStatus(){
            var model = new BorgWarnerMisSQLContext();
            // var p1 = prm.date;
            // var p2 = prm.dateunit;
            // var p3 = prm.status;
            var testData = model.Database.SqlQuery<andonSts>("select * from dbo.qforAndonGeneral where 当前状态 != 4").ToList();
            return testData; 
        }
        //按灯类别，按灯信息汇总 参数 front -> end
        public class queryPrm{
            public string dateunit{get;set;}
            public DateTime start{get;set;}
            public DateTime end{get;set;}
            public string stus{get;set;}
        }
        //按灯信息汇总：安灯数量参数 end -> front
        public class AndonNumPrm{
            public string dateunit{get;set;}
            public DateTime start{get;set;}
            public DateTime end{get;set;}

        }
        //按灯信息汇总：按灯工时损失参数 end -> front
        public class AndonHourRate{
            public string dateunit{get;set;}
            public double procssTime{get;set;}
            public double planTime{get;set;}
            public int andonNum{get;set;}
            public double repairTime{get;set;}
            public double AvgRpsTime{get;set;}
        }
        //安灯数量参数 end -> front
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
        //按灯种类参数 end -> front
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
        //topIssue 类
        //日期滑条参数 front -> end
        public class topissuePrm{
            public string dateunit{get;set;}
            public DateTime start{get;set;}
            public DateTime end{get;set;}
        }
        //日期滑条参数 end -> front
        public class TIQuryPrm{
            public string dateunit{get;set;}
        }
        //设备编号参数 front -> end
        public class equipPrm{
            public string date{get;set;}
            public string dateunit{get;set;}
            public string status{get;set;}
        }
        //设备编号参数 end -> front
        public class equipQueryRes{
            public string equipNum{get;set;}
            public double processTime{get;set;}
            public int andonNum{get;set;}
            public double rate{get;set;} 
        }
        //产线安灯分布参数 front -> end
        public class prodPrm{
            public string date{get;set;}
            public string dateunit{get;set;}
            public string status{get;set;}
        }
        //产线安灯分布参数 end -> front
        public class prodQueryRes{
            public string prodLine{get;set;}
            public double processTime{get;set;}
            public int andonNum{get;set;}
            public double rate{get;set;} 
        }

        //安灯状态表格 end -> front
        public class andonSts{
            public string 工作中心{get;set;}
            public string 是否停线{get;set;}
            public string 设备编号{get;set;}
            public string 问题描述{get;set;}
            public DateTime 按灯时间{get;set;}
            public string 类别描述{get;set;}
            public int 当前状态{get;set;}
            public int 序号{get;set;}
            public int 响应时间{get;set;}
            public int 持续时间{get;set;}

            public string 处理人{get;set;}
            //TODO...
        }
    }
}