using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms.Models;

public enum ControlType
{
    Autocomplete = 1,
    Button,
    Checkbox,
    CustomComponent,
    DateEdit,
    DatePicker,
    DropDown,
    DropDownSearch,
    FileUpload,
    Header = 9,
    Label = 10,
    MoneyEdit,
    PercentEdit,
    Repeater,
    SelectableList,
    Subtitle,
    Table,
    TextArea,
    TextEdit,
    TextSearchEdit,
    ActionMenuItem,
    Form = 21,
    // for Quiz
    Image,
    Paragraph,
}

