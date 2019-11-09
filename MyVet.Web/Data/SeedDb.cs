﻿using MyVet.Web.Data.Entities;
using MyVet.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyVet.Web.Data
{
  public class SeedDb
  {
    private readonly DataContext _dataContext;
    private readonly IUserHelper _userHelper;

    public SeedDb(
        DataContext context,
        IUserHelper userHelper)
    {
      _dataContext = context;
      _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
      await _dataContext.Database.EnsureCreatedAsync();
      await CheckRoles();
      var manager = await CheckUserAsync("1010", "Juan", "Zuluaga", "jzuluaga55@gmail.com", "350 634 2747", "Calle Luna Calle Sol", "Admin");
      var customer = await CheckUserAsync("2020", "Juan", "Zuluaga", "jzuluaga55@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", "Customer");
      await CheckPetTypesAsync();
      await CheckServiceTypesAsync();
      await CheckOwnerAsync(customer);
      await CheckManagerAsync(manager);
      await CheckPetsAsync();
      await CheckAgendasAsync();
    }

    private async Task CheckRoles()
    {
      await _userHelper.CheckRoleAsync("Admin");
      await _userHelper.CheckRoleAsync("Customer");
    }

    private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string role)
    {
      var user = await _userHelper.GetUserByEmailAsync(email);
      if (user == null)
      {
        user = new User
        {
          FirstName = firstName,
          LastName = lastName,
          Email = email,
          UserName = email,
          PhoneNumber = phone,
          Address = address,
          Document = document
        };

        await _userHelper.AddUserAsync(user, "123456");
        await _userHelper.AddUserToRoleAsync(user, role);
      }

      return user;
    }

    private async Task CheckPetsAsync()
    {
      if (!_dataContext.Pets.Any())
      {
        var owner = _dataContext.Owners.FirstOrDefault();
        var petType = _dataContext.PepTypes.FirstOrDefault();
        AddPet("Otto", owner, petType, "Shih tzu");
        AddPet("Killer", owner, petType, "Dobermann");
        await _dataContext.SaveChangesAsync();
      }
    }

    private async Task CheckServiceTypesAsync()
    {
      if (!_dataContext.ServiceTypes.Any())
      {
        _dataContext.ServiceTypes.Add(new ServiceType { Name = "Consulta" });
        _dataContext.ServiceTypes.Add(new ServiceType { Name = "Urgencia" });
        _dataContext.ServiceTypes.Add(new ServiceType { Name = "Vacunación" });
        await _dataContext.SaveChangesAsync();
      }
    }

    private async Task CheckPetTypesAsync()
    {
      if (!_dataContext.PepTypes.Any())
      {
        _dataContext.PepTypes.Add(new PepType { Name = "Perro" });
        _dataContext.PepTypes.Add(new PepType { Name = "Gato" });
        await _dataContext.SaveChangesAsync();
      }
    }

    private async Task CheckOwnerAsync(User user)
    {
      if (!_dataContext.Owners.Any())
      {
        _dataContext.Owners.Add(new Owner { User = user });
        await _dataContext.SaveChangesAsync();
      }
    }

    private async Task CheckManagerAsync(User user)
    {
      if (!_dataContext.Managers.Any())
      {
        _dataContext.Managers.Add(new Manager { User = user });
        await _dataContext.SaveChangesAsync();
      }
    }

    private void AddPet(string name, Owner owner, PepType petType, string race)
    {
      _dataContext.Pets.Add(new Pet
      {
        Born = DateTime.Now.AddYears(-2),
        Name = name,
        Owner = owner,
        PepType = petType,
        Race = race
      });
    }

    private async Task CheckAgendasAsync()
    {
      if (!_dataContext.Agendas.Any())
      {
        var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
        var finalDate = initialDate.AddYears(1);
        while (initialDate < finalDate)
        {
          if (initialDate.DayOfWeek != DayOfWeek.Sunday)
          {
            var finalDate2 = initialDate.AddHours(10);
            while (initialDate < finalDate2)
            {
              _dataContext.Agendas.Add(new Agenda
              {
                Date = initialDate,
                IsAvailable = true
              });

              initialDate = initialDate.AddMinutes(30);
            }

            initialDate = initialDate.AddHours(14);
          }
          else
          {
            initialDate = initialDate.AddDays(1);
          }
        }
      }

      await _dataContext.SaveChangesAsync();
    }
  }
}

//{
//  public class SeedDb
//  {
//    private readonly DataContext _context;

//    public SeedDb(DataContext context)
//    {
//      _context = context;
//    }

//    public async Task SeedAsync()
//    {
//      await _context.Database.EnsureCreatedAsync();
//      await CheckPepTypesAsync();
//      await CheckServiceTypesAsync();
//      await CheckOwnersAsync();
//      await CheckPetsAsync();
//      await CheckAgendasAsync();

//    }

//    private async Task CheckPepTypesAsync()
//    {
//      if (!_context.PepTypes.Any())
//      {
//        _context.PepTypes.Add(new Entities.PepType { Name = "Dog"});
//        _context.PepTypes.Add(new Entities.PepType { Name = "Cat" });
//        _context.PepTypes.Add(new Entities.PepType { Name = "Turtle" });
//        await _context.SaveChangesAsync();
//      }
//    }

//    private async Task CheckPetsAsync()
//    {
//      var owner = _context.Owners.FirstOrDefault();
//      var peptype = _context.PepTypes.FirstOrDefault();

//      if (!_context.Pets.Any())
//      {
//        AddPet("Otto", owner, peptype, "Shih tzu");
//        AddPet("Killer", owner, peptype, "Dobermann");
//        await _context.SaveChangesAsync();
//      }
//    }

//    private void AddPet(string name, Owner owner, PepType petType, string race)
//    {
//      _context.Pets.Add(new Pet
//      {
//        Born = DateTime.Now.AddYears(-2),
//        Name = name,
//        Owner = owner,
//        PepType = petType,
//        Race = race
//      });
//    }

//    private async Task CheckOwnersAsync()
//    {
//      if (!_context.Owners.Any())
//      {
//        AddOwner("8989898", "Juan", "Zuluaga", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
//        AddOwner("7655544", "Jose", "Cardona", "343 3226", "300 322 3221", "Calle 77 #22 21");
//        AddOwner("6565555", "Maria", "López", "450 4332", "350 322 3221", "Carrera 56 #22 21");
//        await _context.SaveChangesAsync();
//      }
//    }
//    private void AddOwner(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
//    {
//      _context.Owners.Add(new Owner
//      {
//        Address = address,
//        CellPhone = cellPhone,
//        Document = document,
//        FirstName = firstName,
//        FixedPhone = fixedPhone,
//        LastName = lastName
//      });
//    }

//    private async Task CheckServiceTypesAsync()
//    {
//      if (!_context.ServiceTypes.Any())
//      {
//        _context.ServiceTypes.Add(new ServiceType { Name = "Consulta" });
//        _context.ServiceTypes.Add(new ServiceType { Name = "Urgencia" });
//        _context.ServiceTypes.Add(new ServiceType { Name = "Vacunación" });
//        await _context.SaveChangesAsync();
//      }
//    }

//    private async Task CheckAgendasAsync()
//    {
//      if (!_context.Agendas.Any())
//      {
//        var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
//        var finalDate = initialDate.AddYears(1);
//        while (initialDate < finalDate)
//        {
//          if (initialDate.DayOfWeek != DayOfWeek.Sunday)
//          {
//            var finalDate2 = initialDate.AddHours(10);
//            while (initialDate < finalDate2)
//            {
//              _context.Agendas.Add(new Agenda
//              {
//                Date = initialDate.ToUniversalTime(),
//                IsAvailable = true
//              });

//              initialDate = initialDate.AddMinutes(30);
//            }

//            initialDate = initialDate.AddHours(14);
//          }
//          else
//          {
//            initialDate = initialDate.AddDays(1);
//          }
//        }

//        await _context.SaveChangesAsync();
//      }
//    }
//  }
//}

