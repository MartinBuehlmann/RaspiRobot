import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PositionModel } from './position-model';
import { Observable } from 'rxjs';
import { SteppingResultModel } from './stepping-result-model';
import { AxisDirection } from './axis-direction';

@Injectable({
  providedIn: 'root'
})
export class MdiRobotService {

  constructor(private http : HttpClient) { }

  stepSingleAxis(axisNumber : number, direction : AxisDirection) : Observable<SteppingResultModel> {
    return this.http.put<SteppingResultModel>(`web/Mdi/Robot/Axis/${axisNumber}/Step/${direction}`, null);
  }
}