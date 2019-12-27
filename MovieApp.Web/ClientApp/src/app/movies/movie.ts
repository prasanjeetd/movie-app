export interface Movie {
  year: number;
  title: string;
  directors: any[];
  releaseDate?: Date;
  genres?: any[];
  imageUrl?: string;
  plot?: string;
  rank?: number;
  actors: any[];
}
