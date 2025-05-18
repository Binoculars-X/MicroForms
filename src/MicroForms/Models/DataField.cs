using MicroForms.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms;

public class DataField : Field
{
    public string Name { get; set; }

    public FormLayout Layout { get; set; }

    public string BindingProperty { get; set; }

    public string TableBindingProperty { get; set; }

    public string BindingControlType { get; set; }

    public FieldBindingType BindingType { get; set; }

    public FieldBinding Binding { get; set; }

    public bool PrimaryKey { get; set; }

    public bool Unique { get; set; }

    public int Order { get; set; }

    public bool Button { get; set; }

    public string Group { get; set; }

    public string FieldSetGroup { get; set; }

    public string ActionLink { get; set; }

    //public List<ConfirmationDetails> Confirmations { get; private set; } = new List<ConfirmationDetails>();


    public Type SelectEntityType { get; set; }

    public string SelectItemsProperty { get; set; }

    public string SelectIdProperty { get; set; }

    public string SelectNameProperty { get; set; }

    public int? MaxLength { get; set; }
}
