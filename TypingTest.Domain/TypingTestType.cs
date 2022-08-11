using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TypingMaster.Domain;

public enum TypingTestType
{
    [Description("Minimalistyczny")] Minimalistic = 0,
    [Description("Krótki")] Short = 1,
    [Description("Standardowy")] Average = 2,
    [Description("Długi")] Long = 3,
    [Description("Bardzo długi")] Verylong = 4
}