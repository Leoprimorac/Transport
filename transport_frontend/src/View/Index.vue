<template>
    <div class="container containerIndex">
      <div class="row rowIndex" >
        <div class="col loginForm">
          <div class="form">
              <p id="title">Taric Transport Time Management Tool</p>
              <p id="subtitle">Login</p>
              <form @submit.prevent="Login">
                <label for="UserName">Enter your username:</label><br>
                <input type="text" placeholder="JohnDoe911" name="UserName" v-model="UserName"> <br><br>

                <label for="Password">Enter your password:</label> <br>
                <input type="text" placeholder="********" name="Password" v-model="Password"> <br>
                <button class="submit" type="submit" >Enter</button>
              </form>
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import '../css/Index.css';
export default {
  data() {
    return {
      UserName: "",
      Password:"",
      user: null
    }
  },
  
  methods:{

    Login: function () {
      const formData = {
        UserName: this.UserName,
        Password: this.Password};

      this.$swatApi.post(this.$api.login, formData)
        .then(response => {
          if (response.status === 200) {
            this.user = response.data;
            this.$store.commit('setUser', this.user);
            this.Routing(response.data);
          }
        })
        .catch(err => {
          if (err.response) {
            alert(err.response.data);
            this.UserName = this.Password = "";
          } else if (err.request) {
            alert("Something went wrong! Please try again");
            this.UserName = this.Password = "";
          } else {
            alert("Something went wrong! Please try again later");
          }
      });
    },
    Routing(userData){
      if(userData.level == "Admin" || userData.level == "SuperUser" ){
        this.$router.push({path: '/Admin/mainMenu'})
      }else if(userData.level == "User"){
        this.$router.push({path: '/User/mainMenu'})
      }
    }
  }
}

</script>
