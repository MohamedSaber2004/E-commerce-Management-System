namespace Domain_Layer.Exceptions
{
    public sealed class DeliveryNotFoundeException(int id):NotFoundException($"No Delivery Method Found With Id = {id}")
    {
    }
}
