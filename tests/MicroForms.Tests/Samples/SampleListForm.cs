using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroForms.Tests.Samples;

public class SampleEditForm : Form<Model1>
{
    protected override void Define()
    {
        Build(f => 
        {
            f.Property(f => f.Id);
        });
    }
}

public class SampleListForm : Form<SampleListForm>
{
    public List<Model1> Data = [];

    protected override void Define()
    {
        BuildFromList(p => p.Data, e =>
        {
            e.Property(p => p.Id);
        });
    }
}

public class SampleQueryForm : Form<Model1>
{
    protected override void Define()
    {
        BuildFromQuery(LoadData, e =>
        {
            e.Property(p => p.Id);
        });
    }

    private async Task<IEnumerable<Model1>> LoadData(QueryOptions options)
    {
        throw new NotImplementedException();
    }
}

public class Model1
{
    public int Id { get; set; }
    public string Name { get; set; }
}

