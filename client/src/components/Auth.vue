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
      </div>
      <input type = "password" v-model="password" maxlength="50"/>
    </div>
    <div class="navButtons">
      <button v-on:click="RegUser">Sign up</button>
      <button v-on:click="UserCheck">Sign in</button>
    </div>
  </div>
</template>

<script>
import httpService from "../httpService.js";

let request = new httpService();

export default {
  name: "App",
  data() {
    return {
      login: "root",
      password: "toor"
    };
  },
  methods: {
    UserCheck() {
      let obj = {
        Login: this.login,
        Password: this.password
      };
      request.ApplyToServer('Auth/SignIn',{ method: 'POST', body: obj, type: 'text' }).then(r => {
        if(r == "1") this.$router.push('/main')
        else alert("Incorrect login or password");
      });
    },
    RegUser(){
      this.$router.push('/reg');
    }
  }
};
</script>

<style scoped>
.Auth, .SendForm, .PassFrame, .SendBut, .navButtons{
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

.navButtons{
  justify-content: space-between;
}

button{
  width: 50%;
  text-align: center;
  height: 5rem;
  background: #2EBC4F;
  font-size:1.7rem;
  font-weight: bold;
  color: white;
  justify-content: center;
  cursor: pointer;
}

button:first-child{
  border-radius: 0 0 0 2rem;
}

button:last-child{
  border-radius: 0 0 2rem 0rem;
}

button:hover{background:#28A745;}

</style>