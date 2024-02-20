using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Interfaces;

public interface ITranslationStore : IAsyncRepository<TranslationEntity>;