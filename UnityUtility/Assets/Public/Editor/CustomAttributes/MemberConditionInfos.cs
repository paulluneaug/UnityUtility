
using System;
using System.Reflection;

namespace UnityUtility.CustomAttributes.Editor
{
    public interface IMemberConditionInfo
    {
        public object GetValue(object obj);
        public Type GetMemberType();
    }

    public class FieldConditionInfos : IMemberConditionInfo
    {
        private readonly FieldInfo m_fieldInfos;
        private readonly IMemberConditionInfo m_parentNestedMember;

        public FieldConditionInfos(FieldInfo fieldInfos, IMemberConditionInfo parentNestedMember)
        {
            m_fieldInfos = fieldInfos;
            m_parentNestedMember = parentNestedMember;
        }

        public Type GetMemberType()
        {
            return m_fieldInfos.FieldType;
        }

        public object GetValue(object serializedObject)
        {
            if (m_parentNestedMember == null)
            {
                return m_fieldInfos.GetValue(serializedObject);
            }
            return m_fieldInfos.GetValue(m_parentNestedMember.GetValue(serializedObject));
        }
    }

    public class PropertyConditionInfos : IMemberConditionInfo
    {
        private readonly PropertyInfo m_propertyInfos;
        private readonly IMemberConditionInfo m_parentNestedMember;

        public PropertyConditionInfos(PropertyInfo fieldInfos, IMemberConditionInfo parentNestedMember)
        {
            m_propertyInfos = fieldInfos;
            m_parentNestedMember = parentNestedMember;
        }

        public Type GetMemberType()
        {
            return m_propertyInfos.PropertyType;
        }

        public object GetValue(object serializedObject)
        {
            if (m_parentNestedMember == null)
            {
                return m_propertyInfos.GetValue(serializedObject);
            }
            return m_propertyInfos.GetValue(m_parentNestedMember.GetValue(serializedObject));
        }
    }
}