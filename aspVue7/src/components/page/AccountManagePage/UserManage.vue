<template>
    <div class="panel">
        <el-card>
        <el-table :data="tableData.slice((currPage-1)*pageSize,currPage*pageSize)" style="width: 100%">
            <el-table-column fixed prop="user" label="用户名" width="150"></el-table-column>
            <el-table-column fixed prop="name" label="姓名" width="150"></el-table-column>
            <el-table-column fixed prop="certificate" label="许可证" width="150"></el-table-column>
            <el-table-column fixed prop="date" label="创建日期" width="150"></el-table-column>
            <el-table-column fixed="right" label="操作" width="100">
                <template slot-scope="scope">
                    <el-button @click="handleClick(scope.row)" type="text" size="small">查看</el-button>
                    <el-button type="text" size="small">编辑</el-button>
                </template>
            </el-table-column>
        </el-table>
         <el-pagination :hide-on-single-page="singlePage" :total="tableData.length" :page-size="pageSize" layout="prev, pager, next" @current-change="changePage($event)"></el-pagination>
        </el-card>
    </div>
</template>
<script>
export default {
    name:"AccountManage",
    data(){
        return{
            tableData:[{
                user: '2016-05-02',
                name: '王小虎1',
                certificate: '上海',
                date: '普陀区'
            },
            {
                user: '2016-05-02',
                name: '王小虎2',
                certificate: '上海',
                date: '普陀区'
            },
            {
                user: '2016-05-02',
                name: '王小虎3',
                certificate: '上海',
                date: '普陀区'
            },
            {
                user: '2016-05-02',
                name: '王小虎4',
                certificate: '上海',
                date: '普陀区'
            },
            {
                user: '2016-05-02',
                name: '王小虎5',
                certificate: '上海',
                date: '普陀区'
            },
            {
                user: '2016-05-02',
                name: '王小虎6',
                certificate: '上海',
                date: '普陀区'
            }],
            singlePage:false,
            currPage:1,
            pageSize:2,
        }
    },
    methods:{
        handleClick(row){
            console.log(row);
        },

        changePage(event){
            this.currPage = event;
            console.log(event);
        },
        // getData(){
        //     fetch('api/')
        // }
    },
    mounted(){

    }
}
</script>
<style scoped>
.panel{
    margin:10px;
    border:0px;
}
</style>