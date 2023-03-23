using PropertyGuard.Attribute;
using PropertyGuard.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuard.Model
{
    /// <summary>
    /// 接受检查的属性
    /// </summary>
    public class PropertyGuardItem
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

        public PropertyGuardItem(Type objectType, PropertyInfo property, System.Attribute guardAttribute, object targetObject, object? targetFieldValue)
        {
            ObjectType = objectType;
            Property = property;
            GuardAttribute = guardAttribute;
            TargetObject = targetObject;
            TargetFieldValue = targetFieldValue;
        }

        /// <summary>
        /// 抛出这个属性的检查异常
        /// </summary>
        /// <param name="message">异常信息</param>
        public void Throw(string message)
        {
            throw new PropertyGuardException(message, ObjectType, Property, GuardAttribute, TargetObject, TargetFieldValue);
        }
    }
}
