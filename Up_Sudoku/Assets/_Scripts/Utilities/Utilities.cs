using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XomracUtilities
{

	public static class XomracUtilites
	{

		public static bool IsCoveredByUI => EventSystem.current.IsPointerOverGameObject();

		public static Vector2 CreateVector2AngleRad(float angleRad)
		{
			return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
		}

		public static Vector2 CreateVector2AngleDeg(float angleDeg)
		{
			return CreateVector2AngleRad(angleDeg * Mathf.Deg2Rad);
		}
		
		public static bool PlayerPrefsGetBool(string key, bool defaultValue = false)
		{
			return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
		}
		
		public static void PlayerPrefsSetBool(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}
		
		public static Vector2 RandomDirection
		{
			get{
				var angle = Random.Range(0f, Mathf.PI * 2);
				return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			}
		}

		public static bool RandomBool => Random.value < 0.5f;

		public static float MapClamped(float sourceValue, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
		{
			float sourceRange = sourceTo - sourceFrom;
			float targetRange = targetTo - targetFrom;
			float percent = Mathf.Clamp01((sourceValue - sourceFrom) / sourceRange);
			return targetFrom + targetRange * percent;
		}

		public static float ApplyJoystickDeadzone(float value, float deadzone, bool fullRangeBetweenDeadzoneAndOne = false)
		{
			if (Mathf.Abs(value) <= deadzone) return 0;
				

			if (fullRangeBetweenDeadzoneAndOne && (deadzone > 0f))
			{
				if (value < 0)
				{
					return MapClamped(value, -1f, -deadzone, -1f, 0f);
				}
				else
				{
					return MapClamped(value, deadzone, 1f, 0f, 1f);
				}
			}

			return value;
		}

		public static float MapClampedJoystick(float sourceValue, float sourceFrom, float sourceTo, float deadzone = 0f, bool fullRangeBetweenDeadzoneAndOne = false)
		{
			float percent = MapClamped(sourceValue, sourceFrom, sourceTo, -1, 1);

			if (deadzone > 0)
			{
				percent = ApplyJoystickDeadzone(percent, deadzone, fullRangeBetweenDeadzoneAndOne);
			}

			return percent;
		}

		public static float GetCenterAngleDeg(float angle1, float angle2)
		{
			return angle1 + Mathf.DeltaAngle(angle1, angle2) / 2f;
		}

		public static float NormalizeAngleDeg360(float angle)
		{
			while (angle < 0)
			{
				angle += 360;
				if (angle >= 360)
				{
					angle %= 360;
				}
			}
			return angle;
		}


		
	}
}