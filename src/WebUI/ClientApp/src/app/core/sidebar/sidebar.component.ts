import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

    @ViewChild('sidenav') sidenav: MatSidenav;
    isExpanded = true;
    showSubmenu: boolean = false;
    isShowing = false;
    showSubSubMenu: boolean = false;

    ngOnInit(): void {
        
    }
    
    mouseenter() {
        debugger;
        if (!this.isExpanded) {
            this.isShowing = true;
        }
        if (this.isExpanded) {
            this.isShowing = false;
        }
    }

    mouseleave() {
        debugger;
        // if (!this.isExpanded) {
        //     this.isShowing = false;
        // }
    }


}
