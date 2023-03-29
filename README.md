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
    
    public static void Main()
    {
        Function(new Program());
    }

}
```

如上面的代码片段所示，通常我们在做业务检查的时候，会针对每一个属性进行单独的检查。对于属性庞大并且应用的位置很多的场景下，这个方式显得不是那么友好。我们可以使用属性警卫来解决这个问题。我将上面的代码片段稍微改动了一下。

```c#
public class Program
{
    [PropertyTextLength(min: 6, max: 16, message: "你的名字不能小于6位，并且不能超过16位。")]
    public string Name { get; set; } = "";

    [PropertyNumberRange(min: 18, message: "至少需要等待18岁才可以继续完成。")]
    public int Age { get; set; }

    public static void Function(Program program)
    {
        try
        {
            GuardBoot.Filter(program);
        }
        catch (PropertyGuardException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
    
    public static void Main()
    {
        Function(new Program());
    }
}
```

运行结果如下所示。

```text
> 你的名字不能小于6位，并且不能超过16位。
```

## 一、如何引用

```text
# 当前的最新版本是1.0.7
dotnet add package PropertyGuard --version 1.0.7
```

或者可以直接使用``Visual Sdutio``直接搜索``PropertyGuard``。

## 二、如何使用

我们使用了``特性``这个概念来完成属性警卫的标注。属性警卫只能被标注在``属性``中，不能被标记在``字段``中。我们提前预设了几个常用的属性警卫。

### 1、预设的警卫特性

#### （1）、空检查

```c#
PropertyNotNull(string message = "");
```

这个特性可以用于任何类型的对象，当被标注的对象是空的时候，属性警卫将会触发异常，并且抛出``message``的值。

#### （2）、数字范围检查

```c#
PropertyNumberRange(double min = 0, double max = double.MaxValue, bool notNull = false, string message = "");
```

这个特性只可以用于能够被强制转换为``double``的对象。将被转换的对象与``min``和``max``做检查，不在这个范围内的时候将``message``通过异常抛出。或者表示``notNull``开启状态并且被标注的值时null时抛出异常。

#### （3）、文本长度检查

```c#
PropertyTextLength(uint min = 0, uint max = uint.MaxValue, bool notNull = false, bool onlyAscii = false, string message = "");
```

这个特性可以用于任何类型的对象。被标注的对象将会主动调用``ToString()``方法获取字符串和长度，如果长度不在``min``和``max``的范围内时，会通过异常把``message``的信息返回。当标记``notNull``开启时，被标注的字段是null，或者标记``onlyAscii``开启时，被标注的字符串不符合ASCII编码时抛出异常。

#### （4）、文本可空检查

```c#
PropertyTextNotEmpty(string message = "");
```

这个特性可以用于任何类型的对象。被标注的对象将会主动调用``ToString()``方法获取字符串和长度，如果这个字符串是空的，或者被标注的对象是``null``，也将会抛出异常。

#### （5）、UTF8字节长度检查

```C#
PropertyUnicodeCount(uint min = 0, uint max = uint.MaxValue, bool notNull = false, string message = "");
```

这个特性用于任何类型的对象，被标注的对象将会调用``Encoding.UTF8.GetByteCount()``来获取实际占用的Unicode字符编码长度，如果不符合规则将会抛出异常。当标记``notNull``开启时，被标注的字段是null将抛出异常。

#### （6）、ASCII字符串编码检查

```C#
PropertyOnlyAscii(bool notNull = false, string message = "");
```

这个特性用于任何类型的对象，被标注的对象会被检查转换成字符串之后，所有的字符内容是否是ASCII字符编码。如果不是将会抛出异常。当标记``notNull``开启时，被标注的对象是``null``，也将会抛出异常。

### 2、异常

我们定义了一个名为``PropertyGuardException``的异常类型来操作属性警卫抛出的错误信息。这个异常继承自``Exception``。

```c#
PropertyGuardException : System.Exception
```

所以具备了Exception的一切属性，当然，再捕获的时候也可以直接捕获Exception，但是我们在这个异常类型中额外增加了一下几个不可修改的属性，如果需要的话可以捕获为``PropertyGuardException``去获取使用。

```c#
/// <summary>
/// 错误信息
/// </summary>
public string Message { get; }

/// <summary>
/// 被检查的类型
/// </summary>
public Type ObjectType { get; }

/// <summary>
/// 被检查的属性
/// </summary>
public PropertyInfo Property { get; }

/// <summary>
/// 触发这个异常的特性
/// </summary>
public System.Attribute GuardAttribute { get; }

/// <summary>
/// 被检查的对象
/// </summary>
public object TargetObject { get; }

/// <summary>
/// 目标属性值
/// </summary>
public object? TargetFieldValue { get; }
```

最后，它还包含一个如下所示的构造方法。

```c#
PropertyGuardException(string? message, Type objectType, PropertyInfo property, System.Attribute guardAttribute, object targetObject, object? targetFieldValue);
```

### 3、使用

属性警卫的使用非常简单，只需要调用一个方法即可。

```c#
GuardBoot.Filter(object targetObject);
```

### 4、自定义

我们也支持自定义一个警卫，你只需要继承``System.Attribute``去实现一个只能被标记在``AttributeTargets.Property``上的特性即可。字段警卫也提供了一个能够拦截并处理警卫特性的事件。

```c#
GuardBoot.OnFieldCustomHandler += (PropertyGuardItem propertyGuardItem) =>
{

};
```

注册这个事件，属性警卫在处理**不是预设类型**的属性特性时，将会通知这些事件。事件会传递一个``PropertyGuardItem``类型的参数给你使用。这个参数能够获取到关于被标记字段的一些有用信息，如下所示。

```c#
/// <summary>
/// 错误信息
/// </summary>
public string Message { get; }

/// <summary>
/// 被检查的类型
/// </summary>
public Type ObjectType { get; }

/// <summary>
/// 被检查的属性
/// </summary>
public PropertyInfo Property { get; }

/// <summary>
/// 触发这个异常的特性
/// </summary>
public System.Attribute GuardAttribute { get; }

/// <summary>
/// 被检查的对象
/// </summary>
public object TargetObject { get; }

/// <summary>
/// 目标属性值
/// </summary>
public object? TargetFieldValue { get; }
```

没错，与``PropertyGuardException``异常的字段完全一致。你可以在这里处理你自己定义的属性警卫特性，如果被检查不通过，可以直接调用``propertyGuardItem.Throw``方法抛出异常即可，这个方法的原型如下。

```c#
PropertyGuardItem.Throw(string message);
```

