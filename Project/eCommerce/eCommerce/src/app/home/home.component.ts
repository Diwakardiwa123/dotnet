import { Component, OnInit } from '@angular/core';
import HelperService from '../../shared/helper.service';
import ProductModel from '../../shared/ProductModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
    public products!: Array<ProductModel>;
    public newProducts!: Array<ProductModel>;
    constructor(private server: HelperService) {

    }   

    ngOnInit() {
        
        this.server.getproducts().subscribe(product => {
            console.warn(product);
        });
        //this.newProducts = this.products.filter(x => x.IsNewCollection = 'true');
    }

    onclick() {
        
    }
}
