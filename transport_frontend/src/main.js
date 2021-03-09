import Vue from 'vue'
import App from './App.vue'
import store from './store';
//bootstrap
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
Vue.use(BootstrapVue)

//axios
import axios from 'axios'

// API
import * as api from './api.js'
Vue.prototype.$api = api;
Vue.prototype.$swatApi = axios.create({
  baseURL: api.apiDomain,
  timeout: 5000,
});

//router
import router from './router';




Vue.use(IconsPlugin)
Vue.config.productionTip = false
Vue.prototype.$eventHub = new Vue();

new Vue({
  render: h => h(App),
  router, store: store,
}).$mount('#app')
