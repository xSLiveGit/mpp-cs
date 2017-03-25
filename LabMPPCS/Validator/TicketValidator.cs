using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;

namespace LabMPPCS.Validator
{
    public class TicketValidator : IValidator<Ticket>
    {
        private List<String> errList;

        public TicketValidator()
        {
            errList = new List<string>();
        }

        private void ValidateQuantity(Int32 quantity)
        {
            if (quantity <= 0)
            {
                errList.Add("Quantity must be nonnegative number.");                
            }
        }

        private void ValidatePerson(String person)
        {
            if(person.Equals(""))
                errList.Add("The person can't be empty.");
            if(person.Length > 30)
                errList.Add("The length of person must be <= 30.");
        }

        public void Validate(Ticket item)
        {
            errList.Clear();
            ValidateQuantity(item.Quantity);
            ValidatePerson(item.Person);
            if (errList.Count > 0)
                throw new RepositoryException(String.Join("\n", errList));
        }
    }
}
