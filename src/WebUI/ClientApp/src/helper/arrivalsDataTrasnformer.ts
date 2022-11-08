import {ILiveArrivals, ILiveArrivalsResult} from '../api/tfl/interface'
import {ITrasnformedLiveArrivalsResults, ILiveArrivalsResults} from '../interfaces/liveArrivalsResult'
import groupByArrayofObject from './groupByArrayofObject';
import getUniquePlatformNames from './getUniquePlatformNames'

const platformName = 'platformName';

const transformArrivalsData = (arrivalsData: ILiveArrivalsResult): ITrasnformedLiveArrivalsResults => {
    const uniquePlatformNames = getUniquePlatformNames(arrivalsData.arrivals)
    const groupByResult = groupByArrayofObject(arrivalsData.arrivals, platformName)
    const result = createResult(uniquePlatformNames, groupByResult, arrivalsData.stationName)
    return result;
  }

  // eslint-disable-next-line
  const createResult = (uniquePlatformNames: any[], data: any[], stationName: string): ITrasnformedLiveArrivalsResults => {

    const result: ITrasnformedLiveArrivalsResults = {
      arrivals: [],
      stationName: stationName
    };

    for (let i = 0; i < uniquePlatformNames.length; i++) {
    
      data[uniquePlatformNames[i]].map((currElement: ILiveArrivals, index: number) => {
        
        let startIndex = 1 + index;
        const liveArrival = {} as ILiveArrivalsResults;
        liveArrival.trainsOrderIdByTimeToStation = startIndex++;
        liveArrival.destination = currElement.destination;
        liveArrival.lineColour = currElement.lineColour;
        liveArrival.lineName = currElement.lineName;
        liveArrival.platformName = currElement.platformName;
        liveArrival.timeToStation = formatTimeToStationNumber(currElement.timeToStation)

        result.arrivals.push(liveArrival)
      })
    }

    return result;
  }

  const formatTimeToStationNumber = (num: number): string => {
    const result = applyRounding(num);

    if (result === "0" || result === "-0") return "Due";

    if (result === "1" || result === "-1") return "1 min"

    return result + " " + "mins";

  }

  const applyRounding = (num: number): string => {

    const rounded = Math.round(num * 10) / 10

    const fixed = rounded.toFixed(0)

    return fixed;
  }

  export default transformArrivalsData;

  
