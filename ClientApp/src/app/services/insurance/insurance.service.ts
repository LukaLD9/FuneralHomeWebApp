import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InsuranceService {

  constructor(private http: HttpClient) { }

  public getAllInsurances() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/Osiguranje`);
  }

  public getInsurancesByUserId(id: number) : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/Osiguranje/Korisnik/${id}`);
  }

  public getInsuranceByDeathId(id: number) : Observable<any> {
    return this.http.get<any>(`${environment.apiUrlHttps}/Osiguranje/SmrtniSlucaj/${id}`);
  }

  public addInsurance(data: any) : Observable<any> {
    return this.http.post(`${environment.apiUrlHttps}/Osiguranje`, data);
  }

  public updateInsurance(id: number, data: any) : Observable<any> {
    return this.http.put(`${environment.apiUrlHttps}/Osiguranje/${id}`, data);
  }

  public deleteInsurance(id: number) : Observable<any> {
    return this.http.delete(`${environment.apiUrlHttps}/Osiguranje/${id}`);
  }

  public getInsurancePackages() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/PaketOsiguranja`);
  }
}
