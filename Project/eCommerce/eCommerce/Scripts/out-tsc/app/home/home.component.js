import { __decorate } from "tslib";
import { Component } from '@angular/core';
let HomeComponent = class HomeComponent {
    constructor(server) {
        this.server = server;
    }
    ngOnInit() {
    }
    onclick() {
        this.server.getproducts().subscribe(product => {
            console.warn(product);
        });
        //this.newProducts = this.products.filter(x => x.IsNewCollection = 'true');
    }
};
HomeComponent = __decorate([
    Component({
        selector: 'app-home',
        templateUrl: './home.component.html',
        styleUrls: ['./home.component.css']
    })
], HomeComponent);
export { HomeComponent };
//# sourceMappingURL=home.component.js.map