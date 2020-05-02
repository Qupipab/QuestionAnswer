<template>
  <div id="Poll" class = "Poll" v-if="showPage">
    <div class="UpWrap">
      <div class="ClosePoll">
        <img v-if="isCreator" :title="close" @click="ClosePoll" type="image/svg+xml" src='../icons/close.svg'>
      </div>
      <div class="Title">
        <h5>Title: {{ poll.title }}</h5>
        <h5>Max Votes: {{ poll.votesCount }}</h5>
      </div>
      <div class="Author">
        <h5><a v-if="!isAnon" @click="VotedUsers">Voted Users</a></h5>
        <h5>Author: {{ poll.author }}</h5>
      </div>
      <div class = "Votes">
        <div class="Vote" v-for = "(item, index) in poll.answers" :key = "index">
          <div class="VoteTitle">
            <label class = "VoteCB" >
              <input maxlength="50"
                type = "checkbox" 
                v-model="voteArr[index]"
                true-value = true
                false-value = false
                v-if="isActive"
              >
              <span>{{ item.title }}</span>
            </label>
            <div class = "PercentWrap"><div class="Percent" :style = "{ width: 0.8 + GetPercent( item.voteCount, poll.generalVotesCount ) + '%' }"></div></div>
          </div>
        </div>
      </div>
    </div>
    <div class="OwnVote" v-if="canAddAnswers">
      <input type="text" v-model="ownVote">
      <div class="OwnVoteBut" @click="AddOwnVote"><span>Add Vote</span></div>
    </div>
    <div class="DownWrap">
      <div class="DeathLineContainer">
        <div class="PollTimer">
          <span>PollDeath:</span><div id="DeathLine">InActive</div>
        </div>
        <div class="InActive" @click="InActive" v-if="isCreator">Set InActive</div>
      </div>
      <div class="Set">
        <button @click="WriteVotes">Vote</button>
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
      showPage: false,
      pollID: 0,
      poll: [],
      creatorID: '',
      checkedNames: [],
      voteArr: [],
      close: 'Close Poll',
      isCreator: false,
      isActive: false,
      isAnon: true,
      ownVote: '',
      canAddAnswers: true
    }
  },
  watch:{

  },
  methods: {
    GetPercent( vote, genVote ){
      if(genVote >= 0) return Math.round( vote / genVote * 100 );
      else return;
    },
    AddOwnVote(){
      if(!(this.poll.answers.length >= 10) && this.ownVote != '')
      {
        let ans = {
          "title": this.ownVote,
          "pollID":  this.poll.pollID,
        };
        request.ApplyToServer('Poll/AddAnswer', { method: 'POST', body: ans, type: 'text' }).then(r => ans.creatorID = r);
        this.poll.answers.push(ans);
        this.ownVote = '';
      }
      else return;
    },
    WriteVotes(){

      let obj = {
        "pollID" : this.poll.pollID,
        "userVotes": []
      }
      for(let i = 0; i < this.poll.answers.length; i++){
        if((/true/i).test(this.voteArr[i])) {
          obj.userVotes.push({"answerID": this.poll.answers[i].answerID});
        }
      }

      let len = parseInt(this.poll.votesCount);

      if(obj.userVotes.length > len){
        alert("Too many votes");
        return;
      }

      if(obj.userVotes.length === 0) return;

      request.ApplyToServer('Poll/Vote', { method: 'POST', body: obj, type: 'bool' }).then(r => {
        if(r) alert("Voted");
        else alert("You have already voted");
      });

      this.voteArr = [];
    },
    VotedUsers(){
      this.$router.push({name: 'VotedUsers', params: { id: this.id }});
    },
    ClosePoll(){
      request.ApplyToServer('Poll/ClosePoll/' + this.pollID, { method: 'POST' }).then(() => { 
        this.$router.push('/main');
      });
    },
    InActive(){
      request.ApplyToServer('Poll/InActive/' + this.pollID, { method: 'POST' }).then(() => { 
        this.isActive = false;
      });
    }
  },
  mounted(){ 
    request.ApplyToServer('Auth/GetLoggedId', { type: 'text' }).then(r => {
      if(r !== "0")
      {
        this.creatorID = r;
        this.showPage = true;
        request.ApplyToServer('Poll/GetPoll/' + this.id).then(r => 
        {
          this.pollID = r[0].pollID;
          this.isCreator = this.creatorID == r[0].userID;
          this.poll = r[0];
          this.isActive = r[0].isActive;
          this.isAnon = r[0].isAnon;
          this.canAddAnswers = r[0].canAddAnswers;
          if(new Date(r[0].closeDate) - new Date().getTime() > 0) myFunc.DeathLine(new Date(r[0].closeDate).getTime());
          else this.isActive = false;
        });
      }
      else this.$router.push({name: 'SignIn', params: { redir: this.$route.fullPath }});
    });
  }
}
</script>

<style scoped>
  .Poll{
    font-size: 2rem;
  }

  .test{
    color: red;
  }

  .Title, .Poll, .Author, .Votes, .Vote, .Set, .Anon, .OwnVote,
  .UserAnswer, .MV, .DL, .VoteTitle, .DeathLineContainer, .ClosePoll,
  .OwnVoteBut, .PollTimer{
    display: flex;
  }

  .ClosePoll{
    justify-content: flex-end;
  }

  .ClosePoll img{
    cursor:pointer;
    margin: 0.5rem 1rem 0 0;
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
    justify-content: space-between;
    padding: .5rem 1rem;
  }

  .Poll .Author a{
    cursor: pointer;
  }

  .Poll .Author a:hover{
    text-decoration: underline;
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

  .OwnVote{
    margin: 0.5rem 1rem;
  }

  .OwnVote input{
    width: 100%;
    height: 3rem;
    padding: 0 1rem;
  }

  .OwnVote .OwnVoteBut{
    cursor: pointer;
    font-size: 2rem;
    background-color: #2EBC4F;
    justify-content: center;
    align-items: center;
    user-select: none;
  }

  .OwnVote .OwnVoteBut span{
    color:white;
    padding: 0 1rem;
  }

  .Set{
    margin-top:1rem;
    justify-content: space-around;
    font-size: 1.7rem;
    font-weight: bold;
    user-select: none;
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

  .Set button:hover, .OwnVote .OwnVoteBut:hover{background:#28A745;}

  .Set .Anon, .Set .UserAnswer, .Set .MV, .Set .DL{
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  button{
    padding: 1rem;
  }

  .DeathLineContainer{
    justify-content: space-between;
    margin-left: 1rem;
  }

  .InActive{
    cursor: pointer;
    margin-right: 1rem;
  }

  .InActive:hover{
    text-decoration: underline;
  }

  #DeathLine{
    margin-left: .5rem;
    user-select: none;
  }
</style>