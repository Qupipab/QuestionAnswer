<template>
  <div id="Auth" class="Auth">
    <div class="AuthTitle">
      <h1>Sign in</h1>
    </div>
    <div class = "SendForm">
      <span>Username</span>
      <input v-model="login" maxlength="50"/> 
      <div class = "PassFrame">
        <span>Password</span>
        <router-link to = '/reg'>Forgot password?</router-link>
      </div>
      <input v-model="password" maxlength="50"/>
    </div>
    <router-link to = '/signin/main' class = "SendBut"><button v-on:click="UserCheck">Sign in</button></router-link>
  </div>
</template>

<script>
import httpService from "../httpService.js";

let request = new httpService();

export default {
  name: "App",
  data() {
    return {
      login: "User1",
      password: "Password1"
    };
  },
  methods: {
    addUser() {
      let obj = {
        Login: this.login,
        Password: this.password
      };
      request.NewUser(obj);
      login = "";
      password = "";
    },
    UserCheck() {
      let obj = {
        Username: this.login,
        Password: this.password
      };
      request.CheckUser(obj);
    }
  }
};
</script>

<style scoped>
.Auth, .SendForm, .PassFrame, .SendBut {
  display: flex;
}

.PassFrame a{
  font-weight: normal;
  font-size: 1.3rem;
  color: blue;
}

h1{
  font-size:2.3rem;
  text-transform: uppercase;
}

.PassFrame a:hover{
  text-decoration: underline;
}

button{ all: unset; }

input{
  height: 4rem;
  font-size: 2rem;
  padding: 1rem;
  margin: .5rem 0;
}

.PassFrame{
  flex-direction: row;
  justify-content: space-between;
}

.Auth {
  -webkit-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  -moz-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  border-radius: 2rem;

  margin: 0 auto;
  margin-top: 30rem;
  justify-content: space-between;
  align-self: center;
  flex-direction: column;
  width: 40rem;
  height: 28rem;
}

.AuthTitle {
  text-align: center;
  margin-top:2rem;
}

.SendForm{
  justify-content: center;
  flex-direction: column;
  margin: 1.5rem;
  font-size: 1.5rem;
  font-weight: bold;
}

.SendBut{
  text-align: center;
  height: 5rem;
  border-radius: 0 0 2rem 2rem;
  background: #2EBC4F;
  font-size:1.7rem;
  font-weight: bold;
  color: white;
  justify-content: center;
}

.SendBut:hover{background:#28A745;}

</style>