import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IVrstaOpremeUslugeData } from 'src/app/interfaces/vrsta-opreme-usluge-data';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(private http: HttpClient) { }

  
  public getAllEquipment() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/OpremaUsluga/Oprema`);
  }

  public getAllServices() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/OpremaUsluga/Usluge`);
  }

  /*
  public getAllEquipmentByType(type: number) : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/OpremaUsluge/Vrste/${type}`);
  }
  */

  public updateEquipment(id: number, data: any) : Observable<any> {
    return this.http.put<any>(`${environment.apiUrlHttps}/OpremaUsluga/${id}`, data);
  }

  public addEquipment(data: any) : Observable<any> {
    return this.http.post<any>(`${environment.apiUrlHttps}/OpremaUsluga`, data);
  }

  public deleteEquipment(id: number) : Observable<any> {
    return this.http.delete<any>(`${environment.apiUrlHttps}/OpremaUsluga/${id}`);
  }

  // VrstaOpremeUsluge

  public getTypesOfEquipment() : Observable<Array<IVrstaOpremeUslugeData>> {
    return this.http.get<Array<IVrstaOpremeUslugeData>>(`${environment.apiUrlHttps}/VrstaOpremeUsluge/Oprema`);
  }

  public getTypesOfServices() : Observable<Array<IVrstaOpremeUslugeData>> {
    return this.http.get<Array<IVrstaOpremeUslugeData>>(`${environment.apiUrlHttps}/VrstaOpremeUsluge/Usluge`);
  }

  public updateTypeOfEquipment(id: number, data: any) : Observable<any> {
    return this.http.put<any>(`${environment.apiUrlHttps}/VrstaOpremeUsluge/${id}`, data);
  }

  public addTypeOfEquipment(data: any) : Observable<any> {
    return this.http.post<any>(`${environment.apiUrlHttps}/VrstaOpremeUsluge`, data);
  }

  public deleteTypeOfEquipment(id: number) : Observable<any> {
    return this.http.delete<any>(`${environment.apiUrlHttps}/VrstaOpremeUsluge/${id}`);
  }

  public getUnits() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/JedinicaMjere`);
  }
}
