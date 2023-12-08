import { Component, OnInit } from '@angular/core';
import HelperService from '../../shared/helper.service';
import ProductModel from '../../shared/ProductModel';
import { delay, map, tap } from "rxjs/operators";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    isLoading = true;

    public products!: Array<ProductModel>;
    public newProducts!: Array<ProductModel>;
    constructor(private server: HelperService) {}   

    ngOnInit()
    {
        setTimeout(
        () =>
        {
            this.server.getproducts()
                .subscribe(product => (this.products = product), error => alert(error), () => console.warn(this.products));

            this.isLoading = false;
            
        }, 2000);
    }

    onclick() {
        console.warn("clicked");
    }
}
