interface Ident {
  id: number;
}

interface NewsIdent {
  newsId: number;
}

interface News extends Ident {
  status: string;
  totalResults: number;
  articles: Article[];
}

interface Rating extends Ident, NewsIdent {
  rating?: number;
}

interface Article extends Rating, Ident, NewsIdent {
  source: Source;
  author?: string;
  title: string;
  description: string;
  url: string;
  urlToImage?: string;
  publishedAt: string;
  content?: string;
}

interface Source extends Ident {
  name: string;
}

export { News, Article, Source, Rating };
export default News;
