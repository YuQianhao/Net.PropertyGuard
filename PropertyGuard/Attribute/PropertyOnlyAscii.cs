using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Attribute
{
    /// <summary>
    /// 属性内容仅支持ASCII编码的特性。使用这个特性标注的字段转换成字符串后，只能包含ASCII字符编码。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyOnlyAscii : System.Attribute
    {

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 是否允许null
        /// </summary>
        public bool NotNull { get; }

        public PropertyOnlyAscii(bool notNull = false, string message = "")
        {
            Message = message;
            NotNull = false;
        }
    }
}
