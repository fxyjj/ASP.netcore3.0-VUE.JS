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
        public List<blpStoreRes> closeOrd([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforCloseOrder @workNo='{prm.workNo}'").ToList();
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

        //计划停机记录录入
        [HttpPost("[action]")]
        public List<stopRes> planStop([FromBody] stopPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<stopRes>($"EXECUTE dbo.QforPlanStop @workNo='{prm.workNo}',@stopType='{prm.stopType}',@sStart='{prm.stopBegin}',@eTime='{prm.stopEnd}',@sMan='{prm.sMan}',@tips='{prm.tips}'").ToList();
            return testData;
        }

        //非计划停机记录录入
        [HttpPost("[action]")]
        public List<stopRes> unPlanStop([FromBody] stopPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<stopRes>($"EXECUTE dbo.QforUnPlanStop @workNo='{prm.workNo}',@stopType='{prm.stopType}',@stopsType='{prm.stopsType}',@sbNo='{prm.sbNo}',@stopDesc='{prm.stopDesc}',@sMan='{prm.sMan}',@sStart='{prm.stopBegin}'").ToList();
            return testData;
        }

        //停机记录查询
        [HttpPost("[action]")]
        public List<logRes> stopLog([FromBody] stopPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<logRes>($"select * from qforStopLog where 报工编号='{prm.workNo}'").ToList();
            return testData;
        }
        //停机继续
        [HttpPost("[action]")]
        public List<stopRes> pContinue([FromBody] stopPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<stopRes>($"EXECUTE dbo.QforPContinue @workNo='{prm.workNo}',@stopTime='{prm.stopEnd}'").ToList();
            return testData;
        }
         //报工结束
        [HttpPost("[action]")]
        public List<stopRes> BGClose([FromBody] bgClosePrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<stopRes>($"EXECUTE dbo.QforBGDClose @bgNo='{prm.workNo}',@testT='{prm.testT}',@startT='{prm.startT}'").ToList();
            return testData;
        }

        //不良品记录表单开启
        [HttpPost("[action]")]
        public List<blpOpenRes> BLPopen([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpOpenRes>($"EXECUTE dbo.QforBLPOpen @workNo='{prm.workNo}'").ToList();
            return testData;
        }
        //不良品记录保存
        [HttpPost("[action]")]
        public List<blpStoreRes> BLPstore([FromBody] blpStorePrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforBLPStore @workNo='{prm.workNo}',@wlNo='{prm.wlNo}',@wlDesc='{prm.wlDesc}',@pLine='{prm.pLine}',@man='{prm.man}',@date='{prm.date}',@Dpart='{prm.Dpart}',@Cgroup='{prm.Cgroup}',@bgNo='{prm.bgNo}',@tabID='{prm.tabID}',@tabwlNo='{prm.tabwlNo}',@tabwlDesc='{prm.tabwlDesc}',@tabNum='{prm.tabNum}',@tabPos='{prm.tabPos}',@tabFDesc='{prm.tabFDesc}',@tabRPsNum='{prm.tabRPsNum}',@tabPreason='{prm.tabPreason}',@tabTip='{prm.tabTip}'").ToList();
            return testData;
        }

         //不良品记录添加
        [HttpPost("[action]")]
        public List<blpStoreRes> BLPAdd([FromBody] blpStorePrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforBLPAdd @workNo='{prm.workNo}',@tabID='{prm.tabID}',@tabwlNo='{prm.tabwlNo}',@tabwlDesc='{prm.tabwlDesc}',@tabNum='{prm.tabNum}',@tabPos='{prm.tabPos}',@tabFDesc='{prm.tabFDesc}',@tabRPsNum='{prm.tabRPsNum}',@tabPreason='{prm.tabPreason}',@tabTip='{prm.tabTip}'").ToList();
            return testData;
        }
        //不良品表格更新
        [HttpPost("[action]")]
        public List<tabRes> getTab([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<tabRes>($"EXECUTE dbo.QforBLPTabreNew @workNo='{prm.workNo}'").ToList();
            return testData;
        }
         //质量安灯打开查询
        [HttpPost("[action]")]
        public List<zlOpenRes> zlAndonOpen([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<zlOpenRes>($"EXECUTE dbo.QforZLAndonOpen @workNo='{prm.workNo}'").ToList();
            return testData;
        }
        //质量安灯确认查询
        [HttpPost("[action]")]
        public List<blpStoreRes> zlAndonCfm([FromBody] zlCfmPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforZLAndon @workNo='{prm.workNo}',@order='{prm.order}',@fillMan='{prm.fillMan}',@Dpt='{prm.dpt}',@Cls='{prm.cls}',@Num='{prm.num}',@wlNo='{prm.wlNo}',@wlDesc='{prm.wlDesc}',@pos='{prm.pos}',@qDesc='{prm.qDesc}',@repassNum='{prm.rePsnum}',@sbNo='{prm.sbNo}',@reason='{prm.reason}',@tips='{prm.tips}',@andonMan='{prm.andonMan}'").ToList();
            return testData;
        }
         //设备安灯确认查询
        [HttpPost("[action]")]
        public List<blpStoreRes> sbAndonCfm([FromBody] sbCfmPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforSBAndon @workNo='{prm.workNo}',@priority='{prm.priority}',@sbStop='{prm.sbStop}',@sbMan='{prm.sbMan}',@sbDpt='{prm.sbDpt}',@sbNo='{prm.sbNo}',@sbName='{prm.sbName}',@station='{prm.station}',@qPart='{prm.qPart}',@qType='{prm.qType}',@qDesc='{prm.qDesc}',@sbSafe='{prm.sbSafe}'").ToList();
            return testData;
        }
         //其他安灯确认查询
        [HttpPost("[action]")]
        public List<blpStoreRes> othAndonCfm([FromBody] othAndonPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforWebAndon @workNo='{prm.workNo}',@andonMan='{prm.adMan}',@andonType='{prm.adType}',@station='{prm.adStation}',@andonStop='{prm.adStop}',@andonsbNo='{prm.adsbNo}',@andonQdesc='{prm.adQdesc}'").ToList();
            return testData;
        }
          //内圈数据更新查询
        [HttpPost("[action]")]
        public List<reNewAdRes> reNewADLog([FromBody]  blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<reNewAdRes>($"EXECUTE dbo.QforRenewAndonCount @workNo='{prm.workNo}'").ToList();
            return testData;
        }
        //安灯记录打开查询
        [HttpPost("[action]")]
        public List<adLogRes> AdLogOpen([FromBody]  adLogPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<adLogRes>($"EXECUTE dbo.QforWebAndonLog @workNo='{prm.workNo}',@qType='{prm.qType}'").ToList();
            return testData;
        }
        //安灯记录刷新查询
        [HttpPost("[action]")]
        public List<adLogTabRes> reNewAdtab([FromBody]  adLogPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<adLogTabRes>($"EXECUTE dbo.QforRenewAndonLog @workNo='{prm.workNo}',@qType='{prm.qType}'").ToList();
            return testData;
        }
        //安灯响应
        [HttpPost("[action]")]
        public List<blpStoreRes> adResp([FromBody]  adRespPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforAndonResponse @prcsMan='{prm.respMan}',@workNo='{prm.workNo}',@qType='{prm.qtype}'").ToList();
            return testData;
        }

        //安灯解决
        [HttpPost("[action]")]
        public List<blpStoreRes> adResolve([FromBody]  adResolvePrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforAndonResolve @solveMan='{prm.solveMan}',@workNo='{prm.workNo}',@qType='{prm.qtype}',@tmpSolve='{prm.tmpSolve}',@fReason='{prm.freason}',@plan='{prm.plan}',@planfnsDate='{prm.planfnsDate}'").ToList();
            return testData;
        }
         //安灯关闭
        [HttpPost("[action]")]
        public List<blpStoreRes> adCfm([FromBody]  adCfmPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"EXECUTE dbo.QforAndonComfirm @cfmMan='{prm.cfmMan}',@workNo='{prm.workNo}',@qType='{prm.qtype}'").ToList();
            return testData;
        }
        //员工信息
         [HttpPost("[action]")]
        public List<empInfoRes> empInfo([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<empInfoRes>($"execute dbo.QforEmployeeSrh @line='{prm.workNo}'").ToList();
            return testData;
        }
        
         //员工信息
         [HttpGet("[action]")]
        public List<empInfoRes> optr2Info(){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<empInfoRes>("SELECT [Name] as name,[Password] as pwd FROM tblemployees WHERE(Job=N'MT' or Job=N'ME') order by [Name]").ToList();
            return testData;
        }

         //设备编号信息
         [HttpPost("[action]")]
        public List<sbInfoRes> sbInfo([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<sbInfoRes>($"select 设备编号, 设备名称 from tblEquipmentInformation where 使用区域='{prm.workNo}' ORDER BY 设备编号").ToList();
            return testData;
        }
        //部门信息
        [HttpGet("[action]")]
        public List<dptInfoRes> dptInfo(){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<dptInfoRes>("select 部门代码, 部门名称 from tblDept").ToList();
            return testData;
        }
        //班组信息
        [HttpPost("[action]")]
        public List<clsInfoRes> clsInfo([FromBody] blpOpenPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<clsInfoRes>($"select 班组名称 from tblShift where 所属车间='{prm.workNo}'").ToList();
            return testData;
        }

        //故障类型
        [HttpGet("[action]")]
        public List<gzTInfoRes> gzTInfo(){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<gzTInfoRes>("select 故障类型 from tblMalfunctionType").ToList();
            return testData;
        }
        //故障部位
        [HttpGet("[action]")]
        public List<gzPInfoRes> gzPInfo(){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<gzPInfoRes>("select 故障部位 from tblMalfunctionPosition").ToList();
            return testData;
        }
        //子物料信息
        // [HttpGet("[action]")]
        // public List<zwlInfoRes> zwlInfo(){
        //     var model = new BorgWarnerMisSQLContext();
        //     var testData = model.Database.SqlQuery<zwlInfoRes>("select top(20) Material as wlNo,[Material Description] as wlDesc from tblSAPMaterial").ToList();
        //     return testData;
        // }
        //工位信息
        // [HttpGet("[action]")]
        // public List<gwInfoRes> gwInfo(){
        //     var model = new BorgWarnerMisSQLContext();
        //     var testData = model.Database.SqlQuery<gwInfoRes>("select 工位,工位描述 from tblPDStation").ToList();
        //     return testData;
        // }
        //报工单总览信息
        [HttpPost("[action]")]
        public List<BGDViewRes> BGDView([FromBody] BGDViewPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<BGDViewRes>($"select * from qforOrderDailySheetAS where 加工单元='{prm.line}' and cast(班次日期 as date) between cast(dbo.dateTransfer('{prm.pdateS}') as date) and cast(dbo.dateTransfer('{prm.pdateE}') as date) order by 班次日期 desc").ToList();
            return testData;
        }
        //报工单编辑信息
        [HttpPost("[action]")]
        public List<blpStoreRes> editCfm([FromBody] BGDeditPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"execute QforBGDEdit @bgNo='{prm.bgNo}',@prodDate='{prm.prodDate}',@clsDate='{prm.clsDate}',@cls='{prm.cls}',@workMan='{prm.workMan}',@testStime='{prm.testStime}',@testEtime='{prm.testEtime}',@workStime='{prm.workStime}',@workEtime='{prm.workEtime}',@planStime='{prm.planStime}',@unplanStime='{prm.unplanStime}',@doneNum='{prm.doneNum}',@passNum='{prm.passNum}',@failNum='{prm.failNum}',@lf='{prm.lf}',@jf='{prm.jf}',@df='{prm.df}',@gf='{prm.gf}',@dcl='{prm.dcl}',@prodMan='{prm.prodMan}',@repassNum='{prm.repassNum}'").ToList();
            return testData;
        }
        [HttpPost("[action]")]
        public List<blpStoreRes> delBGD([FromBody] BGDeditPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<blpStoreRes>($"execute QforBGDDelete @bgNo='{prm.bgNo}'").ToList();
            return testData;
        }
        [HttpPost("[action]")]
        public List<failRec> getFailLog([FromBody] tabPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<failRec>($"select * from dbo.qforFailRec where 报工编号='{prm.bgNo}'").ToList();
            return testData;
        }
        [HttpPost("[action]")]
        public List<stopRec> getStopLog([FromBody] tabPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<stopRec>($"select * from dbo.qforOrderEDownRecordBrowse where 报工编号='{prm.bgNo}'").ToList();
            return testData;
        }
    }

    public class tabPrm{
        public string bgNo{get;set;}
    }
    public class BGDeditPrm{
        public  string bgNo{get;set;}
        public DateTime prodDate{get;set;}
        public DateTime clsDate{get;set;}
        public string cls{get;set;}
        public string workMan{get;set;}
        public DateTime testStime{get;set;}
        public DateTime workStime{get;set;}
        public DateTime workEtime{get;set;}
        public double planStime{get;set;}
        public double unplanStime{get;set;}
        public double doneNum{get;set;}
        public double passNum{get;set;}
        public double failNum{get;set;}
        public double lf{get;set;}
        public double jf{get;set;}
        public double df{get;set;}
        public double gf{get;set;}
        public double dcl{get;set;}
        public int prodMan{get;set;}
        public DateTime testEtime{get;set;}
        public double repassNum{get;set;}
    }
    //报工单总览参数
    public class BGDViewPrm{
        public string  line{get;set;}
        public string pdateS{get;set;}
        public string pdateE{get;set;}
    }
    //报工单总览结果类
    public class BGDViewRes{
        public string 报工编号{get;set;}
        public DateTime 生产日期{get;set;}
        public DateTime 班次日期{get;set;}
        public string 加工单元{get;set;}
        public string 所属班组{get;set;}
        public string 加工人员{get;set;}
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public int 工序号{get;set;}
        public string 工序名称{get;set;}
        public int 定额件数{get;set;}
        public DateTime 调试开始时间{get;set;}
        public double 调试时间{get;set;}
        public DateTime 作业开始时间{get;set;}
        public DateTime 作业完工时间{get;set;}
        public double 计划停机时间{get;set;}
        public double 非计划停机时间{get;set;}
        public double 完工数量{get;set;}
        public double 合格数量{get;set;}
        public double 不良数量{get;set;}
        public double 料废{get;set;}
        public double 机废{get;set;}
        public double 调废{get;set;}
        public double 工废{get;set;}
        public double 待处理{get;set;}
        public double 理论时间{get;set;}
        public double 计划工作时间{get;set;}
        public double 操作时间{get;set;}
        public double 定额产量{get;set;}
        public double 合格率{get;set;}
        public double 员工效率{get;set;}
        public double 设备开动率{get;set;}
        public int 生产人数{get;set;}
        public int 定额人数{get;set;}
        public bool oee{get;set;}
        public string 工作组{get;set;}
         public bool 状态{get;set;}
        public string 报工类别{get;set;}
        public DateTime 调试结束时间{get;set;}
        public double 调试到完工{get;set;}
        public double 作业到完工{get;set;}
        public string 订单类型{get;set;}
        public double 返工合格数量{get;set;}
        public double 一次合格率{get;set;}
        public double 停机率{get;set;}
    }
    //故障类型
    public class gzTInfoRes{
        public string 故障类型{get;set;}
    }
    //故障部位
    public class gzPInfoRes{
        public string 故障部位{get;set;}
    }
    //工位信息
    public class gwInfoRes{
        public string 工位{get;set;}
        public string 工位描述{get;set;}
    }
    //子物料
    public class zwlInfoRes{
        public string wlNo{get;set;}
        public string wlDesc{get;set;}
    }
    //班组信息
    public class clsInfoRes{
        public string 班组名称{get;set;}
    }
    //部门信息
    public class dptInfoRes{
        public string 部门代码{get;set;}
        public string 部门名称{get;set;}
    }
    //设备编号信息
    public class sbInfoRes{
        public string 设备编号{get;set;}
        public string 设备名称{get;set;}
    }
    //员工信息
    public class empInfoRes{
        public string name{get;set;}
        public string pwd{get;set;}
    }
    //按灯确认参数
    public class adCfmPrm{
        public string workNo{get;set;}
        public int qtype{get;set;}
        public string cfmMan{get;set;}
    }
    //按灯解决参数
    public class adResolvePrm{
        public string workNo{get;set;}
        public string solveMan{get;set;}
        public int tmpSolve{get;set;}
        public string freason{get;set;}
        public string plan{get;set;}
        public DateTime planfnsDate{get;set;}
        public int qtype{get;set;}
    }
    //按灯响应参数
    public class adRespPrm{
        public string respMan{get;set;}
        public string workNo{get;set;}
        public int qtype{get;set;}
    }
    //按灯记录打开查询参数
    public class adLogPrm{
        public string workNo{get;set;}
        public int qType{get;set;}
    }
    //安灯记录结果
    public class adLogRes{
        public int adType{get;set;}
        public int adSts{get;set;}
        public DateTime adTime{get;set;}
        public string adqDesc{get;set;}
        public string adMan{get;set;}
        public string adNo{get;set;}
        public string adSolveMan{get;set;}
        public string adRespMan{get;set;}
        public string adCfmMan{get;set;}
    }
    //安灯记录表格刷新结果类
    public class adLogTabRes{
        public DateTime 按灯时间{get;set;}
        public string 按灯人{get;set;}
        public string 设备编号{get;set;}
        public int 序号{get;set;}
        public string 问题描述{get;set;}
        public DateTime 处理时间{get;set;}
        public string 处理人{get;set;}
        public string 关闭人{get;set;}
        public DateTime 关闭时间{get;set;}
        public string 工作中心{get;set;}
        public string 是否停线{get;set;}
        public bool 临时解决{get;set;}
        public string 行动计划{get;set;}
        public string 不能解决原因{get;set;}
        public DateTime 计划完成日期{get;set;}
    }
    //按灯数据更新查询
    public class reNewAdRes{
        public bool resSign{get;set;}
        public int zL{get;set;}
        public int sB{get;set;}
        public int gY{get;set;}
        public int wL{get;set;}
        public int tBD{get;set;}
    }
    //其他按灯类型参数
    public class othAndonPrm{
        public string workNo{get;set;}
        public string adType{get;set;}
        public string adStation{get;set;}
        public string adStop{get;set;}
        public string adMan{get;set;}
        public string adQdesc{get;set;}
        public string adsbNo{get;set;}
    }
    //设备按灯确认查询参数
    public class sbCfmPrm{
        public string workNo{get;set;}
        public string priority{get;set;}
        public string sbStop{get;set;}
        public string sbMan{get;set;}
        public string sbDpt{get;set;}
        public string sbNo{get;set;}
        public string sbName{get;set;}
        public string station{get;set;}
        public string qPart{get;set;}
        public string qType{get;set;}
        public string qDesc{get;set;}
        public string sbSafe{get;set;}
    }
    //质量按灯确认查询参数
    public class zlCfmPrm{
        public string workNo{get;set;}
        public int order{get;set;}
        public string fillMan{get;set;}
        public string dpt{get;set;}
        public string cls{get;set;}
        public int num{get;set;}
        public string wlNo{get;set;}
        public string wlDesc{get;set;}
        public string pos{get;set;}
        public string qDesc{get;set;}
        public int rePsnum{get;set;}
        public string sbNo{get;set;}
        public string reason{get;set;}
        public string tips{get;set;}
        public string andonMan{get;set;}

    }

    //质量安灯打开时查询结果
    public class zlOpenRes{
        public string 发现部门{get;set;}
        public string 发现班组{get;set;}
        public int 序号{get;set;}
        public string 填写人{get;set;}
    }
    //不良品表格更新结果类
    public class tabRes{
        public int id{get;set;}
        public string wlNo{get;set;}
        public string wlDesc{get;set;}
        public int failNum{get;set;}
        public string position{get;set;}
        public string failDesc{get;set;}
        public int repassNum{get;set;}
        public string reason{get;set;}
        public string tips{get;set;}
    }
    //不良品储存记录参数
    public class blpStorePrm{
        public string workNo{get;set;}
        public string wlNo{get;set;}
        public string wlDesc{get;set;}
        public string pLine{get;set;}
        public string man{get;set;}
        public DateTime date{get;set;}
        public string Dpart{get;set;}
        public string Cgroup{get;set;}
        public string bgNo{get;set;}
        public int tabID{get;set;}
        public string tabwlNo{get;set;}
        public string tabwlDesc{get;set;}
        public int tabNum{get;set;}
        public string tabPos{get;set;}
        public string tabFDesc{get;set;}
        public int tabRPsNum{get;set;}
        public string tabPreason{get;set;}
         public string tabTip{get;set;}
    }
    //不良品储存记录结果
    public class blpStoreRes{
        public bool resSign{get;set;}
    }
    //不良品记录打开时查询参数
    public class blpOpenPrm{
        public string workNo{get;set;}
    }
    //不良品记录打开时查询结果
    public class blpOpenRes{
        public string 作业单号{get;set;}
        public string 物料编号{get;set;}
        public string 物料描述{get;set;}
        public string 生产线{get;set;}
        public string 填写人{get;set;}
        public DateTime 填写日期{get;set;}
        public string 发现部门{get;set;}
        public string 发现班组{get;set;}
        public string bgNo{get;set;}
        public int proceNo{get;set;}
        public string proceName{get;set;}

        public int blpId{get;set;} 
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
        //停机记录查询
        public string sType{get;set;}
        public string sDesc{get;set;}
        public string sMan{get;set;}
    }

    //关闭工单参数
    public class bgClosePrm{
        public string workNo{get;set;}
        public DateTime testT{get;set;}
        public DateTime startT{get;set;}
    }

    //非/计划停机&记录 参数
    public class stopPrm{
        public string workNo{get;set;}
        public string stopType{get;set;}
        public string stopsType{get;set;}
        public DateTime stopBegin{get;set;}
        public DateTime stopEnd{get;set;}
        public string sMan{get;set;}
        public string sbNo{get;set;}
        public string stopDesc{get;set;}
        public string tips{get;set;}
    }
    //非/计划停机结果
    public class stopRes{
        public bool resSign{get;set;}
    }
    //停机记录结果
    public class logRes{
        public string 停机类型{get;set;}
        public string 停机描述{get;set;}
        public DateTime 停机开始{get;set;}
        public DateTime 停机结束{get;set;}
        public int 调试暂停{get;set;}
        public string 填写人{get;set;}
        public string 停机小类{get;set;}
        public string 设备编号{get;set;}
        public int 停机时间{get;set;}
        public string 状态{get;set;}
    }
    
}