<template>
  <div class="grey darken-2">
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
      <v-card v-for="article in articlesFiltered" :key="article.source.id" class="mb-3 color-teal">
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
                  <v-icon v-if="article.rating >= i" @click="rateNews(article.id, i, 'if')">star</v-icon>
                  <v-icon v-else @click="rateNews(article.id, i, 'else')">star_border</v-icon>
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
import { Article, News, Source } from '@/types/news';

import globalStore from '@/store/modules/global.store'; // @ = src : config in der tsconfig.json
import newsStore from '@/store/modules/news.store';

@Component({
  components: {},
})
export default class NewsList extends Vue {
  @Prop({ required: true, type: URL }) public newsUrl!: URL;

  protected searchTerm: string = '';

  protected activated() {
    console.log('activated'); // nur bei keep-alive
  }

  protected destroyed() {
    console.log('destroyed');
  }

  get news() {
    return newsStore.getNews;
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

  protected rateNews(articleId: number, rating: number, event: string) {
    console.log('rateNews' + event);
    newsStore
      .setRating({ newsId: this.news.id, id: articleId, rating })
      .catch(err => {
        globalStore.setErrors([err]);
      });
  }

  // lifecycle Vue
  protected mounted() {
    console.log('mounted');
    globalStore
      .loading()
      .then(c => newsStore.loadNews(1))
      .then(c => globalStore.loaded());
  }
}
</script>
