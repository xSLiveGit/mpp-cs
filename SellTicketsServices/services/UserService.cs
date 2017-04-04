using System;
using persistence.repository;
using SellTicketsModel.entity;
using SellTicketsModel.exception;

namespace SellTicketsServices.services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService()
        {
        }

        public UserService(UserRepository repository)
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

        public User LogIn(String username, String password)
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
