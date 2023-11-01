using AutoMapper;
using BaseProject;
using BaseProject.Response;
using BaseProject.Token;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OperationProject.Cqrs;
using SchemaProject;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace OperationProject.Command
{
    public class TokenCommandHandler :
        IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly JwtConfig jwtConfig;

        public TokenCommandHandler(DbContextClass dbContextClass,IOptionsMonitor<BaseProject.Token.JwtConfig> jwtConfig)
        {
            this.dbContextClass = dbContextClass;
            this.jwtConfig = jwtConfig.CurrentValue;
        }
        private Claim[] GetClaims(User user)
        {
            var claims = new[]
            {
            new Claim("Id", user.UserLoginId.ToString()),
            new Claim("Role", user.Role),
            new Claim("Email", user.Email),
            new Claim("UserName", user.UserName)
        };

            return claims;
        }
        public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<User>().FirstOrDefaultAsync(x => x.Email == request.model.Email, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse<TokenResponse>("User not found!");
            }
            var Base64Encoded = Base64.Base64Encode(request.model.Password.ToUpper());
            if(entity.Password != Base64Encoded)
            {
                entity.LastActivityDate = DateTime.UtcNow;
                entity.PasswordRetryCount++;
                await dbContextClass.SaveChangesAsync(cancellationToken);

                return new ApiResponse<TokenResponse>("Invalid user informations");
            }
            if (!entity.IsActive)
            {
                return new ApiResponse<TokenResponse>("Invalid user!");
            }
            if(entity.Status == 0)
            {
                return new ApiResponse<TokenResponse>("The account has been locked.");
            }
            string token = Token(entity);
            TokenResponse tokenResponse = new()
            {
                Token = token,
                ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                UserName = entity.UserName,
                Email = entity.Email
            };

            return new ApiResponse<TokenResponse>(tokenResponse);

        }
        private string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }
    }
}
