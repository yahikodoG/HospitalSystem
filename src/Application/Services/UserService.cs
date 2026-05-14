using Application.Common.Errors;
using Application.Common.Responses;
using Application.DTOs.Patients;
using Application.DTOs.Users;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserFactory _userFactory;
    private readonly IValidator<UserRequest> _validator;
    private readonly IJwtAuthService _jwtAuthService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _uow;

    public UserService(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IUserFactory userFactory,
        IValidator<UserRequest> validator,
        IJwtAuthService jwtAuthService,
        IPasswordHasher passwordHasher,
        IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _userFactory = userFactory;
        _jwtAuthService = jwtAuthService;
        _passwordHasher = passwordHasher;
        _validator = validator;

        _uow = uow;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => u.MapToResponse()).ToList();
    }

    public async Task<ResponseValue<UserResponse?>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return ResponseValue<UserResponse?>.NotFound(UserErrors.ERR_NOT_FOUND);

        return ResponseValue<UserResponse?>.Success(
            user.MapToResponse(),
            "Lấy tài khoản người dùng thành công."
        );
    }

    // =========================
    // Patient
    // =========================
    public async Task<ResponseValue<UserResponse>> CreateAsync(PatientRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<UserResponse>.BadRequest(errors);
        }

        var user = await _userFactory.CreateBaseUser(request);

        await _userRepository.AddAsync(user);

        await _uow.SaveChangesAsync();

        await _userRoleRepository.AddAsync(new UserRole
        {
            UserId = user.UserId,
            RoleId = 6
        });

        await _uow.SaveChangesAsync();

        return ResponseValue<UserResponse>.Success(
            user.MapToResponse(),
            "Tạo tài khoản người dùng thành công.");
    }

    public async Task<ResponseValue<UserLoginResponse>> LoginAsync(UserLoginRequest request)
    {
        var user = await _userRepository.GetUserWithRolesAsync(request.UsernameOrEmail, request.UsernameOrEmail);

        if (user == null)
            return ResponseValue<UserLoginResponse>.BadRequest(UserErrors.ERR_NOT_FOUND);

        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            return ResponseValue<UserLoginResponse>.BadRequest(UserErrors.ERR_INCORRECT_PASSWORD);

        var roles = user.UserRoleUsers
            .Select(ur => ur.Role.RoleName)
            .ToList();

        var token = _jwtAuthService.GenerateToken(user, roles);

        var response =  new UserLoginResponse
        {
            Username = user.Username,
            FullName = user.FullName,
            Role = roles.FirstOrDefault(),
            AccessToken = token
        };

        return ResponseValue<UserLoginResponse>.Success(response, "Đăng nhập thành công.");
    }
}