using Microsoft.EntityFrameworkCore;
using VotingApp.API.Models;

namespace VotingApp.API.Helpers
{
    public class DBAccess : DbContext
    {

        public DBAccess(DbContextOptions<DBAccess> options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<VoteCountModel> VoteCountModel { get; set; }

    }
}