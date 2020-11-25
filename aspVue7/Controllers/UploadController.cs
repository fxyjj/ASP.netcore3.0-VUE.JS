using System;
using System.IO;
using System.Text;
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
using Microsoft.AspNetCore.NodeServices;
using System.Security.Cryptography;
using NPOI.SS.UserModel;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using System.Net.Http.Headers;


namespace aspVue7.Controllers
{
    
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public UploadController(IWebHostEnvironment env)
        {
            this.hostingEnvironment = env;
        }

        [HttpPost("[action]")]
        public List<sheetName> postExcel([FromForm(Name = "file")] IFormFile input){
            var file = input.FileName;
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploadFolder");
            if(!Directory.Exists(uploadsFolder)){
                Directory.CreateDirectory(uploadsFolder);
            }
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + input.FileName;
            string filePath = Path.Combine(uploadsFolder,uniqueFileName);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            input.CopyTo(fs); 
            // fs.Flush();
            fs.Close();
            FileStream fs2 = new FileStream(filePath, FileMode.Open);
            IWorkbook workbook = WorkbookFactory.Create(fs2);
           
            List<sheetName> res = new List<sheetName>();
            for(var i=0;i<workbook.NumberOfSheets;i++){
                var fpath = new sheetName();
                fpath.sname = workbook.GetSheetAt(i).SheetName;
                res.Add(fpath);
            }
            fs2.Close();
            var tempN = new sheetName();
            tempN.sname = filePath;
            res.Add(tempN);
           
            return res;
        }

         [HttpPost("[action]")]
        public List<ArrayList> getExcel([FromBody] snWfs namePrm){
            FileStream fs = new FileStream(namePrm.fs, FileMode.Open);
            IWorkbook workbook = WorkbookFactory.Create(fs);
           
            List<ArrayList> res = new List<ArrayList>();
            List<ArrayList> err = new List<ArrayList>();
            ArrayList errList = new ArrayList();
            errList.Add("表格存在非法字段");
            ArrayList eptSheet = new ArrayList();
            eptSheet.Add("表格无数据");
            ISheet sheet = null;
            for(var i=0;i<workbook.NumberOfSheets;i++){
                sheet = workbook.GetSheetAt(i);
                if(sheet.SheetName == namePrm.sn){
                    break;
                }
            }

            if(sheet.LastRowNum == 0){
                err.Add(eptSheet);
                return err;
            }
            //获取标题行列数
            int index = sheet.GetRow(0).Cells.Count;
            IRow row = null;
            ICell cell = null;
            for(int i = 1; i<=sheet.LastRowNum;i++){
                row = (dynamic) sheet.GetRow(i);
                ArrayList resSub = new ArrayList();
                for(int j = 0;j<index;j++){
                    cell = (dynamic) row.GetCell(j);
                    if(cell == null){
                        resSub.Add("空");
                    }else{
                        if(j == 4 || j == 5 || j == 6){
                            if(cell.CellType == CellType.Numeric){
                                resSub.Add(cell.DateCellValue);
                            }else{
                                errList.Add(i);
                                errList.Add(j);
                                err.Add(errList);
                                return err;
                            }
                            
                        }else if(j == 3){
                            if(cell.CellType == CellType.Numeric){
                                resSub.Add(cell.ToString().Trim());
                            }else{
                                errList.Add(i);
                                errList.Add(j);
                                err.Add(errList);
                                return err;
                            }
                        }else{
                            if(j == 7){
                                if(cell.ToString().Trim()!="EGR阀线" && cell.ToString().Trim()!="Cooler/Module线" && cell.ToString().Trim()!="电子线"){
                                    errList.Add(i);
                                    errList.Add(j);
                                    err.Add(errList);
                                    return err;
                                }
                            }else if(j == 8){
                                if(cell.ToString().Trim()!="创建"){
                                    errList.Add(i);
                                    errList.Add(j);
                                    err.Add(errList);
                                    return err;
                                }
                            }else if(j == 9){
                                 if(cell.ToString().Trim()!="受控订单" && cell.ToString().Trim()!="试制订单"){
                                    errList.Add(i);
                                    errList.Add(j);
                                    err.Add(errList);
                                    return err;
                                }
                            }
                             resSub.Add(cell.ToString().Trim());
                        }
                        
                    }
                }
                res.Add(resSub);
            }
            
            fs.Close();
            // //处理完成后，删除上传的文件
            // if (System.IO.File.Exists(namePrm.fs))
            // {
            //     System.IO.File.Delete(namePrm.fs);
            // }
            return res;
        }

         [HttpPost("[action]")]
        public void delExcel([FromBody] sheetName namePrm){
            if (System.IO.File.Exists(namePrm.sname))
            {
                System.IO.File.Delete(namePrm.sname);
            }
        }

        [HttpPost("[action]")]
        public List<orderRes> createData([FromBody] crtPrm prm){

            var model = new BorgWarnerMisSQLContext();
            FileStream fs = new FileStream(prm.fs, FileMode.Open);
            IWorkbook workbook = WorkbookFactory.Create(fs);
           
            List<ArrayList> res = new List<ArrayList>();
            ISheet sheet = null;
            for(var i=0;i<workbook.NumberOfSheets;i++){
                sheet = workbook.GetSheetAt(i);
                if(sheet.SheetName == prm.sn){
                    break;
                }
            }

            //获取标题行列数
            int index = sheet.GetRow(0).Cells.Count;
            IRow row = null;
            var td = new DataTable();
            for(int i = 1; i<=sheet.LastRowNum;i++){
                row = (dynamic) sheet.GetRow(i);
                var container = new ctr();
                ICell c1 = (dynamic) row.GetCell(0);
                container.ordNo = (c1 == null)?"":c1.ToString().Trim();
                ICell c2 = (dynamic) row.GetCell(1);
                container.wlNo = (c2 == null)?"":c2.ToString().Trim();
                ICell c3 = (dynamic) row.GetCell(2);
                container.wlDesc = (c3 == null)?"":c3.ToString().Trim();
                ICell c4 = (dynamic) row.GetCell(3);
                container.ordNum = int.Parse(c4.ToString().Trim());
                ICell c5 = (dynamic) row.GetCell(4);
                container.sTime = c5.DateCellValue;
                ICell c6 = (dynamic) row.GetCell(5);
                container.eTime = c6.DateCellValue;
                ICell c7 = (dynamic) row.GetCell(6);
                container.ordTime = c7.DateCellValue;
                ICell c8 = (dynamic) row.GetCell(7);
                container.station = (c8 == null)?"":c8.ToString().Trim();
                ICell c9 = (dynamic) row.GetCell(8);
                container.status = (c9 == null)?"":c9.ToString().Trim();
                ICell c10 = (dynamic) row.GetCell(9);
                container.ordType = (c10 == null)?"":c10.ToString().Trim();
                ICell c11 = (dynamic) row.GetCell(10);
                container.tip = (c11 == null)?"":(c11).ToString().Trim();
                //TODO..
                //重复订单号检验！！！
                td = model.Database.SqlQuery($"EXECUTE dbo.QforWebNewOrder @ordNo='{container.ordNo}',@wlNo='{container.wlNo}',@wlDesc='{container.wlDesc}',@ordNum='{container.ordNum}',@sTime='{container.sTime}',@eTime='{container.eTime}',@ordTime='{container.ordTime}',@station='{container.station}',@status='{container.status}',@ordType='{container.ordType}',@tip='{container.tip}' ");
              
            }
            if (System.IO.File.Exists(prm.sn))
            {
                System.IO.File.Delete(prm.sn);
            }
            var testData = model.Database.SqlQuery<orderRes>($"EXECUTE dbo.QforWebOrderRelease @prodLine='{prm.pline}',@srh='{prm.srhCont}' ").ToList();
            return testData;
        }

        //具体表格名
        public class sheetName{
            public string sname{get;set;}
        }
        //返回的具体表名和文件路径
        public class snWfs{
            public string sn{get;set;}
            public string fs{get;set;}
        }
        //上传的返回结果
        public class exlData
        {
            public string usrname{get;set;}
            public string name{get;set;}
            public string ctfct{get;set;}
            public string date{get;set;}
        }
        //添加完成后的信号
        public class newRes{
            public bool resSign{get;set;}
        }
        //更新表格参数
        public class crtPrm{
             public string sn{get;set;}
            public string fs{get;set;}
            public string pline{get;set;}
            public string srhCont{get;set;}
        }
        //表格元素储存容器
        public class ctr{
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
        //更新tab
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
    }
}