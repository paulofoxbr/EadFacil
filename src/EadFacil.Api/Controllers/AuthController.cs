﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EadFacil.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EadFacil.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettins _jwtSettings;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        IOptions<JwtSettins> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new { Message = "Usuário ou senha inválidos." });
        }

        return Ok(await GerarJwt(model.Email));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = _userManager.CreateAsync(user, registerUser.Password).Result;
        if (!result.Succeeded)
        {
            return Problem("Falha ao registrar o usuário.");
        }

        await _signInManager.SignInAsync(user, false);
        return Ok(await GerarJwt(user.Email));
    }

    private async Task<string> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var roles = await _userManager.GetRolesAsync(user!);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user!.UserName!)
        };

        claims.Add(new Claim(ClaimTypes.Email, user.Email!));

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}