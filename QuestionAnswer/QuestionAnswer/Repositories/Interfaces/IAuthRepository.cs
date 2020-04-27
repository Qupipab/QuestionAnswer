using QuestionAnswer.Models;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface IAuthRepository
    {

        public int GetUserId(User user);
        public void AddUser(User user);
        public bool CheckUser(User user);

    }
}
