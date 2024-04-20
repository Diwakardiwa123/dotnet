import { Component } from '@angular/core';
import { UserTable } from 'src/UserModel';
import { ApiService } from 'src/appservices/api.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent {

  constructor(private service: ApiService){ }

  appointmentDate: Date = new Date();
  appointmentTime = "";
  appointmentName = "";
  descriptions = "";

  onSubmitClicked() {
    const appointment = { 
      'appointmentDate' : this.appointmentDate,
      'appointmentTime' : this.appointmentTime,
      'appointmentName' : this.appointmentName,
      'descriptions' : this.descriptions,
    }

    console.log(appointment);
    

    this.service.postAppointment(appointment).subscribe();
  }

}
