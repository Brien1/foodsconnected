using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
namespace foods_connected_brien.Controllers
{
    public interface IUserController
    {


        /* Returns a list of users in the database with key userId and value username. 
        Returns an empty list if no users in database
        */
        public Task<ActionResult<IEnumerable<User>>> GetUser();
        /* Returns a specific user with id
        Returns not found Mvc.task object if user not found
        */
        public Task<ActionResult<User>> GetUser(long id);
        /* Puts a new username in the database if that username doesn't conflict with any other username
        returns Microsoft.AspNetCore.Mvc.BadRequestObjectResult if username exists. If the change happens
        as it should it returns the new user value.
        */
        public Task<ActionResult<User>> PutUser(long id, String new_username);
        /* Posts a user to the database. Takes a user object from Todo.Models namespace. Returns a created
        action Task object with the user details attached. If the username exists it returns
        a bad request object with inner text "username exists"
        */

        public Task<ActionResult<User>> PostUser(User user);
        /* returns deleted user
        if the id provided didn't match returns badRequest object*/
        public Task<ActionResult<User>> DeleteUser(long id);


    }

}