using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace VirusAlertAPI.DB
{
    public class AppUserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>
    {
        DataContext context = new DataContext();

        public Task CreateAsync(AppUser user)
        {
            if (context.AppUsers.Any(x=> String.Equals(x.usrEmail, user.usrEmail, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ApplicationException("Пользователь с такой почтой уже зарегестрирован");
            }
            if (context.AppUsers.Any(x => String.Equals(x.usrOms, user.usrOms, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ApplicationException("Пользователь с таким ОМС уже зарегестрирован");
            }
            context.AppUsers.Add(user);
            return context.SaveChangesAsync();
        }

        public Task DeleteAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByNameAsync(string userName)
        {
            Task<AppUser> task = context.AppUsers.Where(
                                  apu => apu.usrEmail == userName)
                                  .FirstOrDefaultAsync();

            return task;
        }

        public Task UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Task<string> GetPasswordHashAsync(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            string t;
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(user.usrPassword);
                byte[] hash = sha.ComputeHash(textData);
                t = BitConverter.ToString(hash).Replace("-", String.Empty);
            }
            return Task.FromResult(t);
        }

        public Task<bool> HasPasswordAsync(AppUser user)
        {
            
            return Task.FromResult(user.usrPassword != null);
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

    }
}