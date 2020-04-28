<template>
  <div id="Poll" class = "Poll">
    <div class="UpWrap">
      <div class="Title">
        <h5>Title: {{ poll.Title }}</h5>
        <h5>Max Votes: {{ poll.VotesCount }}</h5>
      </div>
      <div class="Author">
        <h5>Author: {{ poll.Author }}</h5>
      </div>
      <div class = "Votes">
        <div class="Vote" v-for = "(item, index) in poll.Answers" :key = "index">
          <div class="VoteTitle">
            <label class = "VoteCB" >
              <input maxlength="50"
                type = "checkbox" 
                v-model="voteArr[index]"
                true-value = true
                false-value = false
              >
              <span>{{ item.Title }}</span>
            </label>
            <div class = "PercentWrap"><div class="Percent" :style = "{ width: 0.8 + GetPercent( item.VoteCount, poll.GeneralVotesCount ) + '%' }"></div></div>
          </div>
        </div>
      </div>
    </div>
    <div class="DownWrap">
      <div class="DeathLineContainer">
        <span>PollDeath:</span><div id="DeathLine"></div>
      </div>
      <div class="Set">
        <button v-on:click = "WriteVotes">Vote</button>
      </div>
    </div>
  </div>
</template>

<script>
import httpService from '../httpService.js'
import myFuncs from '../myFuncs'

let myFunc = new myFuncs();
let request = new httpService();

export default {
  name: 'App',
  props: [
    'id'
  ],
  data(){
    return {
      poll: [],
      votes: [],
      i: 0,
      checkedNames: [],
      voteArr: []
    }
  },
  methods: {
    GetPercent( vote, genVote ){
      if(genVote >= 0) return Math.round( vote / genVote * 100 );
      else return;
    },
    WriteVotes(){
      let obj = {
        "PollID" : this.poll.PollID,
        "UserVotes": []
      }
      for(let i = 0; i < this.poll.Answers.length; i++){
        if((/true/i).test(this.voteArr[i])) {
          obj.UserVotes.push({"AnswerID": this.poll.Answers[i].AnswerID});
        }
      }

      let len = parseInt(this.poll.VotesCount);

      if(obj.UserVotes.length > len){
        alert("Too many votes");
        return;
      }

      request.ApplyToServer('Poll/Vote', { method: 'POST', body: obj, type: 'text' }).then(r => {
        if(r == 1) alert("Voted");
        else alert("You have already voted");
      });

      // request.ApplyToServer('Poll/GetPoll/' + this.id).then(r => 
      // {
      //   this.poll = r[0];
      // });

      this.voteArr = [];
    }
  },
  mounted(){
    request.ApplyToServer('Poll/GetPoll/' + this.id).then(r => 
    {
      this.poll = r[0];
      myFunc.DeathLine(r[0].CloseDate);
    });
  }
}
</script>

<style scoped>
  .Poll{
    font-size: 2rem;
  }

  .Title, .Poll, .Author, .Votes, .Vote, .Set, .Anon,
  .UserAnswer, .MV, .DL, .VoteTitle, .DeathLineContainer{
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
  }

  .Vote{
    width: 100%;
    justify-content: space-between;
  }

  .Poll .Votes .VoteTitle{
    flex-direction: column;
    width: 100%;
  }

  .Poll .Votes .VoteTitle input{
    margin-right: 1rem;
  }

  .Poll .Votes .VoteTitle .VoteCB{
    cursor: pointer;
    user-select: none;
    width: 100%;
  }

  .Poll .Votes .VoteTitle .VoteCB:hover{
    background: #E5E7E9;
  }

  .Poll .Votes .VoteTitle .PercentWrap{
    justify-content: start;
    padding: 0 2rem;
  }

  .Poll .Votes .VoteTitle .Percent{
    border-radius: 1rem;
    height: 0.4rem;
    background-color: #3498DB;
    width: 0.8%;
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
  }

  button{ all: unset; }

  .Set button{
    width: 100%;
    border-radius: 0 0 1rem 1rem;
    background-color: #2EBC4F;
    text-align: center;
    color: white;
    cursor: pointer;
  }

  .Set button:hover{background:#28A745;}

  .Set .Anon, .Set .UserAnswer, .Set .MV, .Set .DL{
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  button{
    padding: 1rem;
  }

  .DeathLineContainer{
    margin-left: 1rem;
  }

  #DeathLine{
    margin-left: .5rem;
    user-select: none;
  }
</style>