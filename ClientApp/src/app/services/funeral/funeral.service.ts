import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPogrebAggregateData } from 'src/app/interfaces/pogreb-aggregate-data';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FuneralService {

  constructor(private http: HttpClient) { }

  public getAllFunereals() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${environment.apiUrlHttps}/Pogreb/PogrebSmrtniSlucaj`);
  }

  public getFuneralDetailById(id: number) : Observable<IPogrebAggregateData> {
    return this.http.get<IPogrebAggregateData>(`${environment.apiUrlHttps}/Pogreb/Aggregate/${id}`);
  }

  public deleteFuneral(id: number) : Observable<any> {
    return this.http.delete<any>(`${environment.apiUrlHttps}/Pogreb/${id}`);
  }
}
