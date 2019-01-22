import Vue from "vue";
import { Component } from "vue-property-decorator";
import Axios from "axios";
import { VueGoodTable } from "vue-good-table";
import flatPickr from "vue-flatpickr-component";
import "flatpickr/dist/flatpickr.css";

export default Vue.extend({
  components: {
    flatPickr
  },
  data() {
    return {
      Staffs: [],
      Selected: null,
      from: null,
      to: null,
      formType: null,
      Days: 0
    };
  },
  methods: {
    changeDay(number) {
      var self: any = this;
      self.Days = number;
    },
    AddDays() {
      var self: any = this;
      Axios.post("/api/leave", { StaffId: self.Selected, Days: self.Days }).then(res =>
        console.log(res)
      );
    }
  },
  created() {
    var self: any = this;
    Axios.get("/api/FileApi/GetStaff").then(res => {
      console.log(res);
      self.Staffs = res.data;
    });
  }
});
