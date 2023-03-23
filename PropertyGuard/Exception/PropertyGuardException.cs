using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PropertyGuard.Attribute;

namespace PropertyGuard.Exception
{
    /// <summary>
    /// 属性守卫抛出的异常
    /// </summary>
    public class PropertyGuardException : System.Exception
    {

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

        public PropertyGuardException(string? message, Type objectType, PropertyInfo property, System.Attribute guardAttribute, object targetObject, object? targetFieldValue) : base(message)
        {
            ObjectType = objectType;
            Property = property;
            GuardAttribute = guardAttribute;
            TargetObject = targetObject;
            TargetFieldValue = targetFieldValue;
        }
    }
}
