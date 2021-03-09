<template>
  <div class="row userMiddle ">
    <!-- <div class="col-3"></div> -->

    <div class="col">
      <div class="flexPosition">
        <ul>
          <li><p>Main Menu</p></li>
          <li><button @click="editTimeSheet">Edit Time Sheet</button></li>
          <li><button>Take Shipping Notes Pictures</button></li>
          <li><button @click="openTimeSheet">Open Time Sheet</button></li>
          <li><button>Create Request</button></li>
          <li><button>Vacation Calendar</button></li>
        </ul>
      </div>
    </div>

    <!-- <div class="col-3"></div> -->
  </div>
</template>
<script>
export default {
  name: "mainMenu",
  methods: {
    editTimeSheet() {
      this.$router.push({ name: "editTimeSheet" });
    },
    openTimeSheet() {
      this.$router.push({ name: "openTimeSheet" });
    },
  },
};
</script>
