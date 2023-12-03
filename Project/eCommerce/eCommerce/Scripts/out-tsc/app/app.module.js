import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import HelperService from '../shared/helper.service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            HomeComponent
        ],
        imports: [
            BrowserModule,
            AppRoutingModule,
            FormsModule,
            HttpClientModule
        ],
        providers: [HelperService],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map