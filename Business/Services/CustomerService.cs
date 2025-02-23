using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class CustomerService
{
    private readonly CustomerRepository _customsRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customsRepository = customerRepository;
    }

    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var existingCustomerEntity = await _customsRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        if (existingCustomerEntity != null)
        {
            throw new InvalidOperationException("En kund med detta namn finns redan.");
        }

        var customerEntity = CustomerFactory.Create(form);
        await _customsRepository.AddAsync(customerEntity!);
    }

    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customsRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var customerEntity = await _customsRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customsRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        if (customer == null || customer.Id <= 0)
            return false;

        var existingEntity = await _customsRepository.GetAsync(x => x.Id == customer.Id);
        if (existingEntity == null)
            return false;

        var duplicate = await _customsRepository.GetAsync(x => x.CustomerName == customer.CustomerName && x.Id != customer.Id);
        if (duplicate != null)
            throw new InvalidOperationException("En kund med detta namn finns redan.");

        CustomerFactory.UpdateEntity(existingEntity, customer);

        try
        {
            await _customsRepository.UpdateAsync(existingEntity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var existingEntity = await _customsRepository.GetAsync(x => x.Id == id);
        if (existingEntity == null)
            return false;

        try
        {
            await _customsRepository.RemoveAsync(existingEntity);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
