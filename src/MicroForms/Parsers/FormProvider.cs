using MicroForms.Exceptions;
using MicroForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms.Parsers;

public interface IFormProvider
{
    FormDetails GetFormDefinition(string name);
    Task<IEnumerable<T>> GetQueryResult<T>(string name, QueryOptions options) where T: class;
}

public class FormProvider : IFormProvider
{
    private readonly IServiceProvider _services;

    public FormProvider(IServiceProvider services)
    {
        _services = services;
    }

    public FormDetails GetFormDefinition(string name)
    {
        var form = CreateForm(name);
        var formDetails = form.GetFormDefinition();
        return formDetails;
    }

    public async Task<IEnumerable<T>> GetQueryResult<T>(string name, QueryOptions options) where T : class
    {
        var form = CreateForm(name) as Form<T>;

        if (form == null)
        {
            throw new InvalidDependencyException($"Cannot instantiate form generic type {name}");
        }

        var result = await form.Query(options);
        return result;
    }

    private Form CreateForm(string name)
    {
        var type = TypeHelper.ResolveType(name);

        if (type == null)
        {
            throw new InvalidDependencyException($"Cannot resolve type {name}");
        }

        var form = _services.GetService(type) as Form;

        if (form == null)
        {
            throw new InvalidDependencyException($"Cannot instantiate type {type.FullName}");
        }

        return form;
    }
}
