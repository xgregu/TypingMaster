using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Interfaces;

public interface ITypingTextsStore : IAsyncRepository<TypingTextEntity>
{
}