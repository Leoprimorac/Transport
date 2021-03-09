<template>
<div>
  <div class="row middle auditFile">
    <table style="width:100% " class="TimeSheetsTable auditTable">
      <tr>
        <th>TimeSheetKey</th>
        <th>DayNo</th>
        <th>Field</th>
        <th>Old Value</th>
        <th>New Value</th>
        <th>Date & Time of Change</th>
        <th>Changed by User</th>
      </tr>

      <tr v-for="auditFile in auditFiles" :key="auditFile.id">
        <td>{{ auditFile.timeSheetKey }}</td>
        <td>{{ auditFile.day }}</td>
        <td>{{ auditFile.field }}</td>
        <td>{{ auditFile.oldValue == -1 ? "": auditFile.oldValue }}</td>
        <td>{{ auditFile.newValue }}</td>
        <td>{{ format_date(auditFile.dateOfChange) }}</td>
        <td>{{ auditFile.changedBy }}</td>
      </tr>
    </table>
    
  </div>
  <button @click.prevent="exportTimeSheets()" class="bttnStyle exportbttn">Export PDF/XLS</button>
  </div>
</template>
<script>
import moment from "moment";
export default {
  data() {
    return {
      auditFiles: null,
    };
  },
  created() {
    this.getTimeSheetByIdDate();
  },
  methods: {
    format_date(value) {
      if (value) {
        return moment(String(value)).format("DD.MM.YYYY");
      }
    },
    getTimeSheetByIdDate() {
      this.$swatApi
        .get(this.$api.getAllAuditFiles)
        .then((response) => {
          if (response.status === 200) {
            this.auditFiles = response.data;
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
    exportTimeSheets(){
      this.$router.push({ name: "exportTimeSheets" });
    },
  },
};
</script>
