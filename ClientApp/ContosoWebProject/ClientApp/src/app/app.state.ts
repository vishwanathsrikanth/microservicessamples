import { RouterReducerState } from "@ngrx/router-store";
import { IUserState, initialUserState } from '././store/state/user.state';

export interface IAppState {
  router?: RouterReducerState;
  user: IUserState;
}

export const initialAppState: IAppState = {
  user: initialUserState
}

export function getInitialAppState(): IAppState {
  return initialAppState;
}
