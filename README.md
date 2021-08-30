# Vue + AspNetCore Project
***
## env:

Vue 2.x
AspNetCore 3.0
Microsoft SQL Management Server

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
***
## Tips Sean make

### run the project:

`dotnet run`

### publish the project:

`dotnet publish -c Release` 后端打包, backend pack command
`npm run build` 前端打包, frontend pack commend

### SQL setup:
~~`appsetting.json` 参见 "ConnectionStrings" ,数据库连接信息~~  **数据库数据为私密数据，所提到文件中的数据库连接脚本不再有效，数据库连接需根据自己的数据库用户名和密码进行连接**
**DB is private, all info referring to DA is no more valid, you should connect the DB according to own account and pwd.**
***
## Project Log

### 1：登录拦截，系统需要正确的用户名和密码，否则无法进入系统进行操作
登陆界面(如图所示)，用户可以在此输入合法的用户名和密码登陆系统并访问相关页面

Log in authentication, now system need correct user name and pwd, otherwise it will failed to log in and show error information, user can input their valid username and pwd to get access to the system.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/登录页面.png)

***

新用户也可通过注册按钮进入注册页面进行新账号的注册，注册前需获取注册许可证（保证账号与系统安全）
new user is allowed to register a new account in this page. registeration need permission code. 

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/注册页面.png)

***

没有许可证是无法注册新账号的，下图为注册失败和成功的显示。其中注册失败有两种情况，用户名重复和许可证过期。

new account cannot be created without a permission code, success and failed creation UI are displayed below.
There are two reasons if creation is failed:
    1: invalid permission code (lack || wrong || out of date)
    3: repeat username

**失败1:许可证失效或不合格**
**fail case1: invalid permission**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/注册失败2.png)

**失败2:用户名重复**
**fail case2: repeat username**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/注册失败1.png)

**成功**
**success case**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/注册成功.png)

**密码修改**
**pwd change**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/重设密码.png)

### 2：OEE界面，表格的显示 (OEE page display, with charts)

OEE页面实现四个柱状图的显示 
OEE page implementation with four column diagrams

*图表背后的数据基于数据库。*
*diagrams are based on database*

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/oee界面.png)

### 3：Andon菜单内各界面功能实现 (function descriptions inside Andon menu )

**按灯信息汇总 (Andon info consolidation)**


两张折线图，可选择日期单位，日期范围和安灯状态来筛选需要的信息
two folding line diagrams, data changing as the selection of different date units, dates and andon status.

**按灯数量 (Andon Number)**


四张折线图，分别表示四个工作组中各产线安灯数量的对比情况，可通过页面顶部选择日期范围，日期单位和安灯产线状态来筛选需要的数据，也可以通过选择显示或隐藏产线折线图信息，更好的做到目标数据的显示。
Four folding line diagrams, each of which stands for one product line, and it showed the andon numbers inside the line, also, diagrams are dynamic as the selection of date range, date units and andon type, moreover, user are able to choose the folding line which he/she wants to see in the diagrams to do better analyse.

**按灯类别(Andon Category)**

三张折线图和四张饼状图，显示按灯的工时，数量和排班占比，饼状图显示相同信息，方便同事该方面的数据显示和pre。同理可选择对应的日期范围，日期单位和安灯状态来筛选想要的数据。此外，由于饼状图少了一个维度，该界面还有一个滑条来控制饼状图的数据显示，滑条用于控制不同日期单位下的数据分布，结合折线图可以更进一步理解数据。
three folding line diagrams and four pieograms, displaying Andon hours, numbers and work schadule rate, pieograms shows the same infomation, just help users analyse the daily data. All the function referred at above diagrams are also implied here.

**TopIssue (Page Name)**

两张折线柱状混合图，显示按灯设备的工时，处理时间和安灯次数的分布。日期单位，日期范围和安灯状态也是两张图的筛选条件，可以按自己意愿进行设置。
Two folding line & column daigrams displyed Andon equipments working hour, response time and andon number. All the functions referred at above diagrams are also implied here.


**安灯状态 (Andon Status)**

实现完毕。在工厂产线分布示意图中加入灯泡图片和闪烁特效，在根据数据库中获取的数据与信号，来控制各条产线的按灯情况。（2020/10/19）
completed. Add light bulb pictures and flash effects to the factory line distribution diagram to control the press of each production line according to the data and signals obtained in the database.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/安灯状态.png)

### 4：OEE界面数据表格轮播功能。(Charts elements rotation in OEE page)

产线需要时刻查看各工作单元数据，此处更新了图表轮播的功能，方便数据的自动更新。
The production line colleagues need to check the data of each work unit at all times. Here, the function of chart rotation is updated to facilitate automatic data updating.


![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/轮播界面.png)

用户可预先设置轮播条件。
Users can set the rotation conditions in advance.


![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/轮播选项.png)

-- 轮播中每隔一个小时自动同步时间，次日清晨8点更新前一天数据到展示面板，不用每天手动更改日期使得最新数据显示到面板。（2020/10/19）
In the carousel, the time is automatically synchronized every hour, and the data of the previous day is updated to the display panel at 8 o'clock in the morning of the next day. There is no need to manually change the date every day to display the latest data to the panel.

### 5：登陆界面的密码加密和账号注册功能 (Password encryption and account registration function of the login interface)

开始开发（2020/10/19）

`npm install crypto-js` Download the js encryption package to encrypt the password(2020/10/20)
`dotnet add package Microsoft.AspNetCore.Cryptography.KeyDerivation --version 3.1.6` 后端对密码进行加密并转存到数据库和验证密码有效性的依赖包 The back-end encrypts the password and transfers it to the database and the dependency package for verifying the validity of the password(2020/10/20)
`dotnet add package Microsoft.AspNetCore.NodeServices` 在cs文件中解码前端加密过后的密码，由于前端crypto-js加密相同字符得到不同的密码，在后端接收到密文后需要用相同方法将密文解码再在后端使用pbkdf2算法加密转存入数据库。
Decode the front-end encrypted password in the cs file. In terms that the front-end crypto-js encrypts the same characters to get different passwords, after receiving the cipher text at the back-end, you need to use the same method to decode the cipher text and then use the pbkdf2 algorithm to encrypt and transfer the back-end. Into the database

### 6：登陆成功后跳转到新的界面，该界面把不同功能的网页区分开来，并以按钮作为入口点击进入。(After logging in successfully, jump to a new interface, which distinguishes web pages with different functions, and click on the button as the entrance to enter)

**第一个版本(Version I)**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/主界面.png)

**第二个版本 (Version II)**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/主界面v2.png)

### 7:新增账号管理界面，内含在用户管理，修改密码，新建许可证和搜索等功能，当前开发到用户管理页面 (New account management interface, including user management, password modification, new license and search functions, currently developed to the user management page)（2020/10/23）

--新增账号管理页面基本功能实现，现在账号管理按钮只对管理员账号（暂为Sean）可见，管理员可以在该界面看到所有注册的用户账号信息，并有权限修改密码，或者注销账号
--The basic functions of the newly added account management page are implemented. Now the account management button is only visible to the administrator account (temporarily Sean). The administrator can see all registered user account information on this interface, and has the authority to modify the password or log out of the account.
--登陆界面新增忘记密码链接，点击过后跳转到重设密码页面，但是加入此功能后如果用户的用户名和令牌信息泄漏的话可以被其他人修改其密码，使得密码失去了意义，该功能现阶段不开放，其后会持续完善。
--A new forgot password link is added to the login interface. After clicking it, you will be redirected to the reset password page, but after adding this function, if the user's username and token information is leaked, the password can be changed by others, making the password meaningless. This function is now The stage is not open, and it will continue to improve thereafter.

### 8: OEE 界面柱状图现在点击OEE表中的任意数据柱会弹出弹窗显示当前数据下的更多详情信息。
The interface histogram now clicks on any data column in the OEE table, and a pop-up window will pop up to display more detailed information under the current data.

帮助开会数据整理需要。
meeting data sorting needs
![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/OEEpopup.png)

### 后续更新日志 (Subsequent update log)

(2020/11/05)------------------------------------------------------------------------
系统主界面ui优化，现在登陆成功后跳转到的主界面加入了其他模块的按钮入口，先暂时为静态按钮，后续开发过程中会逐一模块的完善，如下图。
System Ui has been optimized. Now the main interface that jumps to after a successful login has added the button entries of other modules, which are temporarily static buttons. The subsequent development process will improve the modules one by one, as shown in the figure below.

登录加密的地方修改了加密传输的方式，由于前端使用CryptoJS加密传输到后端再通过AspDotNetCore 中的nodeServices
The encrypted transmission method is modified in the place where the login is encrypted. in terms that the front end uses CryptoJS to encrypt transmission to the back end and then uses the nodeServices
组件调用js脚本来解密，部署到iis后服务器端无法完成任务，原因可能是在项目打包时前后端分离打包使得后端调用的js文件无法随后端代码一同打包，使得服务器端在调用js时找不到文件，从而报错（错误代码:500）
 Component in AspDotNetCore to call the js script to decrypt, the server cannot complete the task after deployment to iis. The reason may be that the project was packaged. The front-end and back-end separate packaging makes the js file called by the back-end cannot be packaged together with the subsequent code, so that the server can not find the file when calling js, thus reporting an error (error code: 500)

OEE界面显示的柱状图现在点击开后会有一个模态框显示当前日期（仅日期单位为'日'时弹出）下当天的报工单，及不合格记录和停机记录（ps:此处现存一bug，模态框弹出时其中的一个饼状图（使用echarts实现）无法实时更新并显示，需要点击一下上方的导航栏才会刷新出来）

现在管理员Sean可以查看所有已注册用户的基本信息（密码除外），并可以注销用户或者修改其密码。

修改了安灯状态页面 'new' block 和 'BPV' block 的小灯位置，之前下方表格显示BPV模组出现按灯提示时会在new模组处闪烁小灯。

The histogram displayed on the OEE interface now has a modal box that displays the current date (only pops up when the date unit is'day'), as well as unqualified records and shutdown records (ps: A bug existing here, when the modal box pops up, one of the pie charts (implemented by echarts) cannot be updated and displayed in real time. You need to click on the upper navigation bar to refresh)

Now the administrator Sean can view the basic information of all registered users (except passwords), and can log out users or modify their passwords.

Modified the small light positions of the'new' block and'BPV' block on the Andon status page. The previous table below shows that the BPV module will flash a small light at the new module when the BPV module is prompted to press the light.

(2020/11/16)------------------------------------------------------------------------
生产计划页面功能实现，现在生产计划页面新增两个面板，分别为订单下达和订单跟踪，在订单下达页面，授权的账户可以根据实际情况新增，下达，关闭和搜索相关订单信息，或者进行批量操作，此外，该页面允许授权账户通过导入excel表格数据来批量生成新的订单，或者将需要的订单数据导出成excel文件并下载到本地。
订单跟踪页面主要是数据显示，显示了未完工的订单的具体运行情况，该页面具有搜索功能来快速找到目标订单信息，此外，授权用户还可以修改正在生产中的订单的完工日期，以延后订单交付。

The production plan page function is implemented. Now the production plan page has two new panels, namely order placement and order tracking. On the order placement page, authorized accounts can be added, released, closed and searched for related order information according to the actual situation, or proceed Batch operation. In addition, this page allows authorized accounts to generate new orders in batches by importing excel table data, or export the required order data into excel files and download them locally.
The order tracking page is mainly for data display, showing the specific operation of the unfinished order. This page has a search function to quickly find the target order information. In addition, authorized users can also modify the completion date of the order in production to delay Order delivery.

**订单下达界面 (Order Placement page)**

选择产线，随后刷新出对应数据
choose product line, then refresh data.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_p1.png)

(例如选择EGR阀线，弹出相应数据)

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_p2.png)

我们也可以通过右上方颜色按钮来筛选数据，分别有创建（无色），下达（蓝色），生产中（黄色）和完工（绿色）四个状态
We can also filter the data through the color buttons on the upper right, which have four states: created (colorless), released (blue), in production (yellow) and completed (green).


![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_p2.png)

下达
placement

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_p3.png)

生产中
producting

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_p4.png)

完工
completed

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_p5.png)

***

除此外，用户可以通过手动输入信息来新建订单。点击上方新增按钮，弹出对话框进行订单新建。
In addition, users can create new orders by manually entering information. Click the Add button above to pop up a dialog box to create an order.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_新增.png)

或者，如果需要新建多个订单，可以点击导入按钮来向系统导入指定格式的excel表格来批量新建订单。
Or, if you need to create multiple orders, you can click the Import button to import the excel sheet in the specified format to the system to create batches of new orders.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/egExcel.png)

点击导入按钮弹出(Click the import button to pop up)：

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_导入.png)

导入后系统解析excel表格，用户选择导入表格的名称。
After importing, the system parses the excel table, and the user selects the name of the imported table.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_导入2.png)

选择好过后，系统读取数据并显示预览数据格式，便于用户检查数据是否正确。
After selection, the system reads the data and displays the preview data format, which is convenient for users to check whether the data is correct.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_导入3.png)

导入成功后，在上个界面可以搜索到刚才导入的数据
After the import is successful, the data just imported can be searched in the previous interface

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_导入4.png)

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_导入5.png)

***

完工的订单需要用户关闭，点击需要关闭的订单数据右侧的关闭按钮，关闭单个订单，或者勾选想要关闭的绿色订单，点击上方批量关闭来关闭多个订单。
The completed order needs to be closed by the user. Click the close button on the right side of the order data that needs to be closed to close a single order, or check the green order that you want to close, and click Bulk Close above to close multiple orders.

单个订单关闭
close single order

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_单项订单关闭.png)

批量关闭
batch close

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_批量关闭.png)

***

用户仍然可以编辑已创建的信息。点击数据右侧的编辑按钮，可以对单条数据进行编辑。
The user can still edit the information that has been created. Click the edit button on the right side of the data to edit a single piece of data.

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单下达_订单编辑.png)

**订单跟踪界面 Order Tracking Page**

*该界面主要对下达的订单去向显示 This interface mainly displays the destination of the placed order*

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单跟踪.png)

该界面具有搜索修改的功能
searching and modifying function enabled here

**搜索(Search)**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单跟踪_搜索.png)

**修改(Modify)**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/订单跟踪_修改.png)


### 最后一次更新，更新内容会在下周一完成。(The last update will be completed next Monday.)

工单管理页面

*该页面是所有报工单生成和信息录入的地方，也是数据库数据的根源。*
*This page is the place where all work orders are generated and information is entered, and it is also the source of database data.*

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/工单管理主页.png)

该页面包括订单（订单下达页面创建并下达的订单）的关闭，报工单基于订单来创建，一个订单对应多个报工单，订单中会包含一定数量的零件数量，每次报工完成后会完成一定数量的合格零件，当订单数量完成后，该订单不可再创建对应报工单，此时需要用户关闭订单。该页面主要流程为：创建报工单，报工单生产数据，生产过程中可以停机，记录不良数据，按灯，响应，解决和关闭按灯，录入生产数据，关闭报工单，关闭订单（如订单生产完毕）。
This page includes the closing of orders (orders created and released on the order release page). Work orders are created based on the order. One order corresponds to multiple work orders. The order will contain a certain number of parts. After each work report is completed A certain number of qualified parts will be completed. When the order quantity is completed, the corresponding work order cannot be created for the order. At this time, the user needs to close the order. The main process of this page is: create a work order, report work order production data, stop during the production process, record bad data, press the light, respond, solve and close the light, enter the production data, close the work order, close the order ( If the order is completed).

**创建报工单 (Report Creation)**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_新建报工单.png)

**不良品记录 (Defective product record)**

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_不良品记录.png)

**停机(Downtime)**

计划停机(Planned downtime)

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_计划停机.png)

非计划停机(accidental downtime)

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_非计划停机.png)

**按灯信息 (Andon Info)**

*按灯意为在生产过程中发生了意料之外的错误，需要提醒工人何时何地哪条产线的哪个设备发生了怎样的意外，并要求员工在一定时间内需要处理，该数据会影响到生产效率。*
*If an unexpected error occurs during the production process, you need to remind the workers when, where, and what kind of data occurred at which production line and time, and require the employees to affect the processing within a certain range, which should be the production efficiency .*
*按灯类型分为质量按灯，设备按灯和其他按灯（物料，工艺，管理等）*

新建质量按灯 Create Quality Andon

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_新增安灯记录-质量.png)

新建设备按灯 Create Equipment Andon

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_设备安灯记录增添.png)

新建其他按灯信息 Create other Andon info

![Image text](https://github.com/fxyjj/ASP.netcore3.0-VUE.JS/blob/master/pic/gd_工艺物料TBD安灯记录增添弹窗.png)


