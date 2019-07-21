import { createSelector } from "@ngrx/store";
import { IAppState } from "../../app.state";
import { IUserState } from "../state/user.state";

const loggedInUser = (state: IAppState) => state.user;

export const selectedUser = createSelector(
  loggedInUser,
  (state: IUserState) => state.user
);
