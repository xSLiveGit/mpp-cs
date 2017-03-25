using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;

namespace LabMPPCS.Validator
{
    public class UserValidator : IValidator<User>
    {
        private List<String> errList;

        public UserValidator()
        {
            errList = new List<string>();
        }

        private void ValidateEntity(String entityValue,String entity)
        {
            if (entityValue.Length < 6 || entityValue.Length > 20)
            {
                errList.Add($"The length of {entity} must be part of [6,20]");
            }
        }

        
        public void Validate(User item)
        {
            errList.Clear();
            ValidateEntity(item.Username,"username");
            ValidateEntity(item.Password,"password");
            if(errList.Count > 0)
                throw new RepositoryException(String.Join("\n",errList));
        }
    }
}
