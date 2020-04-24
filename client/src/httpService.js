export default class httpService{

    Path = 'https://localhost:5001/api/';

    async GetAllUsers(){
      return await fetch(`${ this.Path }User/GetUsers`).then(r => r.json());
    }

    async NewUser(data){
      await fetch(`${ this.Path }User`, {
        body: JSON.stringify(data),
        headers: { 'content-type': 'application/json' },
        method: 'POST'
      })
    }
    
    async AddPoll( poll ){
      await fetch(`${ this.Path }Poll/AddPoll`, {
        body: JSON.stringify( poll ),
        headers: { 'content-type': 'application/json' },
        method: 'POST'
      })
    }

    async AddAnswers( answers ){
      await fetch(`${ this.Path }Poll/AddAnswers`, {
        body: JSON.stringify( answers ),
        headers: { 'content-type': 'application/json' },
        method: 'POST'
      })
    }

    async CheckUser( user ){
      return await fetch(`${ this.Path }Auth/CheckUser`, {
        body: JSON.stringify( user ),
        headers: { 'content-type': 'application/json' },
        method: 'POST',
        credentials: 'include'
      }).then(r => r.text());
    }

    async GetPubPolls(){
      return await fetch(`${ this.Path }Poll/GetPolls`, {
        credentials: 'include'
      }).then(r => r.json());
    }

    async GetUserPolls(){
      return await fetch(`${ this.Path }User/GetUserPolls`, {
        credentials: 'include'
      }).then(r => r.json());
    }

    async GetAuthor(){
      return await fetch(`${ this.Path }Poll/GetAuthor`, {
        credentials: 'include'
      }).then(r => r.json());
    }

    async GetMaxPoll(){
      return await fetch(`${ this.Path }Poll/GetMaxPoll`).then(r => r.json());
    }
}