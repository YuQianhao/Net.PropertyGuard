using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Attribute
{

    /// <summary>
    /// 字符串内容空检查
    /// <para>被标记的属性转换为字符串之后如果是null或者是空的将会抛出异常。</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyTextNotEmpty : System.Attribute
    {
        public PropertyTextNotEmpty(string message = "")
        {
            Message = message;
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; }

    }
}
