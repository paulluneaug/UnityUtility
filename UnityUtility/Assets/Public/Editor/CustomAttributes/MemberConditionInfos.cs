
using System;
using System.Reflection;

namespace UnityUtility.CustomAttributes.Editor
{
    public interface IMemberConditionInfo
    {
        object GetValue(object obj);
        Type GetMemberType();
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

    public class ArrayFieldConditionInfos : IMemberConditionInfo
    {
        private readonly FieldInfo m_fieldInfos;
        private readonly IMemberConditionInfo m_parentNestedMember;
        private readonly int m_arrayIndex;

        public ArrayFieldConditionInfos(FieldInfo fieldInfos, int arrayIndex, IMemberConditionInfo parentNestedMember)
        {
            m_fieldInfos = fieldInfos;
            m_parentNestedMember = parentNestedMember;
            m_arrayIndex = arrayIndex;
        }

        public Type GetMemberType()
        {
            return m_fieldInfos.FieldType.GetElementType();
        }

        public object GetValue(object serializedObject)
        {
            Array array;
            if (m_parentNestedMember == null)
            {
                array = (Array)m_fieldInfos.GetValue(serializedObject);
            }
            else
            {
                array = (Array)m_fieldInfos.GetValue(m_parentNestedMember.GetValue(serializedObject));
            }

            return array.GetValue(m_arrayIndex);
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

    public class ArrayPropertyConditionInfos : IMemberConditionInfo
    {
        private readonly PropertyInfo m_propertyInfos;
        private readonly IMemberConditionInfo m_parentNestedMember;
        private readonly int m_arrayIndex;

        public ArrayPropertyConditionInfos(PropertyInfo fieldInfos, int arrayIndex, IMemberConditionInfo parentNestedMember)
        {
            m_propertyInfos = fieldInfos;
            m_parentNestedMember = parentNestedMember;
            m_arrayIndex = arrayIndex;
        }

        public Type GetMemberType()
        {
            return m_propertyInfos.PropertyType.GetElementType();
        }

        public object GetValue(object serializedObject)
        {
            Array array;
            if (m_parentNestedMember == null)
            {
                array = (Array)m_propertyInfos.GetValue(serializedObject);
            }
            else
            {
                array = (Array)m_propertyInfos.GetValue(m_parentNestedMember.GetValue(serializedObject));
            }

            return array.GetValue(m_arrayIndex);
        }
    }
}