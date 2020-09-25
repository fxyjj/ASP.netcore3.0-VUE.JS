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
此界面还在开发过程中。。。

4：OEE界面数据表格轮播功能。





