using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingApp.API.Helpers;
using VotingApp.API.Models;
using System.Linq;

namespace VotingApp.API.Controllers
{
    /*
     Example Without Repo. DB access direct Use
     */
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
            var _resule = (from users in _dBAccess.Users
                           select new UserModel()
                           {
                               Id = users.Id,
                               Name = users.Name,
                               UserType = users.UserType,
                               IsVoted = users.IsVoted,
                               CandidateVoteTotal = _dBAccess.VoteCountModel.Count(t => t.CandidateUserId == users.Id)
                           }).ToList();

            return _resule;
        }

        [HttpPost]
        public async Task<UserModel> Add([FromBody] UserModel userModel)
        {
            await CheckExist(userModel);
            await _dBAccess.Users.AddAsync(userModel);
            await _dBAccess.SaveChangesAsync();
            return userModel;
        }

        [HttpPut]
        public async Task<UserModel> Update([FromBody] UserModel userModel)
        {
            await CheckExist(userModel);
            var _userModel = _dBAccess.Users.FirstOrDefault(t => t.Id == userModel.Id);
            if (_userModel != null)
            {
                _userModel.Name = userModel.Name;
                _userModel.IsVoted = userModel.IsVoted;
                _dBAccess.Users.Update(_userModel);
                await _dBAccess.SaveChangesAsync();
            }
            return userModel;
        }

        [HttpDelete("/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _dBAccess.Users.Where(t => t.Id == id).ExecuteDeleteAsync();
        }

        private async Task CheckExist(UserModel userModel)
        {
            var exist = _dBAccess.Users.FirstOrDefault(t => t.Name == userModel.Name && t.Id != userModel.Id);
            if (exist != null)
            {
                throw new Exception("User already exist.");
            }
        }
    }
}
