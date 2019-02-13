import { Module, VuexModule, Action, Mutation } from 'vuex-module-decorators';
import store from '../index';
import * as feed from '@/types/news';
import axios from 'axios';

@Module({ dynamic: true, store, name: 'NewsModel' })
export default class NewsModel extends VuexModule {
  private news = {} as feed.News;

  get getNews() {
    return this.news;
  }

  @Action
  public async loadNews(id: number) {
    return axios.get(`/news/${id}`).then(response => {
      if (response.data) {
        this.context.commit('updateNews', response.data as feed.News);
      }
    });
  }

  @Action({ rawError: true})
  public async setRating(payload: any) {
    const index = this.news.articles.findIndex(c => c.id === payload.articleId);
    const article = this.news.articles[index];
    const ratedNews = { ...this.news }; // News = Clone, Articles = ShallowCopy :-()

    article.rating = payload.rating;
    ratedNews.articles.splice(index, 1, article);

    this.context.commit('updateNews', ratedNews);

    await axios
      .post(`/news/${payload.id}/article/${payload.articleId}/rating/${payload.rating}`)
      .catch(err => {
        this.loadNews(ratedNews.id);
        throw err;
      });
  }

  @Mutation
  private updateNews(news: feed.News) {
    this.news = Object.freeze(news);
  }
}
