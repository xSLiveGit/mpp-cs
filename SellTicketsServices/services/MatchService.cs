using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using persistence.repository;
using SellTicketsModel.exception;
using SellTicketsModel.entity;
using Match = SellTicketsModel.entity.Match;


namespace SellTicketsServices.services
{
    public class MatchService
    {
        private readonly MatchRepository _repository;

        public MatchService(MatchRepository repository)
        {
            this._repository = repository;
        }

        //
        public void Add(String team1, String team2, String stage, Int32 tickets, Double price)
        {
            try
            {
                Int32 id = this._repository.AddId(item: new SellTicketsModel.entity.Match(team1, team2, stage, tickets, price));
            }
            catch (RepositoryException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        public void Delete(String idS)
        {
            Int32 id = -1;
            try
            {
                id = Int32.Parse(idS);
            }
            catch (Exception)
            {
                CodeThrowExceptionStatement("Invalid integer number");
            }
            try
            {
                this._repository.Delete(id);
            }
            catch (RepositoryException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        public void Update(String idS, String team1, String team2, String stage, String ticketsS, String priceS)
        {
            Int32 id = 0;
            Int32 tickets = 0;
            Double price = 0;

            try
            {
                id = Int32.Parse(idS);
                tickets = Int32.Parse(ticketsS);
                price = Double.Parse(priceS);
            }
            catch (Exception)
            {
                CodeThrowExceptionStatement("Invalid parameters");
            }

            try
            {
                SellTicketsModel.entity.Match m = new SellTicketsModel.entity.Match(id, team1, team2, stage, tickets, price);
                this._repository.Update(m);
            }
            catch (Exception e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        public List<Match> GetAll()
        {
            try
            {
                return this._repository.GetAll();
            }
            catch (Exception e)
            {
                CodeThrowExceptionStatement(e);
            }
            return null;
        }

        protected void CodeThrowExceptionStatement(Exception e)
        {
            throw new ControllerException(e);
        }

        protected void CodeThrowExceptionStatement(String e)
        {
            throw new ControllerException(e);
        }

        public List<Match> GetAllMatchesWithRemainingTickets()
        {
            return (
                        from s in _repository.GetAll()
                        where s.Tickets > 0
                        orderby s.Tickets descending
                        select s
                       ).ToList();
        }

        public Match FindById(String id)
        {
            Int32 nr = 0;
            Match match = null;

            try
            {
                nr = Int32.Parse(id);
            }
            catch (Exception e)
            {
                throw new ControllerException("Invalid number format.");
            }

            try
            {
                match =  this._repository.FindById(nr);
            }
            catch (RepositoryException e)
            {
                CodeThrowExceptionStatement(e);
            }
            return match;
        }
    }
}
