<template>
  <div>
    <slot></slot>
    <!-- <v-btn @click.native="selectFile" v-if="!uploadEnd && !uploading">
      <v-icon aria-hidden="true">add_a_photo</v-icon>
    </v-btn>
    <input
      id="files"
      type="file"
      name="file"
      ref="uploadInput"
      accept="image/*"
      :multiple="false"
      @change="detectFiles($event)"
    >
    <v-progress-circular
      v-if="uploading && !uploadEnd"
      :size="100"
      :width="15"
      :rotate="360"
      :value="progressUpload"
      color="primary"
    >{{ progressUpload }}%</v-progress-circular>
    <img v-if="uploadEnd" :src="downloadURL" width="100%">
    <div v-if="uploadEnd">
      <v-btn class="ma-0" dark small color="error" @click="deleteImage()">Löschen</v-btn>
    </div>-->
  </div>
</template>

<script>
import axios from 'axios';

export default {
  props: { newsId: Number, articleId: Number },
  // data() {
  //   return {
  //     progressUpload: 0,
  //     fileName: '',
  //     uploading: false,
  //     uploadEnd: false,
  //     downloadURL: '',
  //   };
  // },
  // methods: {
  //   selectFile() {
  //     this.$refs.uploadInput.click();
  //   },
  //   detectFiles(e) {
  //     const fileList = e.target.files || e.dataTransfer.files;
  //     Array.from(Array(fileList.length).keys()).map(x => {
  //       this.upload(fileList[x]);
  //     });
  //   },
  //   upload(file) {
  //     console.log(this.fileName);
  //     this.fileName = file.name;
  //     this.uploading = true;

  //     let formData = new FormData();
  //     formData.append('fileBlob', file);

  //     axios
  //       .post(`/news/${this.newsId}/article/${this.articleId}/file`, formData, {
  //         headers: {
  //           'Content-Type': 'multipart/form-data',
  //         },
  //       })
  //       .finally(() => {
  //         this.uploading = false;
  //       });
  //   },
  // },
};
</script>
