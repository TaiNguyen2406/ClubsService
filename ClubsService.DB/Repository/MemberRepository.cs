using ClubsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsService.DB.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ClubsDbContext _dbContext;
        public MemberRepository(ClubsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Member> UpdateMember(Member member)
        {
            _dbContext.Update(member);
            await _dbContext.SaveChangesAsync();
            return  GetMember(member.Id);
        }

        public Member GetMember(int id)
        {
            var result = _dbContext.Members.FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
