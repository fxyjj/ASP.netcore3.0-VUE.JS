import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
    mode:'history',
    routes: [
        {
            path: '/',
            redirect: to=>{
                console.log("from Index"+to.query.page);
                switch(to.query.page){
                    case "dashboard":
                        return '/dashboard'
                    case "andon":
                        return '/Andon'
                    case "oee":
                        return '/OEE'
                    case "prodPlan":
                        return '/OrderMaking'
                    case "workRep":
                        return '/WorkReport'
                    default:
                        return '/404'
                }
            }//
        },
        {
            path: '/',
            component: () => import(/* webpackChunkName: "home" */ '../components/common/Home.vue'),
            meta: { title: '自述文件' },
            children: [
                {
                    path: '/dashboard',
                    component: () => import(/* webpackChunkName: "dashboard" */ '../components/page/Dashboard.vue'),
                    meta: { title: '系统首页' }
                },
                {
                    path: '/Andon',
                    component: () => import(/* webpackChunkName: "icon" */ '../components/page/AndonMenu/AndonMenu.vue'),
                    meta: { title: '安灯信息汇总' }
                },
                {
                    path: '/AndonNum',
                    component: () => import(/* webpackChunkName: "icon" */ '../components/page/AndonMenu/AndonNum.vue'),
                    meta: { title: '安灯数量' }
                },
                {
                    path: '/AndonCate',
                    component: () => import(/* webpackChunkName: "icon" */ '../components/page/AndonMenu/AndonCate.vue'),
                    meta: { title: '安灯类别' }
                },
                {
                    path: '/TopIssue',
                    component: () => import(/* webpackChunkName: "icon" */ '../components/page/AndonMenu/TopIssue.vue'),
                    meta: { title: 'TopIssue' }
                },
                {
                    path: '/AndonStatus',
                    component: () => import(/* webpackChunkName: "icon" */ '../components/page/AndonMenu/AndonStatus.vue'),
                    meta: { title: '安灯状态' }
                },
                // {
                //     path: '/table',
                //     component: () => import(/* webpackChunkName: "table" */ '../components/page/BaseTable.vue'),
                //     meta: { title: '基础表格' }
                // },
                // {
                //     path: '/tabs',
                //     component: () => import(/* webpackChunkName: "tabs" */ '../components/page/Tabs.vue'),
                //     meta: { title: 'tab选项卡' }
                // },
                // {
                //     path: '/form',
                //     component: () => import(/* webpackChunkName: "form" */ '../components/page/BaseForm.vue'),
                //     meta: { title: '基本表单' }
                // },
                // {
                //     // 富文本编辑器组件
                //     path: '/editor',
                //     component: () => import(/* webpackChunkName: "editor" */ '../components/page/VueEditor.vue'),
                //     meta: { title: '富文本编辑器' }
                // },
                // {
                //     // markdown组件
                //     path: '/markdown',
                //     component: () => import(/* webpackChunkName: "markdown" */ '../components/page/Markdown.vue'),
                //     meta: { title: 'markdown编辑器' }
                // },
                // {
                //     // 图片上传组件
                //     path: '/upload',
                //     component: () => import(/* webpackChunkName: "upload" */ '../components/page/Upload.vue'),
                //     meta: { title: '文件上传' }
                // },
                {
                    // vue-schart组件
                    path: '/OEE',
                    component: () => import(/* webpackChunkName: "chart" */ '../components/page/OEE.vue'),
                    meta: { title: 'OEE' }
                },
                {
                    // vue-schart组件
                    path: '/OrderMaking',
                    component: () => import(/* webpackChunkName: "chart" */ '../components/page/ProductPlan/OrderMaking.vue'),
                    meta: { title: '订单下达' }
                },
                {
                    // vue-schart组件
                    path: '/OrderTrack',
                    component: () => import(/* webpackChunkName: "chart" */ '../components/page/ProductPlan/OrderTrack.vue'),
                    meta: { title: '订单跟踪' }
                },
                {
                    // vue-schart组件
                    path: '/WorkReport',
                    component: () => import(/* webpackChunkName: "chart" */ '../components/page/WorkReportManage/ProductReportManage.vue'),
                    meta: { title: '工单管理' }
                },
                // {
                //     // 拖拽列表组件
                //     path: '/drag',
                //     component: () => import(/* webpackChunkName: "drag" */ '../components/page/DragList.vue'),
                //     meta: { title: '拖拽列表' }
                // },
                // {
                //     // 拖拽Dialog组件
                //     path: '/dialog',
                //     component: () => import(/* webpackChunkName: "dragdialog" */ '../components/page/DragDialog.vue'),
                //     meta: { title: '拖拽弹框' }
                // },
                // {
                //     // 国际化组件
                //     path: '/i18n',
                //     component: () => import(/* webpackChunkName: "i18n" */ '../components/page/I18n.vue'),
                //     meta: { title: '国际化' }
                // },
                // {
                //     // 权限页面
                //     path: '/permission',
                //     component: () => import(/* webpackChunkName: "permission" */ '../components/page/Permission.vue'),
                //     meta: { title: '权限测试', permission: true }
                // },
                {
                    path: '/404',
                    component: () => import(/* webpackChunkName: "404" */ '../components/page/404.vue'),
                    meta: { title: '404' }
                },
                // {
                //     path: '/403',
                //     component: () => import(/* webpackChunkName: "403" */ '../components/page/403.vue'),
                //     meta: { title: '403' }
                // },
                // {
                //     path: '/donate',
                //     component: () => import(/* webpackChunkName: "donate" */ '../components/page/Donate.vue'),
                //     meta: { title: '支持作者' }
                // }
            ]
        },
        {
            path: '/login',
            component: () => import(/* webpackChunkName: "login" */ '../components/page/Login.vue'),
            meta: { title: '登录' }
        },
        {
            path:'/MainPage',
            component: () => import('../components/page/MainPage.vue'),
            meta:{title: '主菜单界面'}
        },
        {
            path:'/AccountManage',
            redirect:'/UserManage',
            component: () => import('../components/page/AccountManage.vue'),
            meta:{title:'账号管理'},
            children:[
                {
                    path:'/UserManage',
                    name:'UM',
                    component:() => import('../components/page/AccountManagePage/UserManage.vue'),
                    meta:{title:'用户管理'}
                },
                {
                    path:'/PwdModify',
                    name:'PM',
                    component:() => import('../components/page/AccountManagePage/PwdModify.vue'),
                    meta:{title:'修改密码'}
                },
                {
                    path:'/CreateKey',
                    name:'CK',
                    component:()=>import('../components/page/AccountManagePage/CreateKey.vue'),
                    meta:{title:'新建许可令牌'}
                }
            ]
        },
        {
            path: '*',
            redirect: '/404'
        }
    ]
});

