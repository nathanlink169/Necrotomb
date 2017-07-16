/******************************************* File Header *******************************************\
 *                                                                                                 *
 * FileName:        ClampedValue                                                                   *
 * FileExtension:   .cs                                                                            *
 * Author:          Nathan Pringle                                                                 *
 * Date:            September 28th, 2016                                                           *
 *                                                                                                 *
 * Helper classes that automatically clamp values between a given maximum and minimum.             *
 *                                                                                                 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR *
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS  *
 * FOR A PARTICULAR PURPOSE.                                                                       *
 *                                                                                                 *
 * V 1.0 - Created File (Nathan Pringle) - September 28th, 2016                                    *
\***************************************************************************************************/

using System;

namespace GameFramework
{
    [Serializable]
    public class ClampedInt
    {
        public ClampedInt(int aValue, int aMinimum, int aMaximum)
        {
            m_Max = aMaximum;
            m_Min = aMinimum;
            Value = aValue;
        }

        public int Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                if (m_Value < MinimumValue) m_Value = MinimumValue;
                if (m_Value > MaximumValue) m_Value = MaximumValue;
            }
        }
        public int MinimumValue
        {
            get { return m_Min; }
            set { if (value > m_Max) return; m_Min = value; Value = m_Value; }
        }
        public int MaximumValue
        {
            get { return m_Max; }
            set { if (value < m_Min) return; m_Max = value; Value = m_Value; }
        }

        private int m_Value;
        private int m_Min;
        private int m_Max;

        // Operators //
        public static int operator +(ClampedInt c1, ClampedInt c2) { return c1.Value + c2.Value; }
        public static int operator -(ClampedInt c1, ClampedInt c2) { return c1.Value - c2.Value; }
        public static int operator *(ClampedInt c1, ClampedInt c2) { return c1.Value * c2.Value; }
        public static int operator /(ClampedInt c1, ClampedInt c2) { return c1.Value / c2.Value; }
        public static int operator +(ClampedInt c1, int c2) { return c1.Value + c2; }
        public static int operator -(ClampedInt c1, int c2) { return c1.Value - c2; }
        public static int operator *(ClampedInt c1, int c2) { return c1.Value * c2; }
        public static int operator /(ClampedInt c1, int c2) { return c1.Value / c2; }

        public static bool operator ==(ClampedInt x, ClampedInt y) { return (x.Value == y.Value); }
        public static bool operator !=(ClampedInt x, ClampedInt y) { return (x.Value != y.Value); }
        public static bool operator ==(ClampedInt x, int y) { return (x.Value == y); }
        public static bool operator !=(ClampedInt x, int y) { return (x.Value != y); }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ClampedInt item = obj as ClampedInt;

            bool i = true;

            if (this.m_Max != item.m_Max)
                i = false;

            if (this.m_Min != item.m_Min)
                i = false;

            if (this.m_Value != item.m_Value)
                i = false;

            return i;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + m_Max.GetHashCode();
            hash = hash * 23 + m_Min.GetHashCode();
            hash = hash * 23 + m_Value.GetHashCode();
            return hash;
        }
    }

    [Serializable]
    public class ClampedFloat
    {
        public ClampedFloat(float aValue, float aMinimum, float aMaximum)
        {
            m_Max = aMaximum;
            m_Min = aMinimum;
            Value = aValue;
        }

        public float Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                if (m_Value < MinimumValue) m_Value = MinimumValue;
                if (m_Value > MaximumValue) m_Value = MaximumValue;
            }
        }
        public float MinimumValue
        {
            get { return m_Min; }
            set { if (value > m_Max) return; m_Min = value; Value = m_Value; }
        }
        public float MaximumValue
        {
            get { return m_Max; }
            set { if (value < m_Min) return; m_Max = value; Value = m_Value; }
        }

        private float m_Value;
        private float m_Min;
        private float m_Max;

        // Operators //
        public static float operator +(ClampedFloat c1, ClampedFloat c2) { return c1.Value + c2.Value; }
        public static float operator -(ClampedFloat c1, ClampedFloat c2) { return c1.Value - c2.Value; }
        public static float operator *(ClampedFloat c1, ClampedFloat c2) { return c1.Value * c2.Value; }
        public static float operator /(ClampedFloat c1, ClampedFloat c2) { return c1.Value / c2.Value; }
        public static float operator +(ClampedFloat c1, float c2) { return c1.Value + c2; }
        public static float operator -(ClampedFloat c1, float c2) { return c1.Value - c2; }
        public static float operator *(ClampedFloat c1, float c2) { return c1.Value * c2; }
        public static float operator /(ClampedFloat c1, float c2) { return c1.Value / c2; }

        public static bool operator ==(ClampedFloat x, ClampedFloat y) { return (x.Value == y.Value); }
        public static bool operator !=(ClampedFloat x, ClampedFloat y) { return (x.Value != y.Value); }
        public static bool operator ==(ClampedFloat x, float y) { return (x.Value == y); }
        public static bool operator !=(ClampedFloat x, float y) { return (x.Value != y); }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ClampedFloat item = obj as ClampedFloat;

            bool i = true;

            if (this.m_Max != item.m_Max)
                i = false;

            if (this.m_Min != item.m_Min)
                i = false;

            if (this.m_Value != item.m_Value)
                i = false;

            return i;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + m_Max.GetHashCode();
            hash = hash * 23 + m_Min.GetHashCode();
            hash = hash * 23 + m_Value.GetHashCode();
            return hash;
        }
    }
}