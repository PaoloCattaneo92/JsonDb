using JsonDb;

User FakeUser()
{
    return new User(
        Faker.RandomNumber.Next(1000, 9999),
        Faker.Name.FullName(),
        Faker.Internet.Email()
        );
}

Configuration FakeConfiguration()
{
    return new Configuration(
        Faker.RandomNumber.Next(1000, 9999),
        FakeParameters(Faker.RandomNumber.Next(2, 5))
        );
}

Parameter FakeParameter()
{
    return new Parameter("Parameter_" + Faker.RandomNumber.Next(1000, 9999), "Value_" + Faker.RandomNumber.Next(1000, 9999));
}

List<Parameter> FakeParameters(int parameters)
{
    var result = new List<Parameter>();
    for (int i = 0; i < parameters; i++)
    {
        result.Add(FakeParameter());
    }
    return result;
}

var users = new List<User>();
for (int i = 0; i < 5; i++)
{
    users.Add(FakeUser());
}

var configurations = new List<Configuration>();
for (int i = 0; i < 3; i++)
{
    configurations.Add(FakeConfiguration());
}

var context = new JsonContext(Path.Combine(Directory.GetCurrentDirectory(), "data"));
context.InsertUpdateList(users);
context.InsertUpdateList(configurations);
context.InsertUpdate(new Car("DaciaSandero", "12345", 1));
context.InsertUpdate(new Car("Toyota", "Yaris2", 43));
context.Delete<Car>("Toyota");
context.Delete<Car>("DaciaSandero");

record User(int Id, string Name, string Email);

record Configuration(int Id, List<Parameter> Parameters);

record Parameter(string Key, string Value);

record Car([property:JsonKey]string CarId, string Serial, int OwnerdId);


