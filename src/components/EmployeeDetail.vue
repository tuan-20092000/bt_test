<template>
  <div class="wrap-form" v-if="showFormDetail" 
    @keyup.shift.tab.exact="shiftTab()" 
    @keyup.esc="cancelFormDetail" 
    @click="clickForm($event)">
    <div class="form-detail" id="form-detail">
      <div class="div-tool">
        <img src="../Resource/img/support.svg" alt="" title="Giúp (F1)">
        <div class="inline" v-on:click="cancelFormDetail()">
          <img src="../Resource/img/x.svg" alt="" title="Đóng (ESC)">
        </div>
      </div>
      <div class="form-header">
        <div class="form-title">Thông tin nhân viên</div>
      </div>
      <div class="form-content">
        <div class="form-content-scroll">
          <div class="form-content-top">
            <div class="form-content-left">
              <div class="row-input">
                <div class="employeeCode flex-col">
                  <button class="btn-focus" id="btn-first"></button>
                  <label for="EmployeeCode">Mã<span style="color: red"> *</span></label>
                  <input v-model="employee.employeeCode" 
                    @blur="handleBlur($event)"
                    ref= "employeeCode"
                    type="text" name="employeeCode" required/>
                </div>
                <div class="employeeName flex-col">
                  <label for="employeeName">Tên<span style="color: red"> *</span></label>
                  <input 
                    v-model="employee.employeeName" 
                    @blur="handleBlur($event)" 
                    ref="employeeName"
                    type="text" name="employeeName" required/>
                </div>
              </div>
              <div class="row-input flex-col">
                <label for="departmentName">Đơn vị<span style="color: red"> *</span></label>
                <Combobox :departmentName="employee.departmentName"
                  @selectDepartment="selectDepartment"
                  :data="[
                    {text:'Phòng nhân sự', value:0},
                    {text:'Phòng kinh doanh', value:1},
                    {text:'Phòng marketing', value:2},
                  ]" :width="392" :height="32"
                />
              </div>
              <div class="row-input flex-col">
                <label for="Position">Chức danh</label>
                <input v-model="employee.employeePosition"
                  ref="employeePosition" 
                  type="text" name="Position" />
              </div>
            </div>
            <div class="form-content-right">
              <div class="row-input">
                <div class="DateOfBirth flex-col" id="picker_dateofbirth">
                  <label for="DateOfBirth">Ngày sinh</label>
                  <div class="input-date">
                    <input v-model="employee.dateOfBirth"
                      type="text" name="DateOfBirth"
                      ref="dateOfBirth" 
                      autocomplete="off"/>
                    <div @click="()=>{showDateOfBirth=!showDateOfBirth}"
                      class="div-icon-datepicker">
                      <img src="../Resource/img/date_picker.svg" alt="">
                    </div>
                  </div>
                  <div v-if="showDateOfBirth" class="date__picker">
                    <v-date-picker
                        v-model="dateOfBirth_PK"
                        no-title
                        locale="vi-VN"
                        @click:date="chooseDateOfBirth()"
                    >
                    <div class="date_picker_footer">
                        <a href="#" @click="()=>{employee.dateOfBirth = getCurrentDay(); showDateOfBirth=false}"> Hôm nay</a>
                    </div>
                    </v-date-picker>
                  </div>
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
                  <input v-model="employee.identityNumber"
                    ref="identityNumber" 
                    type="text" name="EntityCard" />
                </div>
                <div class="DateSupply flex-col" id="picker_identitydate">
                  <label for="identitydate">Ngày cấp</label>
                  <div class="input-date">
                    <input v-model="employee.identityDate" 
                      type="text" name="identitydate"
                      ref="identityDate" 
                      autocomplete="off"/>
                    <div @click="()=>{showIdentityDate=!showIdentityDate}"
                      class="div-icon-datepicker">
                      <img src="../Resource/img/date_picker.svg" alt="">
                    </div>
                  </div>
                  <div v-if="showIdentityDate" class="date__picker" style="right:0px">
                    <v-date-picker
                        v-model="identityDate_PK"
                        no-title
                        locale="vi-VN"
                        @click:date="chooseIdentityDate()"
                    >
                    <div class="date_picker_footer">
                        <a href="#" @click="()=>{employee.identityDate = getCurrentDay(); showIdentityDate=false}"> Hôm nay</a>
                    </div>
                    </v-date-picker>
                  </div>
                </div>
              </div>
              <div class="row-input flex-col">
                <label for="AddressSuplly">Nơi cấp</label>
                <input v-model="employee.identityPlace"
                  ref="identityPlace"  
                  type="text" name="AddressSuplly" />
              </div>
            </div>
          </div>
          <div class="form-content-bottom">
            <div class="row-input flex-col">
              <label for="Address">Địa chỉ</label>
              <input v-model="employee.address" 
                ref="addresss"
                type="text" name="Address" />
            </div>
            <div class="row-input">
              <div class="PhoneNumber div-normal">
                <label for="PhoneNumber">ĐT di động</label>
                <input v-model="employee.phoneNumber" 
                  ref="phoneNumber"
                  type="text" name="PhoneNumber" />
              </div>
              <div class="TelephoneNumber div-normal">
                <label for="TelephoneNumber">ĐT cố định</label>
                <input v-model="employee.telephoneNumber" 
                  ref="telephoneNumber"
                  type="text" name="TelephoneNumber" />
              </div>
              <div class="Email div-normal">
                <label for="Email">Email</label>
                <input v-model="employee.email" 
                  ref="email"
                  type="email" name="Email" />
              </div>
            </div>
            <div class="row-input">
              <div class="BankAccountNumber div-normal">
                <label for="BankAccountNumber">Tài khoản ngân hàng</label>
                <input v-model="employee.bankAccountNumber" 
                  ref="bankAccountNumber"
                  type="text" name="BankAccountNumber" />
              </div>
              <div class="BankName div-normal">
                <label for="BankName">Tên ngân hàng</label>
                <input v-model="employee.bankName" 
                ref="bankName"
                type="text" name="BankName" />
              </div>
              <div class="BankBranchName div-normal">
                <label for="BankBranchName">Tên chi nhánh</label>
                <input v-model="employee.bankBranchName" 
                  ref="bankBranchName"
                  type="text" name="BankBranchName" />
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
            <button class="btn-focus" id="btn-last" @keyup.tab="tab()"></button>
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
export default {
  data () {
    return{
      // ẩn hiện form thông tin
      showFormDetail: false,

      // mode của form (thêm mới hay update ...)
      formMode: null,

      // thông tin nhân viên binding lên form
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
        email : null
      },

      fieldMissingData: null, // trường bắt buộc nhập còn trống đầu tiên
      messageContent: null, // nội dung tin nhắn cần show cho người dùng nếu cần

      showDateOfBirth: false,  // ẩn hiện date-picker của ngày sinh 

      dateOfBirth_PK: null, // ngày sinh để v-model với date picker

      showIdentityDate:false, // ẩn hiên date-picker của ngày cấp

      identityDate_PK: null, // ngày sinh để v-model với date picker
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
          if(this.$refs.employeeCode != undefined){
            this.$refs.employeeCode.focus();
            this.$refs.employeeCode.select();
          }
        })
      }else{
        switch(index){
          case "employeeName":
            this.$nextTick(function (){
              this.$refs.employeeName.focus();
              this.$refs.employeeName.select();
            });
            return;
          case "dateOfBirth":
            this.$nextTick(function (){
              this.$refs.dateOfBirth.focus();
              this.$refs.dateOfBirth.select();
            });
            return;
          case "identityDate":
            this.$nextTick(function (){
              this.$refs.identityDate.focus();
              this.$refs.identityDate.select();
            });
            return;
          case "departmentName":
            this.$nextTick(function (){
              this.$refs.departmentName.focus();
              this.$refs.departmentName.select();
            });
          
            return;
        }
      }
    },

    // hàm tắt form detail
    cancelFormDetail(){
      this.showFormDetail = false;
      this.resetForm();
    },

    //hàm reset form
    resetForm(){
      // null employee
      let nullEmployee = {
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
      };
      this.employee = nullEmployee;
    },

    handlingDate(employee){
      if(employee.dateOfBirth != undefined){
        employee.dateOfBirth = this.convertToDateServer(employee.dateOfBirth);
      }

      if(this.employee.identityDate != undefined){
        employee.identityDate = this.convertToDateServer(employee.identityDate);
      }
      return employee;
    },
    


    // hàm lưu và tắt form
    save(){
      let me = this;
      if(me.checkInputRequired() && me.validateObject()){
        me.handleRequest("save");
      }else{
         // hiên message box thông báo lỗi
        let field = me.fieldMissingData;
        let message = me.messageContent;
        EventBus.$emit("showError", message, field);
      }
    },

    // hàm lưu và thêm mới
    saveAndAdd(){
      let me = this;
      if(me.checkInputRequired() && me.validateObject()){
        me.handleRequest("saveAndAdd");
      }else{
        // hiên message box thông báo lỗi
        let field = me.fieldMissingData;
        let message = me.messageContent;
        EventBus.$emit("showError", message, field);
      }
    },

    // xử lý request tới server (thêm mới hay sửa chữa?)
    handleRequest(e){
      let me = this;
      // xử lý (ngày tháng) trước khi gửi
      me.employee = me.handlingDate(me.employee);
      // trường hợp thêm mới
      EventBus.$emit("onLoading");
        if(me.formMode == "add"){
          axios.post("http://localhost:8080/api/v1/Employees", me.employee).then(response =>{
            console.log(response);
            // reset form và load lại dữ liệu
            EventBus.$emit("loadDataServer");
            // nếu là ấn save thì ẩn form
            if(e == "save")
              me.cancelFormDetail();
            else
              me.resetForm();
          }).catch(error => {
            console.log(error);
            EventBus.$emit("stopLoading");
            let message = error.response.data.messengers[0],
                field = error.response.data.eFieldError;
            EventBus.$emit("showError", message, field);
          })
        }

        // trường hợp chỉnh sửa
        else if(me.formMode == "edit"){
          // let url = "https//localhost:8080/api/v1/Employees";
          console.log(me.employee);
          axios.put("http://localhost:8080/api/v1/Employees", me.employee)
            .then(response =>{
            // reset form và load lại dữ liệu
            console.log(response);
            // nếu là ấn save thì ẩn form
            if(e == "save")
              me.cancelFormDetail();
            else
              me.resetForm();
            EventBus.$emit("loadDataServer");
          }).catch(error => {
            console.log(error);
            EventBus.$emit("stopLoading");
          })
        }
    },

    // hàm xóa 1 nhân viên
    async deleteEmployee(employee){
      let url = "http://localhost:8080/api/v1/Employees/" + employee.employeeId;
      await axios.delete(url).then(res =>{
        console.log(res);
        EventBus.$emit("loadDataServer");
      }).catch((err) =>{
        console.log(err);
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
      let check = true;
      let me = this;
      $('input[required]').each(function(){
        if(check){
          if(!$(this).val()){
            me.fieldMissingData = $(this).attr('name');
            me.messageContent = me.convertToVNese(me.fieldMissingData)+" không được phép để trống.";
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
        case "employeeName":
          return "Họ tên";
        case "departmentName":
          return "Đơn vị";
        case "dateOfBirth":
          return "Ngày sinh";
        case "identityDate":
          return "Ngày cấp";
        default:
          return "";
      }
    },

    validateObject(){
      let me = this;
      if(me.employee.dateOfBirth!=null && !me.validateDate(me.employee.dateOfBirth)){
        me.fieldMissingData = "dateOfBirth";
        me.messageContent = "Ngày sinh không hợp lệ, vui lòng nhập lại."
        return false;
      }
      if(me.employee.identityDate!=null && !me.validateDate(me.employee.identityDate)){
        me.fieldMissingData = "identityDate";
        console.log(me.employee.identityDate);
        me.messageContent = "Ngày cung cấp không hợp lệ, vui lòng nhập lại.";
        return false;
      }
      return true;
    },  

    validateDate(date){
      var date_regex = /^(0[1-9]|1\d|2\d|3[01]|[0-9])\/(0[1-9]|1[0-2]|[0-9])\/(19|20)\d{2}$/;
      if (!(date_regex.test(date))) {
        return false;
      }
      return true;
    },

    formatDate (e) {
        if (!this.employee.dateOfBirth) return null

        const [year, month, day] = this.employee.dateOfBirth.split('-')
        this.employee.dateOfBirth = `${day}/${month}/${year}`
        // return `${month}/${day}/${year}`
    },

    getCurrentDay(){
      let today = new Date(),
        dd = String(today.getDate()).padStart(2, '0'),
        mm = String(today.getMonth() + 1).padStart(2, '0'),
        yyyy = today.getFullYear();
      return dd + '/' + mm + '/' + yyyy;
    },

    tab(){
      if($(':focus') != undefined){
        if($(':focus').attr('id') == "btn-last"){
          this.$refs.employeeCode.focus();
        }
      }
      
    },

    shiftTab(){
      if($(':focus') != undefined){
        if($(':focus').attr('id') == "btn-first"){
          this.$refs.saveAndAdd.focus();
        }
      }
    },

    // sự kiện khi chọn date ở date-picker ngày sinh
    chooseDateOfBirth(){
      let [year, month, day] = this.dateOfBirth_PK.split('-');
      this.employee.dateOfBirth = `${day}/${month}/${year}`;
      this.showDateOfBirth = false;
    },

    // sự kiện khi chọn date ở date-picker ngày cấp
    chooseIdentityDate(){
      let [year, month, day] = this.identityDate_PK.split('-');
      this.employee.identityDate = `${day}/${month}/${year}`;
      this.showIdentityDate = false;
    },

    // sự kiện click vào form để ẩn các popup
    clickForm(e){
      var picker_dateofbirth = document.getElementById("picker_dateofbirth");
      if(picker_dateofbirth != null){
        if(!(picker_dateofbirth.contains(e.target))){
          this.showDateOfBirth = false;
        }
      }
      
      var picker_identityDate = document.getElementById("picker_identitydate");
      if(picker_identityDate != null){
        if(!(picker_identityDate.contains(e.target))){
          this.showIdentityDate = false;
        }
      }
    },

    // binding đơn vị khi người dùng chọn ở combobox
    selectDepartment(departmentName){
      this.employee.departmentName = departmentName;
    },

    // chuyển từ dd/mm/yyyy sang yyyy-mm-dd để gửi tới server
    convertToDateServer(date){
      var [day, month, year] = date.split('/');
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
    },
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

    EventBus.$on("focusAndSelectAll", field=>{
      this.focusAndSelectAll(field);
    })
  },

  updated: function() {
    this.$nextTick(function(){
        if (!this.employee.dateOfBirth) return null
        var [dayDB, monthDB, yearDB] = this.employee.dateOfBirth.split('/');
        if(dayDB!=undefined && monthDB!=undefined && yearDB!=undefined)
          this.dateOfBirth_PK = `${yearDB}-${monthDB.padStart(2, '0')}-${dayDB.padStart(2, '0')}`;
        // return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`

        if (!this.employee.identityDate) return null
        var [dayID, monthID, yearID] = this.employee.identityDate.split('/');
        if(dayID!=undefined && monthID!=undefined && yearID!=undefined)
          this.identityDate_PK = `${yearID}-${monthID.padStart(2, '0')}-${dayID.padStart(2, '0')}`
    })
  }

};
</script>

<style src="../css/employeedetail.css"></style>
<style src="../css/googlefont.css"></style>