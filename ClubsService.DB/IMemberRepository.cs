using ClubsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsService.DB
{
    public interface IMemberRepository
    {
        Member GetMember(int id);
        Task<Member> UpdateMember(Member member);
    }
}
