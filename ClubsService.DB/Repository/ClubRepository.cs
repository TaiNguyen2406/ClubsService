using ClubsService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsService.DB.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ClubsDbContext _dbContext;
        public ClubRepository(ClubsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Club> CreateClub(Club club, Member member)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {

                _dbContext.Add(club);
                member.ClubId = club.Id;
                _dbContext.Members.Update(member);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            return GetClub(club.Id);

        }

        public Club GetClub(Guid id)
        {
            var result = _dbContext.Clubs.Include(c => c.Members).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Club GetClubByName(string name)
        {
            var result = _dbContext.Clubs.FirstOrDefault(x => x.Name == name);
            return result;
        }
    }
}
