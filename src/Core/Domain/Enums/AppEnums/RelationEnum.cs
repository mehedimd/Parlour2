using System.ComponentModel;

namespace Domain.Enums.AppEnums;

public enum RelationEnum
{
    [Description("Father")]
    F,
    [Description("Mother")]
    M,
    [Description("Brother")]
    B,
    [Description("Sister")]
    S,
    [Description("Son")]
    O,
    [Description("Doughter")]
    D,
    [Description("Wife")]
    W,
    [Description("Husband")]
    H,
    [Description("Others")]
    T
}
