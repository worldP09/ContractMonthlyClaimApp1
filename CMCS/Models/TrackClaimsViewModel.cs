using System.Collections.Generic;
using CMCS.Models;

namespace CMCS.Models
{
    public class TrackClaimsViewModel
    {
        public Lecturer Lecturer { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
