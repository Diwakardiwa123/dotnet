import { Component, OnInit } from '@angular/core';
import HelperService from '../shared/helper.service';
import  UserModel  from '../shared/UserModel'


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = "app-angular"
    users!: Array<UserModel>;
    
    constructor(private server: HelperService)
    {
    }

    ngOnInit() {
        
    }


    OnClick(item:any)
    {
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
}
