import {
  Module,
  VuexModule,
  Mutation,
  Action,
  // MutationAction,
} from 'vuex-module-decorators';
import store from '../index';

export interface GlobalModuleType {
  loadingState: boolean;
  loadingText: string;
}

const globalInit = { loadingState: false, loadingText: '' } as GlobalModuleType;

@Module({ dynamic: true, store, name: 'GlobalModule' })
export default class GlobalModule extends VuexModule {
  private global = globalInit;

  get globalState() {
    return this.global;
  }

  // action 'load' commits mutation 'loading' when done with return value as payload
  @Action({ commit: 'updateGlobal' })
  public load(state: GlobalModuleType) {
    return state;
  }

  @Action({ commit: 'updateGlobal' })
  public loading() {
    return { loadingState: true, loadingText: 'Lade....' } as GlobalModuleType;
  }

  // action 'stop' commits mutation 'cancel' when done with return value as payload
  @Action({ commit: 'cancel' })
  public stop() {
    return;
  }

  @Mutation
  private updateGlobal(state: GlobalModuleType) {
    if (this.global.loadingState !== state.loadingState) {
      this.global = { ...state };
    }
  }

  @Mutation
  private cancel() {
    this.global = { ...globalInit };
  }
}
