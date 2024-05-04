import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { Appointment } from 'src/UserModel';
import { ApiService } from 'src/appservices/api.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent {
  value : any;
  editIcon : any = "../assets/images/editor.png";
  constructor(private service: ApiService, private route: ActivatedRoute, private router: Router){ }

  ngOnInit(){

    let id : Number;

    this.route.params.subscribe(params => {
        id = params['id'];
        });

    try{
        this.service.getAppointments()
        .pipe( map((val : Appointment[]) => { 
          this.value = val.find(x => x.appointmentNumber == id);          
         }) )
        .subscribe({ error(err) { } });
        
        console.log(this.value);
      }
      catch (err : any){
      }
      
  }
}
