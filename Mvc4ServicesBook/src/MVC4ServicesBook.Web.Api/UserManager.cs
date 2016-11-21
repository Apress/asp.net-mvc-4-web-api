﻿using MVC4ServicesBook.Data;
using MVC4ServicesBook.Web.Api.Models;
using MVC4ServicesBook.Web.Api.TypeMappers;
using MVC4ServicesBook.Web.Common.Security;

namespace MVC4ServicesBook.Web.Api
{
    public class UserManager : IUserManager
    {
        private readonly IMembershipInfoProvider _membershipAdapter;
        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;

        public UserManager(IMembershipInfoProvider membershipAdapter, IUserRepository userRepository, IUserMapper userMapper)
        {
            _membershipAdapter = membershipAdapter;
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public User CreateUser(string username, string password, string firstname, string lastname, string email)
        {
            var wrapper = _membershipAdapter.CreateUser(username, password, email);

            _userRepository.SaveUser(wrapper.UserId, firstname, lastname);

            var user = _userMapper.CreateUser(username, firstname, lastname, email, wrapper.UserId);

            return user;
        }
    }
}