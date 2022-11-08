export interface ILiveArrivals {
    platformName: string,
    lineName: string,
    destination: string,
    lineColour: string,
    timeToStation: number
}

export interface ILiveArrivalsResult {
    stationName: string,
    arrivals: ILiveArrivals[]
}