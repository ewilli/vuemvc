import { AxiosError } from 'axios';

export interface Loading {
  loadingState: boolean;
  loadingText: string;
}

export interface Errors {
  errors: Array<string | AxiosError>;
}

export interface Global extends Loading, Errors {}
