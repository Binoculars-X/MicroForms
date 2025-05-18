using MicroForms.Bindings;
using MicroForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms;

public abstract class FormBuilderBase
{
    protected readonly FieldControlDetails _formField;
    protected Dictionary<string, FieldControlDetails> _fields = new ();

    public string DisplayName { get => _formField.Name; set => _formField.Name = value; }
    public FormLayout Layout { get => _formField.Layout; set => _formField.Layout = value; }
    public Type ChildProcess { get; set; }
    //public FormAllowAccess Access { get; set; }

    public IEnumerable<FieldControlDetails> Fields { get { return _fields.Values; } }


    public FormBuilderBase()
    {
        var bindingProperty = ModelBinding.FormLevelBinding;

        _formField = new DataField
        {
            BindingProperty = bindingProperty,
            BindingType = FieldBindingType.Form,
            BindingControlType = FieldBindingType.Form.ToString(),
            ControlTypeName = ControlType.Form.ToString()
        };

        _fields[bindingProperty] = _formField;
    }

    public void AssertValid<TEntity>()
    {
        //RuleVirtualPropertyValidation.Validate<TEntity>(_fields.Values);
    }
}
