import { __decorate } from "tslib";
import { Component } from '@angular/core';
import UserModel from '../shared/UserModel';
let AppComponent = class AppComponent {
    constructor(server) {
        this.server = server;
        this.title = "app-angular";
    }
    ngOnInit() {
    }
    OnClick(item) {
        const newProfile = new UserModel();
        newProfile.Username = "new_username";
        newProfile.FirstName = "New";
        newProfile.LastName = "User";
        newProfile.MobileNo = "987-654-3210";
        newProfile.Email = "new.user@example.com";
        newProfile.CurrentAddress = "456 Side St, Town";
        newProfile.DOB = new Date("1995-05-05");
        newProfile.Password = "newpassword789";
        this.server.post(newProfile);
        this.server.OnPostUser().subscribe(users => (this.users = users));
        console.warn(this.users);
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app-root',
        templateUrl: './app.component.html',
        styleUrls: ['./app.component.css']
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map