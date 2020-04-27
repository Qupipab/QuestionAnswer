<template>
  <div class="Main">
    <div class="MainPolls">
      <div class="CabinetWrap">
        <h2>{{ Author }} Polls</h2>
        <div class="navButtons">
          <router-link to="/createpoll" class="navBut">New Poll</router-link>
          <router-link to="/main" class="navBut">Main</router-link>
        </div>
      </div>
      <div class="UserPolls">
        <span>Draft</span>
        <div class="Polls">
          <div v-for="(poll, ID) in UserPolls" :key="ID">
            <router-link to = "/" class="Poll" v-if="!poll.IsActive && poll.IsPrivate">{{ poll.Title }}</router-link>
          </div>
        </div>
        <span>Active</span>
        <div class="Polls">
          <div v-for="(poll, ID) in UserPolls" :key="ID">
            <router-link to = "/" class="Poll" v-if="poll.IsActive && !poll.IsPrivate">{{ poll.Title }}</router-link>
          </div>
        </div>
        <span>Inactive</span>
        <div class="Polls">
          <div v-for="(poll, ID) in UserPolls" :key="ID">
            <router-link to = "/" class="Poll" v-if="!poll.IsActive && !poll.IsPrivate">{{ poll.Title }}</router-link>
          </div>
        </div>
        <span>Closed</span>
        <div class="Polls">
          <div v-for="(poll, ID) in UserPolls" :key="ID">
            <router-link to = "/" class="Poll" v-if="poll.IsActive && poll.IsPrivate">{{ poll.Title }}</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import httpService from "../httpService.js";

let request = new httpService();

export default {
  name: "App",
  data() {
    return {
      UserPolls: [],
      Author: "Default"
    };
  },
  mounted() {
    request.ApplyToServer('Cabinet/GetUserPolls').then(r => {
      this.Author = r[0].Login;
      this.UserPolls = r[0].Polls;
    });
  }
};
</script>

<style scoped>
.CabinetWrap,
.MainPolls,
.Poll,
.Polls {
  display: flex;
}

.Main {
  font-size: 2rem;
}

.CabinetWrap {
  justify-content: space-between;
  margin-top: 1rem;
}

.navButtons{
  margin-top: .5rem;
}

.navBut {
  -webkit-box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.75);
  -moz-box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.75);
  box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.75);
  padding: 0.7rem 4rem;
  border-radius: 2rem;
  background: #2ebc4f;
  color: #fff;
  margin: 0rem 1rem;
}

.navBut:hover {
  background: #28a745;
}

.MainPolls {
  margin: 0 auto;

  max-width: 90rem;
  justify-content: center;
  flex-direction: column;
  padding: 0 5rem;
}

.UserPolls {
  margin: 0.5rem 0;
}

.Polls {
  align-content: flex-start;
  flex-wrap: wrap;
  margin: 0.5rem 0;
}

.Poll {
  margin: 0.4rem;
  justify-content: center;
  align-items: center;
  -webkit-box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.75);
  -moz-box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.75);
  box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.75);
  border-radius: 2rem;
  padding: 2rem;
}

.Poll:hover {
  -webkit-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.75);
  -moz-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.75);
  box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.75);
}
</style>