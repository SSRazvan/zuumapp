import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { faEdit, faInfo, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ContactDTO, ContactsClient, PaginatedDataVmOfContactDTO } from 'src/app/web-api-client';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.scss']
})
export class FavoriteComponent implements OnInit {

  showLoadingSpinner: boolean = true;

  search: string = "";

  contacts: ContactDTO[] = [];

  dataSource = new MatTableDataSource<ContactDTO>(this.contacts);

  displayedColumns: string[] = ['id', 'name', 'email', 'phone', 'actions'];

  faEdit: any = faEdit;
  faTrash: any = faTrash;
  faInfo: any = faInfo;

  pageSize: number = 10;
  pageIndex: number = 1;
  length: number = 0;
  paginatedData: PaginatedDataVmOfContactDTO;

  isFavorite: boolean = false;
  @Output() changeFavorite = new EventEmitter<boolean>();

  constructor(private router: Router,
              private contactsClient: ContactsClient,
              private activatedRoute: ActivatedRoute,
              public dialog: MatDialog,
              private toastr: ToastrService) { }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {
    this.showLoadingSpinner = true;
    this.contactsClient.getFavorites(this.pageIndex, this.pageSize).subscribe(paginatedData => {
      this.showLoadingSpinner = false;
      this.paginatedData = paginatedData;
      this.contacts = paginatedData.data;
      this.dataSource = new MatTableDataSource<ContactDTO>(this.contacts);
      this.length = this.paginatedData.totalCount;
    });
  }
  public handlePage(e: PageEvent) {
    this.pageIndex = e.pageIndex + 1;
    this.pageSize = e.pageSize;
    this.showLoadingSpinner = true;
    this.contactsClient.getFavorites(this.pageIndex, this.pageSize).subscribe(paginatedData => {
      this.showLoadingSpinner = false;
      this.paginatedData = paginatedData;
      this.contacts = paginatedData.data;
      this.dataSource = new MatTableDataSource<ContactDTO>(this.contacts);
      this.length = this.paginatedData.totalCount;
    });
  }
  onCreate() {
    this.router.navigate(['favorites/add-favorite']);
  }

}
