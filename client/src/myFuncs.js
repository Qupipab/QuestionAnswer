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
}