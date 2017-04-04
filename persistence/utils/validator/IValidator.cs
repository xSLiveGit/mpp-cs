namespace persistence.utils.validator
{
    public interface IValidator<T>
    {
        //throw RepositoryException
        void Validate(T item);
    }
}
