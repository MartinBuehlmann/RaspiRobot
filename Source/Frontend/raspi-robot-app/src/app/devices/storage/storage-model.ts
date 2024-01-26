export class StorageModel {
    constructor(name: string, deviceType: string) {
        this.Name = name;
        this.DeviceType = deviceType;
    }

    Name : string;
    DeviceType : string;
}