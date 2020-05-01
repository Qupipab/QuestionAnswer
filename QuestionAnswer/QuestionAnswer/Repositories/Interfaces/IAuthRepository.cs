using QuestionAnswer.Models;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface IAuthRepository
    {

        int GetUserId(User user);
        void AddUser(User user);
        bool CheckUser(User user);

    }
}
