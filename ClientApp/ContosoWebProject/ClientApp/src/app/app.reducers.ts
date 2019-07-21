import { ActionReducerMap } from "@ngrx/store";
import { IAppState } from "./app.state";
import { routerReducer } from "@ngrx/router-store";
import { userReducers } from "./store/reducers/user.reducer";

export const appReducers: ActionReducerMap<IAppState, any> = {
  router: routerReducer,
  user: userReducers
};
