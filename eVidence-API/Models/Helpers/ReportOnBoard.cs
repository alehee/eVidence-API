using eVidence_API.Models.Context;

namespace eVidence_API.Models.Helpers
{
    public class ReportOnBoard
    {
        public ICollection<Entrance> AccountEntrances { get; set; }
        public ICollection<TemporaryEntrance> TemporaryEntrances { get; set; }
    }
}
