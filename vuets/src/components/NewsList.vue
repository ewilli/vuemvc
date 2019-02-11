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
                <div v-for="i in 5" :key="i">
                  <v-icon v-if="article.rating >= i" @click="rateNews(article.id, i, $event)">star</v-icon>
                  <v-icon v-else @click="rateNews(article.id, i, $event)">star_border</v-icon>
                </div>
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
import { Article, News, Source } from '@/models/newsModel';

import { getModule } from 'vuex-module-decorators';
import GlobalModule from '@/store/modules/globalModule'; // @ = src : config in der tsconfig.json

const globalStoreModule = getModule(GlobalModule);

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

  protected rateNews(articleId: number, rating: number, event: Event) {
    axios
      .post(`/news/${this.news.id}/article/${articleId}/rating/${rating}`)
      .then(res => {
        // so
        // this.loadData();

        // oder so
        const index = this.news.articles.findIndex(c => c.id === articleId);
        // TODO denke nicht, dass der private store hier imutable sein muss -> recherieren
        const article = { ...this.news.articles[index] };
        article.rating = rating;
        Vue.set(this.news.articles, index, article); // reactiv Array set -> oder Array.prototype.splice ...

        // am BESTEN über Store!
        // TODO
      })
      .catch(err => {
        console.log(err);
      });
  }

  protected mounted() {
    console.log('mounted');
    this.loadData();
  }

  private loadData() {
    globalStoreModule.loading();
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
        globalStoreModule.stop();
      });
  }
}
</script>
