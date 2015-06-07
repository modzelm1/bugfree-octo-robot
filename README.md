# bugfree-octo-robot
### C# string tools - complex string search

This solution can be used for searching expression inside string data.

To use it first create SearchManager instance:

```cs
var sm = new SearchManager();
```

Then you can call BuildStringQueryFunction and get function which can be used for filtering string data.

```cs
var fun = sm.BuildStringQueryFunction(query);
```

Function fun returns true when pased string parameter satisfies query. You can use complex expressions for query parameters:

```cs
var query = "cat dog";

var query = "cat OR dog";

var query = " ( \"fish cat\"  pig) OR  dog";

var query = " ( \"cat pig dog fly\" OR cat AND tiger)  snake fly (lion \"tiger\")";
```

In ConsoleApplicationTest project you can find example how to use generated function for filtering data in list:

```cs
var query = "small OR cat";

var sm = new SearchManager();
var fun = sm.BuildStringQueryFunction(query);

//using generated function fun for filtering list
foreach (var item in GetTestData().Where(x=>fun(x.description)))
{
    Console.WriteLine("Pet: {0}, {1}", item.name, item.description);
}
```
