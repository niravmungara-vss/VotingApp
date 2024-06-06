
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.API.Models
{
    [Table("VoteCount")]
    public class VoteCountModel
    {
        [Key]
        public int Id { get; set; }

        public int VoterUserId { get; set; }

        public int CandidateUserId { get; set; }
    }
}
