import { initialUserState, IUserState } from "./../state/user.state"
import { UserActions, EUserActions } from "../actions/user.action";

export const userReducers = (
  state= initialUserState,
  action: UserActions
): IUserState => {
  switch (action.type) {
    case EUserActions.GetUser: {
      return {
        ...state, // shallow clone of state object
      }
    }
  }
}
