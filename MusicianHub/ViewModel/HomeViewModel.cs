using System.Collections.Generic;
using MusicianHub.Models;

namespace MusicianHub.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
    }
}