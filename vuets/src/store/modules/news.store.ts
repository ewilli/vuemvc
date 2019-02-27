import { Module, VuexModule, Action, Mutation, getModule } from 'vuex-module-decorators';
import store from '../index';
import * as feed from '@/types/news';
import api from '../api';
import globalStore from './global.store';
// import clonedeep from 'lodash.clonedeep';

function getIndexOfArticle(news: feed.News, id: number): number {
  return news.articles.findIndex(c => c.id === id);
}

@Module({ namespaced: true, dynamic: true, store, name: 'news' })
class NewsModule extends VuexModule {
  private news = {} as feed.News;

  get getNews() {
    console.debug('@Getter:getNews');
    return this.news;
  }

  @Action
  public async loadNews(id: number) {
    console.debug('@Action:loadNews');
    return api.news.getItem(id).then(response => {
      if (response.data) {
        this.context.commit('updateNews', response.data as feed.News);
      } else {
        this.context.commit('updateNews', {});
      }
    });
  }

  @Action({ rawError: true }) // rawError does not decorate the error - so you are able to get the raw error from the action
  public async setRating(payload: feed.Rating) {
    console.debug('@Action:setRating');

    const oldrating = this.news.articles.find(c => c.id === payload.id)!.rating; // https://stackoverflow.com/a/40361053 for !.

    this.context.commit('updateRating', payload);

    return api.article.putFeedRating(payload).catch(err => {
      console.debug(err);
      // this.loadNews(payload.newsId).catch(() => {
      this.context.commit('updateRating', { ...payload, rating: oldrating });
      // });
      throw err; // rethrow for possible outer catch
    });
  }

  @Mutation
  private updateRating(rating: feed.Rating) {
    console.debug('@Mutation:updateRating');
    this.news.articles[getIndexOfArticle(this.news, rating.id)].rating = rating.rating;
  }

  @Mutation
  private updateArticle(article: feed.Article) {
    console.debug('@Mutation:updateArticle');
    this.news.articles.splice(getIndexOfArticle(this.news, article.id), 1, article);
  }

  @Mutation
  private updateNews(news: feed.News) {
    console.debug('@Mutation:updateNews');
    this.news = news;
  }
}

export default getModule(NewsModule);

if (module.hot) {
  // workaround for getModule hot reload problem
  module.hot.decline();
}
