<template>
  <div class="row middle">
    <div class="TableOverflow">
    <table class="manageTable ">
      <tr>
        <th>PersonalNo</th>
        <th>Username</th>
        <th>PW</th>
        <th>Name</th>
        <th>FM#</th>
        <th>Level</th>
        <th>Entry Date</th>
        <th>E-Mail Address</th>
        <th>Edit</th>
        <th>Delete</th>
      </tr>
      <tr v-for="user in users" :key="user.id">
        <td v-if="user.personalNo != 0">{{ user.personalNo }}</td>
        <td v-else>empty</td>
        <td>{{ user.userName }}</td>
        <td v-if="user.pw != 0">{{ user.pw }}</td>
        <td v-else>empty</td>
        <td>{{ user.name }}</td>
        <td>{{ user.fm }}</td>
        <td>{{ user.level }}</td>
        <td>{{ format_date(user.entryDate) }}</td>
        <td v-if="user.email != 0">{{ user.email }}</td>
        <td v-else>empty</td>
        <td>
          <button class="editBttn" @click.prevent="editUser(user)">Edit</button>
        </td>

        <td>
          <button class="deleteBttn" @click.prevent="deleteUser(user.id)">
            Delete
          </button>
        </td>
      </tr>
    </table>
    </div>
  </div>
</template>
<script>
import moment from "moment";
export default {
  name: "ManageUsers",
  data() {
    return {
      users: [],
      id: null,
    };
  },
  created() {
    this.manageUser();
  },
  methods: {
    manageUser: function() {
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
    format_date(value) {
      if (value) {
        return moment(String(value)).format("DD.MM.YYYY hh:MM");
      }
    },
    deleteUser(value) {
      this.$swatApi.post(this.$api.deleteUser + value).then((response) => {
        if (response.status == 200) {
          this.users = this.users.filter((user) => user["id"] != value);
          alert("User deleted");
        }
      });
    },
    editUser(value) {
      this.$router.push({ name: "editUser", params: value });
    },
  },
};
</script>
