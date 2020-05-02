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


  DeathLine(countDownDate){
    let display = document.querySelector('#DeathLine');

    function timer(){

      var now = new Date().getTime();

      var distance = countDownDate - now;

      var days = Math.floor(distance / (1000 * 60 * 60 * 24));
      var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((distance % (1000 * 60)) / 1000);

      display.textContent = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

      if (distance < 0) {
        clearInterval(x);
        display.textContent = "EXPIRED";
      }
      
    }

    timer();
    setInterval(timer, 1000);
  }

  MakeLinkHash(length) {
    var result           = '';
    var characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for ( var i = 0; i < length; i++ ) {
       result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }

  ConvertDate(date) {
    date.split('T').shift();
  }
  
}