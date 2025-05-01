using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms;

public enum FormLayout
{
    Default,
    TwoColumns
}

public enum ConfirmType
{
    ChangesWillBeLost,
    Delete,
    DeleteItem,
    Custom
}

public enum ConfirmButtons
{
    OkCancel,
    YesNo
}
