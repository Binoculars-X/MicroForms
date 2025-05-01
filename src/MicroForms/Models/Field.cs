using MicroForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms;

public class Field
{
    public string DataType { get; set; }

    public string ControlType { get; set; }

    public string ControlTypeName { get; set; }

    public string AddDialogFlow { get; set; }

    //public Type ViewModeControlType { get; set; }

    public string Label { get; set; }

    public bool Required { get; set; }

    public bool Hidden { get; set; }

    public bool? ReadOnly { get; set; }

    public bool Highlighted { get; set; }

    public bool Password { get; set; }

    public bool NoCaption { get; set; }

    public string Hint { get; set; }

    public string Format { get; set; }

    public FieldFilterType FilterType { get; set; }

    public string FilterRefField { get; set; }

    //public List<FieldRule> Rules { get; set; } = new List<FieldRule>();

}
