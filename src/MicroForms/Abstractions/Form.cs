using MicroForms.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms;

public abstract class Form : IModelDefinitionForm
{
    public string DisplayName => throw new NotImplementedException();

    public Type ChildProcess => throw new NotImplementedException();

    public string ItemsPath => throw new NotImplementedException();

    public IEnumerable<DataField> GetDetailsFields()
    {
        throw new NotImplementedException();
    }

    public Type GetDetailsType()
    {
        throw new NotImplementedException();
    }

    public virtual FormDetails GetFormDefinition()
    {
        throw new NotImplementedException();
    }
}

public abstract class Form<TModel> : Form
    where TModel : class
{
    protected FormEntityTypeBuilder<TModel> _builder;
    protected FormListBuilder<TModel> _builderList;

    protected ListFromQueryBuilder<TModel> _fromQueryBuilder;
    public Func<QueryOptions, Task<IEnumerable<TModel>>> Query { get; internal set; }
    public string DisplayName { get; private set; }
    public Type ChildProcess { get; private set; }
    public string ItemsPath { get; private set; }

    /// <summary>
    /// Should use Build or Columns (for List Form) methods to defind a form
    /// </summary>
    protected abstract void Define();

    public Form()
    {
        //_dataFieldProcessor = new DefaultDataFieldProcessor();
        _builder = new();
        _fromQueryBuilder = new();
        Define();
        _builder.AssertValid();

        DisplayName = _builder.DisplayName;
        ChildProcess = _builder.ChildProcess;
        //Access = _builder.Access;
    }

    /// <summary>
    /// Returns all form definition
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override FormDetails GetFormDefinition()
    {
        var form = new FormDetails();
        return form;
    }

    protected void BuildListFromQuery(Func<QueryOptions, Task<IEnumerable<TModel>>> query,
        [NotNull] Action<ListTypeBuilder<TModel>> buildAction)
    {
        Query = query;
        _fromQueryBuilder.WithQuery(query, buildAction);
    }

    protected void Build([NotNull] Action<FormEntityTypeBuilder<TModel>> buildAction)
    {
        buildAction(_builder);
    }

    //protected void BuildList<TKey>(Expression<Func<TModel, IEnumerable<TKey>>> items,
    //    [NotNull] Action<FormListTypeBuilder<TKey>> buildAction)
    //    where TKey : class
    //{
    //    _builderList.List(items, buildAction);
    //}


    public Type GetDetailsType()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DataField> GetDetailsFields()
    {
        throw new NotImplementedException();
    }

    
}
