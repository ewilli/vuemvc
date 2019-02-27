import {
  Module,
  VuexModule,
  Mutation,
  Action,
  getModule,
  // MutationAction,
} from 'vuex-module-decorators';
import store from '../index';
import { Errors, Loading, Global } from '@/types/global';
import { AxiosResponse } from 'axios';

// https://championswimmer.in/vuex-module-decorators/pages/overview.html

const globalInit = { loadingState: false, loadingText: '', errors: [], fail: false } as Global;

@Module({ namespaced: true, dynamic: true, store, name: 'global' })
class GlobalVuexModule extends VuexModule {
  private global = globalInit;

  get getGlobal() {
    return this.global;
  }

  @Action({ commit: 'updateLoading' })
  public async loading(loadingText?: string) {
    return {
      loadingState: true,
      loadingText: loadingText || 'Lade....',
    } as Loading;
  }

  @Action({ commit: 'updateLoading' })
  public async loaded() {
    return {
      loadingState: globalInit.loadingState,
      loadingText: globalInit.loadingText,
    } as Loading;
  }

  @Action({ commit: 'updateErrors' })
  public setErrors(err: Errors) {
    return err;
  }

  @Action({ commit: 'updateErrors' })
  public async resetErrors() {
    return { errors: [], fail: false };
  }

  @Mutation
  private update(state: Global) {
    this.global = { ...state };
  }

  @Mutation
  private updateLoading(state: Loading) {
    this.global = {
      ...this.global,
      loadingState: state.loadingState,
      loadingText: state.loadingText,
    };
  }

  @Mutation
  private updateErrors(state: Errors) {
    this.global = { ...this.global, errors: state.errors, fail: state.fail };
  }

  @Mutation
  private cancelLoading() {
    this.global = {
      ...this.global,
      loadingState: globalInit.loadingState,
      loadingText: globalInit.loadingText,
    };
  }
}

export default getModule(GlobalVuexModule, store);

if (module.hot) {
  module.hot.decline();
}
