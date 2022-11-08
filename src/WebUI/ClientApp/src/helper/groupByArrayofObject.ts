// eslint-disable-next-line
const groupByArrayofObject = (arrayOfData: any[], key: string) => {
    return arrayOfData.reduce((rv, x) => {
        // eslint-disable-next-line
      ((rv[x[key]] = rv[x[key]] || []) as any).push(x);
      return rv;
    }, {});
  };

  export default groupByArrayofObject;
