using JsonDb.Test.Model;

namespace JsonDb.Test;

internal class UTCreateDrop : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
        Context.Drop<User>();
        Context.Drop<Car>();
    }

    [Test]
    public void TestCreate()
    {
        var user = FakerExtensions.User();
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Name, Is.Not.Null);
        Context.InsertUpdate(user);
        Assert.That(File.Exists(Context.FullNameOfFile<User>()));
    }

    [Test]
    public void TestCreateDifferent()
    {
        var user = FakerExtensions.User();
        var car = FakerExtensions.Car();
        Assert.Multiple(() =>
        {
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.Not.Null);
            Assert.That(car, Is.Not.Null);
            Assert.That(car.CarId, Is.Not.Null);
        });

        Context.InsertUpdate(user);
        Context.InsertUpdate(car);
        Assert.Multiple(() =>
        {
            Assert.That(Context.FullNameOfFile<User>(), Does.Exist);
            Assert.That(Context.FullNameOfFile<Car>(), Does.Exist);
            Assert.That(Context.FullNameOfFile<User>(), Is.Not.EqualTo(Context.FullNameOfFile<Car>()));
        });
    }

    [Test]
    public void TestDelete()
    {
        var user = FakerExtensions.User();
        Context.InsertUpdate(user);
        Assert.That(Context.FullNameOfFile<User>(), Does.Exist);
        Context.Drop<User>();
        Assert.That(Context.FullNameOfFile<User>(), Does.Not.Exist);
    }
}