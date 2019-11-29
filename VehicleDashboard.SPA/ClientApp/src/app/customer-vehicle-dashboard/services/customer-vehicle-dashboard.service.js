"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_prod_1 = require("../../../environments/environment.prod");
var CustomerVehicleDashboardService = /** @class */ (function () {
    function CustomerVehicleDashboardService(http) {
        this.http = http;
        this.baseUrl = environment_prod_1.environment.apiEndpoint;
    }
    CustomerVehicleDashboardService.prototype.getcustomerVehiclesList = function () {
        return this.http.get(this.baseUrl + 'CustomerVehicles');
    };
    return CustomerVehicleDashboardService;
}());
exports.CustomerVehicleDashboardService = CustomerVehicleDashboardService;
//# sourceMappingURL=customer-vehicle-dashboard.service.js.map