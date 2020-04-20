export default class httpService{

    async GetAllUsers(){
      return await fetch(`https://localhost:5001/api/User/GetUsers`).then(r => r.json());
    }

    async NewUser(data){
      await fetch(`https://localhost:5001/api/User`, {
        body: JSON.stringify(data),
        headers: { 'content-type': 'application/json' },
        method: 'POST'
      })
    }
    
    async GetPubPolls(){
      return await fetch(`https://localhost:5001/api/Poll`).then(r => r.json());
    }

    async GetUserPolls(){
      return await fetch(`https://localhost:5001/api/user/getuserpolls`).then(r => r.json());
    }
}