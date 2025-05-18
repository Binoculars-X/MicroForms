using MicroForms.Bindings;
using MicroForms.Extensions;
using MicroForms.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms.Builders;

public class ListFromQueryBuilder<M>
    where M : class
{
    ListTypeBuilder<M> _builder;
    public Func<QueryOptions, Task<IEnumerable<M>>> Query { get; protected set; }

    public void WithQuery(Func<QueryOptions, Task<IEnumerable<M>>> query, 
        [NotNull] Action<ListTypeBuilder<M>> buildAction)
    {
        Query = query;
        var builder = new ListTypeBuilder<M>();
        _builder = builder;
        buildAction.Invoke(builder);
        //ListType = typeof(TKey);
    }
}

public abstract class ListTypeBuilder : BuilderBase
{
    protected FieldColumnBuilder _FieldColumnBuilder;
    //protected readonly List<ActionRouteLink> _contextLinks = new List<ActionRouteLink>();
    public string ItemsPath { get; protected set; }
    public FieldColumnBuilder FieldColumnBuilder { get { return _FieldColumnBuilder; } }
    //public IEnumerable<ActionRouteLink> ContextLinks { get { return _contextLinks; } }
    //public List<IBindingFlowReference> RefButtons { get; private set; } = new List<IBindingFlowReference>();
    public abstract void AssertValid();
}

public class ListTypeBuilder<TEntity> : ListTypeBuilder where TEntity : class
{
    protected int _propertyOrder;
    //public Func<QueryOptions, Task<IEnumerable<TEntity>>> Query { get; protected set; }

    public ListTypeBuilder(Expression items)
    {
        ItemsPath = items.ToString().ReplaceLambdaVar();
        // order of properties in the Entity has a low priority - all mentioned properties will be moved up 
        _propertyOrder = 0;
    }

    public ListTypeBuilder(/*Func<QueryOptions, Task<IEnumerable<TEntity>>> query*/)
    {
        //Query = query;
        // order of properties in the Entity has a low priority - all mentioned properties will be moved up 
        _propertyOrder = 0;
    }

    public virtual FieldColumnBuilder<TProperty, TEntity> Property<TProperty>([NotNullAttribute] Expression<Func<TEntity, TProperty>> propertyExpression)
    {
        _propertyOrder++;
        var bindingProperty = propertyExpression.Body.ToString().ReplaceLambdaVar();
        CreateFieldIfNotExists(typeof(TProperty), bindingProperty);
        var resultField = _fields[bindingProperty];
        resultField.Order = _propertyOrder;

        // Set binding types
        resultField.Binding.TableBinding = ItemsPath;
        resultField.Binding.BindingType = FieldBindingType.TableColumn;
        resultField.Binding.BindingControlType = BindingControlTypes.TableColumnBindingControlType;

        // explicitly mentioned property is not hidden anymore
        resultField.DisplayProperties.Visible = true;
        var result = new FieldColumnBuilder<TProperty, TEntity>(resultField);
        _FieldColumnBuilder = result;

        return result;
    }

    private void CreateFieldIfNotExists(Type propertyType, string bindingProperty)
    {
        if (!_fields.ContainsKey(bindingProperty))
        {
            _fields[bindingProperty] = new FieldControlDetails
            {
                Binding = new FieldBinding
                {
                    Binding = bindingProperty,
                },
                //BindingProperty = bindingProperty,
                DataTypeName = propertyType.Name
            };
        }
    }

    //public virtual void InlineButton(string text, string hint = null)
    //{
    //    var bindingProperty = text;
    //    _fields[bindingProperty] = new DataField { Button = true, BindingProperty = bindingProperty, Label = hint, BindingType = FieldBindingType.ActionButton };
    //}

    public override void AssertValid()
    {
        AssertValid<TEntity>();
    }
}

