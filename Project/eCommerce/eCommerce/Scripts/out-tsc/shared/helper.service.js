import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
let HelperService = class HelperService {
    constructor(http) {
        this.http = http;
        this.homeController = 'https://localhost:44364/api/UserProfile/GetUsers';
        this.apiController = 'https://localhost:44364/api/UserProfile/UserPost';
        this.httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }
    OnPostUser() {
        return this.http.get(this.homeController, this.httpOptions);
    }
    post(user) {
        this.http.post(this.apiController, user, this.httpOptions).subscribe();
    }
};
HelperService = __decorate([
    Injectable()
], HelperService);
export default HelperService;
//# sourceMappingURL=helper.service.js.map