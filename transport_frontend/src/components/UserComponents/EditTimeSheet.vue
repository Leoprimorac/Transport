<template>
  <div class="row timeSheetHeight">
    <table class="editTimeSheetTable">
      <template v-for="(timeSheet, index) in timeSheets">
        <tr :key="index">
          <p id="editRowtitle" @click.prevent="toggle(index)">
            Edit Day {{ format_date(timeSheets[index].entryDate) }}
          </p>
        </tr>
        <tr :key="timeSheet.id" v-if="opened.includes(index)">
          <td data-label="Day">
            <input
              type="text"
              :name="index"
              :value="format_date(timeSheets[index].entryDate)"
            />
          </td>
          <td data-label="Type">
            <input type="text" :name="index" v-model="timeSheets[index].type" />
          </td>
          <td data-label="Start Time">
            <input
              type="text"
              :name="index"
              v-model="timeSheets[index].startTime"
            />
          </td>
          <td data-label="End Time">
            <input
              type="text"
              :name="index"
              v-model="timeSheets[index].endTime"
            />
          </td>
          <td data-label="Break">
            <input
              type="text"
              :name="index"
              v-model="timeSheets[index].breaks"
            />
          </td>
          <td data-label="Work Time">
            <input
              type="text"
              :name="index"
              v-model="timeSheets[index].workTime"
              disabled="true"
            />
          </td>

          <td data-label="m3" v-if="timeSheet.m3 != 0">
            <input type="number" :name="index" v-model="timeSheets[index].m3" />
          </td>
          <td data-label="Km - Stand">
            <input
              type="number"
              :name="index"
              v-model="timeSheets[index].kmStand"
            />
          </td>
          <td data-label="Privat">
            <input
              type="number"
              :name="index"
              v-model="timeSheets[index].privat"
            />
          </td>
          <td data-label="Fuel">
            <input
              type="number"
              :name="index"
              v-model="timeSheets[index].fuel"
            />
          </td>
          <td data-label="AdBlue">
            <input
              type="number"
              :name="index"
              v-model="timeSheets[index].adblue"
            />
          </td>
          <td data-label="Notes">
            <input
              type="text"
              :name="index"
              v-model="timeSheets[index].notes"
            />
          </td>
          <button
            class="editTSEditBttn"
            @click.prevent="editTimeSheet(index)"
            v-if="
              timeSheets[index].timeSheetStatus != 'approved by employee' &&
                timeSheets[index].timeSheetStatus != 'approved'
            "
          >
            Edit
          </button>
        </tr>
      </template>
    </table>
    <button class="approveTSBttn" v-b-modal.approveModal :disabled="disabled">
      Approve Time Sheet
    </button>

    <b-modal
      centered
      id="approveModal"
      title="Approval"
      @ok="approveByUser"
      ok-title="Approve"
      ok-variant="danger"
      cancel-title="Cancel"
    >
      <p class="my-4">
        You are approving correct input and data by signing and approving
      </p>
    </b-modal>
  </div>
</template>
<script>
import moment from "moment";
export default {
  data() {
    return {
      timeSheets: null,
      opened: [],
      con: null,
      disabled: true,
    };
  },
  mounted() {
    this.getTimeSheetByStatus();
  },
  computed: {
    timeSheetLast() {
      return this.timeSheets.length - 1;
    },
  },

  methods: {
    approveByUser() {
      this.con = this.timeSheets[this.timeSheetLast];
      this.disabled = true;
      const formData = {
        userId: this.timeSheets[0].userId,
        monthStart: this.timeSheets[0].entryDate,
        monthEnd: this.timeSheets[this.timeSheetLast].entryDate,
        status: "approved by employee",
      };
      if (
        this.con.type != null ||
        this.con.startTime != null ||
        this.con.endTime != null
      ) {
        for (let index = 0; index < this.timeSheets.length; index++) {
          this.timeSheets[index].timeSheetStatus = "approved by employee";
        }
        this.$swatApi
          .post(this.$api.updateTimeSheetStatus, formData)
          .then((response) => {
            if (response.status === 200) {
              this.getTimeSheetByStatus();
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
      }
    },
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
        return moment(String(value)).format("DD.MM.YYYY");
      }
    },
    getTimeSheetByStatus() {
      const formData = {
        userId: this.$store.state.user.id,
        status: "working progress"
      };
      this.$swatApi
        .post(this.$api.getTimeSheetByStatus, formData)
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
            alert("Something went wrong! Please try again");
          } else {
            alert("Something went wrong! Please try again later");
          }
        });
    },
    editTimeSheet(index) {
      if (index == this.timeSheetLast) {
        this.disabled = false;
      }
      const formData = {
        type:
          this.timeSheets[index].type != null
            ? this.timeSheets[index].type
            : "-1",
        startTime:
          this.timeSheets[index].startTime != null
            ? this.timeSheets[index].startTime
            : "-1",
        endTime:
          this.timeSheets[index].endTime != null
            ? this.timeSheets[index].endTime
            : "-1",
        breaks:
          this.timeSheets[index].breaks != null
            ? this.timeSheets[index].breaks
            : "-1",
        workTime:
          this.timeSheets[index].workTime != null
            ? this.timeSheets[index].workTime
            : "-1",
        notes:
          this.timeSheets[index].notes != null
            ? this.timeSheets[index].notes
            : "-1",
        m3:
          this.timeSheets[index].m3 != null
            ? parseInt(this.timeSheets[index].m3)
            : -1,
        adblue:
          this.timeSheets[index].adblue != null
            ? parseInt(this.timeSheets[index].adblue)
            : -1,
        fuel:
          this.timeSheets[index].fuel != null
            ? parseInt(this.timeSheets[index].fuel)
            : -1,
        kmStand:
          this.timeSheets[index].kmStand != null
            ? parseInt(this.timeSheets[index].kmStand)
            : -1,
        privat:
          this.timeSheets[index].privat != null
            ? parseInt(this.timeSheets[index].privat)
            : -1,
        entryDate: this.timeSheets[index].entryDate,
        id: this.timeSheets[index].id,
        timeSheetKey: this.timeSheets[index].timeSheetKey,
        userId: this.timeSheets[index].userId,
      };
      this.$swatApi
        .post(this.$api.editTimeSheetByUser, formData)
        .then((response) => {
          if (response.status === 200) {
            this.getTimeSheetByStatus();
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
<style>
.user p,
.date p {
  background-color: #f2f1ef !important;
  border: 2px solid #697184 !important;
  font-weight: 600 !important;
  font-variant: small-caps;
  color: #413f3d !important;
}
</style>
