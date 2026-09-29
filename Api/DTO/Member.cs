namespace Api.DTO;

public record MemberResponse(
    Guid Id,
    string FullName,
    string Email);