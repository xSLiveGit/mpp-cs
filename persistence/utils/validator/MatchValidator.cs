using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace persistence.utils.validator
{
    public class MatchValidator : IValidator<Match>
    {
        private List<String> errList;

        public MatchValidator()
        {
            errList = new List<string>();
        }


        public void ValidateTeam(String team)
        {
            if (team.Equals(""))
                errList.Add("Team can't be empty.");
            if (team.Length > 30)
                errList.Add("The length of team must be <= 30");
        }

        public void ValidateStage(String stage)
        {
            if (stage.Equals(""))
                errList.Add("Stage can't be empty string.");
            if (stage.Length > 30)
                errList.Add("The length of stage must be <= 30");
            if (!stage.Equals("Final") && !stage.StartsWith("Semifinal ") && !stage.StartsWith("Group "))
            {
                errList.Add("Stage is invalid. Stage must be \"Final\", \"Semifinal x\" or \"Group x\" where x is a big letter.");
            }
        }

        public void ValidatePrice(Double price)
        {
            if (price <= 0)
                errList.Add("The price must be positive integer.");
        }

        public void ValidateTicketsNumber(Int32 ticketsNumber)
        {
            if (ticketsNumber < 0)
                errList.Add("Number of tickets must be non-negative integer.");
        }

        /// <summary>
        /// Throw repository exception if the item is not valid
        /// </summary>
        /// <param name="item"></param>
        public void Validate(Match item)
        {
            errList.Clear();
            ValidateTeam(item.Team1);
            ValidateTeam(item.Team2);
            ValidateStage(item.Stage);
            ValidatePrice(item.Price);
            ValidateTicketsNumber(item.Tickets);
            if (errList.Count > 0)
                throw new RepositoryException(String.Join("\n", errList));
        }
    }
}
