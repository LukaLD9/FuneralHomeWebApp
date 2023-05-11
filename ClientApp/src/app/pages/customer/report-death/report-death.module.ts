import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportDeathComponent } from './report-death.component';
import { MainFooterModule } from 'src/app/components/main-footer/main-footer.module';
import { MainNavigationModule } from 'src/app/components/main-navigation/main-navigation.module';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [ReportDeathComponent],
  imports: [
    CommonModule,
    MainFooterModule,
    MainNavigationModule,
    MatButtonModule
  ]
})
export class ReportDeathModule { }
