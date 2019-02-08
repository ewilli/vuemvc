interface News {
  id: number;
  status: string;
  totalResults: number;
  articles: Article[];
}

interface Article {
  id: number;
  source: Source;
  author?: string;
  title: string;
  description: string;
  url: string;
  urlToImage?: string;
  publishedAt: string;
  content?: string;
}

interface Source {
  id: number;
  name: string;
}

export { News, Article, Source };
