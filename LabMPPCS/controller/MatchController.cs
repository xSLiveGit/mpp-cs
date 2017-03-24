﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;
using LabMPPCS.repository;

namespace LabMPPCS.controller
{
    public class MatchController
    {
        private readonly MatchRepository _repository;

        public MatchController(MatchRepository repository)
        {
            this._repository = repository;
        }

        //
        public void Add(String team1, String team2, String stage, Int32 tickets, Double price)
        {
            try
            {
                Int32 id = this._repository.AddId(new Match(team1, team2, stage, tickets, price));
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
                Match m = new Match(id, team1, team2, stage, tickets, price);
                this._repository.Update(m);
            }
            catch (Exception e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        public IList<Match> GetAll()
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
            return    ( 
                        from s in _repository.GetAll()
                        where s.Tickets > 0
                        orderby s.Tickets descending 
                        select s
                       ).ToList();
        }
    }
}
