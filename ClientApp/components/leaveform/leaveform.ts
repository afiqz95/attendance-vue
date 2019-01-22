import Vue from "vue";
import { Component } from "vue-property-decorator";
import Axios from "axios";
import { VueGoodTable } from "vue-good-table";

export default Vue.extend({
  components: { VueGoodTable },
  data() {
    return {
      id: 0,
      name: "",
      columns: [
        {
          label: 'ID',
          field: 'userId',
          type: 'number'
        },
        {
          label: 'Name',
          field: 'staffName',
        },
      ],
      rows: [],
    };
  },
  methods: {
    insertNewUser() {
      var self: any = this;
      Axios
        .post("/api/fileApi/newUser", { Id: self.id, Name: self.name });
      Axios
        .get("/api/fileApi/getStaff")
        .then(res => { self.rows = res.data })
    },
    selectionChanged(item) {
      var self: any = this;
      console.log(self.$refs['test'].selectedRows);
    },
    deleteSelected() {
      var self: any = this;
      var mapped = self.$refs['test'].selectedRows.map(x => {
        return {
          Id: x.userId
        }
      });
      console.log(mapped);

      Axios.post('/api/fileApi/deleteStaff', mapped).then(res => {
        Axios
          .get("/api/fileApi/getStaff")
          .then(res => { self.rows = res.data });
      })
    }
  },
  created() {
    var self: any = this;
    Axios
      .get("/api/fileApi/getStaff")
      .then(res => { self.rows = res.data })
  }

});
