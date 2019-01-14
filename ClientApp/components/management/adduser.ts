import Vue from "vue";
import { Component } from "vue-property-decorator";
import Axios from "axios";

export default Vue.extend({
  data() {
    return {
      id: 0,
      name: ""
    };
  },
  methods: {
    insertNewUser() {
      var self: any = this;
      Axios.post("/api/fileApi/newUser", { Id: self.id, Name: self.name });
    }
  }
});
