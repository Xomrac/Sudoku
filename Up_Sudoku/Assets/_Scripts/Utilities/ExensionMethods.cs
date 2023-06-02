using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace XomracUtilities.Extensions
{
	public enum RTColors
	{
		Black,
		Blue,
		Brown,
		Cyan,
		Darkblue,
		Green,
		Grey,
		Lightblue,
		Lime,
		Magenta,
		Maroon,
		Navy,
		Olive,
		Orange,
		Purple,
		Red,
		Silver,
		Teal,
		White,
		Yellow
	}
	public static class XomracExtensions
	{

		#region CanvasGroup

		public static void Show(this CanvasGroup cg, bool value)
		{
			cg.alpha = value.ToInt();
			cg.interactable = value;
			cg.blocksRaycasts = value;
		}
		public static void Set(this CanvasGroup canvasGroup, float alpha, bool interactable, bool blockRaycast)
		{
			canvasGroup.alpha = alpha;
			canvasGroup.interactable = interactable;
			canvasGroup.blocksRaycasts = blockRaycast;
		}

		public static void Blink(this CanvasGroup cg, float blinkTime, MonoBehaviour host)
		{
			host.StartCoroutine(BlinkCoroutine());

			IEnumerator BlinkCoroutine()
			{
				cg.alpha = 1;
				float elapsedTime = 0;
				while (elapsedTime < blinkTime)
				{
					elapsedTime.IncrementSeconds();
					float floatValue = Mathf.Lerp(cg.alpha, 0, elapsedTime / blinkTime);
					cg.alpha = floatValue;
					yield return null;
				}
				elapsedTime = 0;
				while (elapsedTime < blinkTime)
				{
					elapsedTime.IncrementSeconds();
					float floatValue = Mathf.Lerp(cg.alpha, 1, elapsedTime / blinkTime);
					cg.alpha = floatValue;
					yield return null;
				}
				host.StartCoroutine(BlinkCoroutine());
			}
		}

		public static void Fade(this CanvasGroup canvasGroup, float startValue, float finalValue, float time, MonoBehaviour hostClass)
		{
			IEnumerator FadeCoroutine()
			{
				float elapsedTime = 0;
				while (elapsedTime < time)
				{
					elapsedTime.IncrementSeconds();
					canvasGroup.alpha = Mathf.Lerp(startValue, finalValue, elapsedTime / time);
					yield return null;
				}
			}

			hostClass.StartCoroutine(FadeCoroutine());
		}

		#endregion

		#region Image

		public static void Blink(this Image img, Color colorA, Color colorB, float blinkTime, MonoBehaviour host)
		{
			host.StartCoroutine(BlinkCoroutine());

			IEnumerator BlinkCoroutine()
			{
				img.color = colorA;
				float elapsedTime = 0;
				while (elapsedTime < blinkTime)
				{
					elapsedTime.IncrementSeconds();
					img.color = Color.Lerp(colorA, colorB, elapsedTime / blinkTime);
					yield return null;
				}
				img.color = colorB;
				elapsedTime = 0;
				while (elapsedTime < blinkTime)
				{
					elapsedTime.IncrementSeconds();
					img.color = Color.Lerp(colorB, colorA, elapsedTime / blinkTime);
					yield return null;
				}
				host.StartCoroutine(BlinkCoroutine());
			}
		}

		#endregion

		#region Int
		
		public static int GetNextIndexCycled(this int currentIndex, int size)
		{
			return (currentIndex + 1) % size;
		}
		public static string ToRoundFormat(this int number)
		{
			return number switch
			{
				< 1000 => number.ToString(),
				< 1000000 => (number / (float)1000).ToString("#.00") + "K",
				< 1000000000 => (number / (float)1000000).ToString("#.00") + "M",
				_ => (number / (float)1000000000).ToString("#.00") + "B"
			};
		}

		public static int Remap(this int number, int fromMin, int fromMax, int toMin, int toMax) => toMin + (number - fromMin) * (toMax - toMin) / (fromMax - fromMin);

		private static string GetOrdinalStringEnd(this int number)
		{
			int x = number % 10;

			return x switch
			{
				1 => "st",
				2 => "nd",
				3 => "rd",
				_ => "th"
			};
		}

		public static bool IsBetweenRange(this int thisValue, int value1, int value2)
		{
			return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
		}

		#endregion

		#region Float

		public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax) => toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);

		public static float Remap(this int number, int fromMin, int fromMax, float toMin, float toMax) => toMin + (number - fromMin) * (toMax - toMin) / (fromMax - fromMin);

		public static float Distance(this float a, float b) => Mathf.Abs(a - b);
		public static void IncrementSeconds(this ref float f)
		{
			f += Time.deltaTime;
		}

		public static bool IsBetweenRange(this float thisValue, float value1, float value2)
		{
			return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
		}

		#endregion

		#region Bool

		public static int ToInt(this bool value) => value ? 1 : 0;

		#endregion

		#region Enum

		public static string GetEnumName<T>(this T e)
		{
			if (!e.GetType().IsEnum)
				throw new ArgumentException("Not an Enum type");

			return Enum.GetName(typeof(T), e);
		}

		#endregion
		
		#region Sprite
		public static Texture2D GetTexture(this Sprite sprite)
		{
			if (sprite == null)
				return null;

			if (sprite.rect.width != sprite.texture.width)
			{
				var texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
				Color[] colors = sprite.texture.GetPixels(Mathf.CeilToInt(sprite.rect.x),
					Mathf.CeilToInt(sprite.rect.y),
					Mathf.CeilToInt(sprite.rect.width),
					Mathf.CeilToInt(sprite.rect.height));

				texture2D.SetPixels(colors);
				texture2D.Apply();

				return texture2D;
			}
			else
			{
				return sprite.texture;
			}
		}

		public static Color32[] GetPixels32(this Sprite sprite)
		{
			if (sprite == null)
				return null;

			if (sprite.rect.width != sprite.texture.width)
			{
				var texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
				Color[] colors = sprite.texture.GetPixels(Mathf.CeilToInt(sprite.rect.x),
					Mathf.CeilToInt(sprite.rect.y),
					Mathf.CeilToInt(sprite.rect.width),
					Mathf.CeilToInt(sprite.rect.height));

				texture2D.SetPixels(colors);

				return texture2D.GetPixels32();
			}
			else
				return sprite.texture.GetPixels32();
		}

		#endregion

		#region Color32

		public static bool IsEqualTo(this Color32 color1, Color32 color2) => color1.r == color2.r && color1.g == color2.g && color1.b == color2.b && color1.a == color2.a;

		#endregion
		
		#region Color

		public static Color Change(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
		{
			if (r.HasValue) color.r = r.Value;
			if (g.HasValue) color.g = g.Value;
			if (b.HasValue) color.b = b.Value;
			if (a.HasValue) color.a = a.Value;
			return color;
		}

		public static Color Opaque(this Color color) => new Color(color.r, color.g, color.b);

		public static Color Invert(this Color color) => new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);

		public static Color WithAlpha(this Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);

		#endregion

		#region List

		public static void RemoveNulls<T>(this List<T> list)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i] == null)
				{
					list.Remove(list[i]);
				}
			}
		}

		public static void AddIfNotPresent<T>(this List<T> list, T element)
		{
			if (!list.Contains(element))
			{
				list.Add(element);
			}
		}

		public static void Shuffle<T>(this List<T> list)
		{
			int count = list.Count;
			for (int i1 = 0; i1 < count; i1++)
			{
				int i2 = Random.Range(0, count);
				(list[i1], list[i2]) = (list[i2], list[i1]);
			}
		}
		
		public static T First<T>(this List<T> list) => list[0];

		public static T Last<T>(this List<T> list) => list[^1];

		public static List<T> Distinct<T>(this List<T> list)
		{
			var temp = new List<T>();

			foreach (T element in list)
			{
				temp.AddIfNotPresent(element);
			}

			return temp;
		}
		
		public static void Remove<T>(this IList<T> list, IEnumerable<T> toRemove)
		{
			foreach (T element in toRemove)
			{
				list.Remove(element);
			}
		}

		public static bool FindAndRemove<T>(this List<T> list, Predicate<T> match)
		{
			T element = list.Find(match);

			return element != null && list.Remove(element);
		}
		
		public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
		{
			foreach (T obj in ie)
			{
				action(obj);
			}
		}

		public static List<T> ShiftLeft<T>(this List<T> list, int shiftBy)
		{
			if (list.Count <= shiftBy)
				return list;

			List<T> result = list.GetRange(shiftBy, list.Count - shiftBy);
			result.AddRange(list.GetRange(0, shiftBy));

			return result;
		}

		public static List<T> ShiftRight<T>(this List<T> list, int shiftBy)
		{
			if (list.Count <= shiftBy)
				return list;

			List<T> result = list.GetRange(list.Count - shiftBy, shiftBy);
			result.AddRange(list.GetRange(0, list.Count - shiftBy));

			return result;
		}
		
		public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;

		public static bool IsInRange<T>(this IList<T> list, int index) => index >= 0 && index < list.Count;

		/// Swap two elements
		public static void Swap<T>(this IList<T> list, int indexA, int indexB) => (list[indexA], list[indexB]) = (list[indexB], list[indexA]);

		public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> dict) => dict == null || dict.Count == 0;

		public static int CountNullSlots<T>(this T[] list, int startIndex = 0, int endIndex = 0)
		{
			if (endIndex == 0)
				endIndex = list.Length;

			int count = 0;

			for (int i = startIndex; i < endIndex; i++)
			{
				if (list[i] == null)
					count++;
			}

			return count;
		}

		public static T GetRandom<T>(this IList<T> list) => list[Random.Range(0, list.Count)];

		public static T GetRandom<T>(this IEnumerable<T> list, T toIgnore)
		{
			var l = new List<T>(list);
			l.Remove(toIgnore);

			return l.GetRandom();
		}

		public static string ToOneLineString<T>(this IEnumerable<T> list, string separator = ", ", string encapsulate = "\"")
		{
			var useEncapsulate = encapsulate.Length > 0;

			var result = new StringBuilder();
			foreach (var element in list)
			{
				if (result.Length > 0)
					result.Append(separator);

				if (useEncapsulate)
					result.Append(encapsulate);

				result.Append(element);

				if (useEncapsulate)
					result.Append(encapsulate);
			}

			return result.ToString();
		}

		#endregion

		#region Array


		public static IEnumerable<T> Flatten<T>(this T[,] matrix)
		{
			var rows = matrix.GetLength(0);
			var cols = matrix.GetLength(1);
			for (var i = 0; i < rows;i++ )
			{
				for (var j = 0; j < cols; j++ )
					yield return matrix[i, j];
			}
		}
		public static T RandomElement<T>(this T[] array)
		{
			int index = Random.Range(0, array.Length);
			return array[index];
		}

		public static void Shuffle<T>(this T[] list)
		{
			int count = list.Length;
			for (int i1 = 0; i1 < count; i1++)
			{
				int i2 = Random.Range(0, count);
				(list[i1], list[i2]) = (list[i2], list[i1]);
			}
		}

		#endregion

		#region Transform

		public static void DestroyChildrens(this GameObject obj)
		{
			for (int i = obj.transform.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(obj.transform.GetChild(i).gameObject);
			}
		}

		public static T GetOrAddComponent<T>(this GameObject obj) where T : Component => obj.TryGetComponent(out T oldComp) ? oldComp : obj.AddComponent<T>();

		public static bool TryGetComponentInChildren<T>(this GameObject obj, out T component) where T : Component
		{
			if (obj.TryGetComponent(out component)) return true;

			foreach (Transform child in obj.transform)
			{
				if (child.gameObject.TryGetComponentInChildren(out component)) return true;
			}
			return false;
		}

		public static bool HasComponent<T>(this GameObject obj) => obj.TryGetComponent(out T _);

		public static void CopyPositionAndRotatationFrom(this Transform transform, Transform source)
		{
			transform.position = source.position;
			transform.rotation = source.rotation;
		}

		public static Transform SetPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
		{
			transform.position = transform.position.Change3(x, y, z);
			return transform;
		}

		public static Transform SetLocalPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
		{
			transform.localPosition = transform.localPosition.Change3(x, y, z);
			return transform;
		}

		public static Transform SetLocalScale(this Transform transform, float? x = null, float? y = null, float? z = null)
		{
			transform.localScale = transform.localScale.Change3(x, y, z);
			return transform;
		}

		public static Transform SetLossyScale(this Transform transform, float? x = null, float? y = null, float? z = null)
		{
			Vector3 lossyscalee = transform.lossyScale;
			Vector3 newScale = lossyscalee.Change3(x, y, z);

			transform.localScale = Vector3.one;
			transform.localScale = new Vector3(newScale.x / lossyscalee.x, newScale.y / lossyscalee.y, newScale.z / lossyscalee.z);

			return transform;
		}

		public static Transform SetEulerAngles(this Transform transform, float? x = null, float? y = null, float? z = null)
		{
			transform.eulerAngles = transform.eulerAngles.Change3(x, y, z);
			return transform;
		}

		public static Transform SetLocalEulerAngles(this Transform transform, float? x = null, float? y = null, float? z = null)
		{
			transform.localEulerAngles = transform.localEulerAngles.Change3(x, y, z);
			return transform;
		}

		#endregion

		#region GameObject

		public static void AssignLayerToHierarchy(this GameObject gameObject, int layer, bool assignToChildrenToo = false)
		{
			if (assignToChildrenToo)
			{
				Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
				foreach (Transform child in transforms)
				{
					child.gameObject.layer = layer;
				}
			}
			else
			{
				gameObject.layer = layer;
			}
		}

		public static void StripCloneFromName(this GameObject gameObject)
		{
			gameObject.name = gameObject.GetNameWithoutClone();
		}

		public static string GetNameWithoutClone(this GameObject gameObject)
		{
			string gameObjectName = gameObject.name;

			int clonePartIndex = gameObjectName.IndexOf("(Clone)", StringComparison.Ordinal);
			return clonePartIndex == -1 ? gameObjectName : gameObjectName[..clonePartIndex];
		}

		#endregion

		#region Vectors

		
		public static Vector2 FlipX(this Vector2 vector) => new Vector2(-vector.x, vector.y);

		public static Vector2 FlipY(this Vector2 vector) => new Vector2(vector.x, -vector.y);

		public static Vector3 FlipX(this Vector3 vector) => new Vector3(-vector.x, vector.y, vector.z);

		public static Vector3 FlipY(this Vector3 vector) => new Vector3(vector.x, -vector.y, vector.z);

		public static Vector3 FlipZ(this Vector3 vector) => new Vector3(vector.x, vector.y, -vector.z);
		public static Vector2 Change2(this Vector2 vector, float? x = null, float? y = null)
		{
			if (x.HasValue) vector.x = x.Value;
			if (y.HasValue) vector.y = y.Value;
			return vector;
		}

		public static Vector3 Change3(this Vector3 vector, float? x = null, float? y = null, float? z = null)
		{
			if (x.HasValue) vector.x = x.Value;
			if (y.HasValue) vector.y = y.Value;
			if (z.HasValue) vector.z = z.Value;
			return vector;
		}

		public static Vector4 Change4(this Vector4 vector, float? x = null, float? y = null, float? z = null, float? w = null)
		{
			if (x.HasValue) vector.x = x.Value;
			if (y.HasValue) vector.y = y.Value;
			if (z.HasValue) vector.z = z.Value;
			if (w.HasValue) vector.w = w.Value;
			return vector;
		}

		public static Vector2 RotateRad(this Vector2 v, float angleRad)
		{
			// http://answers.unity3d.com/questions/661383/whats-the-most-efficient-way-to-rotate-a-vector2-o.html
			float sin = Mathf.Sin(angleRad);
			float cos = Mathf.Cos(angleRad);

			float tx = v.x;
			float ty = v.y;
			v.x = (cos * tx) - (sin * ty);
			v.y = (sin * tx) + (cos * ty);

			return v;
		}

		/// <summary>
		/// Calculates the average position of an array of Vector2.
		/// </summary>
		public static Vector2 CalculateCentroid(this Vector2[] array)
		{
			var sum = new Vector2();
			int count = array.Length;
			for (int i = 0; i < count; i++)
			{
				sum += array[i];
			}
			return sum / count;
		}

		public static Vector3 CalculateCentroid(this Vector3[] array)
		{
			var sum = new Vector3();
			int count = array.Length;
			for (int i = 0; i < count; i++)
			{
				sum += array[i];
			}
			return sum / count;
		}

		public static Vector4 CalculateCentroid(this Vector4[] array)
		{
			var sum = new Vector4();
			int count = array.Length;
			for (int i = 0; i < count; i++)
			{
				sum += array[i];
			}
			return sum / count;
		}

		public static Vector2 CalculateCentroid(this List<Vector2> list)
		{
			var sum = new Vector2();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				sum += list[i];
			}
			return sum / count;
		}

		public static Vector3 CalculateCentroid(this List<Vector3> list)
		{
			var sum = new Vector3();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				sum += list[i];
			}
			return sum / count;
		}

		public static Vector4 CalculateCentroid(this List<Vector4> list)
		{
			var sum = new Vector4();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				sum += list[i];
			}
			return sum / count;
		}

		public static Vector2 RotateDeg(this Vector2 v, float angleDeg)
		{
			return v.RotateRad(angleDeg * Mathf.Deg2Rad);
		}

		public static float GetAngleRad(this Vector2 vector)
		{
			return Mathf.Atan2(vector.y, vector.x);
		}

		public static float GetAngleDeg(this Vector2 vector)
		{
			return vector.GetAngleRad() * Mathf.Rad2Deg;
		}

		#endregion

		#region Rect

		public static Vector2 RandomPosition(this Rect rect, float extendDistance = 0f)
		{
			return new Vector2(Random.Range(rect.xMin - extendDistance, rect.xMax + extendDistance), Random.Range(rect.yMin - extendDistance, rect.yMax + extendDistance));
		}

		public static Rect RandomSubRect(this Rect rect, float width, float height)
		{
			width = Mathf.Min(rect.width, width);
			height = Mathf.Min(rect.height, height);

			float halfWidth = width / 2f;
			float halfHeight = height / 2f;

			float centerX = Random.Range(rect.xMin + halfWidth, rect.xMax - halfWidth);
			float centerY = Random.Range(rect.yMin + halfHeight, rect.yMax - halfHeight);

			return new Rect(centerX - halfWidth, centerY - halfHeight, width, height);
		}

		public static Vector2 Clamp2(this Rect rect, Vector2 position, float extendDistance = 0f)
		{
			return new Vector2(Mathf.Clamp(position.x, rect.xMin - extendDistance, rect.xMax + extendDistance),
				Mathf.Clamp(position.y, rect.yMin - extendDistance, rect.yMax + extendDistance));
		}

		public static Vector3 Clamp3(this Rect rect, Vector3 position, float extendDistance = 0f)
		{
			return new Vector3(Mathf.Clamp(position.x, rect.xMin - extendDistance, rect.xMax + extendDistance),
				Mathf.Clamp(position.y, rect.yMin - extendDistance, rect.yMax + extendDistance),
				position.z);
		}

		public static Rect Extend(this Rect rect, float extendDistance)
		{
			Rect copy = rect;
			copy.xMin -= extendDistance;
			copy.xMax += extendDistance;
			copy.yMin -= extendDistance;
			copy.yMax += extendDistance;
			return copy;
		}

		public static bool Contains(this Rect rect, Vector2 position, float extendDistance)
		{
			return (position.x > rect.xMin + extendDistance) &&
			       (position.y > rect.yMin + extendDistance) &&
			       (position.x < rect.xMax - extendDistance) &&
			       (position.y < rect.yMax - extendDistance);
		}

		public static Vector2[] GetCornerPoints(this Rect rect)
		{
			return new[]
			{
				new Vector2(rect.xMin, rect.yMin),
				new Vector2(rect.xMax, rect.yMin),
				new Vector2(rect.xMax, rect.yMax),
				new Vector2(rect.xMin, rect.yMax)
			};
		}

		#endregion

		#region LayerMask

		public static bool ContainsLayer(this LayerMask mask, int layer)
		{
			return (mask.value & (1 << layer)) != 0;
		}

		#endregion

		#region Camera

		public static Vector2 CalculateViewportWorldSizeAtDistance(this Camera camera, float distance, float aspectRatio = 0)
		{
			if (aspectRatio == 0)
			{
				aspectRatio = camera.aspect;
			}

			float viewportHeightAtDistance = 2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad) * distance;
			float viewportWidthAtDistance = viewportHeightAtDistance * aspectRatio;

			return new Vector2(viewportWidthAtDistance, viewportHeightAtDistance);
		}

		#endregion


		#region MonoBehaviour

		// public static Coroutine ExecuteNextFrame(this MonoBehaviour mb, Action action)
		// {
		// 	IEnumerator CallNextFrame(Action a)
		// 	{
		// 		yield return null;
		//
		// 		a.Invoke();
		// 	}
		//
		// 	return mb.StartCoroutine(CallNextFrame(action));
		// }
		//
		// public static Coroutine ExecuteAfterFrames(this MonoBehaviour mb, Action action, int frames)
		// {
		// 	IEnumerator Wait(Action a)
		// 	{
		// 		for (int i = 0; i < frames; i++)
		// 		{
		// 			yield return null;
		// 		}
		//
		// 		a.Invoke();
		// 	}
		//
		// 	return mb.StartCoroutine(Wait(action));
		// }

		public static Coroutine WaitFrames(this MonoBehaviour mb, int frames)
		{
			IEnumerator Wait()
			{
				var frame = new WaitForEndOfFrame();

				for (int i = 0; i < frames; i++)
				{
					yield return frame;
				}
			}

			return mb.StartCoroutine(Wait());
		}

		// public static Coroutine ExecuteAfterDelay(this MonoBehaviour mb, Action action, float time)
		// {
		// 	IEnumerator Wait(Action a)
		// 	{
		// 		yield return new WaitForSeconds(time);
		//
		// 		a.Invoke();
		// 	}
		//
		// 	return mb.StartCoroutine(Wait(action));
		// }

		#endregion



		#region String

		public static string UppercaseFirst(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			return char.ToUpper(s[0]) + s.Substring(1);
		}

		public static string SnakeCaseToCapitalizedCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			string[] sA = s.Split('_');

			for (int i = 0; i < sA.Length; i++)
			{
				sA[i] = sA[i].UppercaseFirst();
			}

			return string.Join(" ", sA);
		}

		public static string SnakeCaseToUpperCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			string[] sA = s.Split('_');

			for (int i = 0; i < sA.Length; i++)
			{
				sA[i] = sA[i].ToUpper();
			}

			return string.Join(" ", sA);
		}

		public static string CapitalizedCaseToSnakeCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			string ss = s.Replace(" ", "_");

			return ss.ToLower();
		}

		public static string CapitalizedCaseToCamelCase(this string s)
		{
			s = char.ToLower(s[0]) + s.Substring(1);

			return s.Replace(" ", "");
		}

		public static bool IsNullOrEmpty(this string text) => string.IsNullOrEmpty(text);

		public static string SubstringByWords(this string text, int i, char separator = ' ')
		{
			string[] words = text.Split(separator);

			if (i <= words.Length)
			{
				string result = string.Empty;

				for (int j = 0; j <= i - 1; j++)
				{
					result += words[j] + separator;
				}

				return result.TrimEnd(separator);
			}
			else
				return "";
		}

		public static string GetWord(this string text, char separator, int i)
		{
			string[] words = text.Split(separator);

			return i < words.Length + 1 ? words[i - 1] : "";
		}

		public static string FirstWord(this string text, char separator) => text.Split(separator)[0];

		public static string LastWord(this string text, char separator)
		{
			string[] t = text.Split(separator);

			return t[^1];
		}

		public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

		public static string RTBold(this string text) => $"<b>{text}</b>";

		public static string RTSize(this string text, int size) => $"<size={size}>{text}</size>";

		public static string RTColor(this string text, RTColors color) => $"<color={color.ToString().ToLower()}>{text}</color>";

		#endregion
	}

}