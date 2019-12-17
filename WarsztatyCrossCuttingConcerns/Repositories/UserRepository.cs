using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WarsztatyCrossCuttingConcerns.Repositories
{
    public class UserRepository
    {
        public string GetUserById(int id)
        {

            Thread.Sleep(2000);

            return $"User {id}";
        }
    }
}
