import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ContactDTO, ContactsClient, UpdateContactCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrls: ['./update-contact.component.scss']
})
export class UpdateContactComponent implements OnInit {

  isDisabled: boolean = true;

  contact: ContactDTO;

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
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(paramMap => {
      this.contactsClient.getContact(+paramMap.get('id')).subscribe(contact => {
        this.contact = contact;
        this.contactsFormGroup.get("name").setValue(contact.name);
        this.contactsFormGroup.get("email").setValue(contact.email);
        this.contactsFormGroup.get("phoneNumber").setValue(contact.phone);
      });
    });
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
  updateContact(): void {
    this.route.paramMap.subscribe(paramMap => {
      var updateContactCommand = new UpdateContactCommand({
        id: +paramMap.get('id'),
        name: this.contactsFormGroup.value.name,
        email: this.contactsFormGroup.value.email,
        phoneNumber: this.contactsFormGroup.value.phoneNumber.toString()
      });
      this.contactsClient.updateContact(updateContactCommand).subscribe(result => {
        this.router.navigate(['contacts']);
      },
        err => {
          this.toastr.error(err.toString(), "Error");
        },
        () => this.toastr.success('Contact was updated successfully!', 'Success!'));
    })
  }
}
