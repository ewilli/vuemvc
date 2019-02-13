interface News {
  id: number;
  status: string;
  totalResults: number;
  articles: Article[];
}

interface Article {
  id: number;
  newsId: number;
  source: Source;
  author?: string;
  title: string;
  description: string;
  url: string;
  urlToImage?: string;
  publishedAt: string;
  content?: string;
  rating?: number;
}

interface Source {
  id: number;
  name: string;
}

export { News, Article, Source };
export default News;
