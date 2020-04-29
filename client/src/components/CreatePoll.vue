<template>
  <div id = "CreatePoll" class = "Poll">
    <div class="UpWrap">
      <div class="Title">
        <h5>Title: {{ qestTitle }}</h5>
        <input type="text" v-model="qestTitle">
      </div>
      <div class="Author">
        <h5>Author: {{ Author }}</h5>
      </div>
      <div class = "Votes">
        <ul>
          <li v-for = "(vote, index) in Votes" :key ="index">{{ index + 1 }}: {{ Votes[index] }}</li>
        </ul>
        <div class="OwnVote">
          <input type="text" v-model="Vote">
          <div class="OwnVoteBut" @click="AddVote"><span>Add Vote</span></div>
        </div>
      </div>
    </div>
    <div class="DownWrap">
      <div class="Set">
        <div class="Anon">
          <span>Anon</span>
          <input type = "checkbox" v-model="anon"/>
        </div>
        <div class="UserAnswer">
          <span>User Answer</span>
          <input type = "checkbox" v-model="UserAnswer"/>
        </div>
        <div class="MV">
          <span>Max Votes</span>
          <input type="number" name="quantity" min="1" max="10" v-model="MaxVotes"/>
        </div>
        <div class="DL">
          <span>Death Line</span>
          <input type="date" v-model="now"/>
        </div>
        <button v-on:click="AddPoll">Create</button>
      </div>
    </div>
  </div>
</template>

<script>
import MyFuncs from '../myFuncs.js'
import httpService from '../httpService.js'

let myFunc = new MyFuncs();
let request = new httpService();

export default {
  name: 'App',
  data(){
    return {
      qestTitle: 'Title',
      UserID: "",
      Author: "Default",
      Votes: [],
      anon: false,
      UserAnswer: false,
      MaxVotes: 1,
      now: myFunc.DateNowYMD(),
      Vote: ''
    }
  },
  watch:{
    MaxVotes(val) {
      if(val < 0) this.MaxVotes = 0;
      else if(val > 10) this.MaxVotes = 10;
    }
  },
  methods:{
    AddPoll(){
      let toSend = this.Votes;
      const poll = {
        UserID: parseInt(this.UserID),
        Title: this.qestTitle,
        CreateDate: this.now,
        CloseDate: this.now,
        IsPrivate: this.anon,
        IsActive: true,
        VotesCount: parseInt(this.MaxVotes),
        CanAddAnswers: this.UserAnswer,
        Link: 'link'
      }

      request.ApplyToServer('NewPoll/GetLastPoll').then(r =>{
      
        let arr = [];
        for(let i = 0; i < toSend.length; i++)
        {
          const ans = {
            Title: toSend[i],
            CreatorID: 1,
            PollID: r + 1
          }
          arr.push(ans);
        }
        
        request.ApplyToServer('NewPoll/AddPoll', { body: poll, method: 'POST' })
        .then(() => request.ApplyToServer('NewPoll/AddAnswers', { body:arr, method: 'POST' }))})
        .then(() => {
          this.qestTitle = 'Title';
          this.Votes = [];
          this.anon = false;
          this.UserAnswer = false;
          this.MaxVotes = 1;
          this.now = myFunc.DateNowYMD();
        });
        this.Votes = [];
      },
      AddVote(){
        if(this.Vote != '' && !(this.Votes.length >= 10)){
          this.Votes.push(this.Vote);
          this.Vote = '';
        }
      }
  },
  mounted(){
    request.ApplyToServer('Auth/GetLoggedId', {type: 'text'}).then(r => {
      if(r !== "0")
      {
        request.ApplyToServer('NewPoll/GetPollAuthor').then(r => {
          this.Author = r[0].Login;
          this.UserID = r[0].ID;
        });
      }
      else this.$router.push('/signin');
    });
  }
}
</script>

<style scoped>
  .Poll{
    font-size: 2rem;
  }

  .Title, .Poll, .Author, .Votes, .Set, .Anon,
  .UserAnswer, .MV, .DL, .OwnVote, .OwnVoteBut{
    display: flex;
  }

  .Title{
    justify-content: space-between;
  }

  .Poll{
    margin: 15rem auto 0 auto;

    -webkit-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    -moz-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);
    border-radius: 1rem;
    width: 50rem;

    justify-content: space-between;
    flex-direction: column;
  }

  .Poll .Title{
    padding: 1rem 1rem 0rem 1rem;
  }

  .Poll .Author{
    justify-content: flex-end;
    padding: .5rem 1rem;
  }

  .Poll .Votes{
    padding: 0.25rem 1rem 0.25rem 1rem;
    flex-direction: column;
    width: 100%;
    font-size: 1.7rem;
  }

  .Poll .Votes input[type = text]{
    width: 100%;
    height: 3rem;
    font-size: 2rem;
    padding: 1rem;
  }

  .OwnVote{
    margin-top: 1rem;
  }

  .OwnVote .OwnVoteBut{
    cursor: pointer;
    background-color: #2EBC4F;
    font-size: 1.5rem;
    justify-content: center;
    align-items: center;
  }

  .OwnVote .OwnVoteBut:hover{
    background-color: #28A745;
  }

  .OwnVote .OwnVoteBut span{
    padding: 0 1rem;
    color:white;
  }

  .Set{
    margin-top:1rem;
    justify-content: space-around;
    font-size: 1.7rem;
    font-weight: bold;
    padding: 1rem;
  }

  .Set span{
    margin-bottom: .5rem;
  }

  .Set input[type = "number"]{
    width: 4rem;
  }

  .Set .Anon, .Set .UserAnswer, .Set .MV, .Set .DL{
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  button{
    padding: 1rem;
  }
</style>