namespace SellTicketsModel.entity
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}
