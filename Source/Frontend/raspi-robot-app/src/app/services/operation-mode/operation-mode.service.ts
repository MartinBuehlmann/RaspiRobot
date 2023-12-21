import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperationModeService {

  constructor(private http : HttpClient) { }

  getOperationModes(): Observable<string[]> {
    return this.http.get<string[]>('/web/OperationMode/All');
  }

  getCurrentOperationMode(): Observable<string> {
    return this.http.get<string>('web/OperationMode/Current');
  }

  setOperationMode(newOperationMode : string) {
    this.http.put('web/OperationMode/' + newOperationMode, '');
  }
}
