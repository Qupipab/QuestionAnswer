<template>
  <div id="Poll" class = "Poll">
    <div class="UpWrap">
      <div class="Title">
        <h5>Title: {{ poll.Title }}</h5>
        <h5>Max Votes: {{ poll.VotesCount }}</h5>
      </div>
      <div class="Author">
        <h5>Author: {{ poll.Login }}</h5>
      </div>
      <div class = "Votes">
        <div class="Vote" v-for = "(item, answers) in poll.Answers" :key = "answers">
          <div class="VoteTitle">
            <input type = "checkbox" maxlength="50"/><span>{{ item.Title }}</span>
          </div>
          <span>{{ }}</span>
        </div>
      </div>
    </div>
    <div class="DownWrap">
      <div class="Set">
        <button>Vote</button>
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
      poll: [],
      votesCount: []
    }
  },
  watch:{
    
  },
  computed:{
    
  },
  methods:{
    
  },
  mounted(){
    request.ApplyToServer( 'Poll/GetPoll' ).then(r => this.poll = r[0]);
    request.ApplyToServer( 'Answer/GetVotes' ).then(r => this.votesCount = r);
  }
}
</script>

<style scoped>
  .Poll{
    font-size: 2rem;
  }

  .Title, .Poll, .Author, .Votes, .Vote, .Set, .Anon, .UserAnswer, .MV, .DL{
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

  .Poll .Votes .VoteTitle input{
    margin-right: 1rem;
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
</style>