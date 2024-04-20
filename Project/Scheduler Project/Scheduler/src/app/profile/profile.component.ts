import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, switchMap } from 'rxjs';
import { UserTable } from 'src/UserModel';
import { ApiService } from 'src/appservices/api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  isEditing: boolean = false;
  currentUser: any;
  userIcon: any = "../assets/images/user.png";
  
  constructor(private route: Router, private service: ApiService){ }

  ngOnInit(){
      this.service.getUser()
          .pipe( map((val : UserTable) => this.currentUser = val) )
          .subscribe({ error(err) { } }); 
  }

  onDeleteClicked() {
    this.service.deleteUser(this.currentUser)
      .subscribe((res) => {
        localStorage["token"] = "";
        this.route.navigateByUrl("/home/appointment");
      });
  }

  onEditClicked() {
    this.isEditing = !this.isEditing;
  }

  onConfirmClicked() {
    this.service.updateUser(this.currentUser)
      .subscribe({ next(val) { console.log(val);}});
      this.isEditing = !this.isEditing;
  }
}