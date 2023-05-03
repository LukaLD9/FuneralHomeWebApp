import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ISmrtniSlucajData } from 'src/app/interfaces/smrtnislucaj-data';
import { DeathService } from 'src/app/services/death/death.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-death-dialog',
  templateUrl: './death-dialog.component.html',
  styleUrls: ['./death-dialog.component.scss']
})
export class DeathDialogComponent implements OnInit {
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
    private _fb: FormBuilder,
    private _deathService: DeathService,
    private _userService: UserService,
    private _dialogRef: MatDialogRef<DeathDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private readonly snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    // dohvati sve korisnike i spremi njihov id i ime u polje
    this._userService.getAllUsers().subscribe((res) => {
      // iz svakog korisnika izvuci id i ime
      res.forEach((k) => {
        if(k.vrstaKorisnika === 'K'){
          this.korisnici.push( {korisnikId : k.id, imeIprezime : k.ime +  " " + k.prezime});
        }
      });
    });

    console.log("nesto" + this.korisnici)
      if (this.data) {
        this.deathForm.patchValue({
          imePok: this.data.imePok,
          prezimePok: this.data.prezimePok,
          oibpok: this.data.oibpok,
          datumRodenjaPok: this.data.datumRodenjaPok,
          datumSmrtiPok: this.data.datumSmrtiPok,
          korisnikId: this.data.korisnikId,
        });
      }
  }

  onFormSubmit() {
    if (this.deathForm.valid) {
      if (this.data) {
        this.toUpdate = {
          Id: this.data.id,
          ImePok: this.deathForm.value.imePok,
          PrezimePok: this.deathForm.value.prezimePok,
          Oibpok: this.deathForm.value.oibpok,
          DatumRodenjaPok: this.deathForm.value.datumRodenjaPok,
          DatumSmrtiPok: this.deathForm.value.datumSmrtiPok,
          KorisnikId: this.deathForm.value.korisnikId,
        }
        console.log(this.toUpdate);
        this._deathService
          .updateDeath(this.data.id, this.toUpdate)
          .subscribe({
            next: (val: any) => {
              this.snackBar.open('Smrtni slučaj uspješno uređen!', 'U redu', {
                duration: 3000,
              });
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
              this.snackBar.open('Greška prilikom uređivanja smrtnog slučaja!', 'Zatvori', {
                duration: 3000,
              });
            },
          });
      } else {
        this.toUpdate = {
          Id: 0,
          ImePok: this.deathForm.value.imePok,
          PrezimePok: this.deathForm.value.prezimePok,
          Oibpok: this.deathForm.value.oibpok,
          DatumRodenjaPok: this.deathForm.value.datumRodenjaPok,
          DatumSmrtiPok: this.deathForm.value.datumSmrtiPok,
          KorisnikId: this.deathForm.value.korisnikId,
        }
        this._deathService.addDeath(this.toUpdate).subscribe({
          next: (val: any) => {
            this.snackBar.open('Smrtni slučaj uspješno dodan!', 'U redu', {
              duration: 3000,
            });
            this._dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
            this.snackBar.open('Greška prilikom dodavanja smrtnog slučaja!', 'Zatvori', {
              duration: 3000,
            });
          },
        });
        console.log(this.toUpdate);
      }
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