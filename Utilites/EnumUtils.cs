using System;
using System.Collections.Generic;

namespace Utilities {
	public class EnumUtils {
		public static T[] FlagToArr<T>(T value) where T : IConvertible {
			if (value == null)
				throw new NullReferenceException();
			if (!Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
				throw new ArgumentNotDefinedAttributeException(typeof(T), typeof(FlagsAttribute));
			IFormatProvider fmt = System.Threading.Thread.CurrentThread.CurrentCulture;
			var result = new List<T>();
			foreach (T f in Enum.GetValues(typeof(T))) {
				if ((f.ToInt32(fmt) & value.ToInt32(fmt)) > 0) {
					result.Add(f);
				}
			}
			return result.ToArray();
		}

		public static T ArrToFlag<T>(T[] arr) where T : IConvertible {
			if (arr == null)
				throw new NullReferenceException();
			if (!Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
				throw new ArgumentNotDefinedAttributeException(typeof(T), typeof(FlagsAttribute));
			IFormatProvider fmt = System.Threading.Thread.CurrentThread.CurrentCulture;
			int r = 0;
			foreach (T f in arr) {
				r |= f.ToInt32(fmt);
			}
			return (T)Enum.ToObject(typeof(T), r);
		}
	}

	public class ArgumentNotDefinedAttributeException : ArgumentException {
		public ArgumentNotDefinedAttributeException(Type src, Type attr)
			: base(string.Format("The type '{0}' must have an attribute '{1}'.", src, attr)) { }
	}
}
