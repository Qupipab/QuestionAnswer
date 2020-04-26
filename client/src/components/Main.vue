<template>
  <div id = "Main" class = "Main">
    <div class = "MainPolls">
      <div class = "CabinetWrap">
        <h2>Public Polls</h2>
        <router-link to = "/cabinet" class = "Cabinet">Cabinet</router-link>
      </div>
      <div class = "UserPolls" v-for = "(user, ID) in PubPollsList" :key = "ID">
        <span>{{ user.Login }}</span>
        <div class = "Polls">
          <router-link to = "/" class = "Poll" v-for = "(poll, i) in user.Polls" :key = "i">
            {{ poll.Title }}
          </router-link>
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
  data(){
    return {
      PubPollsList: []
    }
  },
  mounted(){
    request.ApplyToServer('Poll/GetPolls').then(r => this.PubPollsList = r);
  }
}
</script>

<style scoped>
  .CabinetWrap, .MainPolls, .Poll, .Polls{
    display: flex;
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
</style>