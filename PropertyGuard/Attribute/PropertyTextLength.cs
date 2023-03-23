using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Attribute
{
    /// <summary>
    /// 文本长度检查
    /// <para>被标记的属性将会转换为<see cref="string"/>后检查长度，如果长度不在<value>Min</value>和<value>Max</value>之间将抛出异常。</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyTextLength : System.Attribute
    {
        /// <summary>
        /// 字符串最小长度，默认为0。
        /// </summary>
        public uint Min { get; }

        /// <summary>
        /// 字符串最大长度，默认为<see cref="uint"/>的最大值。
        /// </summary>
        public uint Max { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; }

        public PropertyTextLength(uint min = 0, uint max = 0, string message = "")
        {
            Min = min;
            Max = max;
            Message = message;
        }
    }
}
