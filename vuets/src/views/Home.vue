<template>
  <div class="home">
    <v-container grid-list-xs>
      <NewsList :newsUrl="newsUrl"/>
    </v-container>
  </div>
</template>

<script lang="ts">
import NewsList from '@/components/NewsList.vue';
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({
  components: {
    NewsList,
  },
})
export default class Home extends Vue {
  private get newsUrl() {
    return new URL('http://localhost:5000/api/news');
  }
}
</script>

// https://newsapi.org/v2/everything?q=test&sortBy=publishedAt&apiKey=[key]
