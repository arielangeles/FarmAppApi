using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmAppApi.Models;
using FarmAppApi.DTO;

namespace FarmAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FarmAppDBContext dbContext;

        public UsersController(FarmAppDBContext context)
        {
            dbContext = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPerson>> GetUser(int id)
        {
            var user = await dbContext.Users
                .Where(u => u.IdUser == id)
                .Select(u => new UserPerson
                {

                }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser(UserP user)
        {
            var userDB = await dbContext.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
            if (userDB == null)
            {
                return NotFound();
            }

            string password = Encoding.Hash(user.Password);
            
            if (!password.Equals(userDB.UserPassword))
            {
                return BadRequest(
                    new BadRequestError() 
                    { ErrorCode = "INVALID_PASSWORD", ErrorDescription = "Incorrect password, try another one" }
                 );
            };

            return Ok(userDB);
        }


        public class BadRequestError
        {
            public string ErrorCode { get; set; }
            public string ErrorDescription { get; set; }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserPerson>> RegisterUser(UserPerson userPerson)
        {
            bool personExists = await dbContext.People.AnyAsync(u => u.EmailAddress == userPerson.EmailAddress);

            if (personExists)
            {
                return BadRequest(
                    new BadRequestError
                    { ErrorCode = "USER_ALREADY_EXISTS", ErrorDescription = "User already exists." }
                );
            }

            bool userExists = await dbContext.Users.AnyAsync(u => u.UserName == userPerson.UserName);
            
            if (userExists)
            {
                return BadRequest(
                    new BadRequestError
                    { ErrorCode = "PERSON_ALREADY_EXISTS", ErrorDescription = "Person already exists." }
                );
            }
                

            Person person = new Person()
            {
                FirstName = userPerson.FirstName,
                LastName = userPerson.LastName,
                BirthDate = userPerson.BirthDate,
                Gender = userPerson.Gender,
                EmailAddress = userPerson.EmailAddress,
                PhoneNumber = userPerson.PhoneNumber,
                HomeAddress = userPerson.HomeAddress,
            };

            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();

            User user = new User()
            {
                IdPerson = person.IdPerson,
                UserName = userPerson.UserName,
                UserPassword = userPerson.Password
            };

            user.UserPassword = Encoding.Hash(user.UserPassword);

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok(userPerson);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return dbContext.Users.Any(e => e.IdUser == id);
        }
    }
}
