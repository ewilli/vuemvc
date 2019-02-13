import Vue from 'vue';
import App from './App.vue';
import './plugins/vuetify';
import './plugins/axios';
import router from './router';
import store from './store';

Vue.config.productionTip = false;
// Vue.config.performance = true; // Usertimings -> Chrome Performance Traces

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
