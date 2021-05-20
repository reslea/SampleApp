using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data.Entities;
using SampleAPI.Data.Repositories;
using SampleApi.Web.Models;
using SampleApi.Web.Utilities;

namespace SampleApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;

        public UsersController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadModel>> Get()
        {
            IEnumerable<User> users = _repository.Get();

            var userModels = _mapper.Map<IEnumerable<UserReadModel>>(users);

            if (!userModels.Any())
            {
                return NoContent();
            }

            return Ok(userModels);
        }

        [HttpPost]
        public ActionResult Create(UserWriteModel model)
        {
            var user = _mapper.Map<User>(model);
            _repository.Create(user);
            _unitOfWork.SaveChanges();

            return Ok(user);
        }

        [HttpPut("")]
        public ActionResult Update(int id, UserWriteModel model)
        {
            var user = _repository.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            var userToUpdate = _mapper.Map<User>(model);
            userToUpdate.Id = id;

            _repository.Update(userToUpdate);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            User user = _repository.Get(id);

            if (user is null)
            {
                await transaction.RollbackAsync();
                return NotFound();
            }

            _repository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return NoContent();
        }
    }
}
