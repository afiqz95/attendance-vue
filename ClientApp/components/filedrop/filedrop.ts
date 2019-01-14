import Vue from "vue";
import vueFilePond from "vue-filepond";
import "filepond/dist/filepond.min.css";
import "../../../wwwroot/dist/filepond.css";
import axios from "axios";

const VueFilePond = vueFilePond();

export default Vue.extend({
  data() {
    return { myFiles: [] };
  },
  methods: {
    upload() {
      var self: any = this;
      var pond: any = this.$refs.pond;
      var file = pond.getFiles();
      var fd = new FormData();
      fd.append("file", file[0].file);
      axios
        .post("/api/fileApi/uploadFile", fd, {
          headers: { "Content-Type": "multipart/form-data" }
        })
        .then(res => console.log(res));
    }
  }
});
