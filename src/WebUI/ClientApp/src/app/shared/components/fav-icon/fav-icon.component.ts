import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { faHeart, faHeartBroken } from '@fortawesome/free-solid-svg-icons';
import { faHeart as hrt } from '@fortawesome/free-regular-svg-icons';
import { AddContactToFavoritesCommand, ContactsClient } from 'src/app/web-api-client';


@Component({
  selector: 'fav-icon',
  templateUrl: './fav-icon.component.html',
  styleUrls: ['./fav-icon.component.scss']
})
export class FavIconComponent implements OnInit {

  public faHeart: any = faHeart;
  public faHeartBroken: any = hrt;

  @Input() isFavorite : boolean;
  @Input() contactId: number;

  @Output() changedFavoriteEvent = new EventEmitter<boolean>();

  constructor(private contactsClient: ContactsClient) { }

  ngOnInit(): void {
  }

  toogleIsFavorite(event: any): void {
    event.stopPropagation();
    this.isFavorite = !this.isFavorite
    var addContactCommand = new AddContactToFavoritesCommand({
      contactId: this.contactId,
      isFavorite: this.isFavorite,
    });
    this.contactsClient.addContactToFavorites(addContactCommand).subscribe(res => {

    });
    this.changedFavoriteEvent.emit(this.isFavorite);
  }

}
