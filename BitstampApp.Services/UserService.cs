using BitstampApp.Core;
using BitstampApp.Core.Models;
using BitstampApp.Core.Services;
using BitstampApp.Services.Helpers;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BitstampApp.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            this._unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public User Create(User model)
        {
            model.Status = true;
            model.CreationDate = DateTime.Now;
            _unitOfWork.Users.AddAsync(model);
            _unitOfWork.CommitAsync();
            return model;
           
        }

        public void Delete(int id)
        {
            var model = this.Get(id);
            _unitOfWork.Users.Remove(model);
             _unitOfWork.CommitAsync();
        }

       public IEnumerable<User> GetAll()
        {
           var list = _unitOfWork.Users.GetAllAsync().Result;

            return list;
        }

        public User Get(int id)
        {
            var model = _unitOfWork.Users
                .GetAsync(id).Result;
            return model;
        }
 

        public void Update(User o)
        {
            var model = _unitOfWork.Users
                 .GetAsync(o.Id).Result;
    
           
             _unitOfWork.CommitAsync();
        }

        public User Authorize(User user)
        {
            var model = _unitOfWork.Users.GetAllAsync().Result.Where(x=>x.Username == user.Username).FirstOrDefault();

            if (model == null)
            {
                return null;
            }

            bool pwdcheck = CheckMatch(model.Password,user.Password);

            if (model.Username == user.Username && pwdcheck)
            {

               

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new Claim[]
                //    {
                //    new Claim(ClaimTypes.Name, model.Id.ToString())
                //    }),
                //    Expires = DateTime.UtcNow.AddDays(7),
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                //};
                //var token = tokenHandler.CreateToken(tokenDescriptor);
                //model.Token = tokenHandler.WriteToken(token);
                model.Password = null;

                return model;
            }
            else
            {
                return null;
            }
        }
        public bool CheckMatch(string hash, string input)
        {
            try
            {
                var parts = hash.Split(':');

                var salt = Convert.FromBase64String(parts[0]);

                var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }


        public void Update(User modelToBeUpdated, User model)
        {
            throw new NotImplementedException();
        }



    }
}
