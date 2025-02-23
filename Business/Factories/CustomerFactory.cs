using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm form)
        => form == null ? null : new CustomerEntity
        {
            CustomerName = form.CustomerName,
        };

    public static Customer? Create(CustomerEntity entity)
        => entity == null ? null : new Customer
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
        };

    public static void UpdateEntity(CustomerEntity entity, Customer customer)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));


        entity.CustomerName = customer.CustomerName;
    }
}
