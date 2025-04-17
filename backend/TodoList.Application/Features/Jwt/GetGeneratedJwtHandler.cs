using MediatR;
using TodoList.Application.Common.Responses.Jwt;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.Jwt;

public class GetGeneratedJwtHandler(IJwtGenerator generator) : IRequestHandler<GetGeneratedJwtQuery, JwtTokenResponse>
{
    public async Task<JwtTokenResponse> Handle(GetGeneratedJwtQuery request, CancellationToken cancellationToken)
    {
        var token = await generator.GenerateToken(request.Step, request.Data);
        
        var response = new JwtTokenResponse
        {
            Token = token,
        };
        
        return response;
    }
}