using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using aspVue7.Model;

namespace aspVue7.Controllers
{
    [Route("api/[controller]")]
    public class WorkReportController:Controller
    {
        [HttpPost("[action]")]
        public List<currRes> currOrd([FromBody] currPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<currRes>($"EXECUTE dbo.QforCurrOrder @station='{prm.prodLine}' ").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<allRes> allOrdDisp([FromBody] currPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<allRes>($"select * from qforAllOrderDisplay where 生产单元='{prm.prodLine}'").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<bgdRes> BGDInfo([FromBody] bgdPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<bgdRes>($"EXECUTE dbo.QforGetBGDInfo @workNo='{prm.workNo}'").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<crtRes> crtBGD([FromBody] crtPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<crtRes>($"EXECUTE dbo.QforCreateBGD @workNo='{prm.workNo}',@crtMan='{prm.crtMan}',@crtDate='{prm.crtDate}'").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<startRes> BGDstart([FromBody] startPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<startRes>($"EXECUTE dbo.QforBGDStart @workUnit='{prm.workunit}'").ToList();//,@workNo='{prm.workNo}'
            return testData;
        }

        [HttpPost("[action]")]
        public List<testRes> BGDtest([FromBody] testPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<testRes>($"EXECUTE dbo.QforBGDTest @pGroup='{prm.prodline}',@workNo='{prm.workNo}',@startTime='{prm.sTime}',@overTime='{prm.eTime}',@testTime='{prm.testTime}',@testsMan='{prm.sMan}',@testeMan='{prm.eMan}',@tip='{prm.tip}',@cGroup='{prm.cgroup}'").ToList();
            return testData;
        }

        // [HttpPost("[action]")]
        // public List<tChkRes> BGDtestChk([FromBody] tChkPrm prm){
        //     var model = new BorgWarnerMisSQLContext();
        //     var testData = model.Database.SqlQuery<tChkRes>($"select 调试开始人 as tsMan,调试结束人 as teMan,调试开始时间 as tsTime,调试结束时间 as teTime,调试累计时间 as duration from tblPartTesting where 作业单号 = '{prm.workNo}' ").ToList();
        //     return testData;
        // }

        [HttpPost("[action]")]
        public List<prodRes> BGDprod([FromBody] prodPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<prodRes>($"EXECUTE dbo.QforProdStart @workNo='{prm.workNo}',@workMan='{prm.workman}',@cGroup='{prm.cgroup}',@startTime='{prm.workStime}',@prodTime='{prm.prodStime}'").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<pBGRes> ProdBG([FromBody] pBGPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<pBGRes>($"EXECUTE dbo.QforProdBG @workNo='{prm.workNo}',@proceNo='{prm.proceNo}',@bgTime='{prm.bgTime}',@passNum='{prm.passNum}',@Lf='{prm.Lf}',@Jf='{prm.Jf}',@Df='{prm.Df}',@Gf='{prm.Gf}',@Dcl='{prm.Dcl}',@FailNum='{prm.failNum}',@rePassNum='{prm.rePassNum}',@pManNum='{prm.pManNum}'").ToList();
            return testData;
        }
    }

    //生产报工参数类
    public class pBGPrm{
        public string workNo{get;set;}
        public DateTime bgTime{get;set;}
        public int passNum{get;set;}
        public int Lf{get;set;}
        public int Jf{get;set;}
        public int Df{get;set;}
        public int Gf{get;set;}
        public int Dcl{get;set;}
        public int failNum{get;set;}
        public int rePassNum{get;set;}
        public int pManNum{get;set;}
        public int proceNo{get;set;}
    }

    public class pBGRes{
       public bool resSign{get;set;}
    }

    //生产参数
    public class prodPrm{
        public string workNo{get;set;}
        public string workman{get;set;}
        public string cgroup{get;set;}
        public DateTime workStime{get;set;}
        public DateTime prodStime{get;set;}
    }
    //生产结果
    public class prodRes{
        public bool resSign{get;set;}
    }


    //调试参数
    public class testPrm{
        public string prodline{get;set;}
        public string workNo{get;set;}
        public DateTime sTime{get;set;}
        public DateTime eTime{get;set;}
        public double testTime{get;set;}
        public string sMan{get;set;}
        public string eMan{get;set;}
        public string tip{get;set;}
        public string cgroup{get;set;}
    }


    //调试结果
    public class testRes{
        public bool resSign{get;set;}
    }
    //当前订单参数：产线名
    public class currPrm{
        public string prodLine{get;set;}
    }
    //当前订单数据结果
    public class currRes{
        public string 作业单号{get;set;}
        public string 报工编号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 订单数量{get;set;}
        public int 合格数量{get;set;}
        public int 不合格数量{get;set;}
        public int 完工数量{get;set;}
        public string 不合格单编号{get;set;}
    }
    //所有订单数据结果
    public class allRes{
        
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public DateTime 开始日期{get;set;}
        public DateTime 完工日期{get;set;}
        public int 调试计划用时{get;set;}
        public int 未完工数量{get;set;}
        public decimal 剩余工时{get;set;} 
        public int 订单数量{get;set;}
    }
    //获取报工单信息参数
    public class bgdPrm{
        public string workNo{get;set;}
    }
    //获取报工单结果类
    public class bgdRes{
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public int 定额件数{get;set;}
        public int 定额人数{get;set;}
        public string 报工类别{get;set;}
        public string 生产单元{get;set;}
        public string 工作组{get;set;}
    }
    //创建报工单参数
    public class crtPrm{
        public string workNo{get;set;}
        public string crtMan{get;set;}
        public DateTime crtDate{get;set;}
    }
    //创建报工单结果类
    public class crtRes{
        public bool resSign{get;set;}
    }
    
    //报工单开始时调取信息参数
    public class startPrm{
        public string workunit{get;set;}
        // public string workNo{get;set;}
    }
    //报工单开始结果类
    public class startRes{
        public string bgNo{get;set;}
        public string initMan{get;set;}
        public DateTime crtDate{get;set;}
        public int proceNo{get;set;}
        public string proceName{get;set;}
        public string bgCate{get;set;}
        public int fixNum{get;set;}
        public string workMan{get;set;}
        public string workUnit{get;set;}
        //调试参数
        public DateTime tsTime{get;set;}
        public DateTime teTime{get;set;}
        public string tsMan{get;set;}
        public string teMan{get;set;}
        public double duration{get;set;}
        //生产参数
        public DateTime prodDate{get;set;}
        public DateTime workStime{get;set;}
        public string workBZ{get;set;}
        public int fixMan{get;set;}
        //报工参数,变量名第一个字母不大写
        public DateTime wgTime{get;set;}
        public int passNum{get;set;}
        public int lf{get;set;}
        public int jf{get;set;}
        public int df{get;set;}
        public int gf{get;set;}
        public int dcl{get;set;}
        public int failNum{get;set;}
        public int rePassNum{get;set;}
    }
}