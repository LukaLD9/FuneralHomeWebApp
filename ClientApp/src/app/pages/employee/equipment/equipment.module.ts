import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainFooterModule } from 'src/app/components/main-footer/main-footer.module';
import { MainNavigationModule } from 'src/app/components/main-navigation/main-navigation.module';
import { EquipmentComponent } from './equipment.component';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { EquipmentDialogModule } from 'src/app/components/dialogs/equipment-dialog/equipment-dialog.module';
import { TypeDialogModule } from 'src/app/components/dialogs/type-dialog/type-dialog.module';
import { EquipmentTypesComponent } from './equipment-types/equipment-types.component';



@NgModule({
  declarations: [EquipmentComponent, EquipmentTypesComponent],
  imports: [
    CommonModule,
    MainFooterModule,
    MainNavigationModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    EquipmentDialogModule,
    TypeDialogModule
  ],
  exports: [EquipmentComponent]
})
export class EquipmentModule { }
