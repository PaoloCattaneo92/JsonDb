using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDb.Test.Model;

record Car([property: JsonKey] string CarId, string Serial, int OwnerdId);
