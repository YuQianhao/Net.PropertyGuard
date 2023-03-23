using PropertyGuard.Attribute;
using PropertyGuard.Boot;
using PropertyGuard.Exception;

namespace Test;

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