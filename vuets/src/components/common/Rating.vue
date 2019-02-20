<template>
  <div>
    <span class="group" v-for="i in stars" :key="i">
      <v-icon v-if="rating >= i" @click="rate(i)">star</v-icon>
      <v-icon v-else @click="rate(i)">star_border</v-icon>
    </span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Prop, Component, Emit } from 'vue-property-decorator';

@Component
export default class Rating extends Vue {
  @Prop({ default: 5 }) public stars!: number;
  @Prop({ required: true, type: Number }) public rating!: number;

  @Emit()
  public rate(rated: number) {
    return rated;
  }

  protected created() {
    if (this.stars < 0 || this.stars > 15) {
      this.stars = 5;
    }
  }
}
</script>
