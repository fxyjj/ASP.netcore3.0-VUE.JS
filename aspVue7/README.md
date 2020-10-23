# clientapp

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Run your tests
```
npm run test
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

## Tips Sean make

--run the project:

`dotnet run`

--publish the project:

`dotnet publish -c Release` 后端打包
`npm run build` 前端打包

--SQL setup:
`appsetting.json` 参见 "ConnectionStrings" ,数据库连接信息

## 已实现功能

1：登录拦截，系统需要正确的用户名和密码，否则无法进入系统进行操作

2：OEE界面，表格的显示

3：Andon菜单内个界面功能实现

--按灯信息汇总
两张折线图，可选择日期单位，日期范围和安灯状态来筛选需要的信息

--按灯数量
四张折线图，分别表示四个工作组中各产线安灯数量的对比情况，可通过页面顶部选择日期范围，日期单位和安灯产线状态来筛选需要的数据，也可以通过选择显示或隐藏产线折线图信息，更好的做到目标数据的显示。

--按灯类别
三张折线图和四张饼状图，显示按灯的工时，数量和排班占比，饼状图显示相同信息，方便同事该方面的数据显示和pre。同理可选择对应的日期范围，日期单位和安灯状态来筛选想要的数据。此外，由于饼状图少了一个维度，该界面还有一个滑条来控制饼状图的数据显示，滑条用于控制不同日期单位下的数据分布，结合折线图可以更进一步理解数据。

--TopIssue
两张折现柱状混合图，显示按灯设备的工时，处理时间和安灯次数的分布。日期单位，日期范围和安灯状态也是两张图的筛选条件，可以按自己意愿进行设置。

--安灯状态

实现完毕。在工厂产线分布示意图中加入灯泡图片和闪烁特效，在根据数据库中获取的数据与信号，来控制各条产线的按灯情况。（2020/10/19）

4：OEE界面数据表格轮播功能。

-- 轮播中每隔一个小时自动同步时间，次日清晨8点更新前一天数据到展示面板，不用每天手动更改日期使得最新数据显示到面板。（2020/10/19）

5：登陆界面的密码加密和账号注册功能
。。开始开发（2020/10/19）
`npm install crypto-js` 下载js加密包对密码进行加密处理 (2020/10/20)
`dotnet add package Microsoft.AspNetCore.Cryptography.KeyDerivation --version 3.1.6` 后端对密码进行加密并转存到数据库和验证密码有效性的依赖包 (2020/10/20)
`dotnet add package Microsoft.AspNetCore.NodeServices` 在cs文件中解码前端加密过后的密码，由于前端crypto-js加密相同字符得到不同的密码，在后端接收到密文后需要用相同方法将密文解码再在后端使用pbkdf2算法加密转存入数据库。

6：登陆成功后跳转到新的界面，该界面把不同功能的网页区分开来，并以按钮作为入口点击进入。

7:新增账号管理界面，内含在用户管理，修改密码，新建许可证和搜索等功能，当前开发到用户管理页面（2020/10/23）


