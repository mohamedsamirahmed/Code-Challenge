"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ErrorInterceptor = /** @class */ (function () {
    function ErrorInterceptor() {
    }
    ErrorInterceptor.prototype.intercept = function (req, next) {
        throw new Error("Method Not Implemented");
    };
    return ErrorInterceptor;
}());
exports.ErrorInterceptor = ErrorInterceptor;
//# sourceMappingURL=error.interceptor.js.map