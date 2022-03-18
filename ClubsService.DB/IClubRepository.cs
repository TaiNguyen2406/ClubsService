using ClubsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsService.DB
{
    public interface IClubRepository
    {
        Club GetClub(Guid id);
        Club GetClubByName(string name);
        Task<Club> CreateClub(Club club, Member member);
    }
}
