<template>
  <div class="row middle">
    <form @submit.prevent="createUser" class="createUserForm">
      <label for="userName">User name:</label><br />
      <input
        type="text"
        id="userName"
        name="userName"
        placeholder="John"
        v-model="userData.userName"
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

      <label for="FM#">FM#:</label><br />
      <input
        type="text"
        id="FM#"
        name="FM#"
        placeholder="123"
        v-model="userData.fm"
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

      <button class="create" type="submit">Create User</button>
    </form>
  </div>
</template>
<script>
export default {
  name: "createUser",
  data() {
    return {
      userData: {
        userName: null,
        name: null,
        fm: null,
        password: null,
        level: "User",
      },
    };
  },
  methods: {
    createUser: function() {
      this.$swatApi
        .post(this.$api.createUser, this.userData)
        .then((response) => {
          if (response.status === 200) {
            alert("User created successfully");
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
