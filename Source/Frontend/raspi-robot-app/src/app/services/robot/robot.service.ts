import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PositionModel } from '../mdi/position-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RobotService {

  constructor(private http : HttpClient) { }

  getAllAxisCurrentPositions() : Observable<PositionModel[]> {
    return this.http.get<PositionModel[]>('web/Robot/Axis/All/CurrentPosition');
  }

  getAxisCurrentPositions(axisNumber : number) : Observable<PositionModel> {
    return this.http.get<PositionModel>(`web/Robot/Axis/${axisNumber}/CurrentPosition`);
  }
}
