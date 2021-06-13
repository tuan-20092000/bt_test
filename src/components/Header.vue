<template>
  <div class="amis-header">
    <div class="three-stripes"></div>
    <div class="tooltip">
      <div class="tooltip-content">CÔNG TY CỔ PHẦN MISA</div>
      <img
        v-on:click="showHideFormMode"
        class="img-expand_more"
        src="../assets/expand_more.svg"
        alt="img-expand_more"
      />
    </div>

    <div class="space"></div>

    <div class="div-search">
      <input type="text" placeholder="Nhập từ khóa tìm kiếm" />
      <img src="../assets/search.svg" alt="search" />
    </div>

    <div class="header-menu-icon">
      <img src="../assets/more.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../assets/setting.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../assets/message.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../assets/help_outline.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../assets/notification.svg" alt="more_svg" />
    </div>

    <div class="div-username">
      Nguyễn Văn Tuấn
      <img
        class="img-expand_more"
        src="../assets/expand_more.svg"
        alt="img-expand_more"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "Header",
  data() {
    return {};
  },

  methods: {
    showHideFormMode() {
      this.$emit("showHideFormMode");
    },
  },
};
</script>

<style src="../css/googlefont.css"></style>
<style scope src="../css/header.css"></style>

