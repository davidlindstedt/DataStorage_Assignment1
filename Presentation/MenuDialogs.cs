using System;
using System.Threading.Tasks;
using Business.Models;
using Business.Services;
using Data.Entities;

namespace Presentation
{
    internal class MenuDialogs
    {
        private readonly CustomerService _customerService;
        private readonly ProjectService _projectService;

        public MenuDialogs(CustomerService customerService, ProjectService projectService)
        {
            _customerService = customerService;
            _projectService = projectService;
        }

        public async Task ShowMenuAsync()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n----- Huvudmeny -----");
                Console.WriteLine("1. Skapa en Customer");
                Console.WriteLine("2. Skapa nytt projekt");
                Console.WriteLine("3. Visa alla Customers");
                Console.WriteLine("4. Visa alla projekt");
                Console.WriteLine("5. Hämta en Customer");
                Console.WriteLine("6. Hämta ett projekt");
                Console.WriteLine("7. Uppdatera en Customer");
                Console.WriteLine("8. Uppdatera ett projekt");
                Console.WriteLine("0. Avsluta");
                Console.Write("Välj ett alternativ: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateCustomerAsync();
                        break;
                    case "2":
                        await CreateProjectAsync();
                        break;
                    case "3":
                        await GetAllCustomersAsync();
                        break;
                    case "4":
                        await GetAllProjectsAsync();
                        break;
                    case "5":
                        await GetCustomerAsync();
                        break;
                    case "6":
                        await GetProjectAsync();
                        break;
                    case "7":
                        await UpdateCustomerDialogAsync();
                        break;
                    case "8":
                        await UpdateProjectDialogAsync();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        private async Task CreateCustomerAsync()
        {
            Console.Write("Ange kundens namn: ");
            string customerName = Console.ReadLine() ?? "";
            var form = new CustomerRegistrationForm { CustomerName = customerName };

            try
            {
                await _customerService.CreateCustomerAsync(form);
                Console.WriteLine("Customer skapad!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid skapande av Customer: " + ex.Message);
            }
        }

        private async Task CreateProjectAsync()
        {
            Console.Write("Ange projektets namn: ");
            string projectName = Console.ReadLine() ?? "";

            var project = new ProjectEntity
            {
                Title = projectName,
                Description = null,                           // Ingen beskrivning anges
                Customer = null!,                             // EF Core sätter navigeringsegenskaperna vid behov
                Status = null!,
                User = null!,
                Product = null!
            };

            try
            {
                await _projectService.CreateProjectAsync(project);
                Console.WriteLine("Projekt skapat!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid skapande av projekt: " + ex.Message);
            }
        }

        private async Task GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerService.GetCustomersAsync();
                Console.WriteLine("------ Alla Customers ------");
                foreach (var customer in customers)
                {
                    if (customer != null)
                        Console.WriteLine($"Id: {customer.Id}, Namn: {customer.CustomerName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid hämtning av Customers: " + ex.Message);
            }
        }

        private async Task GetAllProjectsAsync()
        {
            try
            {
                var projects = await _projectService.GetAllOrdersAsync();
                Console.WriteLine("------ Alla Projekt ------");
                foreach (var project in projects)
                {
                    Console.WriteLine($"Id: {project.Id}, Titel: {project.Title}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid hämtning av Projekt: " + ex.Message);
            }
        }

        private async Task GetCustomerAsync()
        {
            Console.Write("Ange Customer Id: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var customer = await _customerService.GetCustomerAsync(id);
                    if (customer != null)
                        Console.WriteLine($"Id: {customer.Id}, Namn: {customer.CustomerName}");
                    else
                        Console.WriteLine("Customer hittades inte.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fel vid hämtning av Customer: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt Id.");
            }
        }

        private async Task GetProjectAsync()
        {
            Console.Write("Ange Projekt Id: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var project = await _projectService.GetByIdAsync(id);
                    if (project != null)
                        Console.WriteLine($"Id: {project.Id}, Titel: {project.Title}");
                    else
                        Console.WriteLine("Projekt hittades inte.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fel vid hämtning av Projekt: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt Id.");
            }
        }

        private async Task UpdateCustomerDialogAsync()
        {
            Console.Write("Ange Customer Id att uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt Id.");
                return;
            }
            var existingCustomer = await _customerService.GetCustomerAsync(id);
            if (existingCustomer == null)
            {
                Console.WriteLine("Customer hittades inte.");
                return;
            }
            Console.WriteLine($"Nuvarande namn: {existingCustomer.CustomerName}");
            Console.Write("Ange nytt namn: ");
            string newName = Console.ReadLine() ?? "";
            var updatedCustomer = new Customer
            {
                Id = existingCustomer.Id,
                CustomerName = newName
            };
            var result = await _customerService.UpdateCustomerAsync(updatedCustomer);
            Console.WriteLine(result ? "Customer uppdaterad." : "Misslyckades att uppdatera Customer.");
        }

        private async Task UpdateProjectDialogAsync()
        {
            Console.Write("Ange Projekt Id att uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt Id.");
                return;
            }
            var existingProject = await _projectService.GetByIdAsync(id);
            if (existingProject == null)
            {
                Console.WriteLine("Projekt hittades inte.");
                return;
            }
            Console.WriteLine($"Nuvarande titel: {existingProject.Title}");
            Console.Write("Ange ny titel: ");
            string newTitle = Console.ReadLine() ?? "";
            existingProject.Title = newTitle;

            var result = await _projectService.UpdateProjectAsync(existingProject);
            Console.WriteLine(result ? "Projekt uppdaterat." : "Misslyckades att uppdatera Projekt.");
        }
    }
}
