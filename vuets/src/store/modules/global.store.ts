import {
  Module,
  VuexModule,
  Mutation,
  Action,
  // MutationAction,
} from 'vuex-module-decorators';
import store from '../index';

export interface IGlobalModuleType {
  loadingState: boolean;
  loadingText: string;
  errors: string[];
}

// https://championswimmer.in/vuex-module-decorators/pages/overview.html

const globalInit = { loadingState: false, loadingText: '', errors: [] } as IGlobalModuleType;

@Module({ dynamic: true, store, name: 'GlobalModule' })
export default class GlobalModule extends VuexModule {
  private global = globalInit;

  get getGlobal() {
    return this.global;
  }

  @Action
  public async loading() {
    this.context.commit('updateGlobal', {
      ...this.getGlobal,
      loadingState: true,
      loadingText: 'Lade....',
    });
  }

  // action 'stop' commits mutation 'cancel' when done with return value as payload
  @Action({ commit: 'cancelLoading' })
  public async loaded() {
    return;
  }

  @Action({ commit: 'updateGlobalErrors' })
  public setErrors(errors: string[]) {
    return errors;
  }

  @Mutation
  private updateGlobal(state: IGlobalModuleType) {
    this.global = { ...state };
  }

  @Mutation
  private updateGlobalErrors(state: string[]) {
    this.global = { ...this.global, errors: state };
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
