<template>
  <div id="UserVotes" class="UserVotes">
    <h1>UserVotes</h1>
    <div class="Answers">
      <div class="Answer" v-for="(answer, answerID) in userVotes" :key = "answerID">
        <h3>{{ answer.title }}</h3>
        <div class="Users">
          <div class="User" v-for="(user, userID) in answer.users" :key = "userID">
            <span>{{ user.login }}</span>
          </div>
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
  props: [
    'id'
  ],
  data(){
    return {
      userVotes: []
    }
  },
  mounted()
  {
    request.ApplyToServer('Poll/UserVotes/' + this.id).then(r => {
        this.userVotes = r;
    });
  }
}
</script>

<style scoped>
  .Users{
    display: flex;
  }

  .UserVotes{
    margin: 2rem auto 0 auto;
    width: 70rem;
  }

  .UserVotes h1{
    font-size: 3rem;
  }
  
  .Answers{
    margin: 1rem 0;
  }

  .Answer h3{
    font-size: 2rem;
  }

  .Users{
    margin: .5rem 0;
  }

  .User{
    user-select: none;
    cursor: auto;
    border-radius: 1rem;
    margin: .25rem .25rem;
    padding: .5rem 2rem;
    -webkit-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
  }

  .User:hover{
    -webkit-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
  }

  .User span{
    font-size: 2rem;
  }
</style>