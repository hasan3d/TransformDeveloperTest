export interface ILiveArrivalsResults {
    trainsOrderIdByTimeToStation: number
    platformName: string,
    lineName: string,
    destination: string,
    lineColour: string,
    timeToStation: string
}

export interface ITrasnformedLiveArrivalsResults {
    stationName: string,
    arrivals: ILiveArrivalsResults[]
}