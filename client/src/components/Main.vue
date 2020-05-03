<template>
  <div id = "Main" class = "Main">
    <div class = "MainPolls">
      <div class = "CabinetWrap">
        <h2>Public Polls</h2>
        <div class="nav">
          <router-link to = "/cabinet" class = "Cabinet" v-if="auth">Cabinet</router-link>
          <div class="SignOut" v-on:click = "SignOut" v-if="auth">Sign Out</div>
          <router-link to = "/signin" class = "Cabinet" v-if="!auth">Sign In</router-link>
        </div>
      </div>
      <div class = "UserPolls" v-for = "(user, ID) in pubPollsList" :key = "ID">
        <span>{{ user.login }}</span>
        <div class = "Polls">
          <a class = "Poll" v-bind:class="GetColor(poll.isActive)" v-bind:href="GetUrl(poll.link)" v-for = "(poll, p) in user.polls" :key = "p">
            {{ poll.title }}
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import httpService from '../httpService.js'

let request = new httpService();

export default {
  name: 'App',
  props: ['Poll'],
  data(){
    return {
      auth: false,
      pubPollsList: []
    }
  },
  methods:{
    GetUrl(val){
      return this.$route.fullPath + "/Poll/" + val;
    },
    GetColor(val){
      if(val) return "Green";
      else return "Red";
    },
    SignOut(){
      request.ApplyToServer('Auth/SignOut').then(() => this.$router.push('/signin'));
    }
  },
  mounted(){
    request.ApplyToServer('Auth/GetLoggedId', { type: 'text' }).then(r => { 
      if(r != "0") this.auth = true;
    }).then(() => request.ApplyToServer('Main/GetPubPolls').then(r => this.pubPollsList = r));
  }
}
</script>

<style scoped>
  .CabinetWrap, .MainPolls, .Poll, .Polls, .nav, .SignOut{
    display: flex;
  }

  .Red{
    background-color: #EC7063;
  }

  .Green{
    background-color: #28B463;
  }

  .Main{
    font-size: 2rem;
  }

  .CabinetWrap{
    justify-content: space-between;
    margin-top: 1rem;
  }

  .Cabinet{
    -webkit-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    padding: .7rem 4rem;
    border-radius: 2rem;
    background: #2EBC4F;
    color:#FFF;
  }

  .Cabinet:hover{
    background: #28A745;
  }

  .MainPolls
  {
    margin: 0 auto;

    max-width: 90rem;
    justify-content: center;
    flex-direction: column;
    padding: 0 5rem;
  }

  .UserPolls{
    margin: .5rem 0;
  }

  .Polls{
    align-content: flex-start;
    flex-wrap: wrap;
    margin-top: .5rem;
  }

  .Poll
  {
    cursor: pointer;
    margin: .4rem;
    justify-content: center;
    align-items: center;
    -webkit-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    border-radius: 2rem;
    padding: 2rem;
  }

  .Poll:hover{
    -webkit-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  }

  .nav .SignOut{
    cursor: pointer;
    margin-left: 2rem;
    padding: 0 3rem;
    -webkit-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    border-radius: 2rem;
    background: #2EBC4F;
    color: white;
    justify-content: center;
    align-items: center;
  }

  .nav .SignOut:hover{
    background-color: #28A745;
  }
</style>