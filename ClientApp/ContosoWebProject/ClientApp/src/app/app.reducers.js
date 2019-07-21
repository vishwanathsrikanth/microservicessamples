"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_store_1 = require("@ngrx/router-store");
var user_reducer_1 = require("./store/reducers/user.reducer");
exports.appReducers = {
    router: router_store_1.routerReducer,
    user: user_reducer_1.userReducers
};
//# sourceMappingURL=app.reducers.js.map