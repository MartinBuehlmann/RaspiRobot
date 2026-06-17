import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlarmModel } from './alarm-model';
import { PositionModel } from './position-model';
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

  getAllAlarms() : Observable<AlarmModel[]> {
    return this.http.get<AlarmModel[]>('web/Robot/Alarms');
  }

  updateAlarmIsActive(alarmCode : string, isActive : boolean) : Observable<AlarmModel> {
    return this.http.patch<AlarmModel>(`web/Robot/Alarms/${alarmCode}/IsActive`, { isActive });
  }
}
