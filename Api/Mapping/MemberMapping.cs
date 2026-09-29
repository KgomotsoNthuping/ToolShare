using Api.DTO;
using Api.Models;

namespace Api.Mapping;

public static class MemberMapping
{
    public static MemberResponse ToResponse(this Member member)
    {
        return new MemberResponse(
            member.Id,
            member.FullName,
            member.Email);
    }
}