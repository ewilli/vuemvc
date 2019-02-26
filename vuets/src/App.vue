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
      ></v-progress-linear>
    </v-toolbar>
    <v-content>
      <v-container fluid fill-height>
        <v-layout justify-center align-center>
          <v-flex fill-height>
            <div v-if="errors.length > 0">
              <div class="mb-2" v-for="(err, index) in errors" :key="index">
                <v-alert :value="true" type="error">{{err}}</v-alert>
              </div>
            </div>
            <keep-alive>
              <router-view/>
            </keep-alive>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer app fixed>
      <span>&copy; 2019</span>
    </v-footer>
  </v-app>
</template>

<script <script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import globalStore from '@/store/modules/global.store';
import { AxiosError } from 'axios';

@Component
export default class App extends Vue {
  @Prop(String) public source!: string; // if Decorator warning -> Rootdirectory is hierarchic; Solution: Workspace for VSCode!

  private drawer = false;
  get showloader() {
    return globalStore.getGlobal.loadingState;
  }

  // Type Guards -> https://www.typescriptlang.org/docs/handbook/advanced-types.html
  private isAxiosError(error: string | AxiosError): error is AxiosError {
    return (error as AxiosError).message !== undefined;
  }

  get errors() {
    return globalStore.getGlobal.errors.map(c => {
      if (this.isAxiosError(c)) {
        return 'Verbindungsproblem: ' + c.message;
      }
      return c;
    });
  }
}
</script>
