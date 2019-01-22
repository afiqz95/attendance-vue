import "./css/site.css";
import "bootstrap";
import Vue from "vue";
import VueRouter from "vue-router";
Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    component: require("./components/filedrop/filedrop.vue.html"),
    meta: { title: "File Upload" }
  },
  {
    path: "/counter",
    component: require("./components/counter/counter.vue.html")
  },
  {
    path: "/fetchdata",
    component: require("./components/fetchdata/fetchdata.vue.html")
  },
  {
    path: "/addUser",
    component: require("./components/management/adduser.vue.html"),
    meta: { title: "Add User" }
  },
  {
    path: "/leaveForm",
    component: require("./components/leaveform/leaveform.vue.html"),
    meta: { title: "Leave Form" }
  }
];

new Vue({
  el: "#app-root",
  router: new VueRouter({ mode: "history", routes: routes }),
  render: h => h(require("./components/app/app.vue.html"))
});
