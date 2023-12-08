import { __decorate } from "tslib";
import { Component } from '@angular/core';
let HomeComponent = class HomeComponent {
    constructor(server) {
        this.server = server;
        this.isLoading = true;
    }
    ngOnInit() {
        setTimeout(() => {
            this.server.getproducts()
                .subscribe(product => (this.products = product), error => alert(error), () => console.warn(this.products));
            this.isLoading = false;
        }, 2000);
    }
    onclick() {
        console.warn("clicked");
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