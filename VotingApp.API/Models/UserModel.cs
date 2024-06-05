using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.API.Models
{
    public enum UserTypes
    {
        Candidates = 1,
        Voters = 2
    }

    [Table("Users")]
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public UserTypes UserType { get; set; }

        public bool IsVoated { get; set; }
    }

}
