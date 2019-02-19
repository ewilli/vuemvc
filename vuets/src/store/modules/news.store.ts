import { Module, VuexModule, Action, Mutation, getModule } from 'vuex-module-decorators';
import store from '../index';
import * as feed from '@/types/news';
import axios from 'axios';
// import clonedeep from 'lodash.clonedeep';

function getIndexOfArticle(news: feed.News, id: number): number {
  return news.articles.findIndex(c => c.id === id);
}

@Module({ namespaced: true, dynamic: true, store, name: 'news' })
class NewsModule extends VuexModule {
  private news = {} as feed.News;

  get getNews() {
    console.log('@Getter:getNews');
    return this.news;
  }

  @Action
  public async loadNews(id: number) {
    console.log('@Action:loadNews');
    return axios.get(`/news/${id}`).then(response => {
      if (response.data) {
        this.context.commit('updateNews', response.data as feed.News);
      } else {
        this.context.commit('updateNews', {});
      }
    });
  }

  @Action({ rawError: true })
  public async setRating(payload: feed.Rating) {
    console.log('@Action:setRating');
    this.context.commit('updateRating', payload);

    await axios
      .post(`/news/${payload.newsId}/article/${payload.id}/rating/${payload.rating}`)
      .catch(err => {
        console.log(err);
        this.loadNews(payload.newsId);
        throw err;
      });
  }

  @Mutation
  private updateRating(rating: feed.Rating) {
    console.log('@Mutation:updateRating');
    this.news.articles[getIndexOfArticle(this.news, rating.id)].rating = rating.rating;
  }

  @Mutation
  private updateArticle(article: feed.Article) {
    console.log('@Mutation:updateArticle');
    this.news.articles.splice(getIndexOfArticle(this.news, article.id), 1, article);
  }

  @Mutation
  private updateNews(news: feed.News) {
    console.log('@Mutation:updateNews');
    this.news = news;
  }
}

export default getModule(NewsModule);

if (module.hot) {
  module.hot.decline();
}
