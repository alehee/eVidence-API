using eVidence_API.Enums;

namespace eVidence_API.Models.Helpers
{
    public class CardAssignation
    {
        public CardType Type { get; set; }
        public object? Instance { get; set; } = null;
    }
}
