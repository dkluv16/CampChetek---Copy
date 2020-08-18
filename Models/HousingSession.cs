using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CampChetek.Models
{
    public class HousingSession
    {
        
        private const string PasswordKey = "mypassword";
        private const string NameKey = "name";

        private ISession session { get; set; }
        public HousingSession(ISession session)
        {
            this.session = session;
        }

        public void SetName(string UserEmail = "friend")
        {
            session.SetString(NameKey, UserEmail);
        }
        public string GetName() => session.GetString(NameKey);

        public string GetPassword() => session.GetString(PasswordKey);

        public void RemoveMyUser()
        {
            session.Remove(NameKey);
            session.Remove(PasswordKey);
        }
    }
}
