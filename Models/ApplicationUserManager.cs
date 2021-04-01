using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThePortal.Models
{
    public class ApplicationUserManager : UserManager<ApplicationUser> 
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            
        }

        public override Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return Users.Include(c => c.FacebookData).Include(c => c.GoogleData).FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<FacebookData> UpdateFacebookAccessTokenAsync(string userId,string accessToken)
        {
            ApplicationUser user = await Users.FirstOrDefaultAsync(item => item.Id == userId);
            if(user.FacebookData == null)
            {
                user.FacebookData = new FacebookData();
            }
            user.FacebookData.AccessToken = accessToken;
            await this.UpdateAsync(user);
            return user.FacebookData;
        }
       
    }
}
