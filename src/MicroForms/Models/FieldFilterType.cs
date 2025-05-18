using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms.Models;

public enum FieldFilterType
{
    None,
    Text,
    TextStarts,
    TextContains,
    TextEnds,
    NumExpression,
    DateExpressionEqual,
    DateExpressionToDate,
    DateExpressionFromDate,
    DateExpressionRange,
    Select,
    MultiSelect,
    DecimalEqual,
    DecimalLessThan,
    DecimalGreaterThan,
    DecimalRange
}

public enum FieldFilterPositionType
{
    FirstControl,
    SecondControl
}
