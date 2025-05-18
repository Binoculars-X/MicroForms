using MicroForms.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MicroForms;

public class FieldControlDetails: ContainerDetails
{
    //public string ModelBinding { get; set; }
    //public string ModelBindingType { get; set; }
    //public string ModelItems { get; set; }
    //public string ModelItemId { get; set; }
    //public string ModelItemName { get; set; }
    //public string ModelTableBinding { get; set; }
    //public string ModelTargetBinding { get; set; }

    public FormLayout Layout { get; set; }
    // new binding concept
    public FieldBinding Binding { get; set; }

    // Navigation
    public string ActionLink { get; set; }
}
