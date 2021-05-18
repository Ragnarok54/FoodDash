using FoodDash.Web.Common.Enums;
using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.DataAccess.Repository.Interfaces;
using FoodDash.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodDash.Web.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address
            };

            if (model.Photo != null)
            {
                using var br = new BinaryReader(model.Photo.OpenReadStream());
                user.Photo = br.ReadBytes((int)model.Photo.Length);
            }


            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return result;
        }

        public async Task<SignInResult> SignInAsync(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public bool IsUserLoggedIn(ClaimsPrincipal User)
        {
            return _signInManager.IsSignedIn(User);
        }

        public ProfileModel GetProfile(string userId)
        {
            var user = _userRepository.Find(u => u.Id == userId).First();

            return new ProfileModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address
            };
        }

        public async Task UpdateProfileAsync(ProfileModel model, string userId)
        {
            var user = _userRepository.Find(u => u.Id == userId).First();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Address = model.Address;

            if (model.Photo != null)
            {
                using var br = new BinaryReader(model.Photo.OpenReadStream());
                user.Photo = br.ReadBytes((int)model.Photo.Length);
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            _userRepository.SaveChanges();
        }

        public byte[] GetUserPhoto(string userId)
        {
            var user = _userRepository.Find(u => u.Id == userId).First();
            return user.Photo;
            //var base64 = user.Photo != null ? Convert.ToBase64String(user.Photo) : string.Empty;
            //return string.Format("data:image/jpg;base64,{0}", base64);
        }
    }
}
