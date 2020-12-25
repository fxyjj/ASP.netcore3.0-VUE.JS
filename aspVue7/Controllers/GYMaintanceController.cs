using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using aspVue7.Model;

namespace aspVue7.Controllers
{
    [Route("api/[controller]")]
    public class GYMaintanceController:Controller
    {
        [HttpGet("[action]")]
        public List<res> getData(){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<res>("Select * from qforRoute").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<res> srh([FromBody] gysrhPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<res>($"execute dbo.QforSrhRoute @srhcont='{prm.srhcont}'").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<gydelRes> GYdelete([FromBody] gydelPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<gydelRes>($"execute dbo.QforGYdelete @wlNo='{prm.wlNo}'").ToList();
            return testData;
        }

        [HttpPost("[action]")]
        public List<gydelRes> editTab([FromBody] gyeditPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<gydelRes>($"execute dbo.QforGYEdit @wlNo='{prm.wlNo}',@station='{prm.station}',@planCT='{prm.planCT}',@wlDesc='{prm.wlDesc}',@opac='{prm.opAc}',@oprTxt='{prm.oprTxt}',@bNum='{prm.bNum}',@cGroup='{prm.cGroup}',@type='{prm.type}',@stdVal='{prm.stdVal}',@binCap='{prm.binCap}',@bchNum='{prm.bchNum}',@prt='{prm.prt}',@subsidy='{prm.subsidy}',@coeff='{prm.coeff}'").ToList();
            return testData;
        }
        [HttpPost("[action]")]
        public List<gydelRes> newTab([FromBody] gyeditPrm prm){
            var model = new BorgWarnerMisSQLContext();
            var testData = model.Database.SqlQuery<gydelRes>($"execute dbo.QforGYAdd @wlNo='{prm.wlNo}',@station='{prm.station}',@planCT='{prm.planCT}',@wlDesc='{prm.wlDesc}',@opac='{prm.opAc}',@oprTxt='{prm.oprTxt}',@bNum='{prm.bNum}',@cGroup='{prm.cGroup}',@type='{prm.type}',@stdVal='{prm.stdVal}',@binCap='{prm.binCap}',@bchNum='{prm.bchNum}',@prt='{prm.prt}',@subsidy='{prm.subsidy}',@coeff='{prm.coeff}'").ToList();
            return testData;
        }
    }

    public class gyeditPrm{
        public string wlNo{get;set;}
        public string station{get;set;}
        public int planCT{get;set;}
        public string wlDesc{get;set;}
        public  int opAc{get;set;}
        public string oprTxt{get;set;}
        public int bNum{get;set;}
        public string cGroup{get;set;}
        public string type{get;set;}
        public int stdVal{get;set;}
        public int binCap{get;set;}
        public int bchNum{get;set;}
        public int prt{get;set;}
        public int subsidy{get;set;}
        public double coeff{get;set;}
    }

    public class gydelRes{
        public bool resSign{get;set;}
    }

    public class gydelPrm{
        public string wlNo{get;set;}
    }

    public class gysrhPrm{
        public string srhcont{get;set;}
    }

    public class res{
        public string wlNo{get;set;}
        public string station{get;set;}
        public int planCT{get;set;}
        public string wlDesc{get;set;}
        public string opAc{get;set;}
        public string oprTxt{get;set;}
        public int bNum{get;set;}
        public string cGroup{get;set;}
        public string type{get;set;}
        public int stdVal{get;set;}
        public int binCap{get;set;}
        public int bchNum{get;set;}
        public bool print{get;set;}
        public decimal subsidy{get;set;}
        public double coeff{get;set;}
    }
}