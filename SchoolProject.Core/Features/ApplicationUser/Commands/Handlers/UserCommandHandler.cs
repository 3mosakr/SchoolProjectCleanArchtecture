using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>
    {

        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public UserCommandHandler(UserManager<User> userManager,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handler
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Check if Email is Exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.EmailIsExist]);
            }

            // Check if UserName is Exist
            user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);
            }

            // mapping 
            user = _mapper.Map<User>(request);

            // Create operation
            var CreateResult = await _userManager.CreateAsync(user, request.Password);
            // faild - return message
            if (!CreateResult.Succeeded)
                return BadRequest<string>(CreateResult.Errors.FirstOrDefault().Description);

            // Success 
            return Created("");

        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            // Check if User is Exist
            var ExistingUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (ExistingUser == null)
                return NotFound<string>();

            // mapping 
            var newUser = _mapper.Map(request, ExistingUser);
            // update
            var result = await _userManager.UpdateAsync(newUser);
            // check result is success 
            if (!result.Succeeded)
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            // return message
            return Success((string)_localizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Check if User is Exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return NotFound<string>();
            // Delete
            var result = await _userManager.DeleteAsync(user);
            // check result is not success 
            if (!result.Succeeded)
                return BadRequest<string>(_localizer[SharedResourcesKeys.DeletedFailed]);
            // return message
            return Success((string)_localizer[SharedResourcesKeys.Deleted]);

        }

        #endregion


    }
}
