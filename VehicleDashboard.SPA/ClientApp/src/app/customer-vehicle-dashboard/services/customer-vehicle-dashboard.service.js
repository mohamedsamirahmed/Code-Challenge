"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_prod_1 = require("../../../environments/environment.prod");
var CustomerVehicleDashboard = /** @class */ (function () {
    function CustomerVehicleDashboard(http) {
        this.http = http;
        this.baseUrl = environment_prod_1.environment.apiEndpoint;
    }
    CustomerVehicleDashboard.prototype.getcustomerVehiclesList = function () {
        return this.http.get(this.baseUrl + '/Values');
    };
    CustomerVehicleDashboard.prototype.getCustomerVehicleDetails = function (customerId, vehicleId) {
        return this.http.get(this.baseUrl + '/Values' + customerId + '/' + vehicleId);
    };
    return CustomerVehicleDashboard;
}());
exports.CustomerVehicleDashboard = CustomerVehicleDashboard;
//# sourceMappingURL=customer-vehicle-dashboard.service.js.map