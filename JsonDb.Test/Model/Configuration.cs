using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JsonDb.Test.Model;

record Configuration(int Id, List<Parameter> Parameters);
