using Microsoft.AspNetCore.Identity;
using MyVet.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
  public interface IUserHelper
  {
    Task<User> GetUserByEmailAsync(string email);

    Task<IdentityResult> AddUserAsync(User user, string password);

    Task CheckRoleAsync(string rolename);

    Task AddUserToRoleAsync(User user, string rolename);

    Task<bool> IsUserInRoleAsync(User user, string rolename);



  }
}
