<template>
    <div class="sidebar">
        <el-menu
            class="sidebar-el-menu"
            :default-active="onRoutes"
            :collapse="collapse"
            background-color="#324157"
            text-color="#bfcbd9"
            active-text-color="#20a0ff"
            unique-opened
            router
        >
            <template v-for="item in items" >
                <template v-if="item.subs">
                    <el-submenu :index="item.index" :key="item.index">
                        <template slot="title">
                            <i :class="item.icon"></i>
                            <span slot="title">{{ item.title }}</span>
                        </template>
                        <template v-for="subItem in item.subs">
                            <el-submenu
                                v-if="subItem.subs"
                                :index="subItem.index"
                                :key="subItem.index"
                            >
                                <template slot="title">{{ subItem.title }}</template>
                                <el-menu-item
                                    v-for="(threeItem,i) in subItem.subs"
                                    :key="i"
                                    :index="threeItem.index"
                                >{{ threeItem.title }}</el-menu-item>
                            </el-submenu>
                            <el-menu-item
                                v-else
                                :index="subItem.index"
                                :key="subItem.index"
                            >{{ subItem.title }}</el-menu-item>
                        </template>
                    </el-submenu>
                </template>
                <template v-else>
                    <el-menu-item :index="item.index" :key="item.index">
                        <i :class="item.icon"></i>
                        <span slot="title">{{ item.title }}</span>
                    </el-menu-item>
                </template>
            </template>
        </el-menu>
    </div>
</template>

<script>
import bus from '../common/bus';
export default {
    data() {
        return {
            collapse: true,
            items: [
                // {
                //     icon: 'el-icon-menu',
                //     index: 'table',
                //     title: '基础表格'
                // },
                // {
                //     icon: 'el-icon-s-claim',
                //     index: 'tabs',
                //     title: 'tab选项卡'
                // },
                // {
                //     icon: 'el-icon-tickets',
                //     index: '3',
                //     title: '表单相关',
                //     subs: [
                //         {
                //             icon:'el-icon-s-marketing',
                //             index: 'form',
                //             title: '基本表单'
                //         },
                //         {
                //             icon:'el-icon-s-operation',
                //             index: '3-2',
                //             title: '三级菜单',
                //             subs: [
                //                 {
                //                     icon:'el-icon-s-flag',
                //                     index: 'editor',
                //                     title: '富文本编辑器'
                //                 },
                //                 {
                //                     icon:'el-icon-receiving',
                //                     index: 'markdown',
                //                     title: 'markdown编辑器'
                //                 }
                //             ]
                //         },
                //         {
                //             index: 'upload',
                //             title: '文件上传'
                //         }
                //     ]
                // },
               
                // {
                //     icon: 'el-icon-rank',
                //     index: '6',
                //     title: '拖拽组件',
                //     subs: [
                //         {
                //             index: 'drag',
                //             title: '拖拽列表'
                //         },
                //         {
                //             index: 'dialog',
                //             title: '拖拽弹框'
                //         }
                //     ]
                // },
                // {
                //     icon: 'el-icon-s-operation',
                //     index: 'i18n',
                //     title: '国际化功能'
                // },
                // {
                //     icon: 'el-icon-s-flag',
                //     index: '7',
                //     title: '错误处理',
                //     subs: [
                //         {
                //             index: 'permission',
                //             title: '权限测试'
                //         },
                //         {
                //             index: '404',
                //             title: '404页面'
                //         }
                //     ]
                // },
                // {
                //     icon: 'el-icon-receiving',
                //     index: '/donate',
                //     title: '支持作者'
                // }
            ],
            //首页内容
            item_dashboard:[
                 {
                    icon: 'el-icon-s-home',
                    index: 'dashboard',
                    title: '系统首页'
                },
            ],
            //Andon页面内容
            item_andon:[
                        {
                        icon:'el-icon-info',
                        index: 'Andon',
                        title: '安灯信息汇总'
                    },
                        {
                        icon:'el-icon-files',
                        index: 'AndonNum',
                        title: '安灯数量'
                    },
                        {
                        icon:'el-icon-folder',
                        index: 'AndonCate',
                        title: '安灯类别'
                    },
                        {
                        icon:'el-icon-question',
                        index: 'TopIssue',
                        title: 'Top Issues'
                    },
                    {
                        icon:'el-icon-share',
                        index: 'AndonStatus',
                        title: '安灯状态'
                    },
                    {
                        icon:'el-icon-notebook-1',
                        index: 'AndonLog',
                        title: '安灯历史'
                    }
            ],
            //OEE页面内容
            item_oee:[
                 {
                    icon: 'el-icon-data-analysis',
                    index: 'OEE',
                    title: 'OEE'
                },
            ],
            //生产计划页面
            item_prodPlan:[
                {
                    icon: 'el-icon-document-checked',
                    index: 'OrderMaking',
                    title: '订单下达'
                },
                 {
                    icon: 'el-icon-aim',
                    index: 'OrderTrack',
                    title: '订单跟踪'
                },
            ], 
            //工单管理页面内容
            item_workRep:[
                 {
                    icon: 'el-icon-tickets',
                    index: 'WorkReport',
                    title: '工单管理'
                },
                {
                    icon: 'el-icon-document',
                    index: 'ReportOverview',
                    title: '工单总览'
                },
            ], 
            //工艺维护页面内容
            item_gyMaint:[
                {
                    icon: 'el-icon-set-up',
                    index: 'GYMaintance',
                    title: '工艺维护'
                },
            ], 
        };
    },
    computed: {
        onRoutes() {
            return this.$route.path.replace('/', '');
        }
    },
    created() {
        // 通过 Event Bus 进行组件间通信，来折叠侧边栏
        bus.$on('collapse', msg => {
            this.collapse = msg;
            bus.$emit('collapse-content', msg);
        }); 
    },
    mounted(){
        console.log("传过来的"+this.$route.query.page)
        // this.items = this.item_andon
        switch(this.$route.query.page){
            case "dashboard":
                this.items = this.item_dashboard;
                console.log("this is dashboard")
                break;
            case "andon":
                this.items = this.item_andon;
                console.log(this.items)
                console.log("this is andon")
                break;
            case "oee":
                this.items = this.item_oee;
                console.log("this is oee")
                break;
            case "prodPlan":
                this.items = this.item_prodPlan;
                break;
            case "workRep":
                this.items = this.item_workRep;
                break;
            case "gyMaint":
                this.items = this.item_gyMaint;
                break;
        }
    }
};
</script>

<style scoped>
.sidebar {
    display: block;
    position: absolute;
    left: 0;
    top: 50px;
    bottom: 0;
    overflow-y: scroll;
}
.sidebar::-webkit-scrollbar {
    width: 0;
}
.sidebar-el-menu:not(.el-menu--collapse) {
    width: 250px;
}
.sidebar > ul {
    height: 100%;
}
</style>
