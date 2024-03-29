﻿using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Records;
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
            var salt = PasswordHelper.GenerateSalt();
            var hash = PasswordHelper.Hash(newPassword, salt);

            var user = GetUser(userId) with
            {
                Salt = salt,
                HashedPassword = hash
            };

            _userRepo.Save(user);
        }

        public void SetVolume(
            Guid userId,
            int volume)
        {
            var user = GetUser(userId);
            user.Volume = volume;

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

            //Recreate the user with all info
            user = user with
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName
            };

            //Save the user to the database
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
                    Username = "admin",
                    Volume = 85
                };

                _userRepo.Save(user);
            }
        }
    }
}
