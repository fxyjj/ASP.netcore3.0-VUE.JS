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

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<diaRes> dialog1([FromBody] diaParam prm){
            var model = new BorgWarnerMisSQLContext();
            
            var testdata = model.Database.SqlQuery<diaRes>($"EXECUTE dbo.QforBGDGeneral @qdate='{prm.qDate.Date}',@pLine='{prm.prodLine}'").ToList();
            // res.second =  model.Database.SqlQuery<failRec>($"select * from dbo.qforFailRec where 填写日期='{prm.qDate.Date}'").ToList();
            // res.third = model.Database.SqlQuery<stopRec>($"select * from dbo.qforOrderEDownRecordBrowse where 停机开始='{prm.qDate.Date}' ").ToList();
            return testdata; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<failRec> tab1([FromBody] tabParam prm){
            var model = new BorgWarnerMisSQLContext();
            var testdata =  model.Database.SqlQuery<failRec>($"select * from dbo.qforFailRec where 填写日期='{prm.qDate.Date}' and 报工编号='{prm.bgbh}'").ToList();
            // res.third = model.Database.SqlQuery<stopRec>($"select * from dbo.qforOrderEDownRecordBrowse where 停机开始='{prm.qDate.Date}' ").ToList();
            return testdata; 
        }

        [Route("api/[controller]")]
        [HttpPost("[action]")]
        public List<stopRec> tab2([FromBody] tabParam prm){
            var model = new BorgWarnerMisSQLContext();
            // var testdata =  model.Database.SqlQuery<failRec>($"select * from dbo.qforFailRec where 填写日期='{prm.qDate.Date}' and 报工编号='{prm.bgbh}'").ToList();
            var testdata = model.Database.SqlQuery<stopRec>($"select * from dbo.qforOrderEDownRecordBrowse where convert(date,停机开始)='{prm.qDate.Date}' and 报工编号='{prm.bgbh}'").ToList();
            return testdata; 
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

    //弹窗参数
    public class diaParam{
        public DateTime qDate{get;set;}
        public string prodLine{get;set;}
    }

    //弹窗请求结果
    public class diaRes{
        public DateTime 生产日期{get;set;}
        public string 报工编号{get;set;}
        public string 加工人员{get;set;}
        public string 所属班组{get;set;}
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public double 完工数量{get;set;}
        public double 合格数量{get;set;}
        public double 不良数量{get;set;}
        public double 料废{get;set;}
        public double 机废{get;set;}
        public double 调废{get;set;}
        public double 工废{get;set;}
        public double 待处理{get;set;}
        public double  返工合格数量{get;set;}
        public DateTime 作业开始时间{get;set;}
        public double 一次合格率{get;set;}
        public double 设备开动率{get;set;}
        public double 员工效率{get;set;}
        public double 合格率{get;set;}
    }
    //不合格记录参数
    public class tabParam{
        public DateTime qDate{get;set;}
        public string bgbh{get;set;}
    }
    //不合格记录结果
    public class failRec{
        public string 不合格单号{get;set;}
        public string 生产线{get;set;}
        public DateTime 填写日期{get;set;}
        public string 填写人{get;set;}
        public int 序号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public string 不合格描述{get;set;}
        public int 不合格数量{get;set;}
        public int 报废数{get;set;}
        public int 返工合格数{get;set;}
        public int 降级数{get;set;}
        public int 偏差数{get;set;}
        public int 合格数{get;set;}
        public int Sign{get;set;}
        public bool 状态{get;set;}
        public string 报工编号{get;set;}
        public string 设备编号{get;set;}
        public string 作业单号{get;set;}
    }
    //停机记录
    public class stopRec{
        // public int ID{get;set;}
        // public string 报工编号{get;set;}
        // public string 作业单号{get;set;}
        public string 停机类型{get;set;}
        public string 停机描述{get;set;}
        public DateTime 停机开始{get;set;}
        public DateTime 停机结束{get;set;}
        public int 调试暂停{get;set;}
        public string 填写人{get;set;}
        public string 设备编号{get;set;}
        public double 停机{get;set;}
    }
    //结果集合类
    public class diaCollect{
        public List<diaRes> first{get;set;}
        public List<failRec> second{get;set;}
        public List<stopRec> third{get;set;}
    }
}