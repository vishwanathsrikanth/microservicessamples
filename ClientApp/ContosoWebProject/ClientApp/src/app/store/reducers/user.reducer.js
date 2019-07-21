"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
var user_state_1 = require("./../state/user.state");
var user_action_1 = require("../actions/user.action");
exports.userReducers = function (state, action) {
    if (state === void 0) { state = user_state_1.initialUserState; }
    switch (action.type) {
        case user_action_1.EUserActions.GetUserSuccess: {
            return __assign({}, state, { user: action.user });
        }
        default: return state;
    }
};
//# sourceMappingURL=user.reducer.js.map