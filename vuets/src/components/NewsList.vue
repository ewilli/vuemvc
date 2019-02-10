<template>
  <div class="grey lighten-3">
    <v-toolbar color="teal" dark>
      <v-toolbar-title>News</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-flex xs3>
        <v-text-field label="Suche" v-model="searchTerm" single-line></v-text-field>
      </v-flex>
      <v-btn icon>
        <v-icon>search</v-icon>
      </v-btn>
    </v-toolbar>
    <div v-if="showNews">
      <v-card v-for="article in articlesFiltered" :key="article.source.id">
        <v-container fluid grid-list-lg>
          <v-flex xs12>
            <v-card class="white--text">
              <v-layout row>
                <v-flex xs7>
                  <v-card-title primary-title>
                    <div>
                      <div class="headline">{{article.title}}</div>
                      <div>{{article.description}}</div>
                    </div>
                  </v-card-title>
                </v-flex>
              </v-layout>
              <v-divider dark></v-divider>
              <v-card-actions class="pa-3">Rate this News
                <v-spacer></v-spacer>
                <v-icon>star_border</v-icon>
                <v-icon>star_border</v-icon>
                <v-icon>star_border</v-icon>
                <v-icon>star_border</v-icon>
                <v-icon>star_border</v-icon>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-container>
      </v-card>
    </div>
  </div>
</template>

<script lang="ts">
import axios from 'axios';
import { Component, Prop, Vue } from 'vue-property-decorator';
import { Article, News, Source } from '../models/newsModel';

import { getModule } from 'vuex-module-decorators';
import GlobalModule from '@/store/modules/globalModule';  // @ = src

const globalModule = getModule(GlobalModule);

@Component({
  components: {},
})
export default class NewsList extends Vue {
  @Prop({ required: true, type: URL }) public newsUrl!: URL;

  protected news: News = {} as News;
  protected searchTerm: string = '';

  protected activated() {
    console.log('activated'); // nur bei keep-alive
  }

  protected destroyed() {
    console.log('destroyed');
  }

  get articlesFiltered() {
    if (this.searchTerm) {
      return this.news.articles.filter(
        c => c.title.search(new RegExp(this.searchTerm, 'i')) !== -1,
      );
    }
    return this.news.articles;
  }

  get showNews() {
    return this.news && this.news.id > 0;
  }

  protected mounted() {
    console.log('mounted');
    globalModule.loading();
    axios
      .get(this.newsUrl.toString())
      .then(response => {
        if (response.data && response.data.length > 0) {
          this.news = response.data[0] as News;
        }
      })
      .catch(err => {
        console.log(err);
      })
      .finally(() => {
        globalModule.stop();
      });
  }
}
</script>
