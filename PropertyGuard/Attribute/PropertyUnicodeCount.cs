using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Attribute
{
    /// <summary>
    /// 字段内容的Unicode数据长度特性。
    /// <para>这个特性会将被注解的字段转换为UTF-8，在此之后计算实际占用的字符数量。</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyUnicodeCount : System.Attribute
    {
        /// <summary>
        /// 最小长度
        /// </summary>
        public uint Min;

        /// <summary>
        /// 最大长度
        /// </summary>
        public uint Max;

        /// <summary>
        /// 不符合规则时的报错
        /// </summary>
        public string Message;

        public PropertyUnicodeCount(uint min = 0, uint max = uint.MaxValue, string message = "")
        {
            Min = min;
            Max = max;
            Message = message;
        }
    }
}
