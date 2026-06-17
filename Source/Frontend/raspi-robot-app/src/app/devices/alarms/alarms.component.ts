import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RobotService } from '../../services/robot/robot.service';
import { Observable, forkJoin } from 'rxjs';
import { AlarmModel } from '../../services/robot/alarm-model';

@Component({
  selector: 'app-alarms',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './alarms.component.html',
  styleUrls: ['./alarms.component.scss']
})

// TODO: As soon as other devices also support alarms, this view should be reused and the service should be replacable.
// For now, it's only for the robot, so we can directly use the RobotService here.
export class AlarmsComponent implements OnInit {

  alarms: AlarmModel[] = [];

  // Track changes
  private originalState = new Map<string, boolean>();

  loading = false;
  saving = false;

  constructor(
    private robotService: RobotService,
    private changeDetection : ChangeDetectorRef) {}

    ngOnInit(): void {
    this.loadAlarms();
  }

  loadAlarms(): void {
    this.loading = true;

    this.robotService.getAllAlarms()
    .subscribe({
      next: (data) => {
        this.alarms = data;

        // store original state
        this.originalState.clear();
        data.forEach(a => this.originalState.set(a.code, a.isActive));
        this.loading = false;
        this.changeDetection.detectChanges();
      },
      error: (err) => console.error('Failed to load alarms', err),
    });
  }

  hasChanges(): boolean {
    return this.alarms.some(a => this.originalState.get(a.code) !== a.isActive);
  }

  save(): void {
    const updates: Observable<any>[] = [];

    this.alarms.forEach(alarm => {
      const original = this.originalState.get(alarm.code);
      if (original !== alarm.isActive) {
        updates.push(
          this.robotService.updateAlarmIsActive(alarm.code, alarm.isActive)
        );
      }
    });

    if (updates.length === 0) {
      return;
    }

    this.saving = true;

    forkJoin(updates)
    .subscribe({
      next: () => {
        console.log('Updates successful');
        this.loadAlarms(); // reload to sync state
      },
      error: (err) => console.error('Update failed', err),
      complete: () => this.saving = false
    });
  }
}