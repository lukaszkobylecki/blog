using Blog.Common.Extensions;
using Blog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public class User : DateTrackingEntityBase
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public string Username { get; private set; }

        protected User() { }

        public User(string email, string password,
            string salt, string username)
            : base()
        {
            SetEmail(email);
            SetPassword(password, salt);
            SetUsername(username);
        }

        public void SetEmail(string email)
        {
            if (email.Empty())
                throw new DomainException(ErrorCodes.InvalidEmail, "Email can not be empty");
            if (email == Email)
                return;

            Email = email;
            UpdateModificationDate();
        }

        public void SetPassword(string password, string salt)
        {
            if (password.Empty())
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not be empty.");
            if (salt.Empty())
                throw new DomainException(ErrorCodes.InvalidPassword, "Salt can not be empty.");
            if (Password == password)
                return;

            Password = password;
            Salt = salt;
            UpdateModificationDate();
        }

        public void SetUsername(string username)
        {
            if (username.Empty())
                throw new DomainException(ErrorCodes.InvalidUsername, "Username can not be empty.");
            if (Username == username)
                return;

            Username = username;
            UpdateModificationDate();
        }
    }
}
