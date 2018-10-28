using Artists.Subscribers.DataAccess.Models;
using System;
using System.Linq;

namespace Artists.Subscribers.Services.UsersSecurity
{
    public class UserSecurity
    {
        public static bool LogIn(string username, string password)
        {
            using (SubscribersDBEntities entities = new SubscribersDBEntities())
            {             
                    return entities.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                                   && user.Password == password);              
            }
        }
    }
}
