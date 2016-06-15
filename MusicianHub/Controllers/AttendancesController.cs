using Microsoft.AspNet.Identity;
using MusicianHub.Dtos;
using MusicianHub.Models;
using System.Linq;
using System.Web.Http;

namespace MusicianHub.Controllers
{
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }


        /*FormBody Cuz our gigId in body not in the URL*/

        /*Add in header:*/
        /*Content-Type:  application/json*/
        /*Link:  http://localhost:404/api/Attendances */
        [HttpPost]
        public IHttpActionResult Attend(AttendancesDTO dto)
        {
            string userId = User.Identity.GetUserId();

            var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exists)
            {
                return BadRequest("The Attendance Already Exists.");
            }
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
