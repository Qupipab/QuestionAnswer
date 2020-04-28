export default class myFuncs{

  DateNowYMD(){
    let dateObj = new Date();
    let month = dateObj.getUTCMonth() + 1;
    if(month < 10) month = "0" + month;
    let day = dateObj.getUTCDate() + 1;
    if(day < 10) day = "0" + day;
    let year = dateObj.getUTCFullYear();
    let newdate = year + "-" + month + "-" + day;
    return newdate;
  }

  DeathLine( date ){

    let now = new Date().getTime();

    var countDownDate = new Date(date).getTime();

    if((countDownDate - now) < 0) 
    {
      document.getElementById("DeathLine").innerHTML = "InActive";
      return;
    }

    var x = setInterval(function() {
      let now = new Date().getTime();
      var distance = countDownDate - now;

      var days = Math.floor(distance / (1000 * 60 * 60 * 24));
      var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((distance % (1000 * 60)) / 1000);

      document.getElementById("DeathLine").innerHTML = days + "d " + hours + "h "
      + minutes + "m " + seconds + "s ";

      if (distance < 0) {
        clearInterval(x);
        document.getElementById("demo").innerHTML = "EXPIRED";
      }  
    }, 200);
  }
}