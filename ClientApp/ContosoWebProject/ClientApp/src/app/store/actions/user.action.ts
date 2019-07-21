import { Action } from "@ngrx/store"
import { IUser } from '../../../models/user.interface';

export enum EUserActions {
  GetUser = "[User] Get User",
}

export class GetUser implements Action {
  public readonly type = EUserActions.GetUser;
}

export type UserActions = GetUser
