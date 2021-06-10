<template>
    <div>
        <div v-if="loading" class="wrap-loading">
            <div class="loader"></div>
        </div>
        <div v-if="warning" class="wrap-loading">
            <div class="warning">
                <div class="warning-icon">
                    <img src="../assets/warning.svg" alt="warning">
                </div>
                <div class="warning-content">
                    <div>Bạn có chắc muốn xóa nhân viên "{{employee.fullName}}"</div>
                    <div>mã nhân viên là "{{employee.employeeCode}}" không?</div>
                </div>
                <div class="space"></div>
                <div class="warning-control">
                    <button v-on:click="cancelWarning" class="btn-cancel">Hủy</button>
                    <button v-on:click="deleteEmployee" class="btn-ok">Đồng ý</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import EventBus from './../main.js';
export default {
    data() {
        return {
            loading: false, // ẩn hiện loading
            warning: false,  // ẩn hiện form cảnh báo

            // nhân viên hiển thị trên form warning
            employee : {
                employeeCode : null,
                fullName : null,
                gender : 0,
                dateOfBirth : null,
                identityCard : null,
                position : null,
                companyName : null,
                accountNumber : null,
                bankName : null,
                branch : null,
            },
        }
    },

    methods: {
        cancelWarning(){
            this.warning = false;
        },

        showWarning(employee){
            this.employee = {...employee};
            this.warning = true;
        },

        deleteEmployee(){
            EventBus.$emit("deleteEmployee", this.employee);
            this.warning = false;
        }
    },

    mounted() {
        // sự kiện ẩn hiện loading
        EventBus.$on("onLoading", () => {
            this.loading = true;
        })

        EventBus.$on("stopLoading", () => {
            this.loading = false;
        })

        EventBus.$on("showWarning", (employee) => {
            this.showWarning(employee);
        })
    }
}
</script>

<style src="../css/loading.css"></style>