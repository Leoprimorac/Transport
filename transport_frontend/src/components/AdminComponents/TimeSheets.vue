<template>
  <div>
    <div class="row middle">
      <div class="col-8 col-md-5 col-sm-4">
        <form class="TimeSheetFormUser">
          <label for="users">User:</label>
          <select id="users" v-model="id">
            <option v-for="user in users" :key="user.id" :value="user.id">{{
              user.userName
            }}</option>
          </select>
        </form>
      </div>
      <div class="col-4 col-md-7 col-sm-8">
        <form class="TimeSheetForm leftDropdown">
          <label for="year">Year:</label>
          <select id="year" required v-model="year">
            <option :value="year" v-for="year in years" :key="year">{{
              year
            }}</option>
          </select>
        </form>

        <form class="TimeSheetForm leftDropdown">
          <label for="month">Month:</label>
          <select id="month" required v-model="month">
            <option value="1">January</option>
            <option value="2">February</option>
            <option value="3">March</option>
            <option value="4">April</option>
            <option value="5">May</option>
            <option value="6">June</option>
            <option value="7">July</option>
            <option value="8">August</option>
            <option value="9">September</option>
            <option value="10">October</option>
            <option value="11">November</option>
            <option value="12">December</option>
          </select>
        </form>
      </div>
    </div>
    <div class="row TableOverflow" >
      <table class="TimeSheetsTable">
        <tr>
          <th>Day</th>
          <th>Type</th>
          <th>Start Time</th>
          <th>End Time</th>
          <th>Break</th>
          <th>Work Time</th>
          <th>M<sup>3</sup></th>
          <th>Km - Stand</th>
          <th>Privat</th>
          <th>Fuel</th>
          <th>AdBlue</th>
          <th>Notes</th>
        </tr>

        <tr v-for="(timeSheet, index) in timeSheets" :key="timeSheet.id">
          <td>{{ format_date(timeSheet.entryDate) }}</td>
          <td>{{ timeSheet.type != -1 ? timeSheet.type : "" }}</td>
          <td>
            <input
              type="text"
              :name="index"
              v-model="editedSheets[index].startTime"
            />
          </td>
          <td>
            <input
              type="text"
              :name="index"
              v-model="editedSheets[index].endTime"
            />
          </td>
          <td>
            <input
              type="text"
              :name="index"
              v-model="editedSheets[index].breaks"
            />
          </td>
          <td>{{ timeSheet.workTime != -1 ? timeSheet.workTime : "" }}</td>
          <td>{{ timeSheet.m3 != "-1" ? timeSheet.m3 : "" }}</td>
          <td>{{ timeSheet.kmStand != "-1" ? timeSheet.kmStand : "" }}</td>
          <td>{{ timeSheet.privat != "-1" ? timeSheet.privat : "" }}</td>
          <td>{{ timeSheet.fuel != "-1" ? timeSheet.fuel : "" }}</td>
          <td>{{ timeSheet.adblue != -1 ? timeSheet.adblue : "" }}</td>
          <td>{{ timeSheet.notes != -1 ? timeSheet.notes : "" }}</td>
        </tr>
      </table>
    </div>

    <div class="row">
      <div class="col-3"></div>

      <div class="col-6 timeSheetButtons">
        <button @click.prevent="exportTimeSheets()">Export(PDF/XLS)</button>
        <button @click.prevent="createAuditFile()">Save Edit</button>
        <button
          @click.prevent="approveBySuperUser"
          :disabled="disabled"
        >
          Approve & Close
        </button>
      </div>

      <div class="col-3"></div>
    </div>
  </div>
</template>
<script>
import moment from "moment";
export default {
  name: "TimeSheets",
  data() {
    return {
      users: null,
      id: 57,
      month: JSON.stringify(new Date().getMonth() + 1),
      year: 2021,
      timeSheets: null,
      editedSheets: [],
      length: 2,
      disabled:false
    };
  },
  created() {
    this.getAllUsers();
  },
  computed: {
    years() {
      const year = new Date().getFullYear();
      return Array.from(
        { length: year - 1990 },
        (value, index) => year - index
      );
    },
  },
  mounted() {
    this.$watch(
      (ts) => [ts.id, ts.month, ts.year],
      (val) => {
        this.getTimeSheetByIdDate(val);
      },
      {
        immediate: true,
        deep: true,
      }
    );
  },
  /*watch:{
        length: function() {
                this.pushEdit();
            }
    },*/
  methods: {
    filterTimeSheets() {
      this.editedSheets = [];
      this.timeSheets.forEach((element, index) => {
        if (element.startTime == -1) {
          this.timeSheets[index].startTime = null;
        }
        if (element.endTime == -1) {
          this.timeSheets[index].endTime = null;
        }
        if (element.breaks == -1) {
          this.timeSheets[index].breaks = null;
        }
        if(element.timeSheetStatus == "approved"){
          this.disabled = true;
        }
      });
      this.pushEdit();
      
    },
    pushEdit() {
      
      for (const sheet of this.timeSheets) {
        this.editedSheets.push({
          startTime: sheet.startTime,
          endTime: sheet.endTime,
          id: sheet.id,
          breaks: sheet.breaks,
          userId: sheet.userId,
          day: this.format_date(sheet.entryDate),
          timeSheetKey: sheet.timeSheetKey,
          changedBy: this.$store.state.user.userName,
        });
      }
    },
    format_date(value) {
      if (value) {
        return moment(String(value)).format("DD");
      }
    },
    getAllUsers() {
      this.$swatApi
        .get(this.$api.manageUser)
        .then((response) => {
          if (response.status === 200) {
            this.users = response.data;
          }
        })
        .catch((err) => {
          if (err.response) {
            alert(err.response.data);
          } else if (err.request) {
            alert("Something went wrong! Please try again");
          } else {
            alert("Something went wrong! Please try again later");
          }
        });
    },
    getTimeSheetByIdDate() {
      const formData = {
        id: this.id,
        month: this.month,
        year: this.year,
      };
      this.$swatApi
        .post(this.$api.getTimeSheetByIdDate, formData)
        .then((response) => {
          if (response.status === 200) {
            this.timeSheets = response.data;
            this.length = this.timeSheets.length;
            this.filterTimeSheets();
          }
        })
        .catch((err) => {
          if (err.response) {
            alert(err.response.data);
          } else if (err.request) {
            alert("Something went wronghhhhh! Please try again");
          } else {
            alert("Something went wrongrrrrrr! Please try again later");
          }
        });
    },
    createAuditFile() {
      const formData= [];
      this.editedSheets.forEach((sheet, index)=>{
        formData.push({
          startTime: sheet.startTime != null ? sheet.startTime : "-1",
          endTime: sheet.endTime != null ? sheet.endTime : "-1",
          breaks: sheet.breaks != null ? sheet.breaks : "-1",
          userId: this.id,
          id: this.timeSheets[index].id,
          timeSheetKey: this.timeSheets[index].timeSheetKey,
          day: this.format_date(this.timeSheets[index].entryDate),
          changedBy: this.$store.state.user.userName
        })
      })
      
      
      this.$swatApi
        .post(this.$api.createAuditFile, formData)
        .then((response) => {
          if (response.status === 200) {
            this.getTimeSheetByIdDate();
            alert("Time Sheet edited")
          }
        })
        .catch((err) => {
          if (err.response) {
            alert(err.response.data);
          } else if (err.request) {
            alert("Something went wronghhhhh! Please try again");
          } else {
            alert("Something went wrongrrrrrr! Please try again later");
          }
        });
    },
    approveBySuperUser() {
      this.con = this.timeSheets[this.length - 1];
      const formData = {
        userId: this.timeSheets[0].userId,
        monthStart: this.timeSheets[0].entryDate,
        monthEnd: this.timeSheets[this.length - 1].entryDate,
        status: "approved",
      };
      if (
        this.con.type != null ||
        this.con.startTime != null ||
        this.con.endTime != null
      ) {
        for (let index = 0; index < this.timeSheets.length - 1; index++) {
          this.timeSheets[index].timeSheetStatus = "approved";
        }
        this.$swatApi
          .post(this.$api.updateTimeSheetStatus, formData)
          .then((response) => {
            if (response.status === 200) {
              alert("Approved")
            }
          })
          .catch((err) => {
            if (err.response) {
              alert(err.response.data);
            } else if (err.request) {
              alert("Something went wronghhhhh! Please try again");
            } else {
              alert("Something went wrongrrrrrr! Please try again later");
            }
          });
      }
    },
    exportTimeSheets(){
      this.$router.push({ name: "exportTimeSheets" });
    },
  },
};
</script>
