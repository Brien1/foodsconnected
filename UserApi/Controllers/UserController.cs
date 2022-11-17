using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TodoApi.Models;
namespace foods_connected_brien.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserController
    {
        private readonly UserContext _context;
        public UserController()
        {
            _context = new UserContext();
        }

        // GET: api/User
        [HttpGet]
        [SwaggerOperation(
            Summary = "Fetches list of users",
            Description = "Returns a list of users ordered by userId which may be empty if database is empty.\n",
            Tags = new[] { "Fetch Users" }
        )]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }



        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Enters a new username onto an existing userID in the database",
            Description = "Enters a new User into the database with parameters {userId: long, username: string} \n userId is the existing users ID, username is the new username being entered\n\n On success returns CreatedAction containing the details of the entry. \n Username cannot equal another users name else BadRequest is thrown",
            Tags = new[] { "Update username" }
        )]
        [SwaggerResponse(400)]
        [SwaggerResponse(200)]


        public async Task<ActionResult<User>> PutUser(long id, String new_username)
        {
            var f = await _context.User.ToListAsync();
            if (!UserExists(id))
            {
                return NotFound("id doesn't exist");
            }
            if (id != id || f.Any((e) => { return e.username.Equals(new_username); }))
            {
                return BadRequest("username must be new and unique");
            }
            User u = _context.User.Find(id);
            u.username = new_username;
            _context.User.Update(u);
            _context.Entry(u).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {


            }

            return u;
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "Enters a new User into the database",
            Description = "Enters a new User into the database with parameters {username: sting} \n\n On success returns CreatedAction containing the details of the entry. \n\n Username cannot equal another users name else BadRequest is thrown",
            Tags = new[] { "Insert User" }
        )]
        [SwaggerResponse(201, "success")]
        [SwaggerResponse(400, "username exists")]

        public async Task<ActionResult<User>> PostUser(string username)
        {

            var f = await _context.User.ToListAsync();
            if (f.Any((e) => { return e.username.Equals(username); }))
            {
                return NotFound("username exists");
            }

            else
            {
                var nu= new User();
                nu.username = username;
                _context.User.Add(nu);
                await _context.SaveChangesAsync();
                return nu;
            }

        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Removes a User from the database",
            Description = "Removes a User from the database with parameters {id: userId} \n\n On success returns the deleted user User{userId: long, username: string}. \n\n If userId does not exist returns NotFound() warning",
            Tags = new[] { "Delete User" }
        )]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]

        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var r = System.Text.Json.JsonSerializer.Serialize(user);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            User f = System.Text.Json.JsonSerializer.Deserialize<User>(r);
            return f;
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.userId == id);
        }
    }
}
