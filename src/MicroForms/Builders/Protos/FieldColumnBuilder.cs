using MicroForms.Extensions;
using MicroForms.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace MicroForms;

public class FieldColumnBuilder
{
    protected FieldControlDetails _field;
    public FieldControlDetails Field { get { return _field; } }
}

public class FieldColumnBuilder<TProperty, TEntity> : FieldColumnBuilder where TEntity : class
{

    //public FieldColumnBuilder(string bindingProperty)
    //{
    //    _field = new DataField { BindingProperty = bindingProperty, DataType = typeof(TProperty).Name };
    //}

    public FieldColumnBuilder(FieldControlDetails field)
    {
        _field = field;
    }

    // class rule
    //public virtual FieldColumnBuilder<TProperty, TEntity> Rule([NotNull] Type ruleType, FormRuleTriggers trigger = FormRuleTriggers.Changed)
    //{
    //    _field.Rules.Add(new FieldRule { RuleType = ruleType, Trigger = trigger });
    //    return this;
    //}

    public virtual FieldColumnBuilder<TProperty, TEntity> IsRequired(bool required = true)
    {
        _field.DisplayProperties.Required = required;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> IsHidden(bool hidden = true)
    {
        _field.DisplayProperties.Visible = !hidden;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> IsPrimaryKey(bool pk = true)
    {
        _field.DisplayProperties.IsPrimaryKey = pk;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> IsReadOnly(bool readOnly = true)
    {
        _field.DisplayProperties.Disabled = readOnly;
        return this;
    }
    public virtual FieldColumnBuilder<TProperty, TEntity> IsHighlighted(bool b = true)
    {
        _field.DisplayProperties.Highlighted = b;
        return this;
    }
    public virtual FieldColumnBuilder<TProperty, TEntity> IsPassword(bool b = true)
    {
        _field.DisplayProperties.Password = b;
        return this;
    }
    public virtual FieldColumnBuilder<TProperty, TEntity> NoCaption(bool b = true)
    {
        _field.DisplayProperties.NoCaption = b;
        return this;
    }

    //public virtual FieldColumnBuilder<TProperty, TEntity> IsUnique(bool unique = true)
    //{
    //    _field.Unique = unique;
    //    return this;
    //}

    public virtual FieldColumnBuilder<TProperty, TEntity> Label(string label)
    {
        _field.Caption = label;
        return this;
    }

    //public virtual FieldColumnBuilder<TProperty, TEntity> MaxLength(int length)
    //{
    //    _field.MaxLength = length;
    //    return this;
    //}

    public virtual FieldColumnBuilder<TProperty, TEntity> Hint(string s)
    {
        _field.DisplayProperties.Hint = s;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> Name(string name)
    {
        _field.Name = name;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> Filter(FieldFilterType type)
    {
        _field.DisplayProperties.FilterType = type;
        return this;
    }
    public virtual FieldColumnBuilder<TProperty, TEntity> FilterRefField(string s)
    {
        _field.DisplayProperties.FilterRefField = s;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> Format(string format)
    {
        _field.DisplayProperties.Format = format;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> Control(string controlType)
    {
        _field.ControlType = controlType;
        return this;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> ControlReadOnly(Type controlType)
    {
        //_field.ViewModeControlType = controlType;
        return this;
    }

    public virtual DropdownFieldColumnBuilder<TEntity2> Dropdown<TEntity2>()
    {
        return new DropdownFieldColumnBuilder<TEntity2>(this, _field);
    }

    public class DropdownFieldColumnBuilder<TEntity2>
    {
        private FieldColumnBuilder<TProperty, TEntity> _parent;
        private FieldControlDetails _field;

        public DropdownFieldColumnBuilder(FieldColumnBuilder<TProperty, TEntity> parent, FieldControlDetails field)
        {
            _field = field;
            _parent = parent;
        }

        public FieldColumnBuilder<TProperty, TEntity> Set<TKey2, TKey3>(
            [NotNull] Expression<Func<TEntity2, TKey2>> id,
            [NotNull] Expression<Func<TEntity2, TKey3>> name)
        {
            _field.ControlType = ControlType.DropDown.ToString();
            //_field.ControlType = typeof(DefaultDropdownControl);
            //_field.ViewModeControlType = typeof(DefaultDropdownReadonlyControl);

            _field.DataTypeName = typeof(TEntity2).Name;
            _field.Binding.IdBinding = id.Body.ToString().ReplaceLambdaVar();
            _field.Binding.NameBinding = name.Body.ToString().ReplaceLambdaVar();

            return _parent;
        }
    }
}
