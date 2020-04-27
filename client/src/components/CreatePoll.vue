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
        <input type = "text" v-model="Votes[0]" maxlength="50"/>
        <input type = "text" v-model="Votes[1]" maxlength="50"/>
        <input type = "text" v-model="Votes[2]" maxlength="50"/>
        <input type = "text" v-model="Votes[3]" maxlength="50"/>
        <input type = "text" v-model="Votes[4]" maxlength="50"/>
        <input type = "text" v-model="Votes[5]" maxlength="50"/>
        <input type = "text" v-model="Votes[6]" maxlength="50"/>
        <input type = "text" v-model="Votes[7]" maxlength="50"/>
        <input type = "text" v-model="Votes[8]" maxlength="50"/>
        <input type = "text" v-model="Votes[9]" maxlength="50"/>
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
      UserID: 1,
      Author: "Default",
      Votes: [],
      anon: false,
      UserAnswer: false,
      MaxVotes: 1,
      now: myFunc.DateNowYMD()
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
      let temp = [];

      for(let i = 0; i < this.Votes.length; i++)
        if(this.Votes[i] !== undefined) temp.push(this.Votes[i]);
        
      const poll = {
        UserID: parseInt(this.UserID),
        Title: this.qestTitle,
        CreateDate: this.now,
        CloseDate: this.now,
        IsPrivate: this.anon,
        IsActive: false,
        VotesCount: parseInt(this.MaxVotes),
        CanAddAnswers: this.UserAnswer,
        Link: 'link'
      }

      request.ApplyToServer('NewPoll/GetLastPoll').then(r =>{
        let arr = [];
        for(let i = 0; i < temp.length; i++)
        {
          const ans = {
            Title: temp[i],
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
    }
  },
  mounted(){
    request.ApplyToServer('NewPoll/GetPollAuthor').then(r => {
      this.Author = r[0].Login;
      this.UserID = r[0].ID;
    });

  }
}
</script>

<style scoped>
  .Poll{
    font-size: 2rem;
  }

  .Title, .Poll, .Author, .Votes, .Set, .Anon, .UserAnswer, .MV, .DL{
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
    flex-direction: column;
    width: 100%;
  }

  .Poll .Votes{
    padding: 0.25rem 1rem 0.25rem 1rem;
  }

  .Poll .Votes input[type = text]{
    height: 4rem;
    font-size: 2rem;
    padding: 1rem;
    margin: .5rem 0;
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