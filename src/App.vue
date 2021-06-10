<template>
  <div id="app">
    <div class="wrapper">
      <SideBar/>
        <div class="content">
          <WorkingMode 
            v-if="showModeWorking"
            v-on:showHideFormMode="showHideFormMode"
          />
          <Header v-on:showHideFormMode="showHideFormMode"/>
          <Main/>
        </div>
    </div>
    <EmployeeDetail />
    <Loading />
  </div>
</template>

<script>
import SideBar from './components/SideBar.vue';
import Header from './components/Header.vue';
import Main from './components/Main.vue';
import WorkingMode from './components/working_mode.vue';
import EmployeeDetail from './components/EmployeeDetail.vue';
import Loading from './components/Loading.vue';
export default {
  data(){
    return {
      showModeWorking : false,
    }
  },
  components: {
    SideBar, Header, Main, WorkingMode, EmployeeDetail, Loading
  },

  methods: {
    showHideFormMode(){
      this.showModeWorking = !this.showModeWorking;
    },
  }
}
</script>

<style src="./css/app.css"></style>
