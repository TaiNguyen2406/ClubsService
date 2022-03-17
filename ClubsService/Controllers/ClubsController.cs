using ClubsService.DB;
using ClubsService.Models;
using ClubsService.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClubsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubsController : ControllerBase
    {
        private readonly ILogger<ClubsController> _logger;
        private readonly IClubRepository _clubRepository;
        private readonly IMemberRepository _memberRepository;

        public ClubsController(IClubRepository clubRepository, IMemberRepository memberRepository, ILogger<ClubsController> logger)
        {
            _clubRepository = clubRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }

        [HttpGet("{clubId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClub(Guid clubId)
        {
            var result = _clubRepository.GetClub(clubId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateClub([FromBody] CreateUpdateClubDto data)
        {
            var check = int.TryParse(Request.Headers["Player-Id"].ToString(), out int memberId);
            if (!check)
            {
                _logger.LogError($"Player-Id is missing");
                throw new Exception($"Player-Id is missing");
            }
            var member = IsMemberExist(memberId);
            var chkClub =  _clubRepository.GetClubByName(data.Name);
            if(chkClub != null)
            {
                return Conflict($"The club with name {data.Name} already exists");
            }    
            var club = new Club
            {
                Name = data.Name
            };
            var result = await _clubRepository.CreateClub(club);
            member.ClubId = result.Id;
            await _memberRepository.UpdateMember(member);
            return CreatedAtAction(nameof(CreateClub), result);
        }

        [HttpPost("{clubId}/members")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddMemberToClub(Guid clubId, [FromBody] AddMemberToClubDto player)
        {
            IsClubExist(clubId);
            var member = IsMemberExist(player.PlayerId);
            member.ClubId = clubId;
            await _memberRepository.UpdateMember(member);
            return Ok(null);
        }

        private Club IsClubExist(Guid clubId)
        {
            var club = _clubRepository.GetClub(clubId);
            if (club == null)
            {
                _logger.LogError($"Not found club with id {clubId}");
                throw new Exception($"Not found club with id {clubId}");
            }
            return club;
        }

        private Member IsMemberExist(int memberId)
        {
            var member = _memberRepository.GetMember(memberId);
            if (member == null)
            {
                _logger.LogError($"Not found member with id {memberId}");
                throw new Exception($"Not found member with id {memberId}");
            }
            return member;
        }
    }
}