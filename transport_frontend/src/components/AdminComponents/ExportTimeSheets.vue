<template>
  <div class="container">
    <div class="row middle">
      <div class="col">
        <p>Export Time Sheets</p>
      </div>
    </div>
    <div class="row exportTSSelectors">
      <div class="col-6">
        <form class="">
          <label for="users">User:</label>
          <select id="users" v-model="id">
            <option v-for="user in users" :key="user.id" :value="user.id">{{
              user.userName
            }}</option>
          </select>
        </form>
        <form class="">
          <label for="year">Year:</label>
          <select id="year" required v-model="year" >
            <option :value="year" v-for="year in years" :key="year" >{{
              year
            }}</option>
          </select>
        </form>

        <form class="">
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
        <button @click.prevent="AddData()" class="bttnStyle">Add TimeSheet</button>
      </div>
      <div class="col-6">
        <p>Selected Time Sheets:</p>
        <div class="ExportTableOverflow">
        <table style="height: 100%">
          <tr v-for="(item, index) in data" :key="index">
            <td>{{ item.userName }} -</td>
            <td>{{ item.month }}/{{ item.year }}</td>
            <td><button @click.prevent="UncheckTimeSheet(index)" class="bttnStyle cancelBttn">uncheck</button></td>
          </tr>
        </table>
        </div>
        <button @click.prevent="UncheckAllTimeSheets()" class="bttnStyle">Uncheck All</button>
      </div>
    </div>
    <button @click.prevent="exportTimeSheets() && exportTimeSheetPDF()" class="bttnStyle exportbttn">Export PDF/XLS</button>
  </div>
</template>
<script>
export default {
  name: "exportTimeSheets",
  data() {
    return {
      users: null,
      id: null,
      month: null,
      year: null,
      timeSheets: null,
      data: [],
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

  methods: {
      
      exportTimeSheets(){
          const formData=[];
          this.data.forEach(element => {
              formData.push( {id: element.id, month: element.month, year:  element.year});
          });
          this.$swatApi
        .post(this.$api.exportTimeSheets, formData, {responseType: 'blob'})
        .then((response) => {
          if (response.status === 200) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'TimeSheet.xlsx');
            document.body.appendChild(link);
            link.click();
            this.exportTimeSheetPDF();
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
      exportTimeSheetPDF(){
          const formData=[];
          this.data.forEach(element => {
              formData.push( {id: element.id, month: element.month, year:  element.year});
          });
          this.$swatApi
        .post(this.$api.exportTimeSheetPDF, formData, {responseType: 'blob'})
        .then((response) => {
          if (response.status === 200) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'TimeSheet.pdf');
            document.body.appendChild(link);
            link.click();
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

      UncheckAllTimeSheets(){
          this.data = [];
      },
      UncheckTimeSheet(index){
          this.data.splice(index,1)
      },
    AddData() {
      let user = this.users.filter((user) => user.id == this.id);
      const formData = {
        id: this.id,
        month: this.month,
        year: this.year,
        userName: user[0].userName,
      };
      let log = this.data.filter(
        (value) =>
          value.id == formData.id &&
          value.month == formData.month &&
          value.year == formData.year
      );

      if (!log.length) {
        this.data.push(formData);
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
  },
};
</script>
