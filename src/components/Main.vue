<template>
  <div class="main" @keyup.up.exact="arrowUp()" @keyup.down.exact="arrowDown()">
    <div class="main-title" style="top:0px">
      <div class="main-title-content">
        <div class="title">Nhân viên</div>
        <div class="main-title-btn">
          <button class="btn-more">
            <span>Tiện ích</span><img src="../assets/drop_down.svg" alt="" />
          </button>
          <button v-on:click="addEmployee" class="btn-add">Thêm</button>
          <button class="btn-excel">
            <img src="../assets/drop_down.svg" alt="" />
          </button>
        </div>
      </div>
      </div>
    <div class="layout">
        <div class="grid">
          <div class="excution">
            <span>Thực hiện hàng loạt</span>
            <img src="../assets/drop_down.svg" alt="" />
          </div>
        <div class="right-control">
          <div class="search-name-id">
            <input
              id="search-name-id"
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
            />
            <img src="../Resource/img/find.svg" alt="search" />
          </div>
          <div class="three-btn">
            <img v-on:click="refresh" src="../Resource/img/refresh.svg" alt="" />
            <img src="../Resource/img/excel.svg" alt="" />
            <img src="../Resource/img/setting.svg" alt="" />
          </div>
        </div>
      </div>
      <div class="div-flex">
          <TableEmployee />
      </div>
    </div>
  </div>
</template>

<script>
import TableEmployee from "./TableEmployee.vue";
import EventBus from "./../main.js";
import $ from "jquery";
export default {
  components: {
    TableEmployee,
  },

  methods: {
    // hiển thị form thêm mới nhân viên
    addEmployee() {
      EventBus.$emit("addEmployee");
    },

    // sự kiện ấn refresh
    async refresh() {
      await EventBus.$emit("loadDataServer");
      $("#search-name-id").val("");
    },

    arrowUp() {
      EventBus.$emit("arrowUp");
    },

    arrowDown() {
      EventBus.$emit("arrowDown");
    },
  },

  mounted() {
    // lắng nghe sự kiện người dùng ấn enter trên input tìm kiếm
    $("#search-name-id").on("keyup", function (e) {
      if (e.key === "Enter" || e.keyCode === 13) {
        let s = this.value;
        EventBus.$emit("searchByNameId", s);
      }
    });
  },
};
</script>

<style src="../css/main.css"></style>
