 // eslint-disable-next-line
 const getUniquePlatformNames = (arrayOfData: any[]) => {
    return arrayOfData.map(item => item.platformName)
    .filter((value, index, self) => self.indexOf(value) === index);
}

export default getUniquePlatformNames;