export default class httpService{

    async GetAllUsers(){
      return await fetch(`https://localhost:5001/api/User`).then(r => r.json());
    }

    async NewUser(data){
      await fetch(`https://localhost:5001/api/User`, {
        body: JSON.stringify(data),
        headers: { 'content-type': 'application/json' },
        method: 'POST'
      })
    }

}