namespace Domain_Layer.Exceptions
{
    public sealed class BasketNotFoundException(string Key) : NotFoundException($"Basket With Key {Key} Is Not Found")
    {
    }
}
