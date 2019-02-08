import Vue from 'vue';
import Vuetify from 'vuetify';
import de from 'vuetify/src/locale/de';
import 'vuetify/src/stylus/app.styl';

Vue.use(Vuetify, {
  iconfont: 'md',
  lang: {
    locales: { de },
    current: 'de',
  },
});
