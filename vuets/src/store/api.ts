import axios from 'axios';
import * as feed from '@/types/news';

const newsApi = {
  async getItem(id: number) {
    return axios.get(`/news/${id}`);
  },
};

const articleApi = {
  async putFeedRating(rating: feed.Rating) {
    return this.putRating(rating.newsId, rating.id, rating.rating || 0);
  },
  async putRating(id: number, articleId: number, rating: number) {
    return axios.put(`/news/${id}/article/${articleId}/rating`, { rating });
  },
};

export default {
  news: newsApi,
  article: articleApi,
};
