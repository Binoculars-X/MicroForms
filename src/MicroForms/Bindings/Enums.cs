using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms;

public enum FieldBindingPathType
{
    Unsupported,
    Straight,
    Column,
    SingleSelect
}

public enum FieldBindingType
{
    SingleField = 1,
    SingleSelect,
    SelectableList,
    Table,
    Repeater,
    TableColumn,
    TableColumnSingleSelect,
    TableCount,
    TableFooter,
    TableColumnContextMenu,
    ActionButton,
    ListFormContextMenu,
    FlowReferenceButtons,
    ListFormContextMenuItem,
    FlowReferenceButtonsItem,
    Form,
    List,
    ListCard,
    RepeaterActionButton
}
