using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace MicroForms;

public abstract class Form<TModel> : IModelDefinitionForm
    where TModel : class
{
    //protected readonly IDataFieldProcessor _dataFieldProcessor;
    protected FormEntityTypeBuilder<TModel> _builder;
    protected FormListBuilder<TModel> _builderList;

    //private Dictionary<Type, string> _formats = new Dictionary<Type, string>()
    //{
    //    { typeof(DateTime), "dd/MM/yyyy" }
    //    ,{ typeof(DateTime?), "dd/MM/yyyy" }
    //};

    public Form()
    {
        //_dataFieldProcessor = new DefaultDataFieldProcessor();
        _builder = new FormEntityTypeBuilder<TModel>();
        Define();
        _builder.AssertValid();

        DisplayName = _builder.DisplayName;
        ChildProcess = _builder.ChildProcess;
        //Access = _builder.Access;
    }

    public string DisplayName { get; private set; }
    public Type ChildProcess { get; private set; }
    //public FormAllowAccess Access { get; private set; }
    public string ItemsPath { get; private set; }
    //protected abstract void Define(FormEntityTypeBuilder<TModel> builder);

    /// <summary>
    /// Should use Build or Columns (for List Form) methods to defind a form
    /// </summary>
    protected abstract void Define();

    protected void Build([NotNull] Action<FormEntityTypeBuilder<TModel>> buildAction)
    {
        buildAction(_builder);
    }

    protected void BuildFromList<TKey>(Expression<Func<TModel, IEnumerable<TKey>>> items, 
        [NotNull] Action<FormListTypeBuilder<TKey>> buildAction)
        where TKey : class
    {
        _builderList.List(items, buildAction);
    }

    protected void BuildFromQuery<TKey>(Func<QueryOptions, Task<IEnumerable<TKey>>> query,
        [NotNull] Action<FormListTypeBuilder<TKey>> buildAction)
        where TKey : class
    {
        //_builderList.List(items, buildAction);
    }

    IEnumerable<DataField> IModelDefinitionForm.GetDetailsFields()
    {
        var fields = new List<DataField>();
        fields.AddRange(_builder.Fields.OrderBy(f => f.Order));
        var repeaterFields = _builder.RepeaterBuilders.SelectMany(r => r.Fields.OrderBy(f => f.Order));
        fields.AddRange(repeaterFields);

        if (fields.Any())
        {
            //_dataFieldProcessor.PrepareFields(fields.ToList(), GetDetailsType());
        }

        return fields;
    }

    public Type GetDetailsType()
    {
        return typeof(TModel);
    }

    //public IEnumerable<DialogButtonDetails> GetButtons()
    //{
    //    var result = _builder.ActionButtons; 
    //    return result;
    //}

    //public IEnumerable<IBindingFlowReference> GetButtonNavigations()
    //{
    //    var result = new List<IBindingFlowReference>();
    //    return result;
    //}

    // ToDo: should it be moved to DataEntryProvider?
    //public string GetFieldFormat(DataField field)
    //{
    //    var format = field.Format ?? FindDefaultFormat(field.DataType);
    //    return format;
    //}


    //private string FindDefaultFormat(Type dataType)
    //{
    //    if (_formats.ContainsKey(dataType))
    //    {
    //        return _formats[dataType];
    //    }

    //    return "";
    //}

    //public IEnumerable<ActionRouteLink> GetContextLinks()
    //{
    //    return new List<ActionRouteLink>();
    //}
}
