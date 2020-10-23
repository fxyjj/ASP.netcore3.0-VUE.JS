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
using Microsoft.AspNetCore.NodeServices;


namespace aspVue7.Controllers
{
    
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly INodeServices _services;

        public LoginController(INodeServices service)
        {
            _services = service;
        }

        // [HttpPost]
        [HttpPost("[action]")]
        public async Task<bool> validate([FromBody] usrInfo usrname)
        {
            
            var rng = new Random();
            var model = new BorgWarnerMisSQLContext();
            var usr = usrname.username;
            var testData = model.Database.SqlQuery<usrDBO>($"select Password from dbo.tblWebUser where UsrName='{usr}' ").ToList();
             //前端密文解密
            string oriText = await _services.InvokeAsync<string>("./scripts/decrypt",usrname.happyword);
            bool validate = PasswordHasher.VerifyHashedPassword(oriText,testData[0].Password);

            return validate;
        }

        [HttpPost("[action]")]
        public async Task<List<regRes>> registration([FromBody] regParam info)
        {
            
            var rng = new Random();
            var model = new BorgWarnerMisSQLContext();
            //前端密文解密
            string oriText = await _services.InvokeAsync<string>("./scripts/decrypt",info.happyword);
            //密码加密
            var cipherText = PasswordHasher.HasPassword(oriText);
            
            var testData = model.Database.SqlQuery<regRes>($"EXECUTE dbo.QforAccountValidate @name='{info.name}',@username='{info.username}',@pswd='{cipherText}',@token='{info.token}' ").ToList();
            return testData;
        }

        public class usrInfo{
            public string username{get;set;}
            public string happyword{get;set;}
        }

        public class usrDBO{
            public string Password {get;set;}
        }

        //注册参数
        public class regParam{
            public string name{get;set;}
            public string token{get;set;}
            public string username{get;set;}
            public string happyword{get;set;}
        }

        //注册结果
        public class regRes{
            public int 令牌有效性{get;set;}
            public int 令牌使用情况{get;set;}

            public int 用户名使用情况{get;set;}
        }

    }
}