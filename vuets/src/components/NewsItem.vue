<template>
  <v-card-title primary-title>
    <uploader :url="`/news/${this.newsId}/article/${this.articleId}/document`" >
      <div class="headline">{{title}}</div>
      <div>{{description}}</div>
    </uploader>
  </v-card-title>
</template>

<script lang="ts">
import Vue from 'vue';
import { Prop, Component } from 'vue-property-decorator';

import uploader from './common/Uploader.vue';

@Component({
  components: {
    uploader,
  },
})
export default class NewsItem extends Vue {
  @Prop(Number) public newsId!: number;
  @Prop(Number) public articleId!: number;
  @Prop(String) public title!: string;
  @Prop(String) public description!: string;
}
</script>
