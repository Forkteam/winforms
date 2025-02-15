﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
    /// <summary>
    ///  Base class for value editors that extend basic functionality.
    ///  calls will be delegated to the "base value editor".
    /// </summary>
    internal class Com2ExtendedTypeConverter : TypeConverter
    {
        private readonly TypeConverter? _innerConverter;

        public Com2ExtendedTypeConverter(TypeConverter? innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public Com2ExtendedTypeConverter(Type baseType)
        {
            _innerConverter = TypeDescriptor.GetConverter(baseType);
        }

        public TypeConverter? InnerConverter
        {
            get
            {
                return _innerConverter;
            }
        }

        public TypeConverter? GetWrappedConverter(Type t)
        {
            TypeConverter? converter = _innerConverter;

            while (converter is not null)
            {
                if (t.IsInstanceOfType(converter))
                {
                    return converter;
                }

                if (converter is Com2ExtendedTypeConverter com2ExtendedTypeConverter)
                {
                    converter = com2ExtendedTypeConverter.InnerConverter;
                }
                else
                {
                    break;
                }
            }

            return null;
        }

        /// <summary>
        ///  Determines if this converter can convert an object in the given source
        ///  type to the native type of the converter.
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.CanConvertFrom(context, sourceType);
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        ///  Determines if this converter can convert an object to the given destination
        ///  type.
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.CanConvertTo(context, destinationType);
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        ///  Converts the given object to the converter's native type.
        /// </summary>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.ConvertFrom(context, culture, value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        ///  Converts the given object to another type.  The most common types to convert
        ///  are to and from a string object.  The default implementation will make a call
        ///  to ToString on the object if the object is valid and if the destination
        ///  type is string.  If this cannot convert to the destination type, this will
        ///  throw a NotSupportedException.
        /// </summary>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.ConvertTo(context, culture, value, destinationType);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        ///  Creates an instance of this type given a set of property values
        ///  for the object.  This is useful for objects that are immutable, but still
        ///  want to provide changable properties.
        /// </summary>
        public override object? CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.CreateInstance(context, propertyValues);
            }

            return base.CreateInstance(context, propertyValues);
        }

        /// <summary>
        ///  Determines if changing a value on this object should require a call to
        ///  CreateInstance to create a new value.
        /// </summary>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.GetCreateInstanceSupported(context);
            }

            return base.GetCreateInstanceSupported(context);
        }

        /// <summary>
        ///  Retrieves the set of properties for this type.  By default, a type has
        ///  does not return any properties.  An easy implementation of this method
        ///  can just call TypeDescriptor.GetProperties for the correct data type.
        /// </summary>
        public override PropertyDescriptorCollection? GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.GetProperties(context, value, attributes);
            }

            return base.GetProperties(context, value, attributes);
        }

        /// <summary>
        ///  Determines if this object supports properties.  By default, this
        ///  is false.
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.GetPropertiesSupported(context);
            }

            return base.GetPropertiesSupported(context);
        }

        /// <summary>
        ///  Retrieves a collection containing a set of standard values
        ///  for the data type this validator is designed for.  This
        ///  will return null if the data type does not support a
        ///  standard set of values.
        /// </summary>
        public override StandardValuesCollection? GetStandardValues(ITypeDescriptorContext? context)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.GetStandardValues(context);
            }

            return base.GetStandardValues(context);
        }

        /// <summary>
        ///  Determines if the list of standard values returned from
        ///  GetStandardValues is an exclusive list.  If the list
        ///  is exclusive, then no other values are valid, such as
        ///  in an enum data type.  If the list is not exclusive,
        ///  then there are other valid values besides the list of
        ///  standard values GetStandardValues provides.
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.GetStandardValuesExclusive(context);
            }

            return base.GetStandardValuesExclusive(context);
        }

        /// <summary>
        ///  Determines if this object supports a standard set of values
        ///  that can be picked from a list.
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.GetStandardValuesSupported(context);
            }

            return base.GetStandardValuesSupported(context);
        }

        /// <summary>
        ///  Determines if the given object value is valid for this type.
        /// </summary>
        public override bool IsValid(ITypeDescriptorContext? context, object? value)
        {
            if (_innerConverter is not null)
            {
                return _innerConverter.IsValid(context, value);
            }

            return base.IsValid(context, value);
        }
    }
}
