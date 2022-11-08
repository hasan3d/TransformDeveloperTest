import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';

import {
    ILiveArrivalsResult
  } from '@/api/tfl/interface';

  class TflClient {
    private readonly client: AxiosInstance;

    constructor() {
      const baseURL = process.env.VUE_APP_TFL_API_URL;
      this.client = axios.create({
        baseURL
      });    
    }

    public getStationArrivals = () =>
    this.get<ILiveArrivalsResult>({
        url: '/api/Tfl/GetStationArrivals',
    });

    protected get<T>(config: AxiosRequestConfig) {
        return this.makeRequest<T>({
          ...config,
          method: 'get',
        });
      }

      private makeRequest = <T>(config: AxiosRequestConfig) =>
        this.client.request<T>(config).then((x) => x.data);
  }

  export default new TflClient();