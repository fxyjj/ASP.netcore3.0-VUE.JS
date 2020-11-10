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


namespace aspVue7.Controllers
{
    
    [Route("api/[controller]")]
    public class UploadController : Controller
    {

        [HttpPost("[action]")]
        public List<sheetName> postExcel([FromForm(Name = "file")] IFormFile input){
            var file = input.FileName;
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploadFolder");
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
            ISheet sheet = null;
            for(var i=0;i<workbook.NumberOfSheets;i++){
                sheet = workbook.GetSheetAt(i);
                if(sheet.SheetName == namePrm.sn){
                    break;
                }
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
                         resSub.Add(cell.ToString().Trim());
                    }
                }
                res.Add(resSub);
            }
            
            fs.Close();
            //处理完成后，删除上传的文件
            if (System.IO.File.Exists(namePrm.fs))
            {
                System.IO.File.Delete(namePrm.fs);
            }
            return res;
        }

         [HttpPost("[action]")]
        public void delExcel([FromBody] sheetName namePrm){
            if (System.IO.File.Exists(namePrm.sname))
            {
                System.IO.File.Delete(namePrm.sname);
            }
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
    }
}