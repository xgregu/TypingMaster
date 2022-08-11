using System.Collections.Generic;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;
public record TestTableDataResponse(IReadOnlyList<Test> TestList, int TestCount);
