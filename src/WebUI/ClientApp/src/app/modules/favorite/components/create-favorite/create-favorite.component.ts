import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddContactToFavoritesCommand, ContactDTO, ContactsClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-create-favorite',
  templateUrl: './create-favorite.component.html',
  styleUrls: ['./create-favorite.component.scss']
})
export class CreateFavoriteComponent implements OnInit {

  isDisabled: boolean = true;
  contacts: ContactDTO[] = [];


  contactsFormGroup = new FormGroup({
    contact: new FormControl('', [
      Validators.required
    ]),
  });

  constructor(private contactsClient: ContactsClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {

  }
  ngOnInit() {
    this.contactsClient.getContacts(null, null, null).subscribe(paginatedData => {
      this.contacts = paginatedData.data;
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
  createFavorite(): void {

    var addContactCommand = new AddContactToFavoritesCommand({
      contactId: this.contactsFormGroup.value.contact,
      isFavorite: true,
    });
    this.contactsClient.addContactToFavorites(addContactCommand).subscribe(res => {
      this.router.navigate(['favorites']);
    },
      err => {
        this.toastr.error(err.toString(), "Error");
      },
      () => this.toastr.success('Contact was created successfully!', 'Success!'));


    }

}
