import { Component, Input, OnInit, Inject, Optional } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-general-dialog',
  templateUrl: './general-dialog.component.html',
  styleUrls: ['./general-dialog.component.scss']
})
export class GeneralDialogComponent implements OnInit {
  
  @Input() title: string = "Are you sure you want to delete?";

 
  constructor(private router : Router,
              @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {
  }
  ngOnInit(): void {
    
  }
  


}

