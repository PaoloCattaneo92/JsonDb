using JsonDb.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDb.Test;

internal static class FakerExtensions
{
    internal static User User() => new(
        Faker.RandomNumber.Next(1000, 9999),
        Faker.Name.FullName(),
        Faker.Internet.Email()
        );

    internal static Car Car() => new(
        Faker.Identification.UkPassportNumber(),
        Faker.Identification.UkNhsNumber(),
        Faker.RandomNumber.Next(1000, 9999));

    internal static List<Parameter> Parameters(int parameters)
    {
        var result = new List<Parameter>();
        for (int i = 0; i < parameters; i++)
        {
            result.Add(Parameter());
        }
        return result;
    }

    internal static Parameter Parameter() => new ("Parameter_" + Faker.RandomNumber.Next(1000, 9999), "Value_" + Faker.RandomNumber.Next(1000, 9999));

    internal static Configuration Configuration() =>
        new (
        Faker.RandomNumber.Next(1000, 9999),
        Parameters(Faker.RandomNumber.Next(2, 5))
        );
}
