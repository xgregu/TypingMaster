
namespace TypingMaster.Shared.Dtos;

public record PagedTestResponse(ICollection<TypingTestDto> Tests, long TotalCount);
