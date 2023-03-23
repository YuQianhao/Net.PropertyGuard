using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Attribute
{
    /// <summary>
    /// 数值范围检查
    /// <para>被标记的属性，转换成<see cref="double"/>后，与<value>Min</value>和<value>Max</value>匹配检查，不在指定的范围将抛出异常。</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNumberRange : System.Attribute
    {
        /// <summary>
        /// 最小值，默认为0。
        /// </summary>
        public double Min { get; }

        /// <summary>
        /// 最大值，默认为<see cref="double" />的最大值。
        /// </summary>
        public double Max { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; }

        public PropertyNumberRange(double min = 0, double max = double.MaxValue, string message = "")
        {
            Min = min;
            Max = max;
            Message = message;
        }
    }
}
