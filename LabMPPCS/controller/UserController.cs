using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;
using LabMPPCS.repository;
using LabMPPCS.utils;

namespace LabMPPCS.controller
{
    public class UserController
    {
        private readonly UserRepository _repository;

        public UserController()
        {
        }

        public UserController(UserRepository repository)
        {
            this._repository = repository;
        }

        public void Add(String username, String password)
        {
            try
            {
                password = Crypto.Encode(password);
                _repository.Add(new User(username, password));
            }
            catch (RepositoryException e)
            {
                CodeThrowControllerExceptionStatement(e);
            }
        }

        public User LogIn(String username,String password)
        {

            try
            {
                User u = _repository.FindById(username);
                if (Crypto.Encode(password).Equals(u.Password))
                    return u;
                CodeThrowControllerExceptionStatement("Invalid username or password");
            }
            catch (RepositoryException)
            {
                CodeThrowControllerExceptionStatement("Invalid username or password");
            }
            return null;
        }

        private void CodeThrowControllerExceptionStatement(Exception e)
        {
            throw new ControllerException(e);
        }

        private void CodeThrowControllerExceptionStatement(String e)
        {
            throw new ControllerException(e);
        }
    }
}
