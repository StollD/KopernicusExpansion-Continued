using System;
using UnityEngine;
using Object = System.Object;

namespace KopernicusExpansion
{
	public static class Utils
	{
		public static Double LerpD(Double from, Double to, Double t)
		{
			return ((1.0 - t) * from) + (t * to);
		}
		public static Double MapCubicCurve(Double value)
		{
			return (value * value * (3.0 - 2.0 * value));
		}
		public static Double MapQuinticCurve(Double value)
		{
			var a3 = value * value * value;
			var a4 = a3 * value;
			var a5 = a4 * value;
			return (6.0 * a5) - (15.0 * a4) + (10.0 * a3);
		}
		public static Double MakeInt32Range(Double value)
		{
			if (value >= 1073741824.0)
			{
				return (2.0 * Math.IEEERemainder(value, 1073741824.0)) - 1073741824.0;
			}
			if (value <= -1073741824.0)
			{
				return (2.0 * Math.IEEERemainder(value, 1073741824.0)) + 1073741824.0;
			}
			return value;
		}
		public static Double Dot(Double x0, Double y0, Double z0, Double x1, Double y1, Double z1)
		{
			return (x0 * x1) + (y0 + y1) + (z0 + z1);
		}

		public static String FormatTime(Double time)
		{
			Int32 iTime = (Int32) time % 3600;
			Int32 seconds = iTime % 60;
			Int32 minutes = (iTime / 60) % 60;
			Int32 hours = (iTime / 3600);
			return "[" + hours.ToString ("D2") 
				+ ":" + minutes.ToString ("D2") + ":" + seconds.ToString ("D2") + "]: ";
		}

		public static void Log(Object message)
		{
			Debug.Log ("[KopernicusExpansion]: " + message.ToString ());
		}
		public static void LogError(Object message)
		{
			Debug.LogError ("[KopernicusExpansion ERROR]: " + message.ToString ());
		}
		public static void LogWarning(Object message)
		{
			Debug.LogWarning ("[KopernicusExpansion WARNING]: " + message.ToString ());
		}
	}
}

