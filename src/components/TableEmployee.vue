<template>
  <div class="wrap-table">
    <div class="ms-table">
      <table>
        <caption></caption>
        <thead>
          <tr>
            <th id="" class="out-left-white bgc-white"></th>
            <th style="left:16px;" class="check-all " id="check-all">
              <input type="checkbox" name="" id="" />
            </th>
            <th
              style="min-width: 150px; width: 150px;"
              id=""
            >
              MÃ NHÂN VIÊN
            </th>
            <th
              style="min-width: 250px; width: 250px;"
              id=""
            >
              TÊN NHÂN VIÊN
            </th>
            <th
              style="min-width: 120px; width: 120px;"
              id=""
            >
              GIỚI TÍNH
            </th>
            <th
              style="min-width: 150px; width: 150px;"
              id=""
            >
              NGÀY SINH
            </th>
            <th
              style="min-width: 200px; width: 200px;"
              id=""
            >
              SỐ CMND
            </th>
            <th
              style="min-width: 250px; width: 250px;"
              id=""
            >
              CHỨC DANH
            </th>
            <th
              style="min-width: 250px; width: 250px;"
              id=""
            >
              TÊN ĐƠN VỊ
            </th>
            <th
              style="min-width: 150px; width: 150px;"
              id=""
            >
              SỐ TÀI KHOẢN
            </th>
            <th
              style="min-width:250px; width:250px;"
              id=""
            >
              TÊN NGÂN HÀNG
            </th>
            <th
              style="min-width:250px; width:250px;border-right:none;"
              id=""
            >
              CHI NHÁNH TK NGÂN HÀNG
            </th>
            <th
              style="width:120px; min-width:120px; right: 30px"
              class="wiget border-left "
              id=""
            >
              <div>CHỨC NĂNG</div>
            </th>
            <th style="right:0px;" id="" class="out-right bgc-white"></th>
            <th style="right:-30px;" id="" class="out-right bgc-gray"></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(employee, index) in employeeList"
            :key="index"
            v-on:click="active(index)"
            v-on:dblclick="edit(index)"
          >
            <td id="" class="out-left-white bgc-white"></td>
            <td v-bind:class="{ selected: index == selectedRow }" class="col-check">
              <input type="checkbox" name="" id="" />
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.employeeCode }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.employeeName }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ convertGender(employee.gender) }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ convertDate(employee.dateOfBirth) }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.identityNumber }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.employeePosition }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.departmentName }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.bankAccountNumber }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.bankName }}
            </td>
            <td style="border-right:none;" v-bind:class="{ selected: index == selectedRow }">
              {{ employee.bankBranchName }}
            </td>
            <td
              style="right:30px"
              v-bind:class="{ selected: index == selectedRow }"
              class="sticky border-left"
            >
              <div class="col-edit">
                <span v-on:click="edit(index)">Sửa</span>
                <span v-on:click="showWarning(index)">Xóa</span>
              </div>
            </td>
            <td style="right:0px;" class="out-right bgc-white"></td>
            <td style="right:-30px;" class="out-right bgc-gray"></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div style="bottom:0px;" class="wrap-footer">
      <div class="footer-content">
        <div class="count-record">
          Tổng số: {{ countEmployee.length }} bản ghi
        </div>
        <div class="right-footer">
          <div>
            <select
            v-model="countEmployeePerPage"
            @change="changeCountPerPage"
            name=""
            id=""
          >
            <option value="10" selected>10 bản ghi trên một trang</option>
            <option value="20">20 bản ghi trên một trang</option>
            <option value="30">30 bản ghi trên một trang</option>
            <option value="50">50 bản ghi trên một trang</option>
            <option value="100">100 bản ghi trên một trang</option>
          </select>
          <div v-on:click="prevPage" class="front controlpage">Trước</div>
          <div
            v-for="page in countPage"
            :key="page"
            v-on:click="getDataPage(page)"
            :class="{ selectedPage: selectedPage == page }"
            class="page-number"
          >
            {{ page }}
          </div>
          <div v-on:click="nextPage" class="behind controlpage">Sau</div>
          </div>
          
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const axios = require("axios");
import EventBus from "../main.js";
import swal from "sweetalert";
export default {
  data() {
    return {
      employeeList: {}, // những nhân viên hiển thị lên màn hình
      countEmployee: {}, // tất cả nhân viên trong csdl
      selectedRow: 0, // hàng đang chọn
      countEmployeePerPage: 10, // số lượng bản ghi / trang
      countPage: 1, // số trang
      selectedPage: 1, // trang đang chọn
      employeeNameWarning: null, // tên khách hàng định xóa
      employeeCodeWarning: null, // mã khách hàng định xóa
    };
  },

  methods: {
    // chuyển đổi kiểu giới tính để hiển thị
    convertGender(gd) {
      switch (gd) {
        case 0:
          return "Nam";
        case 1:
          return "Nữ";
        case 2:
          return "Khác";
      }
    },

    // sự kiện khi ấn vào các hàng
    active(index) {
      this.selectedRow = index;
    },

    // sự kiện khi ấn sửa
    edit(index) {
      let employee = this.employeeList[index];
      employee.dateOfBirth = this.convertDateToSendForm(employee.dateOfBirth);
      EventBus.$emit("editEmployee", employee);
    },

    // hàm lấy dữ liệu từ server cho trang thứ page
    async getDataPage(page) {
      this.selectedRow = 0;
      this.selectedPage = page;
      let startIndex = (page - 1) * this.countEmployeePerPage;
      this.employeeList = this.countEmployee.slice(
        startIndex,
        startIndex + this.countEmployeePerPage
      );
    },

    // thay đổi số lượng nhân viên hiển thị trên 1 trang
    changeCountPerPage() {
      this.employeeList = this.countEmployee.slice(
        0,
        this.countEmployeePerPage
      );
      this.countPage =
        Math.floor(this.countEmployee.length / this.countEmployeePerPage) + 1;
      this.selectedPage = 1;
    },

    // hàm previous page
    prevPage() {
      if (this.selectedPage > 1) {
        this.selectedPage = this.selectedPage - 1;
        this.getDataPage(this.selectedPage);
      }
    },

    // hàm next page
    nextPage() {
      if (this.selectedPage < this.countPage) {
        this.selectedPage = this.selectedPage + 1;
        this.getDataPage(this.selectedPage);
      }
    },

    // chuyển đổi ngày tháng năm từ server thành dd/mm/yyyy để hiển thị
    convertDate(dateSrc) {
      let date = new Date(dateSrc),
        year = date.getFullYear().toString(),
        month = (date.getMonth() + 1).toString().padStart(2, "0"),
        day = date.getDate().toString().padStart(2, "0");
      return `${day}/${month}/${year}`;
    },

    // chuyển đổi kiểu dữ liệu ngày tháng thành yyyy-mm-dd để gửi sang formdetail
    convertDateToSendForm(dateSrc) {
      let date = new Date(dateSrc),
        year = date.getFullYear().toString(),
        month = (date.getMonth() + 1).toString().padStart(2, "0"),
        day = date.getDate().toString().padStart(2, "0");
      return `${year}-${month}-${day}`;
    },

    // hàm lấy dữ liệu từ server
    async getDataServe() {
      EventBus.$emit("onLoading");
      await axios
        .get("http://localhost:8080/api/v1/Employees")
        .then((res) => {
          this.countEmployee = res.data;
          this.countPage =
            Math.floor(this.countEmployee.length / this.countEmployeePerPage) +
            1;
          this.employeeList = this.countEmployee.slice(
            0,
            this.countEmployeePerPage
          );
          EventBus.$emit("stopLoading");
          swal({
            title: "Success",
            icon: "success",
            timer: 1500,
          });
        })
        .catch((err) => {
          console.log(err);
        });
    },

    // show form cảnh báo trước khi xóa
    showWarning(index) {
      let employee = this.employeeList[index];
      EventBus.$emit("showWarning", employee);
      console.log(employee);
    },

    arrowUp() {
      console.log("up");
      this.selectedRow = this.selectedRow + 1;
    },
  },

  mounted() {
    //lấy dữ liệu từ server sau khi khởi tạo xong component
    this.getDataServe();

    //hàm lắng nghe sự kiện khi các component khác gọi loadData
    EventBus.$on("loadDataServer", () => {
      this.getDataServe();
    });

    //hàm lắng nghe sự kiện khi người dùng ấn enter ở ô tìm kiếm
    EventBus.$on("searchByNameId", async (s) => {
      EventBus.$emit("onLoading");
      let url = "http://localhost:8080/api/v1/employees/search?s=" + s;
      await axios.get(url).then((res) => {
        this.countEmployee = res.data;
        this.countPage =
          Math.floor(this.countEmployee.length / this.countEmployeePerPage) + 1;
        this.employeeList = this.countEmployee.slice(
          0,
          this.countEmployeePerPage
        );
        this.selectedPage = 1;
      });

      EventBus.$emit("stopLoading");
    }),

    EventBus.$on("arrowUp", () => {
      if (this.selectedRow > 0) this.selectedRow--;
    }),

    EventBus.$on("arrowDown", () => {
      this.selectedRow++;
    });
  },
};
</script>

<style src="../css/googlefont.css"></style>
<style src="../css/table.css"></style>
