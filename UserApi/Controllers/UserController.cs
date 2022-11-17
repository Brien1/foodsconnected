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
            Description = "Returns a list of users which may be empty if database is empty.\n",
            Tags = new[] { "Fetch User" }
        )]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Fetches a specific User",
            Description = "Fetch a User from the database with parameters {userId: long} \n\nReturns User{ userId: long, username: string}.\n\n If the userId does not exist returns NotFound() error ",
            Tags = new[] { "Fetch User" }
        )]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Enters a new username onto an existing userID in the database",
            Description = "Enters a new User into the database with parameters {userId: long, username: string} \n userId is the existing users ID, username is the new username being entered\n\n On success returns CreatedAction containing the details of the entry. \n Username cannot equal another users name else BadRequest is thrown",
            Tags = new[] { "Update username" }
        )]
        public async Task<ActionResult<User>> PutUser(long id, String new_username)
        {
            var f = await _context.User.ToListAsync();
            if (id != id || f.Any((e) => { return e.username.Equals(new_username); }))
            {
                return BadRequest();
            }
            if (!UserExists(id))
            {
                return NotFound();
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
            Description = "Enters a new User into the database with parameters {user: User} \n\n On success returns CreatedAction containing the details of the entry. \n\n Username cannot equal another users name else BadRequest is thrown",
            Tags = new[] { "Insert User" }
        )]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            var f = await _context.User.ToListAsync();
            if (f.Any((e) => { return e.username.Equals(user.username); }))
            {
                return BadRequest("username exists");
            }

            else
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = user.userId }, user);
            }

        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Removes a User from the database",
            Description = "Removes a User from the database with parameters {id: userId} \n\n On success returns the deleted user User{userId: long, username: string}. \n\n If userId does not exist returns NotFound() warning",
            Tags = new[] { "Delete User" }
        )]
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
