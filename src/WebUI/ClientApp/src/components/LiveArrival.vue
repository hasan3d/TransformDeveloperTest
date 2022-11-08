<template>
     <table class="table table-bordered mt-5">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Platform Name</th>
          <th scope="col">Line Name</th>
          <th scope="col">Destination</th>
          <th scope="col">Expected Arrival</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="arrivalData in liveArrivalsData" :key="arrivalData.trainsOrderIdByTimeToStation">
          <td v-bind:style="{ 'background': arrivalData.lineColour }">
              {{ arrivalData.trainsOrderIdByTimeToStation }}
          </td>
          <td>
            {{arrivalData.platformName}}
          </td>
          <td>
            {{arrivalData.lineName}}
          </td>
          <td>
            {{arrivalData.destination}}
          </td>
          <td>
            {{arrivalData.timeToStation}}
          </td>
        </tr>
      </tbody>
    </table>
</template>
  
<script lang="ts">
import { defineComponent, PropType } from 'vue';
import {ILiveArrivalsResults} from '../interfaces/liveArrivalsResult'
export default defineComponent({

    props: {
    liveArrivalsData: {
            required: true,
            type: Array as PropType<ILiveArrivalsResults[]>
        }
    },
});
</script>
  
<style scoped>
   
  </style>