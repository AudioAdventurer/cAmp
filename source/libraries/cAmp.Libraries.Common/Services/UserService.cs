using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Repos;

namespace cAmp.Libraries.Common.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        private readonly IcAmpLogger _logger;

        public UserService(
            UserRepo userRepo,
            IcAmpLogger logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }

        public List<User> GetUsers()
        {
            return _userRepo.GetAll();
        }

        public User GetUser(Guid userId)
        {
            return _userRepo.GetById(userId);
        }

        public User GetUserByUsername(string username)
        {
            return _userRepo.GetByUsername(username);
        }

        public bool ValidatePassword(
            Guid userId,
            string plainPassword)
        {
            var user = GetUser(userId);

            var hash = PasswordHelper.Hash(plainPassword, user.Salt);

            if (hash.Equals(user.HashedPassword))
            {
                return true;
            }

            return false;
        }

        public void SetPassword(
            Guid userId,
            string newPassword)
        {
            var user = GetUser(userId);

            user.Salt = PasswordHelper.GenerateSalt();
            user.HashedPassword = PasswordHelper.Hash(newPassword, user.Salt);

            _userRepo.Save(user);
        }

        public void SaveUser(
            Guid userId,
            string firstName,
            string lastName,
            string userName)
        {
            var user = GetUser(userId);

            if (user == null)
            {
                user = new User {Id = userId};
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Username = userName;

            _userRepo.Save(user);
        }

        public void EnsureUserExists()
        {
            var users = GetUsers();
            if (users.Count == 0)
            {
                User user = new User
                {
                    FirstName = "Admin", 
                    LastName = "User", 
                    Username = "admin"
                };

                _userRepo.Save(user);
            }
        }
    }
}
