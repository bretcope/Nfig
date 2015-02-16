﻿using System;
using System.Linq;

namespace Nfig
{
    public class SettingConverterAttribute : Attribute
    {
        public object Converter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converterType">The type must implement SettingConverter&lt;T&gt; where T is the property type of the setting.</param>
        public SettingConverterAttribute(Type converterType)
        {
            // make sure type implements SettingsConverter<>
            var genericType = typeof(SettingConverter<>);
            if (!converterType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType))
            {
                throw new InvalidOperationException("Cannot use type " + converterType.Name + " as a setting converter. It does not implement SettingConverter<T>.");
            }

            Converter = Activator.CreateInstance(converterType);
        }
    }

    public interface SettingConverter<TValue>
    {
        string GetString(TValue value);
        TValue GetValue(string s);
    }

    #region binaryconverters

    public class BooleanSettingConverter : SettingConverter<bool>
    {
        public string GetString(bool b) { return b.ToString(); }
        public bool GetValue(string s) { return bool.Parse(s); }
    }

    #endregion

    #region NumericConverters

    public class ByteSettingConverter : SettingConverter<byte>
    {
        public string GetString(byte value) { return value.ToString(); }
        public byte GetValue(string s) { return byte.Parse(s); }
    }

    public class ShortSettingConverter: SettingConverter<short>
    {
        public string GetString(short value) { return value.ToString(); }
        public short GetValue(string s) { return short.Parse(s); }
    }

    public class UShortSettingConverter: SettingConverter<ushort>
    {
        public string GetString(ushort value) { return value.ToString(); }
        public ushort GetValue(string s) { return ushort.Parse(s); }
    }

    public class IntSettingConverter: SettingConverter<int>
    {
        public string GetString(int value) { return value.ToString(); }
        public int GetValue(string s) { return int.Parse(s); }
    }

    public class UIntSettingConverter: SettingConverter<uint>
    {
        public string GetString(uint value) { return value.ToString(); }
        public uint GetValue(string s) { return uint.Parse(s); }
    }

    public class LongSettingConverter: SettingConverter<long>
    {
        public string GetString(long value) { return value.ToString(); }
        public long GetValue(string s) { return long.Parse(s); }
    }

    public class ULongSettingConverter: SettingConverter<ulong>
    {
        public string GetString(ulong value) { return value.ToString(); }
        public ulong GetValue(string s) { return ulong.Parse(s); }
    }

    public class FloatSettingConverter: SettingConverter<float>
    {
        public string GetString(float value) { return value.ToString(); }
        public float GetValue(string s) { return float.Parse(s); }
    }

    public class DoubleSettingConverter: SettingConverter<double>
    {
        public string GetString(double value) { return value.ToString(); }
        public double GetValue(string s) { return double.Parse(s); }
    }

    #endregion

    #region TextConverters

    public class StringSettingConverter : SettingConverter<string>
    {
        public string GetString(string value) { return value; }
        public string GetValue(string s) { return s; }
    }

    public class CharSettingConverter : SettingConverter<char>
    {
        public string GetString(char value)
        {
            return value.ToString();
        }

        public char GetValue(string s)
        {
            if (s == null || s.Length != 1)
                throw new Exception("Cannot convert \"" + s + "\" to char.");

            return s[0];
        }
    }

    #endregion
}