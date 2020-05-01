using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer
{
    public static class Review
    {

        public static void NullReview<T>(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

    }
}
