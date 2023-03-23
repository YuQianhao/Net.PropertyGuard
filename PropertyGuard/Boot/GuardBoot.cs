using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PropertyGuard.Attribute;
using PropertyGuard.Exception;
using PropertyGuard.Model;

namespace PropertyGuard.Boot
{
    public class GuardBoot
    {

        /// <summary>
        /// 自定义属性属性守卫处理器
        /// <para>当属性警卫匹配到相关联的特性时，将会调用这个函数。</para>
        /// </summary>
        /// <param name="propertyGuardItem">接受检查的属性</param>
        public delegate void FieldCustomHandler(PropertyGuardItem propertyGuardItem);

        /// <summary>
        /// 自定义属性属性守卫事件
        /// </summary>
        public static event FieldCustomHandler? OnFieldCustomHandler;

        public static void Filter(object targetObject)
        {
            Type type = targetObject.GetType();
            // 获取这个类型的属性
            foreach (var fieldInfo in type.GetProperties())
            {
                var customAttribute = fieldInfo.GetCustomAttributes(false);
                foreach (var attribute in customAttribute)
                {
                    var item = new PropertyGuardItem(type, fieldInfo, (System.Attribute)attribute, targetObject, fieldInfo.GetValue(targetObject));
                    switch (item.GuardAttribute)
                    {
                        case PropertyNotNull fieldNotNull when item.TargetFieldValue == null:
                            throw new PropertyGuardException(fieldNotNull.Message, type, fieldInfo, fieldNotNull, targetObject, item.TargetFieldValue);
                        case PropertyNumberRange fieldNumberRange:
                            {
                                if (item.TargetFieldValue is not double)
                                {
                                    throw new PropertyGuardException(fieldNumberRange.Message, type, fieldInfo, fieldNumberRange, targetObject, item.TargetFieldValue);
                                }
                                var targetValue = double.Parse(item.TargetFieldValue.ToString() ?? string.Empty);
                                if (targetValue > fieldNumberRange.Max || targetValue < fieldNumberRange.Min)
                                {
                                    throw new PropertyGuardException(fieldNumberRange.Message, type, fieldInfo, fieldNumberRange, targetObject, item.TargetFieldValue);
                                }
                                break;
                            }
                        case PropertyTextLength fieldTextLength:
                            {
                                if (item.TargetFieldValue == null)
                                {
                                    throw new PropertyGuardException(fieldTextLength.Message, type, fieldInfo, fieldTextLength, targetObject, item.TargetFieldValue);
                                }
                                var text = item.TargetFieldValue.ToString();
                                if (text == null || text.Length > fieldTextLength.Max || text.Length < fieldTextLength.Min)
                                {
                                    throw new PropertyGuardException(fieldTextLength.Message, type, fieldInfo, fieldTextLength, targetObject, item.TargetFieldValue);
                                }
                                break;
                            }
                        case PropertyTextNotEmpty fieldTextNotEmpty:
                            {
                                if (item.TargetFieldValue == null || string.IsNullOrEmpty(item.TargetFieldValue.ToString()))
                                {
                                    throw new PropertyGuardException(fieldTextNotEmpty.Message, type, fieldInfo, fieldTextNotEmpty, targetObject, item.TargetFieldValue);
                                }
                                break;
                            }
                        default:
                            {
                                OnFieldCustomHandler?.Invoke(item);
                                break;
                            }
                    }
                }
            }
        }

    }
}
