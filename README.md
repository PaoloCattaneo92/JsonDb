# JsonDb
Simplest database ever (database as json files in a data folder). 

Very useful when you have a simple application to fetch some data locally. The data are stored into several JSON files (one for every type collection).

## Installation
Install as Nuget (search for **SimpleJsonDb**), find it or [nuget.org](https://www.nuget.org/packages/SimpleJsonDb/)

## Getting started
- Have your model classes, like User or Car
- Add to a property the [JsonKey] attribute (optional if there is a property named "Id"), this is used because the method *InsertUpdate* tries to update the object with same key (update). If not found then it insert at the end of the list

~~~csharp
record User([property: JsonKey]string Name, string Email);
record Car(string Id, string Serial); //automatically consider "Id" as key for the object


var user = new User("Paolo Cattaneo", 31);
var car = new Car("Dacia Sandero", "123456");
var context = new JsonContext("C:/pathto/data");

context.InsertUpdate(user); //automatically creates file "user.json" in data folder
context.InsertUpdate(car);

context.Delete(user); //delete the user inserted from "user.json" and leave it as empty list
context.Drop<Car>(); //delete the whole "car.json" file
~~~

## List update
You can update lists in bulk
~~~csharp
var users = new List<User>() 
{
  new User("Paolo Cattaneo", 32),
  new User("Clara Ferro", 23)
};

context.InsertUpdateList(users); //update the "user.json" file with the update list of users

~~~
