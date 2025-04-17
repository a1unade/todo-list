using MediatR;
using TodoList.Application.Common.Requests.Jwt;
using TodoList.Application.Common.Responses.Jwt;

namespace TodoList.Application.Features.Jwt;

public class GetGeneratedJwtQuery: GenerateJwtRequest, IRequest<JwtTokenResponse>
{
    public GetGeneratedJwtQuery(GenerateJwtRequest request) : base(request)
    {
    }
}