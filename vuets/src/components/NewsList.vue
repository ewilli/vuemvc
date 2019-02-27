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
                  <NewsItem
                    :title="article.title"
                    :description="article.description"
                    :newsId="article.newsId"
                    :articleId="article.id"
                  ></NewsItem>
                </v-flex>
              </v-layout>
              <v-divider dark></v-divider>
              <v-card-actions class="pa-3">Rate this News
                <v-spacer></v-spacer>
                <Rating
                  :stars="stars"
                  :rating="article.rating"
                  @rate="rateNews($event, article.id )"
                />
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-container>
      </v-card>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { Article, News, Source } from '@/types/news';

import NewsItem from './NewsItem.vue';
import Rating from './common/Rating.vue';

import globalStore from '@/store/modules/global.store'; // @ = src > config in tsconfig.json
import newsStore from '@/store/modules/news.store';

@Component({
  components: { NewsItem, Rating },
})
export default class NewsList extends Vue {
  @Prop({ required: true, type: URL }) public newsUrl!: URL;
  @Prop({ default: 5 }) public stars!: number;

  protected searchTerm: string = ''; // reactive - warning! undefined is *not* reactive but null is

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

  protected rateNews(rating: number, articleId: number) {
    console.debug(`rateNews: ${rating} / ${articleId}`);
    newsStore
      .setRating({ newsId: this.news.id, id: articleId, rating })
      .then(() => {
        globalStore.resetErrors();
      })
      .catch(err => {
        globalStore.setErrors({ errors: [err], fail: false });
      });
  }

  // lifecycle Vue
  protected created() {
    console.debug('created');
  }

  protected mounted() {
    console.debug('mounted');
    globalStore
      .loading()
      .then(c => newsStore.loadNews(1))
      .then(c => globalStore.loaded());
  }

  protected activated() {
    console.debug('activated'); // only for keep-alive
  }

  protected deactivated() {
    console.debug('deactivated'); // only for keep-alive
  }

  protected destroyed() {
    console.debug('destroyed');
  }
}
</script>
