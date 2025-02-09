using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
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

            // Check if UserName is Exist
            var userByUserName = await _userManager
                   .Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            if (userByUserName != null)
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);

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

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            // Check if User is Exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return NotFound<string>();

            // --------
            // anther way you can delete old password and add new password from user
            //var user1 = await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);
            // --------
            // change user password with check current password with old password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            // Result
            if (!result.Succeeded)
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_localizer[SharedResourcesKeys.Success]);


        }

        #endregion


    }
}
