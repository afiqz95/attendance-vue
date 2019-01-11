import Vue from "vue";
import vueFilePond from "vue-filepond";
import "filepond/dist/filepond.min.css";
import "../../../wwwroot/dist/filepond.css";

const VueFilePond = vueFilePond();

export default Vue.extend({
  data() {
    return { myFiles: [] };
  }
});
