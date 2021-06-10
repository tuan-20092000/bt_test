<template>
  <div class="working_mode">
    <div class="content_mode">
      <div class="title">Chế độ làm việc</div>
      <div class="input-radio">
        <input
          type="radio"
          name="working_mode"
          id="accountant"
          value="Kế toán"
          checked
        />
        <label for="accountant">Kế toán</label>
        <input type="radio" name="working_mode" id="stocker" value="Thủ kho" />
        <label for="stocker">Thủ kho</label>
        <input
          type="radio"
          name="working_mode"
          id="treasurer"
          value="Thủ quỹ"
        />
        <label for="treasurer">Thủ quỹ</label>
      </div>
      <div class="line"></div>
      <div class="div-btn">
        <button class="btn-cancel" v-on:click="showHideFormMode">Hủy</button>
        <button class="btn-ok">Đồng ý</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "WorkingMode",
  methods: {
    showHideFormMode() {
      this.$emit("showHideFormMode");
    },
  },
};
</script>

<style src="../css/working_mode.css"></style>
