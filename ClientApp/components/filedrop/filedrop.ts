import Vue from "vue";
import vueFilePond from "vue-filepond";
import "filepond/dist/filepond.min.css";
import "../../../wwwroot/dist/filepond.css";
import axios from "axios";
import {VueGoodTable}  from 'vue-good-table';
import "vue-good-table/dist/vue-good-table.css";

const VueFilePond = vueFilePond();

export default Vue.extend({
  components: { VueGoodTable },
  data() {
    return {
      myFiles: [],
      columns: [
        {
          label: 'ID',
          field: 'userId',
          type:'number'
        },
        {
          label: 'Name',
          field: 'staffName',
        },
        {
          label: 'Attend Time',
          field: 'dateTime',
          type: 'date',
          dateInputFormat: "YYYY-MM-DDTHH:mm", //2019-01-13T15:24:26.1878036+08:00
          dateOutputFormat: "hh:mm aa DD-MM-YYYY" // outputs Mar 16th 2018
        },
      ],
      rows: [],
    };
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
        .then(res => {
          self.rows = res.data.data;
        });
      
    }
  }
});
