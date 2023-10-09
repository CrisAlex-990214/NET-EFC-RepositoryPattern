using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> repository;

        public UserController(IGenericRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            var newUser = await repository.Add(user);
            await repository.Save();
            return Ok(newUser.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var user = await repository.Get(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            var userExists = (await repository.Get(user.Id)) is not null;
            if(!userExists) return NotFound();

            repository.Update(user);
            await repository.Save();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = (await repository.Get(id));
            if (user is null) return NotFound();

            repository.Delete(user);
            await repository.Save();
            return NoContent();
        }
    }
}
