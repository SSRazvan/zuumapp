import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SharedModule } from '../shared/shared.module';
//import { FooterComponent } from './footer/footer.component';
//


@NgModule({
    //declarations: //[FooterComponent],//
    imports: [
        SharedModule,
        CommonModule,
        CoreRoutingModule
    ],
    declarations: [
      SidebarComponent
    ],
    exports: [
        SidebarComponent
    ]
})
export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error(
                'CoreModule is already loaded. Import it in the AppModule only'
            );
        }
    }
}
