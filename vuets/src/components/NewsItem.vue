<template>
  <v-card-title primary-title>
    <div>
      <div class="headline">{{title}}</div>
      <div>{{description}}</div>
    </div>
  </v-card-title>
</template>

<script lang="ts">
import Vue from 'vue';
import { Prop, Component } from 'vue-property-decorator';

@Component
export default class NewsItem extends Vue {
  @Prop(String) public title!: string;
  @Prop(String) public description!: string;
}
</script>
