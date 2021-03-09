<template>
  <form @submit.prevent="editUser" class="editForm style">
    <div class="row middle">
      <div class="col-3"></div>
      <div class="col-3 col6One">
        <label for="personalNo">PersonalNo:</label><br />
        <input
          type="text"
          id="personalNo"
          name="personalNo"
          placeholder="John"
          v-model="userData.personalNo"
          required
        /><br />

        <label for="userName">User name:</label><br />
        <input
          type="text"
          id="userName"
          name="userName"
          placeholder="John"
          v-model="userData.userName"
          required
        /><br />

        <label for="pw">PW:</label><br />
        <input
          type="text"
          id="pw"
          name="pw"
          placeholder="John"
          v-model="userData.pw"
          required
        /><br />

        <label for="name">Full name:</label><br />
        <input
          type="text"
          id="lname"
          name="name"
          placeholder="Doe"
          v-model="userData.name"
          required
        /><br />
      </div>
      <div class="col-3">
        <label for="FM#">FM#:</label><br />
        <input
          type="text"
          id="FM#"
          name="FM#"
          placeholder="123"
          v-model="userData.fm"
          required
        /><br />

        <label for="email">Email:</label><br />
        <input
          type="text"
          id="email"
          name="email"
          placeholder="John"
          v-model="userData.email"
          required
        /><br />

        <label for="password">Password:</label><br />
        <input
          type="text"
          id="password"
          name="password"
          placeholder="******"
          v-model="userData.password"
          required
        /><br />

        <label for="level">Choose user's level:</label><br />
        <select id="level" v-model="userData.level" required>
          <option value="Admin" selected>Admin</option>
          <option value="SuperUser">SuperUser</option>
          <option value="User">User</option>
        </select>
      </div>
      <div class="col-3"></div>
    </div>
    <button class="editUserBttn" type="submit">Save Changes</button>
  </form>
</template>
<script>
export default {
  name: "editUser",
  data() {
    return {
      userData: null,
    };
  },
  created() {
    this.userData = this.$route.params;
  },
  methods: {
    editUser() {
      this.$swatApi
        .post(this.$api.editUser, this.userData)
        .then((response) => {
          if (response.status === 200) {
            alert("User edited successfully");
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
