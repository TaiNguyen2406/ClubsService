using ClubsService.Models;

namespace ClubsService.Publisher
{
    public interface IAddClubPublisher
    {
        void Publish(Club club);
    }
}
