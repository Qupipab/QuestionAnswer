import HttpService from '../httpService.js'

export default class Poll{
  
  constructor(title, answers, params = {}){
    this.request = new HttpService();
    this.title = title;
    this.answers = answers;

    let{ 
      votesCount = 1, 
      canAddAnswers = false,
      isPrivate = false,
      isDraft = false,
      isActive = true,
      isAnon = true,
      link = '',
      closeDate = '',
      draftedAnswers = []
    } = params;

    this.votesCount = votesCount;
    this.canAddAnswers = canAddAnswers;
    this.isPrivate = isPrivate;
    this.isDraft = isDraft;
    this.isActive = isActive;
    this.isAnon = isAnon;
    this.link = link;
    this.closeDate = closeDate;
    this.draftedAnswers = draftedAnswers;
  }

  SendToServer(mode){

    const poll = {
      "title": this.title,
      "votesCount": parseInt(this.votesCount),
      "canAddAnswers": this.canAddAnswers,
      "isPrivate": this.isPrivate,
      "isDraft": this.isDraft,
      "isActive": this.isActive,
      "isAnon": this.isAnon,
      "link": this.link,
      "closeDate": this.closeDate,
      "answers": []
    }

    console.log(this.draftedAnswers);

    for(let i = 0; i < this.answers.length; i++) poll.answers.push( { "title": this.answers[i] } );
    
    if(mode == 'INSERT') this.request.ApplyToServer('NewPoll/AddPoll', { body: poll, method: 'POST', type: 'bool' });
    if(mode == 'UPDATE') this.request.ApplyToServer('NewPoll/UpdatePoll', { body: poll, method: 'POST', type: 'bool' });
  }

}