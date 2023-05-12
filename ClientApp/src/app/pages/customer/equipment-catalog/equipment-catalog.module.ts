import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EquipmentCatalogComponent } from './equipment-catalog.component';
import { MainNavigationModule } from 'src/app/components/main-navigation/main-navigation.module';
import { MainFooterModule } from 'src/app/components/main-footer/main-footer.module';



@NgModule({
  declarations: [EquipmentCatalogComponent],
  imports: [
    CommonModule,
    MainFooterModule,
    MainNavigationModule,
  ]
})
export class EquipmentCatalogModule { }