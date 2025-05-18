using MicroForms.Bindings;
using MicroForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms.Builders;

public abstract class BuilderBase
{
    protected readonly FieldControlDetails _formField;
    protected Dictionary<string, FieldControlDetails> _fields = new();

    public string DisplayName { get => _formField.Name; set => _formField.Name = value; }
    public FormLayout Layout { get => _formField.Layout; set => _formField.Layout = value; }
    public Type ChildProcess { get; set; }
    //public FormAllowAccess Access { get; set; }

    public IEnumerable<FieldControlDetails> Fields { get { return _fields.Values; } }


    public BuilderBase()
    {
        var bindingProperty = ModelBinding.FormLevelBinding;

        _formField = new FieldControlDetails
        {
            Binding = new FieldBinding
            {
                Binding = bindingProperty,
                BindingType = FieldBindingType.Form,
                BindingControlType = FieldBindingType.Form.ToString()
            },
            ControlType = ControlType.Form.ToString()
        };

        _fields[bindingProperty] = _formField;
    }

    public void AssertValid<TEntity>()
    {
        //RuleVirtualPropertyValidation.Validate<TEntity>(_fields.Values);
    }
}

