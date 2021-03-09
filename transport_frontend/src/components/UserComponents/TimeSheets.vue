<template>
  <div class="row">
    <div class="col UserTimeSheet">
      <form class="UserTimeSheetForm ">
        <label for="year">Year:</label>
        <select id="year" required v-model="year">
          <option :value="year" v-for="year in years" :key="year">{{
            year
          }}</option>
        </select>
      </form>

      <form class="UserTimeSheetForm ">
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

    <table class="editTimeSheetTable">
      <template v-for="(timeSheet, index) in timeSheets">
        <tr :key="index">
          <p id="editRowtitle" @click.prevent="toggle(index)">
            Open Day {{ format_date(timeSheet.entryDate) }}
          </p>
        </tr>
        <tr :key="timeSheet.id" v-if="opened.includes(index)">
          <td data-label="Day">{{ format_date(timeSheet.entryDate) }}</td>
          <td data-label="Type">{{ timeSheet.type }}</td>
          <td data-label="Start Time">{{ timeSheet.startTime }}</td>
          <td data-label="End Time">{{ timeSheet.endTime }}</td>
          <td data-label="Break">{{ timeSheet.breaks }}</td>
          <td data-label="Work Time">{{ timeSheet.workTime }}</td>

          <td data-label="m3" v-if="timeSheet.m3 != 0">{{ timeSheet.m3 }}</td>
          <td data-label="Km - Stand">{{ timeSheet.kmStand }}</td>
          <td data-label="Privat">{{ timeSheet.privat }}</td>
          <td data-label="Fuel">{{ timeSheet.fuel }}</td>
          <td data-label="AdBlue">{{ timeSheet.adblue }}</td>
          <td data-label="Notes">{{ timeSheet.notes }}</td>
        </tr>
      </template>
    </table>
  </div>
</template>
<script>
import moment from "moment";
export default {
  data() {
    return {
      timeSheets: null,
      id: this.$store.state.user.id,
      month: JSON.stringify(new Date().getMonth() + 1),
      year: 2021,
      opened: [],
    };
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
      (ts) => [ts.month, ts.year],
      (val) => {
        this.getTimeSheetByIdDate(val);
      },
      {
        immediate: true,
        deep: true,
      }
    );
  },
  methods: {
    toggle(id) {
      const index = this.opened.indexOf(id);
      if (index > -1) {
        this.opened.splice(index, 1);
      } else {
        this.opened.push(id);
      }
    },
    filterTimeSheets() {
      this.timeSheets.forEach((element, index) => {
        if (element.type == -1) {
          this.timeSheets[index].type = null;
        }
        if (element.startTime == -1) {
          this.timeSheets[index].startTime = null;
        }
        if (element.endTime == -1) {
          this.timeSheets[index].endTime = null;
        }
        if (element.breaks == -1) {
          this.timeSheets[index].breaks = null;
        }
        if (element.workTime == -1) {
          this.timeSheets[index].workTime = null;
        }
        if (element.notes == -1) {
          this.timeSheets[index].notes = null;
        }
        if (element.m3 == -1) {
          this.timeSheets[index].m3 = null;
        }
        if (element.kmStand == -1) {
          this.timeSheets[index].kmStand = null;
        }
        if (element.privat == -1) {
          this.timeSheets[index].privat = null;
        }
        if (element.fuel == -1) {
          this.timeSheets[index].fuel = null;
        }
        if (element.adblue == -1) {
          this.timeSheets[index].adblue = null;
        }
      });
    },
    format_date(value) {
      if (value) {
        return moment(String(value)).format("D");
      }
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
  },
};
</script>
