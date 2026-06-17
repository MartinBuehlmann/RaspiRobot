import { Severity } from "./severity";

export interface AlarmModel {
    code: string;
    message: string;
    severity : Severity;
    dateTime: Date;
    isActive: boolean;
}

