<template>
  <div id = "CreatePoll" class = "Poll">
    <div class="UpWrap">
      <div class="Title">
        <h5>Title: {{ title }}</h5>
        <input type="text" v-model="title">
      </div>
      <div class = "Votes">
        <ul>
          <li v-for = "(vote, index) in answers" :key ="index">{{ index + 1 }}: {{ answers[index] }}</li>
        </ul>
      </div>
    </div>
    <div class="DownWrap">
      <div class="OwnVote">
          <input type="text" v-model="vote">
          <div class="OwnVoteBut" @click="AddVote"><span>Add Vote</span></div>
      </div>
      <div class="PollLink" v-if="isPrivate">
        <span>Poll Link: </span>
        <span>Poll/{{ link }}</span>
      </div>
      <div class="Set">
        <div class="Private">
          <span>Private</span>
          <input type = "checkbox" v-model="isPrivate"/>
        </div>
        <div class="Anon">
          <span>Anon</span>
          <input type = "checkbox" v-model="isAnon"/>
        </div>
        <div class="UserAnswer">
          <span>User Answer</span>
          <input type = "checkbox" v-model="canAddAnswers"/>
        </div>
        <div class="MV">
          <span>Max Votes</span>
          <input type="number" name="quantity" min="1" max="10" v-model="votesCount"/>
        </div>
        <div class="DL">
          <span>Death Line</span>
          <input type="date" v-model="closeDate"/>
        </div>
        <div class="ConfButtons">
          <button class="DraftBut" @click="DraftPoll">Draft</button>
          <button class="CreateBut" @click="CreatePoll">Create</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MyFuncs from '../myFuncs.js'
import httpService from '../httpService.js'
import Poll from '../models/Poll.js'

let myFunc = new MyFuncs();
let request = new httpService();

export default {
  name: 'App',
  props: [
    'draftPoll'
  ],
  data(){
    return {
      title: 'Title',
      author: "Default",
      answers: [],
      isPrivate: false,
      isAnon: false,
      canAddAnswers: false,
      votesCount: 0,
      closeDate: '',
      vote: 'Answer1',
      link: ''
    }
  },
  watch:{
    votesCount(val) {
      if(val < 0) this.votesCount = 0;
      else if(val > this.answers.length) this.votesCount = this.answers.length;
    },
    isPrivate(val) {
      if(val) this.link += myFunc.MakeLinkHash(5);
      else this.link = '';
    }
  },
  methods:{
    CreatePoll(){
      if(this.draftPoll) this.SendPoll(false, 'UPDATE');
      else this.SendPoll(false, 'INSERT');
    },
    DraftPoll(){
      if(this.draftPoll) this.SendPoll(true, 'UPDATE');
      else this.SendPoll(true, 'INSERT');
    },
    SendPoll(val, mode){
      if(this.closeDate == '' || this.answers.length < 2 || this.votesCount <= 0)
      {
        alert('Something wrong!');
        return;
      }

      let poll = new Poll(this.title, this.answers, {
        votesCount: this.votesCount,
        canAddAnswers: this.canAddAnswers,
        isPrivate: this.isPrivate,
        isDraft: val,
        isActive: !(val),
        isAnon: this.isAnon,
        link: this.link,
        closeDate: this.closeDate
      });

      poll.SendToServer(mode);
      this.FieldsToDefault();
    },
    AddVote(){
      if(this.vote != '' && !(this.answers.length >= 10)){
        this.answers.push(this.vote);
        this.vote = 'Answer';
      }
    },
    FieldsToDefault()
    {
      this.title = 'Title';
      this.answers = [];
      this.isPrivate = false;
      this.isAnon = false;
      this.canAddAnswers = false;
      this.votesCount = 1;
      this.closeDate = '';
    }
  },
  mounted()
  {
    request.ApplyToServer('Auth/GetLoggedId', { type: 'text' }).then(r => { 
      if(r == "0") this.$router.push({name: 'SignIn', params: { redir: this.$route.fullPath }});
    });
    if(this.draftPoll){
      request.ApplyToServer('Poll/GetPoll/' + this.draftPoll).then(r => 
      {
        console.log(r);
        this.title = r[0].title;
        this.isPrivate = r[0].isPrivate;
        this.isAnon = r[0].isAnon;
        this.canAddAnswers = r[0].canAddAnswers;
        for(let i = 0; i < r[0].answers.length; i++){
          this.answers.push(r[0].answers[i].title);
        }
        this.votesCount = r[0].votesCount;
        this.closeDate = myFunc.ConvertDate(r[0].closeDate);
      });
    }
  }
}
</script>

<style scoped>
  button{ all: unset; }

  .Poll{
    font-size: 2rem;
  }

  .Title, .Poll, .Author, .Votes, .Set, .Anon,
  .UserAnswer, .MV, .DL, .OwnVote, .OwnVoteBut,
  .CreateBut, .DraftBut, .ConfButtons, .Private,
  .PollLink{
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
    width: 70rem;
    min-height: 30rem;
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

 .OwnVote input[type = text]{
    width: 100%;
    height: 3rem;
    font-size: 2rem;
    padding: 1rem;
  }

  .OwnVote{
    margin: 1rem 1rem 0 1rem;
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
    justify-content: space-between;
    font-size: 1.7rem;
    font-weight: bold;
  }

  .PollLink{
    margin:0.5rem 1rem;
  }

  .PollLink:first-child{
    margin-right: 3rem;
  }

  .Set span{
    margin-bottom: .5rem;
  }

  .Set input[type = "number"]{
    width: 4rem;
  }

  .Set .Anon, .Set .UserAnswer, .Set .MV, .Set .DL, .Private{
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  .Anon, .UserAnswer, .MV, .DL, .Private{
    margin: 1rem 0 1rem 1rem;
  }

  .CreateBut, .DraftBut{
    justify-content: center;
    align-items: center;
    cursor: pointer;
    color: white;
    background-color: #2EBC4F;
    padding: 1rem 2rem;
  }

  .CreateBut{
    border-radius: 0 0 1rem 0;
  }

  .CreateBut:hover, .DraftBut:hover{
    background-color: #28A745;
  }

</style>