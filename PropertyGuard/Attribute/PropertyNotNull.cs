using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Attribute
{

    /// <summary>
    /// 对象空检查
    /// <para>被标记的属性会检查是否是null，如果是null将会触发</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNotNull : System.Attribute
    {
        public PropertyNotNull(string message = "")
        {
            Message = message;
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; }



    }
}
