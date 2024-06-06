using Microsoft.AspNetCore.Mvc;
using VotingApp.API.Helpers;
using VotingApp.API.Models;
using VotingApp.API.Repo;

namespace VotingApp.API.Controllers
{
    /*
     Example With Repo. DB access we can use through Repo. 
    We can Add a Business Logic layer on Repo. I didn't use it here because not required here so...
     */
    [Route("api/[controller]")]
    [ApiController]
    public class VoteCountController : ControllerBase
    {
        private readonly IVoteCountRepository _voteCountRepository;

        public VoteCountController(IVoteCountRepository voteCountRepository)
        {
            _voteCountRepository = voteCountRepository;
        }

        [HttpPost]
        public async Task<int> Add([FromBody] VoteCountModel voteCountModel)
        {
            return await _voteCountRepository.Add(voteCountModel);
        }
    }
}
