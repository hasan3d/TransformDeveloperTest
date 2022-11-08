<template>
  <LayoutHeader />
  <div class="container">
    <h2 class="mt-2">{{stationName}}</h2>
    <h3 class="mt-3">Live arrivals at {{new Date().toLocaleTimeString()}}</h3>
  </div>
  <div class="container" v-for="platformName in uniquePlatformNames" :key="platformName">
    <LiveArrivals :liveArrivalsData="groupByArrayofObject(transformedData.arrivals, 'platformName')[platformName]" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, onUnmounted} from 'vue';
import LayoutHeader from './components/layout/LayoutHeader.vue'
import LiveArrivals from './components/LiveArrival.vue'
import {ITrasnformedLiveArrivalsResults} from './interfaces/liveArrivalsResult'
import tflApi from './api/tfl'
import transformArrivalsData from './helper/arrivalsDataTrasnformer'
import groupByArrayofObject from './helper/groupByArrayofObject';
import getUniquePlatformNames from './helper/getUniquePlatformNames'

export default defineComponent({
  name: 'App',
  components: {
    LayoutHeader,
    LiveArrivals
  },
  setup() { 

    // eslint-disable-next-line
    const uniquePlatformNames = ref<any[]>([]);

    const stationName = ref<string>('');

    // eslint-disable-next-line
    const transformedData = ref<ITrasnformedLiveArrivalsResults>();

    const getStationArrivals = async () => {
      const response = await tflApi.getStationArrivals()
      transformedData.value = transformArrivalsData(response)
      uniquePlatformNames.value = getUniquePlatformNames(transformedData.value.arrivals);
      stationName.value = response.stationName;
    }
    onMounted(() => {
      getStationArrivals();
      const interval = setInterval(function() {
        getStationArrivals();
     }, 30000); // every 30 seconds
     
     onUnmounted(() => {
      clearInterval(interval);
     })

    })
    return {
      uniquePlatformNames,
      groupByArrayofObject,
      transformedData,
      stationName
    }
  }
});
</script>

<style>

</style>
