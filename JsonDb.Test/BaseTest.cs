using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDb.Test;

internal abstract class BaseTest
{
    protected static string DATA_FOLDER = "data";

    protected JsonContext Context { get; }

    protected BaseTest()
    {
        Context = new JsonContext(DATA_FOLDER);
    }
}
