using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingApp.API.Helpers;
using VotingApp.API.Models;
using System.Linq;

namespace VotingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBAccess _dBAccess;

        public UserController(DBAccess dBAccess)
        {
            _dBAccess = dBAccess;
        }

        [HttpGet]
        public async Task<List<UserModel>> GetAll()
        {
            return await _dBAccess.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<UserModel> Add([FromBody] UserModel userModel)
        {
            await _dBAccess.Users.AddAsync(userModel);
            await _dBAccess.SaveChangesAsync();
            return userModel;
        }

        [HttpPut]
        public async Task<UserModel> Update([FromBody] UserModel userModel)
        {
            var _userModel = _dBAccess.Users.FirstOrDefault(t => t.Id == userModel.Id);
            if (_userModel == null)
            {
                _dBAccess.Users.Update(userModel);
                await _dBAccess.SaveChangesAsync();
            }
            return userModel;
        }

        [HttpDelete("/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _dBAccess.Users.Where(t => t.Id == id).ExecuteDeleteAsync();
        }
    }
}
