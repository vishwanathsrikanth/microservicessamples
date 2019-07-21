import { initialUserState, IUserState } from "./../state/user.state"
import { UserActions, EUserActions } from "../actions/user.action";

export const userReducers = (
  state= initialUserState,
  action: UserActions
): IUserState => {
  switch (action.type) {
    case EUserActions.GetUserSuccess: {
      return {
        ...state, // shallow clone of state object
        user: action.user
      }
    }
    default: return state;
  }
}
