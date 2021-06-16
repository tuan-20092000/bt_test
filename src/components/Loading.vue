<template>
    <div class="ms-wrap-message">
        <div v-if="loading" class="wrap-message-form">
            <img src="../Resource/loading.svg" alt="loading">
        </div>
        <div v-if="warning" class="wrap-message-form">
            <div class="message-box">
                <div class="div-content">
                    <div class="icon-message">
                        <img src="../Resource/img/warning.svg" alt="">
                    </div>
                    <div class="message-content">
                        <div>Bạn có chắc muốn xóa nhân viên "{{employee.employeeName}}"
                            , mã nhân viên là "{{employee.employeeCode}}" không?</div>
                    </div>
                    
                </div>
                <div class="space"></div>
                <div class="mess-footer">
                    <button v-on:click="cancelMessageBox()" 
                        class="btn-no"
                        ref="btnNo"
                    >Hủy</button>
                    <button v-on:click="deleteEmployee()" class="btn-ok">Có</button>
                </div>
            </div>
        </div>
        <div v-if="error" class="wrap-message-form">
            <div class="message-box">
                <div class="div-content">
                    <div class="icon-message">
                        <img src="../Resource/img/warning.svg" alt="">
                    </div>
                    <div class="message-content">
                        <div> {{messageContent}}</div>
                    </div>
                    
                </div>
                <div class="space"></div>
                <div class="mess-footer">
                    <button v-on:click="cancelErrorBox()" 
                        class="btn-ok"
                        ref="btnOk" 
                    >Đồng ý</button>
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
            messageContent: null, // nội dung thông báo cho người dùng
            error: false, // ẩn hiện box báo lỗi
            field: null, // trường cần focus sau khi tắt message-box

            // nhân viên hiển thị trên form warning
            employee : {
              employeeCode : null,
              employeeName : null,
              gender : 0,
              dateOfBirth : null,
              departmentName: null,
              identityDate : null,
              identityPlace : null,
              employeePosition : null,
              address : null,
              bankAccountNumber : null,
              bankName : null,
              bankBranchName : null,
              bankProvinceName : null,
              phoneNumber : null,
              telephoneNumber : null,
              email : null,
            },
        }
    },

    methods:{
        // hàm đóng message box
        cancelMessageBox(){
            this.warning = false;
        },

        cancelErrorBox(){
            this.error = false;
            if(this.field !=null){
                EventBus.$emit("focusAndSelectAll", this.field);
                this.field = null;
            }
        },

        // hàm hiện hộp cảnh báo
        showWarning(employee){
            this.employee = {...employee};
            this.warning = true;
            this.$nextTick(function () {
                if(this.$refs != undefined)
                    this.$refs.btnNo.focus();
            })
        },

        // hàm xóa nhân viên
        deleteEmployee(){
            EventBus.$emit("deleteEmployee", this.employee);
            this.warning = false;
        },
        
        // hàm hiển thị message box thông báo lỗi
        showError(message, field){
            this.messageContent = message;
            this.error = true;
            this.field = field;
            this.$nextTick(function () {
                if(this.$refs.btnOk != undefined)
                    this.$refs.btnOk.focus();
            })
            
        }
    },

    mounted() {
        // sự kiện ẩn hiện loading
        EventBus.$on("onLoading", () => {
            this.loading = true;
        })

        // sự kiện tắt loading
        EventBus.$on("stopLoading", () => {
            this.loading = false;
        })

        // lắng nghe sự kiện hiện form cảnh báo
        EventBus.$on("showWarning", (employee) => {
            this.showWarning(employee);
        })

        // lắng nghe sự kiện hiện thông báo lỗi
        EventBus.$on("showError", (content ,field)=> {
           this.showError(content, field);
        })
    }
}
</script>

<style src="../css/loading.css"></style>
<style src="../css/googlefont.css"></style>