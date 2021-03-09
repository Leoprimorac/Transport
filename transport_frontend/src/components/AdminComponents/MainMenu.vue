<template>
  <div class="row middle ">
    <div class="col">
      <ul>
        <li><p>Main Menu</p></li>
        <li><button @click="createUserComponent()" :hidden="$store.state.user.level != 'Admin'">Create User</button></li>
        <li><button @click="editUserComponent()" :hidden="$store.state.user.level != 'Admin'">Edit User</button></li>
        <li><button @click="timeSheets()">Time Sheets</button></li>
        <li><button @click="auditFiles()">Audit File</button></li>
        <li><button>Vacation Calendar</button></li>
        <li><button @click="exportTimeSheets()">Export Time Sheets</button></li>
        <li><button>Requests & Approvals</button></li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "mainMenu",
  methods: {
    createUserComponent() {
      this.$router.push({ name: "createUser" });
    },
    editUserComponent() {
      this.$router.push({ name: "manageUser" });
    },
    timeSheets() {
      this.$router.push({ name: "timeSheet" });
    },
    auditFiles() {
      this.$router.push({ name: "auditFiles" });
    },
    exportTimeSheets(){
      this.$router.push({ name: "exportTimeSheets" });
    },
  },
};
</script>
