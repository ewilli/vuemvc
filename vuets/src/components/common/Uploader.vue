<template>
  <div>
    <slot></slot>
    <v-btn @click.native="selectFile" v-if="!uploadEnd && !uploading">
      <v-icon aria-hidden="true">add_a_photo</v-icon>
    </v-btn>
    <input
      hidden
      id="files"
      type="file"
      name="file"
      ref="uploadInput"
      accept="image/*"
      :multiple="true"
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
  </div>
</template>
<script>
// Sample for non TS
import axios from 'axios';

export default {
  props: { url: String },
    data() {
      return {
        progressUpload: 0,
        fileName: '',
        uploading: false,
        uploadEnd: false,
      };
    },
    methods: {
      selectFile() {
        this.$refs.uploadInput.click();
      },
      detectFiles(e) {
        const fileList = e.target.files || e.dataTransfer.files;
        Array.from(Array(fileList.length).keys()).map(x => {
          this.upload(fileList[x]);
        });
      },
      upload(file) {
        console.debug('start upload: ' + file.name);

        this.fileName = file.name;
        this.uploading = true;

        const formData = new FormData();
        formData.append('fileBlob', file);

        axios
          .post(this.$props.url,
            formData,
            {
              headers: {
                'Content-Type': 'multipart/form-data',
              },
            },
          )
          .finally(() => {
            this.uploading = false;
          });
      },
    },
};
</script>
