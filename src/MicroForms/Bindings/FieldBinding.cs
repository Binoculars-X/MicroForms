using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MicroForms.Bindings;

public class FieldBinding //: IFieldBinding, IFastReflectionBinding
{
    public const string ColumnIndexMarker = "[__index]";

    //[JsonIgnore]
    //public Func<object, object> FastReflectionGetter { get; set; }

    //[JsonIgnore]
    //public Func<object, object> FastReflectionNameGetter { get; set; }

    //[JsonIgnore]
    //public Func<object, object> FastReflectionIdGetter { get; set; }

    //[JsonIgnore]
    //public Func<object, object> FastReflectionItemsGetter { get; set; }

    //[JsonIgnore]
    //public Func<object, object> FastReflectionTableGetter { get; set; }

    //[JsonIgnore]
    //public Action<object, object> FastReflectionSetter { get; set; }

    public string Binding { get; set; }

    public string ItemsBinding { get; set; }

    public string IdBinding { get; set; }

    public string NameBinding { get; set; }

    public string TableBinding { get; set; }

    public string TargetBinding { get; set; }

    public int? RowIndex { get; set; }

    //public BindingParameters Parameters { get; set; }

    public FieldBindingType BindingType { get; set; }

    public string BindingControlType { get; set; }

    //public ActionType ActionType { get; set; }

    //public List<BindingFlowAction> ContextMenuActions { get; set; }

    public string FilterRefField { get; set; }

    public string FilterType { get; set; }

    public bool IsResolved
    {
        get
        {
            string key = Key;
            return ((key != null) ? new bool?(!key.Contains("[__index]")) : null).GetValueOrDefault();
        }
    }

    public string TemplateKey
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(TableBinding))
            {
                return TableBinding + "[__index]" + Binding?.Replace("$", "");
            }

            return Binding;
        }
    }

    public string Key
    {
        get
        {
            if (RowIndex.HasValue)
            {
                return GetResolvedKey(RowIndex.Value);
            }

            return TemplateKey;
        }
    }

    public string ResolvedBinding
    {
        get
        {
            if (BindingType == FieldBindingType.SelectableList)
            {
                return TargetBinding;
            }

            return Key;
        }
    }

    public FieldBinding CopyWithKey()
    {
        return new FieldBinding
        {
            Binding = Binding,
            ItemsBinding = ItemsBinding,
            IdBinding = IdBinding,
            NameBinding = NameBinding,
            TableBinding = TableBinding,
            TargetBinding = TargetBinding,
            BindingType = BindingType,
            BindingControlType = BindingControlType,
            RowIndex = RowIndex,
            FilterType = FilterType,
            FilterRefField = FilterRefField
        };
    }

    public FieldBindingPathType GetPathType()
    {
        if (BindingType == FieldBindingType.Form || Binding == null || BindingType == FieldBindingType.Repeater || BindingType == FieldBindingType.Table || BindingType == FieldBindingType.ActionButton || BindingType == FieldBindingType.RepeaterActionButton)
        {
            return FieldBindingPathType.Unsupported;
        }

        if (BindingType == FieldBindingType.SingleSelect)
        {
            return FieldBindingPathType.SingleSelect;
        }

        if (BindingType == FieldBindingType.TableColumn || BindingType == FieldBindingType.TableColumnSingleSelect || BindingType == FieldBindingType.ListCard)
        {
            return FieldBindingPathType.Column;
        }

        return FieldBindingPathType.Straight;
    }

    public void ResolveKey(FieldBindingArgs args)
    {
        RowIndex = args.RowIndex;
    }

    public string GetResolvedKey(int rowIndex)
    {
        return TemplateKey.Replace("[__index]", $"[{rowIndex}]");
    }
}

public class FieldBindingArgs
{
    public int RowIndex { get; set; }
}
