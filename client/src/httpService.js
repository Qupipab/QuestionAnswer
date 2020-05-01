export default class httpService{

  async ApplyToServer(path, params = {}){
    let { method = 'GET', body, type } = params;
    let url = 'https://localhost:5001/api/' + path;
    const headers = { 'content-type': 'application/json' }

    body = body ? JSON.stringify(body) : body;
    
    return await fetch(url, {
      method: method,
      body: body,
      headers: headers,
      credentials: 'include'
    }).then(r => {
      switch(type){
        case 'text': return r.text();
        case 'bool': return r.text().then(r => (r == 'true'));
          break;
        case 'cons': r.json().then(r => console.log(r));
          break;
        default: return r.json().catch(e => {});
      }
    });
  }
}