<template>
  <div id = "Auth">
    <div class = "SignIn">
      <h3>{{ login }}</h3>
      <input v-model = "login">
      <h3>{{ password }}</h3>
      <input v-model = "password">
    </div>
    <div>
      <button v-on:click="addUser">Send</button>
    </div>
    <div class = "Users">
      <h3>List of users</h3>
      <ul>
        <li v-for="(user, ID) in users" :key="ID">Login: {{ user.Login }} Password: {{ user.Password }}</li>
      </ul>
    </div>
  </div>
</template>

<script>
import httpService from './httpService.js'

let request = new httpService();

export default {
  name: 'App',
  data(){
    return{
      users: [],
      login: 'Login',
      password: 'Password'
    }
  },
  methods: {
    addUser() {
      let obj = {
        Login: this.login,
        Password: this.password
      }
      request.NewUser(obj);
      login = '';
      password = '';
    }
  },
  created()
  {
    request.GetAllUsers().then(r => this.users = r);
  }
}

</script>

<style scoped>
  
</style>