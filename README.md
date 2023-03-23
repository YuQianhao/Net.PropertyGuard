# PropertyGuard

属性警卫是一个用于检查对象属性是否符合规则的工具。

```c#
public class Program
{
    public string Name { get; set; } = "";

    public int Age { get; set; }

    public static void Function(Program program)
    {

        if (string.IsNullOrEmpty(program.Name))
        {
            throw new Exception("名字不能是空的。");
        }

        if (program.Age < 18)
        {
            throw new Exception("这个人没有成年。");
        }
    }

}
```

如上面的代码片段所示，通常我们在做业务检查的时候，会针对每一个属性进行单独的检查。对于属性庞大并且应用的位置很多的场景下，这个方式显得不是那么友好。我们可以使用属性警卫来解决这个问题。

```c#
public class Program
{
    public string Name { get; set; } = "";

    public int Age { get; set; }

    public static void Function(Program program)
    {

        if (string.IsNullOrEmpty(program.Name))
        {
            throw new Exception("名字不能是空的。");
        }

        if (program.Age < 18)
        {
            throw new Exception("这个人没有成年。");
        }
    }

}
```

