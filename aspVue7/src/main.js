import Vue from 'vue'
import App from './App.vue'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import router from './router';
import "babel-polyfill";
import "./assets/third_party_icon/iconfont.css"
// import axios from 'axios';
// Vue.prototype.$axios = axios
Vue.config.productionTip = false
Vue.use(ElementUI, {
	size: 'small'
});

let base = process.env.NODE_ENV === 'production'? 'http://10.225.116.24:5008':'http://localhost:8080';
Vue.prototype.baseURL = base;

//使用钩子函数对路由进行权限跳转
router.beforeEach((to, from, next) => {
	document.title = `${to.meta.title} | MiS`;
	const role = localStorage.getItem('ms_username');
	if (role) {
		next();
	}else{
		if(to.path === '/Login'){
			next();
		}else{
			next('/Login');
		}
	}
});

new Vue({
	router,
	render: h => h(App),
}).$mount('#app')
