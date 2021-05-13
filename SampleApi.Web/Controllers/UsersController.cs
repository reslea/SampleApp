using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            IEnumerable<User> users = _repository.Get();

            var userModels = _mapper.Map<IEnumerable<UserModel>>(users);

            if (!userModels.Any())
            {
                return NoContent();
            }

            return Ok(userModels);
        }

        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            var user = _mapper.Map<User>(model);
            _repository.Create(user);
            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            User user = _repository.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            _repository.Delete(user);
            _unitOfWork.SaveChanges();
            return NoContent();
        }
    }
}
