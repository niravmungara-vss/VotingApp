using VotingApp.API.Helpers;
using VotingApp.API.Models;
using System.Linq;

namespace VotingApp.API.Repo
{

    public interface IVoteCountRepository
    {
        Task<int> Add(VoteCountModel voteCountModel);
    }

    public class VoteCountRepository : IVoteCountRepository
    {
        private readonly DBAccess _dBAccess;
        public VoteCountRepository(DBAccess dBAccess)
        {
            _dBAccess = dBAccess;
        }

        public async Task<int> Add(VoteCountModel voteCountModel)
        {
            var exist = _dBAccess.VoteCountModel.FirstOrDefault(t => t.VoterUserId == voteCountModel.VoterUserId && t.CandidateUserId == voteCountModel.CandidateUserId);

            if(exist != null)
            {
                throw new Exception("The voter already voted for this candidate");
            }

            var _userModel = _dBAccess.Users.FirstOrDefault(t => t.Id == voteCountModel.VoterUserId);
            if (_userModel != null)
            {
                _userModel.IsVoted = true;
                _dBAccess.Users.Update(_userModel);
                await _dBAccess.SaveChangesAsync();
            }
            await _dBAccess.VoteCountModel.AddAsync(voteCountModel);
            return await _dBAccess.SaveChangesAsync();
        }
    }
}
