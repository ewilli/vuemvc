<template>
  <v-app id="inspire" dark>
    <v-navigation-drawer v-model="drawer" clipped fixed app>
      <v-list dense>
        <v-list-tile>
          <v-list-tile-action>
            <v-icon>dashboard</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>
              <router-link to="/">News</router-link>
            </v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-action>
            <v-icon>settings</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>
              <router-link to="/about">Settings</router-link>
            </v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar app fixed clipped-left extension-height="0">
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>Application</v-toolbar-title>
      <v-progress-linear
        v-if="showloader"
        slot="extension"
        class="mt-2"
        :indeterminate="true"
      >Progress</v-progress-linear>
    </v-toolbar>
    <v-content>
      <v-container fluid fill-height>
        <v-layout justify-center align-center>
          <v-flex fill-height>
            <div v-if="errors.length > 0">
              <div v-for="(err, index) in errors" :key="index">{{err}}</div>
            </div>
            <keep-alive>
              <router-view/>
            </keep-alive>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer app fixed>
      <span>&copy; 2017</span>
    </v-footer>
  </v-app>
</template>

<script <script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import globalModule from '@/store/modules/global.store';

const GlobalModule = getModule(globalModule);

@Component
export default class App extends Vue {
  @Prop(String) public source!: string; // bei einer Decorator Warning ist das Rootverzeichnis geschachtelt -> Lösung ist einen Workspace zu machen, sodass tslint.config auf der richtigen Ebene liegt

  private drawer = false;
  get showloader() {
    return GlobalModule.getGlobal.loadingState;
  }

  get errors() {
    return GlobalModule.getGlobal.errors;
  }
}
</script>
