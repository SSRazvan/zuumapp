import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddContactCommand, ContactsClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-create-contact',
  templateUrl: './create-contact.component.html',
  styleUrls: ['./create-contact.component.scss']
})
export class CreateContactComponent implements OnInit {


  isDisabled: boolean = true;

  contactsFormGroup = new FormGroup({
    name: new FormControl('', [
      Validators.required
    ]),
    email: new FormControl('', [
      Validators.required, Validators.email
    ]),
    phoneNumber: new FormControl('', [
      Validators.required
    ]),
  });

  constructor(private contactsClient: ContactsClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {

  }

  ngOnInit() {
    this.onChanges();
  }
  onChanges() {
    this.contactsFormGroup.valueChanges.subscribe(rez => {
      if (this.contactsFormGroup.valid) {
        this.isDisabled = false;
      }
      if (!this.contactsFormGroup.valid) {
        this.isDisabled = true;
      }
    });
  }
  createContact(): void {
    var addContactCommand = new AddContactCommand({
      name: this.contactsFormGroup.value.name,
      email: this.contactsFormGroup.value.email,
      phoneNumber: this.contactsFormGroup.value.phoneNumber.toString()
    });
    this.contactsClient.addContact(addContactCommand).subscribe(result => {
      this.router.navigate(['contacts']);
    },
      err => {
        this.toastr.error(err.toString(), "Error");
      },
      () => this.toastr.success('Contact was created successfully!', 'Success!'));
  }


}
