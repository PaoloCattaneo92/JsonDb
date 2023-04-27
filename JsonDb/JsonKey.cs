using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDb;

[AttributeUsage(AttributeTargets.Property)]
public class JsonKey : Attribute
{
}
