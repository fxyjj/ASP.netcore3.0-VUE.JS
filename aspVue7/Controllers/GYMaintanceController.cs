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