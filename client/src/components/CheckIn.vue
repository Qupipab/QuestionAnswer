<template>
  <div id="CheckIn" class="CheckIn">
    <div class="CheckInTitle">
      <h1>Sign up</h1>
    </div>
    <div class = "SendForm">
      <span>Username</span>
      <input v-model="login" maxlength="50"/> 
      <span>Password</span>
      <input type ="password" v-model="password" maxlength="50"/>
      <span>Confirm password</span>
      <input type ="password" v-model="conPas" maxlength="50"/>
    </div>
    <button v-on:click="addUser">Sign up</button>
  </div>
</template>

<script>
import httpService from '../httpService.js'

let request = new httpService();

export default {
  name: 'App',
  data(){
    return {
      login: 'User',
      password: 'Password',
      conPas: 'Password'
    }
  },
  methods: {
    addUser() {
      if(this.login == '' || this.password == '') 
      {
        alert("Login or password cant be empty");
        return;
      }
      else if(this.password !== this.conPas) {
        alert("Password mismatch");
        return;
      }

      let obj = {
        Login: this.login,
        Password: this.password
      }
      
      request.ApplyToServer('Auth/NewUser', { method: 'POST', body: obj, type: 'text' }).then(r => {
        if(r === "1") 
        {
          alert("User added");
          this.$router.push('/signin');
        }
        else if(r === "0") alert("User already exists");
      });

      this.login = '';
      this.password = '';
      this.conPas = '';
    }
  }
}

</script>

<style scoped>
.CheckIn, .SendForm {
  display: flex;
}

a{
  font-weight: normal;
  font-size: 1.3rem;
  color: blue;
}

h1{
  font-size:2.3rem;
  text-transform: uppercase;
}

a:hover{
  text-decoration: underline;
}

button{ all: unset; }

input{
  height: 4rem;
  font-size: 2rem;
  padding: 1rem;
  margin: .5rem 0;
}

.CheckIn {
  -webkit-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  -moz-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  border-radius: 2rem;

  margin: 0 auto;
  margin-top: 20rem;
  justify-content: space-between;
  align-self: center;
  flex-direction: column;
  width: 40rem;
  height: 35rem;
}

.CheckInTitle {
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

button{
  text-align: center;
  height: 5rem;
  border-radius: 0 0 2rem 2rem;
  background: #2EBC4F;
  font-size:1.7rem;
  font-weight: bold;
  color: white;
}

button:hover{background:#28A745;}

</style>