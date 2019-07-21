import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from "@ngrx/effects";
import { GetUser, EUserActions, GetUserSuccess } from "../actions/user.action";
import { of } from "rxjs";
import { UserService } from './../../services/user.service'
import { switchMap } from 'rxjs/operators'
import { IUser } from "../../../models/user.interface";

@Injectable()
export class UserEffects {

  constructor(private _userservice: UserService, private _action$: Actions) { }

  @Effect()
  getUser$ = this._action$.pipe(
    ofType<GetUser>(EUserActions.GetUser),
      switchMap(() => this._userservice.getUser()),
      switchMap((user: IUser) => {
          return of(new GetUserSuccess(user));
      })
  )
}
