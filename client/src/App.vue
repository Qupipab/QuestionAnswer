<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
import Auth from './components/Auth.vue'

export default {
  name: 'App',
  components: {
    Auth
  }
}
</script>

<style>
*
{
  margin: 0;
  padding: 0;
  white-space: nowrap;

	box-sizing: border-box;
	color:#2c3e50;
	text-decoration: none;
	list-style: none;
}

html
{
	font-family: 'Roboto', sans-serif;
	font-size: 10px;
}

/* input { border: none; }
input:focus { outline: none; } */

</style>
