<template>
  <div class="wrap-form" v-if="showFormDetail" @keyup.shift.tab.exact="lastTab()" @keyup.esc="cancelFormDetail">
    <div class="form-detail" >
      <div class="form-header">
        <div class="form-title">Thông tin nhân viên</div>
        <div class="checkbox">
          <input type="checkbox" name="customer" value="customer" />
          <label for="customer">Là khách hàng</label>
        </div>
        <div class="checkbox">
          <input type="checkbox" name="supplier" value="supplier" />
          <label for="supplier">Là nhà cung cấp</label>
        </div>
      </div>
      <div class="form-content">
        <div class="form-content-scroll">
          <div class="form-content-top">
            <div class="form-content-left">
              <div class="row-input">
                <div class="employeeCode flex-col">
                  <button class="btn-focus" id="id1"></button>
                  <label for="EmployeeCode">Mã<span style="color: red"> *</span></label>
                  <input v-model="employee.employeeCode" 
                    @blur="handleBlur($event)"
                    ref= "employeeCode"
                    
                    type="text" name="employeeCode" required/>
                </div>
                <div class="fullName flex-col">
                  <label for="FullName">Tên<span style="color: red"> *</span></label>
                  <input 
                    v-model="employee.fullName" 
                    @blur="handleBlur($event)" 
                    ref="fullName"
                    type="text" name="fullName" required/>
                </div>
              </div>
              <div class="row-input flex-col">
                <label for="companyName">Đơn vị<span style="color: red"> *</span></label>
                <!-- <input 
                  v-model="employee.companyName" 
                  @blur="handleBlur($event)" 
                  ref="companyName"
                  type="text" name="companyName" required/> -->
                <Combobox :data="[
                  {text:'Công ty cổ phần MISA', value:0},
                  {text:'Công ty cổ phần ARMY', value:1},
                  {text:'Công ty cổ phần SAMI', value:2},
                ]" :width="392" :height="32" />
              </div>
              <div class="row-input flex-col">
                <label for="Position">Chức danh</label>
                <input v-model="employee.position" type="text" name="Position" />
              </div>
            </div>
            <div class="form-content-right">
              <div class="row-input">
                <div class="DateOfBirth flex-col">
                  <label for="DateOfBirth">Ngày sinh</label>
                  <input v-model="employee.dateOfBirth" type="date" name="DateOfBirth" />
                </div>
                <div class="Gender flex-col">
                  <div class="label-gender">Giới tính</div>
                  <div class="radio-gender">
                    <input v-model="employee.gender" type="radio" name="Gender" id ="male" value= 0 />
                    <label for="male">Nam</label>
                    <input v-model="employee.gender" type="radio" name="Gender" id="female" value= 1 />
                    <label for="female">Nữ</label>
                    <input v-model="employee.gender" type="radio" name="Gender" id="other" value= 2 />
                    <label for="other">Khác</label>
                  </div>
                </div>
              </div>
              <div class="row-input">
                <div class="EntityCard flex-col">
                  <label for="EntityCard">Số CMND</label>
                  <input v-model="employee.identityCard" type="text" name="EntityCard" />
                </div>
                <div class="DateSupply flex-col">
                  <label for="DateSupply">Ngày cung cấp</label>
                  <input type="date" name="DateSupply" />
                </div>
              </div>
              <div class="row-input flex-col">
                <label for="AddressSuplly">Nơi cấp</label>
                <input type="text" name="AddressSuplly" />
              </div>
            </div>
          </div>
          <div class="form-content-bottom">
            <div class="row-input flex-col">
              <label for="Address">Địa chỉ</label>
              <input type="text" name="Address" />
            </div>
            <div class="row-input">
              <div class="PhoneNumber div-normal">
                <label for="PhoneNumber">ĐT di động</label>
                <input v-model="employee.phoneNumber" type="text" name="PhoneNumber" />
              </div>
              <div class="TelephoneNumber div-normal">
                <label for="TelephoneNumber">ĐT cố định</label>
                <input v-model="employee.telephoneNumber" type="text" name="TelephoneNumber" />
              </div>
              <div class="Email div-normal">
                <label for="Email">Email</label>
                <input type="email" name="Email" />
              </div>
            </div>
            <div class="row-input">
              <div class="BankAccountNumber div-normal">
                <label for="BankAccountNumber">Tài khoản ngân hàng</label>
                <input v-model="employee.phoneNumber" type="text" name="BankAccountNumber" />
              </div>
              <div class="BankName div-normal">
                <label for="BankName">Tên ngân hàng</label>
                <input v-model="employee.phoneNumber" type="text" name="BankName" />
              </div>
              <div class="BankBranchName div-normal">
                <label for="BankBranchName">Tên chi nhánh</label>
                <input v-model="employee.phoneNumber" type="text" name="BankBranchName" />
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="form-footer">
          <div class="div-line"></div>
          <button v-on:click="cancelFormDetail" class="btn-cancel btn-primary">Hủy</button>
          <div class="float-right">
            <button v-on:click="save" class="btn-save btn-primary">Cất</button>
            <button v-on:click="saveAndAdd" class="btn-saveAndadd btn-secondary" ref="saveAndAdd">Cất và thêm</button>
            <button class="btn-focus" id="id" @keyup.tab="tabFirst"></button>
          </div>
      </div>
    </div>
  </div>
</template>

<script>
const axios = require("axios");
import Combobox from './Combobox.vue';
import EventBus from './../main.js';
import $ from "jquery";
import swal from 'sweetalert';
export default {
  data () {
    return{
      showFormDetail: false,
      formMode: null,
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

      fieldMissingData: null,
    }
  },

  components:{
    Combobox,
  },

  methods: {
    // hàm mở form
    showForm(){
      // mở form
      this.showFormDetail = true;
      // auto focus vào input đầu và bôi đen hết text bên trong nếu có
      this.focusAndSelectAll();
    },

    // hàm focus và bôi đen text trong input nếu có
    focusAndSelectAll(index){
      if(!index || index == "employeeCode"){
        this.$nextTick(function (){
          this.$refs.employeeCode.focus();
          this.$refs.employeeCode.select();
        })
      }else{
        switch(index){
          case "fullName":
            this.$nextTick(function (){
              this.$refs.fullName.focus();
              this.$refs.fullName.select();
            });
            break;
          case "companyName":
            this.$nextTick(function (){
              this.$refs.companyName.focus();
              this.$refs.companyName.select();
            });
        }
      }
    },

    // hàm tắt form detail
    cancelFormDetail(){
      this.showFormDetail = false;
    },

    //hàm reset form
    resetForm(){
      let nullEmployee = {
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
      };
      this.employee = nullEmployee;
    },

    // hàm lưu và tắt form
    save(){
      let me = this;
      if(me.checkInputRequired()){
        me.handleRequest();
        me.cancelFormDetail();
      }else{
        // hiên sweet thông báo
        me.showSwal();
      }
    },

    // hàm lưu và thêm mới
    async saveAndAdd(){
      let me = this;
      if(me.checkInputRequired()){
        me.handleRequest();
        me.resetForm();
      }else{
        // hiên sweet thông báo
        me.showSwal();
      }
    },

    // xử lý request tới server (thêm mới hay sửa chữa?)
    handleRequest(){
      let me = this;
      // trường hợp thêm mới
        if(me.formMode == "add"){
          axios.post("http://localhost:8080/api/v1/employees", me.employee).then(res =>{
            console.log(res);
            // reset form và load lại dữ liệu
            me.resetForm();
            EventBus.$emit("loadDataServer");
          }).catch(err =>{
            console.log(err);
          })
        }

        // trường hợp chỉnh sửa
        else if(me.formMode == "edit"){
          let url = "http://localhost:8080/api/v1/employees/" + me.employee.employeeId;
          axios.put(url, me.employee).then(() =>{
            // reset form và load lại dữ liệu
            me.resetForm();
            EventBus.$emit("loadDataServer");
          }).catch((err, res) =>{
            console.log(err , res);
          })
        }
    },

    // hàm xóa 1 nhân viên
    async deleteEmployee(employee){
      let url = "http://localhost:8080/api/v1/employees/" + employee.employeeId;
      await axios.delete(url).then(res =>{
        console.log(res);
        EventBus.$emit("loadDataServer");
      }).catch((err) =>{
        console.log("err",err);
      })
    },

    // xử lý sự kiên blur trên các input required
    handleBlur(e){
      let value = e.target.value;
      if(!value){
        $(e.path[0]).addClass('bgc-red');
        $(e.path[0]).attr('title', 'Trường này không được để trống!');
      }else{
        $(e.path[0]).removeClass('bgc-red');
      }
    },

    // hàm kiểm tra các input required có dữ liệu hay chưa
    // trả về true nếu đã đủ, false nếu có ít nhất 1 input chưa có
    checkInputRequired(){
      console.log("check"); 
      let check = true;
      let me = this;
      $('input[required]').each(function(){
        if(check){
          if(!$(this).val()){
            me.fieldMissingData = $(this).attr('name');
            me.focusAndSelectAll(me.fieldMissingData);
            check = false;
          }
        }
      })
      return check;
    },

    convertToVNese(fieldName){
      switch(fieldName){
        case "employeeCode":
          return "Mã nhân viên";
        case "fullName":
          return "Họ tên";
        case "companyName":
          return "Đơn vị";
        default:
          return "";
      }
    },

    // sweet alert hiển thị thông báo lỗi
    showSwal(){
      swal({
          title: "\""+ this.convertToVNese(this.fieldMissingData)+ "\"" + " không được để trống",
          icon: "error",
          timer: 3000,
        });
    },

    tabFirst(){
      if($(':focus') != undefined){
        if($(':focus').attr('id') == "id"){
          this.$refs.employeeCode.focus();
        }
      }
      
    },

    lastTab(){
      if($(':focus') != undefined){
        if($(':focus').attr('id') == "id1"){
          this.$refs.saveAndAdd.focus();
        }
      }
    }
  },

  mounted(){
    // bắt sự kiện nhấn phím sửa thông tin nhân viên
    EventBus.$on("editEmployee", (employee) => {
      if(employee != null){
        let me = this;
        me.employee = {...employee};
        me.formMode = "edit";
        me.showForm();
      }
    });

    // bắt sự kiên thêm mới nhân viên
    EventBus.$on("addEmployee",  () =>{
      let me = this;
      me.resetForm();
      me.formMode = "add";
      me.showForm();
    });

    // bắt sự kiện xóa nhân viên
    EventBus.$on("deleteEmployee",  (employee) =>{
      this.deleteEmployee(employee);
    });

  }
};
</script>

<style src="../css/employeedetail.css"></style>
<style src="../css/googlefont.css"></style>