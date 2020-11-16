using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using aspVue7.Model;


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

        [HttpPost("[action]")]
        public List<newRes> newOrder([FromBody] newPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<newRes>($"EXECUTE dbo.QforWebNewOrder @ordNo='{prm.ordNo}',@wlNo='{prm.wlNo}',@wlDesc='{prm.wlDesc}',@ordNum='{prm.ordNum}',@sTime='{prm.sTime}',@eTime='{prm.eTime}',@ordTime='{prm.ordTime}',@station='{prm.station}',@status='{prm.status}',@ordType='{prm.ordType}',@tip='{prm.tip}' ").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<newRes> editOrder([FromBody] newPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<newRes>($"EXECUTE dbo.QforEditOrder @ordNo='{prm.ordNo}',@wlNo='{prm.wlNo}',@wlDesc='{prm.wlDesc}',@ordNum='{prm.ordNum}',@sTime='{prm.sTime}',@eTime='{prm.eTime}',@ordTime='{prm.ordTime}',@station='{prm.station}',@status='{prm.status}',@ordType='{prm.ordType}',@tip='{prm.tip}' ").ToList();
            return testData;
        }

        [HttpPost("[action]")]
         public List<newRes> delOrder([FromBody] delPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<newRes>($"EXECUTE dbo.QforDeleteOrder @ordNo='{prm.ordNo}' ").ToList();
            return testData;
        }

         [HttpPost("[action]")]
         public List<newRes> xiadaOrder([FromBody] xiadaPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<newRes>($"EXECUTE dbo.QforXiadaOrder @ordNo='{prm.ordNo}',@wlNo='{prm.wlNo}',@workNo='{prm.ordNo}',@ordNum='{prm.ordNum}',@sTime='{prm.sTime}',@eTime='{prm.eTime}',@station='{prm.station}' ").ToList();
            return testData;
        }

         [HttpPost("[action]")]
         public List<ordTrack1Res> ordTrack1([FromBody] otPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<ordTrack1Res>($"select * from dbo.qforWebOrderTrackTab1 where 生产单元='{prm.workUnit}' ").ToList();
            return testData;
        }

         [HttpPost("[action]")]
         public List<ordTrack2Res> ordTrack2([FromBody] otPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<ordTrack2Res>($"select * from dbo.qforWebOrderTrackTab2 where 加工单元='{prm.workUnit}' ").ToList();
            return testData;
        }

         [HttpPost("[action]")]
         public List<ordTrack3Res> ordTrack3([FromBody] otPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<ordTrack3Res>($"select * from dbo.tblOrdersub where 作业单号='{prm.workUnit}' ").ToList();
            return testData;
        }

        [HttpPost("[action]")]
         public List<ordTrack1Res> otSearch([FromBody] otSrhPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<ordTrack1Res>($"select * from dbo.qforWebOrderTrackTab1 where 生产单元='{prm.workUnit}' and (顺序号 like '%'+'{prm.srhCont}'+'%' or 作业单号 like '%'+'{prm.srhCont}'+'%' or 物料编号 like '%'+'{prm.srhCont}'+'%' or 物料描述 like '%'+'{prm.srhCont}'+'%' or 工序名称 like '%'+'{prm.srhCont}'+'%' )").ToList();
            return testData;
        }
        [HttpPost("[action]")]
         public List<newRes> mdfyDate([FromBody] mdfyPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<newRes>($"EXECUTE dbo.QforOrderDateModify @workNo='{prm.workNo}',@start='{prm.sDate}',@end='{prm.eDate}'").ToList();
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
    //新增/编辑数据的前端参数
    public class newPrm{
        public string ordNo{get;set;}
        public string wlNo{get;set;}
        public string wlDesc{get;set;}
        public int ordNum{get;set;}
        public DateTime sTime{get;set;}
        public DateTime eTime{get;set;}
        public DateTime ordTime{get;set;}
        public string station{get;set;}
        public string status{get;set;}
        public string ordType{get;set;}
        public string tip{get;set;}
    }

    public class newRes{
        public bool resSign{get;set;}
    }
    //删除数据的订单编号
    public class delPrm{
        public string ordNo{get;set;}
    }
    //下达参数
    public class xiadaPrm{
        public string ordNo{get;set;}
        public string wlNo{get;set;}
        public string workNo{get;set;}
        public int ordNum{get;set;}
        public DateTime sTime{get;set;}
        public DateTime eTime {get;set;}
        public string station{get;set;}
    }
    //跟踪表1/2/3参数
    public class otPrm{
        public string workUnit{get;set;}
    }
    //订单跟踪表1查询结果类
    public class ordTrack1Res{
        public string 顺序号{get;set;}
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public DateTime 开始日期{get;set;}
        public DateTime 完工日期{get;set;}
        public int 调试计划用时{get;set;}
        public int 未完工{get;set;}
        public decimal 剩余工时{get;set;}
    }
    //订单跟踪表2查询结果类
    public class ordTrack2Res{
        public string 报工编号{get;set;}
        public string 作业单号{get;set;}
        public string 创建人{get;set;}
        public DateTime 创建日期{get;set;}
        public string 物料描述{get;set;}
        public string 物料编号{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public string 报工类别{get;set;}
        public int 定额件数{get;set;}
        public string 加工人员{get;set;}
        public string 加工单元{get;set;}
        public DateTime 生产日期{get;set;}
        public DateTime 调试开始时间{get;set;}
        public double 调试暂停{get;set;}
        public double 调试时间{get;set;}
        public DateTime 调试结束时间{get;set;}
        public DateTime 作业开始时间{get;set;}
        public double 计划停机时间{get;set;}
        public DateTime 作业完工时间{get;set;}
        public double 非计划停机时间{get;set;}
        public double 完工数量{get;set;}
        public double 不良数量{get;set;}
        public string 不合格单编号{get;set;}
        public bool 状态{get;set;}
        public int 定额人数{get;set;}
        public string 工作组{get;set;}
    }
    //订单跟踪查询参数
    public class otSrhPrm{
        public string workUnit{get;set;}
        public string srhCont{get;set;}
    }

    //订单跟踪表3查询结果类
    public class ordTrack3Res{
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public int 完工数量{get;set;}
        public int 待处理{get;set;}
        public int 报废数量{get;set;}
        public int 降级数量{get;set;}
        public int 合格数量{get;set;}
        public string 状态{get;set;}
    }

    //订单跟踪页面修改日期参数
    public class mdfyPrm{
        public string workNo{get;set;}
        public DateTime sDate{get;set;}
        public DateTime eDate{get;set;}
    }

}