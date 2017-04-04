using System;
using System.Collections.Generic;
using SellTicketsModel.entity;
using SellTicketsModel.exception;

namespace persistence.utils.validator
{
    public class UserValidator : IValidator<User>
    {
        private List<String> errList;

        public UserValidator()
        {
            errList = new List<string>();
        }

        private void ValidateEntity(String entityValue, String entity)
        {
            if (entityValue.Length < 6 || entityValue.Length > 20)
            {
                errList.Add($"The length of {entity} must be part of [6,20]");
            }
        }


        public void Validate(User item)
        {
            errList.Clear();
            ValidateEntity(item.Username, "username");
            ValidateEntity(item.Password, "password");
            if (errList.Count > 0)
                throw new RepositoryException(String.Join("\n", errList));
        }
    }
}
