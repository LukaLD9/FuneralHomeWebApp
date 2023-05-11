import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ISmrtniSlucajData } from 'src/app/interfaces/smrtnislucaj-data';
import { DeathService } from 'src/app/services/death/death.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-death-customer-form',
  templateUrl: './death-customer-form.component.html',
  styleUrls: ['./death-customer-form.component.scss']
})
export class DeathCustomerFormComponent implements OnInit {
  hide = true;
  deathForm: FormGroup = new FormGroup({
    imePok: new FormControl('', [Validators.required]),
    prezimePok: new FormControl('', [Validators.required]),
    oibpok: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]{11}$'),
    ]),
    datumRodenjaPok: new FormControl('', [Validators.required]),
    datumSmrtiPok: new FormControl('', [Validators.required]),
    korisnikId: new FormControl('', [Validators.required]),
  });
  
  toUpdate: ISmrtniSlucajData = {} as ISmrtniSlucajData;

  korisnici : Korisnik[] = [];

  constructor(
    private _deathService: DeathService,
    private _userService: UserService,
    private readonly snackBar: MatSnackBar,
    private dateAdapter: DateAdapter<Date>
  ) {
    this.dateAdapter.setLocale('hr');
  }

  ngOnInit() {
   
  }

  onFormSubmit() {
    if (this.deathForm.valid) {
      if(this.deathForm.value.datumRodenjaPok > this.deathForm.value.datumSmrtiPok){
        this.snackBar.open('Datum smrti ne može biti prije datuma rođenja!', 'U redu', {
          duration: 3000,
        });
        return;
      }
      if(this.deathForm.value.datumSmrtiPok > new Date()){
        this.snackBar.open('Datum smrti ne može biti u budućnosti!', 'U redu', {
          duration: 3000,
        });
        return;
      }
      if(this.deathForm.value.datumRodenjaPok > new Date()){
        this.snackBar.open('Datum rođenja ne može biti u budućnosti!', 'U redu', {
          duration: 3000,
        });
        return;
      }
        this.toUpdate = {
          Id: 0,
          ImePok: this.deathForm.value.imePok,
          PrezimePok: this.deathForm.value.prezimePok,
          Oibpok: this.deathForm.value.oibpok,
          DatumRodenjaPok: new Date(new Date(this.deathForm.value.datumRodenjaPok).getTime() 
            - new Date(this.deathForm.value.datumRodenjaPok).getTimezoneOffset() * 60000),
          DatumSmrtiPok: new Date(new Date(this.deathForm.value.datumSmrtiPok).getTime()
            - new Date(this.deathForm.value.datumSmrtiPok).getTimezoneOffset() * 60000),
          KorisnikId: this.deathForm.value.korisnikId,
        }
        this._deathService.addDeath(this.toUpdate).subscribe({
          next: (val: any) => {
            this.snackBar.open('Smrtni slučaj uspješno dodan!', 'U redu', {
              duration: 3000,
            });
          },
          error: (err: any) => {
            console.error(err);
            this.snackBar.open('Greška prilikom dodavanja smrtnog slučaja!', 'Zatvori', {
              duration: 3000,
            });
          },
        });
        console.log(this.toUpdate);
    } else {
      this.snackBar.open('Popunite sva polja!', 'U redu', {
        duration: 3000,
      });
    }
  }
}

type Korisnik = {
  korisnikId: number;
  imeIprezime: string;
}