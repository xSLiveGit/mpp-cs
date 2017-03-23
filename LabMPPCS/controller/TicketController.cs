using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;
using LabMPPCS.repository;

namespace LabMPPCS.controller
{
    class TicketController
    {
        private readonly TicketRepository _ticketRepository;
        private readonly MatchRepository _matchRepository;
        public TicketController(TicketRepository ticketRepository,MatchRepository matchRepository)
        {
            _ticketRepository = ticketRepository;
            _matchRepository = matchRepository;
        }

        public void Add(String quantityS,String idMatchS)
        {
            Int32 idMatch = 0;
            Int32 quantity = 0;
            try
            {
                idMatch = Int32.Parse(idMatchS);
                quantity = Int32.Parse(quantityS);
            }
            catch (Exception e)
            {
                CodeThrowExceptionStatement("Invalid arguments");
            }

            try
            {
                var match = this._matchRepository.FindById(idMatch);
                if (quantity > match.Tickets)
                {
                    CodeThrowExceptionStatement("Insuficien ttickets");
                }
                match.Tickets -= quantity;
                this._matchRepository.Update(match);
                this._ticketRepository.AddId(new Ticket(quantity, match.Id));
            }
            catch (Exception e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        protected void CodeThrowExceptionStatement(Exception e)
        {
            throw new ControllerException(e);
        }

        protected void CodeThrowExceptionStatement(String e)
        {
            throw new ControllerException(e);
        }
    }
}
