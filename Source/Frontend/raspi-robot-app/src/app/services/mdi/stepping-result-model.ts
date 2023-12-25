import { PositionModel } from "./position-model";

export interface SteppingResultModel {
    executed : boolean;
    newPosition : PositionModel;
}