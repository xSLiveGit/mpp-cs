using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellTicketsModel.entity;

namespace SellTicketsNetworking
{
    public interface IResponse
    {
    }

    [Serializable]
    public class OkResponse : IResponse
    {

    }

    [Serializable]
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(string message)
        {
            this.Message = message;
        }

        public virtual string Message { get; }
    }

    [Serializable]
    public class GetAllReponse : IResponse
    {
        public List<MatchDTO> List { get; }

        public GetAllReponse(List<MatchDTO> list)
        {
            List = list;
        }

        public GetAllReponse(List<Match> list)
        {
            List = new List<MatchDTO>(list.Count);
            List<Match> lst = new List<Match>(list);
            lst.ForEach(el => List.Add(new MatchDTO(el)
            ));
        }
    }

    [Serializable]
    public class GetAllFilteredAndSortedResponse : IResponse
    {
        public List<MatchDTO> List { get; }

        public GetAllFilteredAndSortedResponse(List<MatchDTO> list)
        {
            List = list;
        }

        public GetAllFilteredAndSortedResponse(List<Match> list)
        {
            List = new List<MatchDTO>(list.Count);
            list.ForEach(el => List.Add(new MatchDTO(el)
            ));
        }
    }

    public interface IUpdateResponse : IResponse
    {
    }

    [Serializable]
    public class ShowModifiesResponse : IUpdateResponse
    {
        public ShowModifiesResponse(MatchDTO match)
        {
            Match = match;
        }

        public MatchDTO Match { get; }
    }
}

