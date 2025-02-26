using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012B2 RID: 4786
	public class StudioTween : MonoBehaviour
	{
		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06009E6B RID: 40555 RVA: 0x00408B9E File Offset: 0x00406F9E
		public List<Hashtable> listArguments
		{
			get
			{
				return this._listArguments;
			}
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06009E6C RID: 40556 RVA: 0x00408BA6 File Offset: 0x00406FA6
		public float percentage
		{
			get
			{
				return this._percentage;
			}
		}

		// Token: 0x06009E6D RID: 40557 RVA: 0x00408BAE File Offset: 0x00406FAE
		public static void CameraFadeFrom(Hashtable args)
		{
			if (StudioTween.cameraFade)
			{
				StudioTween.ColorFrom(StudioTween.cameraFade, args);
			}
		}

		// Token: 0x06009E6E RID: 40558 RVA: 0x00408BD0 File Offset: 0x00406FD0
		public static void CameraFadeTo(float amount, float time)
		{
			if (StudioTween.cameraFade)
			{
				StudioTween.CameraFadeTo(StudioTween.Hash(new object[]
				{
					"amount",
					amount,
					"time",
					time
				}));
			}
		}

		// Token: 0x06009E6F RID: 40559 RVA: 0x00408C23 File Offset: 0x00407023
		public static void CameraFadeTo(Hashtable args)
		{
			if (StudioTween.cameraFade)
			{
				StudioTween.ColorTo(StudioTween.cameraFade, args);
			}
		}

		// Token: 0x06009E70 RID: 40560 RVA: 0x00408C44 File Offset: 0x00407044
		public static void ValueTo(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (!args.Contains("from") || !args.Contains("to"))
			{
				return;
			}
			args["type"] = "value";
			if (args["from"].GetType() == typeof(Vector2))
			{
				args["method"] = "vector2";
			}
			else if (args["from"].GetType() == typeof(Vector3))
			{
				args["method"] = "vector3";
			}
			else if (args["from"].GetType() == typeof(Rect))
			{
				args["method"] = "rect";
			}
			else if (args["from"].GetType() == typeof(float))
			{
				args["method"] = "float";
			}
			else
			{
				if (!(args["from"].GetType() == typeof(Color)))
				{
					return;
				}
				args["method"] = "color";
			}
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", StudioTween.EaseType.linear);
			}
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E71 RID: 40561 RVA: 0x00408DD2 File Offset: 0x004071D2
		public static void FadeFrom(GameObject target, float alpha, float time)
		{
			StudioTween.FadeFrom(target, StudioTween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06009E72 RID: 40562 RVA: 0x00408E07 File Offset: 0x00407207
		public static void FadeFrom(GameObject target, Hashtable args)
		{
			StudioTween.ColorFrom(target, args);
		}

		// Token: 0x06009E73 RID: 40563 RVA: 0x00408E10 File Offset: 0x00407210
		public static void FadeTo(GameObject target, float alpha, float time)
		{
			StudioTween.FadeTo(target, StudioTween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06009E74 RID: 40564 RVA: 0x00408E45 File Offset: 0x00407245
		public static void FadeTo(GameObject target, Hashtable args)
		{
			StudioTween.ColorTo(target, args);
		}

		// Token: 0x06009E75 RID: 40565 RVA: 0x00408E4E File Offset: 0x0040724E
		public static void ColorFrom(GameObject target, Color color, float time)
		{
			StudioTween.ColorFrom(target, StudioTween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06009E76 RID: 40566 RVA: 0x00408E84 File Offset: 0x00407284
		public static void ColorFrom(GameObject target, Hashtable args)
		{
			Color color = default(Color);
			Color color2 = default(Color);
			args = StudioTween.CleanArgs(args);
			if (!args.Contains("includechildren") || (bool)args["includechildren"])
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						Hashtable hashtable = (Hashtable)args.Clone();
						hashtable["ischild"] = true;
						StudioTween.ColorFrom(transform.gameObject, hashtable);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", StudioTween.EaseType.linear);
			}
			if (target.GetComponent(typeof(GUITexture)))
			{
				color = (color2 = target.GetComponent<GUITexture>().color);
			}
			else if (target.GetComponent(typeof(GUIText)))
			{
				color = (color2 = target.GetComponent<GUIText>().material.color);
			}
			else if (target.GetComponent<Renderer>())
			{
				color = (color2 = target.GetComponent<Renderer>().material.color);
			}
			else if (target.GetComponent<Light>())
			{
				color = (color2 = target.GetComponent<Light>().color);
			}
			if (args.Contains("color"))
			{
				color = (Color)args["color"];
			}
			else
			{
				if (args.Contains("r"))
				{
					color.r = (float)args["r"];
				}
				if (args.Contains("g"))
				{
					color.g = (float)args["g"];
				}
				if (args.Contains("b"))
				{
					color.b = (float)args["b"];
				}
				if (args.Contains("a"))
				{
					color.a = (float)args["a"];
				}
			}
			if (args.Contains("amount"))
			{
				color.a = (float)args["amount"];
				args.Remove("amount");
			}
			else if (args.Contains("alpha"))
			{
				color.a = (float)args["alpha"];
				args.Remove("alpha");
			}
			if (target.GetComponent(typeof(GUITexture)))
			{
				target.GetComponent<GUITexture>().color = color;
			}
			else if (target.GetComponent(typeof(GUIText)))
			{
				target.GetComponent<GUIText>().material.color = color;
			}
			else if (target.GetComponent<Renderer>())
			{
				target.GetComponent<Renderer>().material.color = color;
			}
			else if (target.GetComponent<Light>())
			{
				target.GetComponent<Light>().color = color;
			}
			args["color"] = color2;
			args["type"] = "color";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E77 RID: 40567 RVA: 0x00409218 File Offset: 0x00407618
		public static void ColorTo(GameObject target, Color color, float time)
		{
			StudioTween.ColorTo(target, StudioTween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06009E78 RID: 40568 RVA: 0x00409250 File Offset: 0x00407650
		public static void ColorTo(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (!args.Contains("includechildren") || (bool)args["includechildren"])
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						Hashtable hashtable = (Hashtable)args.Clone();
						hashtable["ischild"] = true;
						StudioTween.ColorTo(transform.gameObject, hashtable);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", StudioTween.EaseType.linear);
			}
			args["type"] = "color";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E79 RID: 40569 RVA: 0x00409350 File Offset: 0x00407750
		public static void LookFrom(GameObject target, Vector3 looktarget, float time)
		{
			StudioTween.LookFrom(target, StudioTween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06009E7A RID: 40570 RVA: 0x00409388 File Offset: 0x00407788
		public static void LookFrom(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			Vector3 eulerAngles = target.transform.eulerAngles;
			if (args["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = target.transform;
				Transform target2 = (Transform)args["looktarget"];
				Vector3? vector = (Vector3?)args["up"];
				transform.LookAt(target2, (vector == null) ? StudioTween.Defaults.up : vector.Value);
			}
			else if (args["looktarget"].GetType() == typeof(Vector3))
			{
				Transform transform2 = target.transform;
				Vector3 worldPosition = (Vector3)args["looktarget"];
				Vector3? vector2 = (Vector3?)args["up"];
				transform2.LookAt(worldPosition, (vector2 == null) ? StudioTween.Defaults.up : vector2.Value);
			}
			if (args.Contains("axis"))
			{
				Vector3 eulerAngles2 = target.transform.eulerAngles;
				string text = (string)args["axis"];
				if (text != null)
				{
					if (!(text == "x"))
					{
						if (!(text == "y"))
						{
							if (text == "z")
							{
								eulerAngles2.x = eulerAngles.x;
								eulerAngles2.y = eulerAngles.y;
							}
						}
						else
						{
							eulerAngles2.x = eulerAngles.x;
							eulerAngles2.z = eulerAngles.z;
						}
					}
					else
					{
						eulerAngles2.y = eulerAngles.y;
						eulerAngles2.z = eulerAngles.z;
					}
				}
				target.transform.eulerAngles = eulerAngles2;
			}
			args["rotation"] = eulerAngles;
			args["type"] = "rotate";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E7B RID: 40571 RVA: 0x0040959D File Offset: 0x0040799D
		public static void LookTo(GameObject target, Vector3 looktarget, float time)
		{
			StudioTween.LookTo(target, StudioTween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06009E7C RID: 40572 RVA: 0x004095D4 File Offset: 0x004079D4
		public static void LookTo(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["looktarget"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			}
			args["type"] = "look";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E7D RID: 40573 RVA: 0x004096D7 File Offset: 0x00407AD7
		public static void MoveTo(GameObject target, Vector3 position, float time)
		{
			StudioTween.MoveTo(target, StudioTween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06009E7E RID: 40574 RVA: 0x00409710 File Offset: 0x00407B10
		public static StudioTween MoveTo(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["position"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "move";
			args["method"] = "to";
			return StudioTween.Launch(target, args);
		}

		// Token: 0x06009E7F RID: 40575 RVA: 0x00409854 File Offset: 0x00407C54
		public static void MoveFrom(GameObject target, Vector3 position, float time)
		{
			StudioTween.MoveFrom(target, StudioTween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06009E80 RID: 40576 RVA: 0x0040988C File Offset: 0x00407C8C
		public static void MoveFrom(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			bool flag = (!args.Contains("islocal")) ? StudioTween.Defaults.isLocal : ((bool)args["islocal"]);
			if (args.Contains("path"))
			{
				Vector3[] array2;
				if (args["path"].GetType() == typeof(Vector3[]))
				{
					Vector3[] array = (Vector3[])args["path"];
					array2 = new Vector3[array.Length];
					Array.Copy(array, array2, array.Length);
				}
				else
				{
					Transform[] array3 = (Transform[])args["path"];
					array2 = new Vector3[array3.Length];
					for (int i = 0; i < array3.Length; i++)
					{
						array2[i] = array3[i].position;
					}
				}
				if (array2[array2.Length - 1] != target.transform.position)
				{
					Vector3[] array4 = new Vector3[array2.Length + 1];
					Array.Copy(array2, array4, array2.Length);
					if (flag)
					{
						array4[array4.Length - 1] = target.transform.localPosition;
						target.transform.localPosition = array4[0];
					}
					else
					{
						array4[array4.Length - 1] = target.transform.position;
						target.transform.position = array4[0];
					}
					args["path"] = array4;
				}
				else
				{
					if (flag)
					{
						target.transform.localPosition = array2[0];
					}
					else
					{
						target.transform.position = array2[0];
					}
					args["path"] = array2;
				}
			}
			else
			{
				Vector3 vector2;
				Vector3 vector;
				if (flag)
				{
					vector = (vector2 = target.transform.localPosition);
				}
				else
				{
					vector = (vector2 = target.transform.position);
				}
				if (args.Contains("position"))
				{
					if (args["position"].GetType() == typeof(Transform))
					{
						Transform transform = (Transform)args["position"];
						vector = transform.position;
					}
					else if (args["position"].GetType() == typeof(Vector3))
					{
						vector = (Vector3)args["position"];
					}
				}
				else
				{
					if (args.Contains("x"))
					{
						vector.x = (float)args["x"];
					}
					if (args.Contains("y"))
					{
						vector.y = (float)args["y"];
					}
					if (args.Contains("z"))
					{
						vector.z = (float)args["z"];
					}
				}
				if (flag)
				{
					target.transform.localPosition = vector;
				}
				else
				{
					target.transform.position = vector;
				}
				args["position"] = vector2;
			}
			args["type"] = "move";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E81 RID: 40577 RVA: 0x00409C07 File Offset: 0x00408007
		public static void MoveAdd(GameObject target, Vector3 amount, float time)
		{
			StudioTween.MoveAdd(target, StudioTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009E82 RID: 40578 RVA: 0x00409C3C File Offset: 0x0040803C
		public static void MoveAdd(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "add";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E83 RID: 40579 RVA: 0x00409C6E File Offset: 0x0040806E
		public static void MoveBy(GameObject target, Vector3 amount, float time)
		{
			StudioTween.MoveBy(target, StudioTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009E84 RID: 40580 RVA: 0x00409CA3 File Offset: 0x004080A3
		public static void MoveBy(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "by";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E85 RID: 40581 RVA: 0x00409CD5 File Offset: 0x004080D5
		public static void ScaleTo(GameObject target, Vector3 scale, float time)
		{
			StudioTween.ScaleTo(target, StudioTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06009E86 RID: 40582 RVA: 0x00409D0C File Offset: 0x0040810C
		public static void ScaleTo(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["scale"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "scale";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E87 RID: 40583 RVA: 0x00409E51 File Offset: 0x00408251
		public static void ScaleFrom(GameObject target, Vector3 scale, float time)
		{
			StudioTween.ScaleFrom(target, StudioTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06009E88 RID: 40584 RVA: 0x00409E88 File Offset: 0x00408288
		public static void ScaleFrom(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			Vector3 localScale2;
			Vector3 localScale = localScale2 = target.transform.localScale;
			if (args.Contains("scale"))
			{
				if (args["scale"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["scale"];
					localScale = transform.localScale;
				}
				else if (args["scale"].GetType() == typeof(Vector3))
				{
					localScale = (Vector3)args["scale"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					localScale.x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					localScale.y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					localScale.z = (float)args["z"];
				}
			}
			target.transform.localScale = localScale;
			args["scale"] = localScale2;
			args["type"] = "scale";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E89 RID: 40585 RVA: 0x00409FF0 File Offset: 0x004083F0
		public static void ScaleAdd(GameObject target, Vector3 amount, float time)
		{
			StudioTween.ScaleAdd(target, StudioTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009E8A RID: 40586 RVA: 0x0040A025 File Offset: 0x00408425
		public static void ScaleAdd(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "add";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E8B RID: 40587 RVA: 0x0040A057 File Offset: 0x00408457
		public static void ScaleBy(GameObject target, Vector3 amount, float time)
		{
			StudioTween.ScaleBy(target, StudioTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009E8C RID: 40588 RVA: 0x0040A08C File Offset: 0x0040848C
		public static void ScaleBy(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "by";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E8D RID: 40589 RVA: 0x0040A0BE File Offset: 0x004084BE
		public static void RotateTo(GameObject target, Vector3 rotation, float time)
		{
			StudioTween.RotateTo(target, StudioTween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06009E8E RID: 40590 RVA: 0x0040A0F4 File Offset: 0x004084F4
		public static void RotateTo(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["rotation"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "rotate";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E8F RID: 40591 RVA: 0x0040A239 File Offset: 0x00408639
		public static void RotateFrom(GameObject target, Vector3 rotation, float time)
		{
			StudioTween.RotateFrom(target, StudioTween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06009E90 RID: 40592 RVA: 0x0040A270 File Offset: 0x00408670
		public static void RotateFrom(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			bool flag = (!args.Contains("islocal")) ? StudioTween.Defaults.isLocal : ((bool)args["islocal"]);
			Vector3 vector2;
			Vector3 vector;
			if (flag)
			{
				vector = (vector2 = target.transform.localEulerAngles);
			}
			else
			{
				vector = (vector2 = target.transform.eulerAngles);
			}
			if (args.Contains("rotation"))
			{
				if (args["rotation"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["rotation"];
					vector = transform.eulerAngles;
				}
				else if (args["rotation"].GetType() == typeof(Vector3))
				{
					vector = (Vector3)args["rotation"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					vector.x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					vector.y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					vector.z = (float)args["z"];
				}
			}
			if (flag)
			{
				target.transform.localEulerAngles = vector;
			}
			else
			{
				target.transform.eulerAngles = vector;
			}
			args["rotation"] = vector2;
			args["type"] = "rotate";
			args["method"] = "to";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E91 RID: 40593 RVA: 0x0040A433 File Offset: 0x00408833
		public static void RotateAdd(GameObject target, Vector3 amount, float time)
		{
			StudioTween.RotateAdd(target, StudioTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009E92 RID: 40594 RVA: 0x0040A468 File Offset: 0x00408868
		public static void RotateAdd(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "add";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E93 RID: 40595 RVA: 0x0040A49A File Offset: 0x0040889A
		public static void RotateBy(GameObject target, Vector3 amount, float time)
		{
			StudioTween.RotateBy(target, StudioTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009E94 RID: 40596 RVA: 0x0040A4CF File Offset: 0x004088CF
		public static void RotateBy(GameObject target, Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "by";
			StudioTween.Launch(target, args);
		}

		// Token: 0x06009E95 RID: 40597 RVA: 0x0040A504 File Offset: 0x00408904
		private void GenerateTargets()
		{
			string text = this.type;
			if (text != null)
			{
				if (!(text == "value"))
				{
					if (!(text == "color"))
					{
						if (!(text == "move"))
						{
							if (!(text == "scale"))
							{
								if (!(text == "rotate"))
								{
									if (text == "look")
									{
										string text2 = this.method;
										if (text2 != null)
										{
											if (text2 == "to")
											{
												this.GenerateLookToTargets();
												this.apply = new StudioTween.ApplyTween(this.ApplyLookToTargets);
											}
										}
									}
								}
								else
								{
									string text3 = this.method;
									if (text3 != null)
									{
										if (!(text3 == "to"))
										{
											if (!(text3 == "add"))
											{
												if (text3 == "by")
												{
													this.GenerateRotateByTargets();
													this.apply = new StudioTween.ApplyTween(this.ApplyRotateAddTargets);
												}
											}
											else
											{
												this.GenerateRotateAddTargets();
												this.apply = new StudioTween.ApplyTween(this.ApplyRotateAddTargets);
											}
										}
										else
										{
											this.GenerateRotateToTargets();
											this.apply = new StudioTween.ApplyTween(this.ApplyRotateToTargets);
										}
									}
								}
							}
							else
							{
								string text4 = this.method;
								if (text4 != null)
								{
									if (!(text4 == "to"))
									{
										if (!(text4 == "by"))
										{
											if (text4 == "add")
											{
												this.GenerateScaleAddTargets();
												this.apply = new StudioTween.ApplyTween(this.ApplyScaleToTargets);
											}
										}
										else
										{
											this.GenerateScaleByTargets();
											this.apply = new StudioTween.ApplyTween(this.ApplyScaleToTargets);
										}
									}
									else
									{
										this.GenerateScaleToTargets();
										this.apply = new StudioTween.ApplyTween(this.ApplyScaleToTargets);
									}
								}
							}
						}
						else
						{
							string text5 = this.method;
							if (text5 != null)
							{
								if (!(text5 == "to"))
								{
									if (text5 == "by" || text5 == "add")
									{
										this.GenerateMoveByTargets();
										this.apply = new StudioTween.ApplyTween(this.ApplyMoveByTargets);
									}
								}
								else if (this.tweenArguments.Contains("path"))
								{
									this.GenerateMoveToPathTargets();
									this.apply = new StudioTween.ApplyTween(this.ApplyMoveToPathTargets);
								}
								else
								{
									this.GenerateMoveToTargets();
									this.apply = new StudioTween.ApplyTween(this.ApplyMoveToTargets);
								}
							}
						}
					}
					else
					{
						string text6 = this.method;
						if (text6 != null)
						{
							if (text6 == "to")
							{
								this.GenerateColorToTargets();
								this.apply = new StudioTween.ApplyTween(this.ApplyColorToTargets);
							}
						}
					}
				}
				else
				{
					string text7 = this.method;
					if (text7 != null)
					{
						if (!(text7 == "float"))
						{
							if (!(text7 == "vector2"))
							{
								if (!(text7 == "vector3"))
								{
									if (!(text7 == "color"))
									{
										if (text7 == "rect")
										{
											this.GenerateRectTargets();
											this.apply = new StudioTween.ApplyTween(this.ApplyRectTargets);
										}
									}
									else
									{
										this.GenerateColorTargets();
										this.apply = new StudioTween.ApplyTween(this.ApplyColorTargets);
									}
								}
								else
								{
									this.GenerateVector3Targets();
									this.apply = new StudioTween.ApplyTween(this.ApplyVector3Targets);
								}
							}
							else
							{
								this.GenerateVector2Targets();
								this.apply = new StudioTween.ApplyTween(this.ApplyVector2Targets);
							}
						}
						else
						{
							this.GenerateFloatTargets();
							this.apply = new StudioTween.ApplyTween(this.ApplyFloatTargets);
						}
					}
				}
			}
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x0040A900 File Offset: 0x00408D00
		private void GenerateRectTargets()
		{
			this.rects = new Rect[3];
			this.rects[0] = (Rect)this.tweenArguments["from"];
			this.rects[1] = (Rect)this.tweenArguments["to"];
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x0040A968 File Offset: 0x00408D68
		private void GenerateColorTargets()
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (Color)this.tweenArguments["from"];
			this.colors[0, 1] = (Color)this.tweenArguments["to"];
		}

		// Token: 0x06009E98 RID: 40600 RVA: 0x0040A9D0 File Offset: 0x00408DD0
		private void GenerateVector3Targets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (Vector3)this.tweenArguments["from"];
			this.vector3s[1] = (Vector3)this.tweenArguments["to"];
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009E99 RID: 40601 RVA: 0x0040AA94 File Offset: 0x00408E94
		private void GenerateVector2Targets()
		{
			this.vector2s = new Vector2[3];
			this.vector2s[0] = (Vector2)this.tweenArguments["from"];
			this.vector2s[1] = (Vector2)this.tweenArguments["to"];
			if (this.tweenArguments.Contains("speed"))
			{
				Vector3 a = new Vector3(this.vector2s[0].x, this.vector2s[0].y, 0f);
				Vector3 b = new Vector3(this.vector2s[1].x, this.vector2s[1].y, 0f);
				float num = Math.Abs(Vector3.Distance(a, b));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009E9A RID: 40602 RVA: 0x0040AB94 File Offset: 0x00408F94
		private void GenerateFloatTargets()
		{
			this.floats = new float[3];
			this.floats[0] = (float)this.tweenArguments["from"];
			this.floats[1] = (float)this.tweenArguments["to"];
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(this.floats[0] - this.floats[1]);
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009E9B RID: 40603 RVA: 0x0040AC30 File Offset: 0x00409030
		private void GenerateColorToTargets()
		{
			if (base.GetComponent(typeof(GUITexture)))
			{
				this.colors = new Color[1, 3];
				this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<GUITexture>().color);
			}
			else if (base.GetComponent(typeof(GUIText)))
			{
				this.colors = new Color[1, 3];
				this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<GUIText>().material.color);
			}
			else if (base.GetComponent<Renderer>())
			{
				this.colors = new Color[base.GetComponent<Renderer>().materials.Length, 3];
				for (int i = 0; i < base.GetComponent<Renderer>().materials.Length; i++)
				{
					this.colors[i, 0] = base.GetComponent<Renderer>().materials[i].GetColor(this.namedcolorvalue.ToString());
					this.colors[i, 1] = base.GetComponent<Renderer>().materials[i].GetColor(this.namedcolorvalue.ToString());
				}
			}
			else if (base.GetComponent<Light>())
			{
				this.colors = new Color[1, 3];
				this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<Light>().color);
			}
			else
			{
				this.colors = new Color[1, 3];
			}
			if (this.tweenArguments.Contains("color"))
			{
				for (int j = 0; j < this.colors.GetLength(0); j++)
				{
					this.colors[j, 1] = (Color)this.tweenArguments["color"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("r"))
				{
					for (int k = 0; k < this.colors.GetLength(0); k++)
					{
						this.colors[k, 1].r = (float)this.tweenArguments["r"];
					}
				}
				if (this.tweenArguments.Contains("g"))
				{
					for (int l = 0; l < this.colors.GetLength(0); l++)
					{
						this.colors[l, 1].g = (float)this.tweenArguments["g"];
					}
				}
				if (this.tweenArguments.Contains("b"))
				{
					for (int m = 0; m < this.colors.GetLength(0); m++)
					{
						this.colors[m, 1].b = (float)this.tweenArguments["b"];
					}
				}
				if (this.tweenArguments.Contains("a"))
				{
					for (int n = 0; n < this.colors.GetLength(0); n++)
					{
						this.colors[n, 1].a = (float)this.tweenArguments["a"];
					}
				}
			}
			if (this.tweenArguments.Contains("amount"))
			{
				for (int num = 0; num < this.colors.GetLength(0); num++)
				{
					this.colors[num, 1].a = (float)this.tweenArguments["amount"];
				}
			}
			else if (this.tweenArguments.Contains("alpha"))
			{
				for (int num2 = 0; num2 < this.colors.GetLength(0); num2++)
				{
					this.colors[num2, 1].a = (float)this.tweenArguments["alpha"];
				}
			}
		}

		// Token: 0x06009E9C RID: 40604 RVA: 0x0040B0A8 File Offset: 0x004094A8
		private void GenerateStabTargets()
		{
			if (this.tweenArguments.Contains("audiosource"))
			{
				this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
			}
			else if (base.GetComponent(typeof(AudioSource)))
			{
				this.audioSource = base.GetComponent<AudioSource>();
			}
			else
			{
				base.gameObject.AddComponent(typeof(AudioSource));
				this.audioSource = base.GetComponent<AudioSource>();
				this.audioSource.playOnAwake = false;
			}
			this.audioSource.clip = (AudioClip)this.tweenArguments["audioclip"];
			if (this.tweenArguments.Contains("pitch"))
			{
				this.audioSource.pitch = (float)this.tweenArguments["pitch"];
			}
			if (this.tweenArguments.Contains("volume"))
			{
				this.audioSource.volume = (float)this.tweenArguments["volume"];
			}
			this.time = this.audioSource.clip.length / this.audioSource.pitch;
		}

		// Token: 0x06009E9D RID: 40605 RVA: 0x0040B1F0 File Offset: 0x004095F0
		private void GenerateLookToTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = base.transform.eulerAngles;
			if (this.tweenArguments.Contains("looktarget"))
			{
				if (this.tweenArguments["looktarget"].GetType() == typeof(Transform))
				{
					Transform transform = base.transform;
					Transform target = (Transform)this.tweenArguments["looktarget"];
					Vector3? vector = (Vector3?)this.tweenArguments["up"];
					transform.LookAt(target, (vector == null) ? StudioTween.Defaults.up : vector.Value);
				}
				else if (this.tweenArguments["looktarget"].GetType() == typeof(Vector3))
				{
					Transform transform2 = base.transform;
					Vector3 worldPosition = (Vector3)this.tweenArguments["looktarget"];
					Vector3? vector2 = (Vector3?)this.tweenArguments["up"];
					transform2.LookAt(worldPosition, (vector2 == null) ? StudioTween.Defaults.up : vector2.Value);
				}
			}
			else
			{
				this.Dispose();
			}
			this.vector3s[1] = base.transform.eulerAngles;
			base.transform.eulerAngles = this.vector3s[0];
			if (this.tweenArguments.Contains("axis"))
			{
				string text = (string)this.tweenArguments["axis"];
				if (text != null)
				{
					if (!(text == "x"))
					{
						if (!(text == "y"))
						{
							if (text == "z")
							{
								this.vector3s[1].x = this.vector3s[0].x;
								this.vector3s[1].y = this.vector3s[0].y;
							}
						}
						else
						{
							this.vector3s[1].x = this.vector3s[0].x;
							this.vector3s[1].z = this.vector3s[0].z;
						}
					}
					else
					{
						this.vector3s[1].y = this.vector3s[0].y;
						this.vector3s[1].z = this.vector3s[0].z;
					}
				}
			}
			this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009E9E RID: 40606 RVA: 0x0040B5B8 File Offset: 0x004099B8
		private void GenerateMoveToPathTargets()
		{
			Vector3[] array2;
			if (this.tweenArguments["path"].GetType() == typeof(Vector3[]))
			{
				Vector3[] array = (Vector3[])this.tweenArguments["path"];
				if (array.Length == 1)
				{
					this.Dispose();
				}
				array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
			}
			else
			{
				Transform[] array3 = (Transform[])this.tweenArguments["path"];
				if (array3.Length == 1)
				{
					this.Dispose();
				}
				array2 = (from t in array3
				select t.position).ToArray<Vector3>();
			}
			bool flag;
			int num;
			if (base.transform.position != array2[0])
			{
				if (!this.tweenArguments.Contains("movetopath") || (bool)this.tweenArguments["movetopath"])
				{
					flag = true;
					num = 3;
				}
				else
				{
					flag = false;
					num = 2;
				}
			}
			else
			{
				flag = false;
				num = 2;
			}
			this.vector3s = new Vector3[array2.Length + num];
			if (flag)
			{
				this.vector3s[1] = base.transform.position;
				num = 2;
			}
			else
			{
				num = 1;
			}
			Array.Copy(array2, 0, this.vector3s, num, array2.Length);
			this.vector3s[0] = this.vector3s[1] + (this.vector3s[1] - this.vector3s[2]);
			this.vector3s[this.vector3s.Length - 1] = this.vector3s[this.vector3s.Length - 2] + (this.vector3s[this.vector3s.Length - 2] - this.vector3s[this.vector3s.Length - 3]);
			if (this.vector3s[1] == this.vector3s[this.vector3s.Length - 2])
			{
				Vector3[] array4 = new Vector3[this.vector3s.Length];
				Array.Copy(this.vector3s, array4, this.vector3s.Length);
				array4[0] = array4[array4.Length - 3];
				array4[array4.Length - 1] = array4[2];
				this.vector3s = new Vector3[array4.Length];
				Array.Copy(array4, this.vector3s, array4.Length);
			}
			this.path = new StudioTween.CRSpline(this.vector3s);
			if (this.tweenArguments.Contains("speed"))
			{
				float num2 = StudioTween.PathLength(this.vector3s);
				this.time = num2 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009E9F RID: 40607 RVA: 0x0040B8FC File Offset: 0x00409CFC
		private void GenerateMoveToTargets()
		{
			this.vector3s = new Vector3[3];
			if (this.isLocal)
			{
				this.vector3s[0] = (this.vector3s[1] = base.transform.localPosition);
			}
			else
			{
				this.vector3s[0] = (this.vector3s[1] = base.transform.position);
			}
			if (this.tweenArguments.Contains("position"))
			{
				if (this.tweenArguments["position"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)this.tweenArguments["position"];
					this.vector3s[1] = transform.position;
				}
				else if (this.tweenArguments["position"].GetType() == typeof(Vector3))
				{
					this.vector3s[1] = (Vector3)this.tweenArguments["position"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
			{
				this.tweenArguments["looktarget"] = this.vector3s[1];
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA0 RID: 40608 RVA: 0x0040BBB0 File Offset: 0x00409FB0
		private void GenerateMoveByTargets()
		{
			this.vector3s = new Vector3[6];
			this.vector3s[4] = base.transform.eulerAngles;
			this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = base.transform.position));
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = this.vector3s[0] + (Vector3)this.tweenArguments["amount"];
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = this.vector3s[0].x + (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = this.vector3s[0].y + (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = this.vector3s[0].z + (float)this.tweenArguments["z"];
				}
			}
			base.transform.Translate(this.vector3s[1], this.space);
			this.vector3s[5] = base.transform.position;
			base.transform.position = this.vector3s[0];
			if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
			{
				this.tweenArguments["looktarget"] = this.vector3s[1];
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA1 RID: 40609 RVA: 0x0040BE74 File Offset: 0x0040A274
		private void GenerateScaleToTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (this.vector3s[1] = base.transform.localScale);
			if (this.tweenArguments.Contains("scale"))
			{
				if (this.tweenArguments["scale"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)this.tweenArguments["scale"];
					this.vector3s[1] = transform.localScale;
				}
				else if (this.tweenArguments["scale"].GetType() == typeof(Vector3))
				{
					this.vector3s[1] = (Vector3)this.tweenArguments["scale"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x0040C094 File Offset: 0x0040A494
		private void GenerateScaleByTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (this.vector3s[1] = base.transform.localScale);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = Vector3.Scale(this.vector3s[1], (Vector3)this.tweenArguments["amount"]);
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x * (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y * (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z * (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA3 RID: 40611 RVA: 0x0040C258 File Offset: 0x0040A658
		private void GenerateScaleAddTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (this.vector3s[1] = base.transform.localScale);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x + (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA4 RID: 40612 RVA: 0x0040C414 File Offset: 0x0040A814
		private void GenerateRotateToTargets()
		{
			this.vector3s = new Vector3[3];
			if (this.isLocal)
			{
				this.vector3s[0] = (this.vector3s[1] = base.transform.localEulerAngles);
			}
			else
			{
				this.vector3s[0] = (this.vector3s[1] = base.transform.eulerAngles);
			}
			if (this.tweenArguments.Contains("rotation"))
			{
				if (this.tweenArguments["rotation"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)this.tweenArguments["rotation"];
					this.vector3s[1] = transform.eulerAngles;
				}
				else if (this.tweenArguments["rotation"].GetType() == typeof(Vector3))
				{
					this.vector3s[1] = (Vector3)this.tweenArguments["rotation"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = (float)this.tweenArguments["z"];
				}
			}
			this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA5 RID: 40613 RVA: 0x0040C710 File Offset: 0x0040AB10
		private void GenerateRotateAddTargets()
		{
			this.vector3s = new Vector3[5];
			this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = base.transform.eulerAngles));
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x + (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA6 RID: 40614 RVA: 0x0040C8E0 File Offset: 0x0040ACE0
		private void GenerateRotateByTargets()
		{
			this.vector3s = new Vector3[4];
			this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = base.transform.eulerAngles));
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] += Vector3.Scale((Vector3)this.tweenArguments["amount"], new Vector3(360f, 360f, 360f));
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x + 360f * (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y + 360f * (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z + 360f * (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009EA7 RID: 40615 RVA: 0x0040CAD8 File Offset: 0x0040AED8
		private void ApplyRectTargets()
		{
			this.rects[2].x = this.ease(this.rects[0].x, this.rects[1].x, this._percentage);
			this.rects[2].y = this.ease(this.rects[0].y, this.rects[1].y, this._percentage);
			this.rects[2].width = this.ease(this.rects[0].width, this.rects[1].width, this._percentage);
			this.rects[2].height = this.ease(this.rects[0].height, this.rects[1].height, this._percentage);
			this.tweenArguments["onupdateparams"] = this.rects[2];
			if (this._percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.rects[1];
			}
		}

		// Token: 0x06009EA8 RID: 40616 RVA: 0x0040CC54 File Offset: 0x0040B054
		private void ApplyColorTargets()
		{
			this.colors[0, 2].r = this.ease(this.colors[0, 0].r, this.colors[0, 1].r, this._percentage);
			this.colors[0, 2].g = this.ease(this.colors[0, 0].g, this.colors[0, 1].g, this._percentage);
			this.colors[0, 2].b = this.ease(this.colors[0, 0].b, this.colors[0, 1].b, this._percentage);
			this.colors[0, 2].a = this.ease(this.colors[0, 0].a, this.colors[0, 1].a, this._percentage);
			this.tweenArguments["onupdateparams"] = this.colors[0, 2];
			if (this._percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.colors[0, 1];
			}
		}

		// Token: 0x06009EA9 RID: 40617 RVA: 0x0040CDD4 File Offset: 0x0040B1D4
		private void ApplyVector3Targets()
		{
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			this.tweenArguments["onupdateparams"] = this.vector3s[2];
			if (this._percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.vector3s[1];
			}
		}

		// Token: 0x06009EAA RID: 40618 RVA: 0x0040CF0C File Offset: 0x0040B30C
		private void ApplyVector2Targets()
		{
			this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this._percentage);
			this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this._percentage);
			this.tweenArguments["onupdateparams"] = this.vector2s[2];
			if (this._percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.vector2s[1];
			}
		}

		// Token: 0x06009EAB RID: 40619 RVA: 0x0040D000 File Offset: 0x0040B400
		private void ApplyFloatTargets()
		{
			this.floats[2] = this.ease(this.floats[0], this.floats[1], this._percentage);
			this.tweenArguments["onupdateparams"] = this.floats[2];
			if (this._percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.floats[1];
			}
		}

		// Token: 0x06009EAC RID: 40620 RVA: 0x0040D080 File Offset: 0x0040B480
		private void ApplyColorToTargets()
		{
			for (int i = 0; i < this.colors.GetLength(0); i++)
			{
				this.colors[i, 2].r = this.ease(this.colors[i, 0].r, this.colors[i, 1].r, this._percentage);
				this.colors[i, 2].g = this.ease(this.colors[i, 0].g, this.colors[i, 1].g, this._percentage);
				this.colors[i, 2].b = this.ease(this.colors[i, 0].b, this.colors[i, 1].b, this._percentage);
				this.colors[i, 2].a = this.ease(this.colors[i, 0].a, this.colors[i, 1].a, this._percentage);
			}
			if (base.GetComponent(typeof(GUITexture)))
			{
				base.GetComponent<GUITexture>().color = this.colors[0, 2];
			}
			else if (base.GetComponent(typeof(GUIText)))
			{
				base.GetComponent<GUIText>().material.color = this.colors[0, 2];
			}
			else if (base.GetComponent<Renderer>())
			{
				for (int j = 0; j < this.colors.GetLength(0); j++)
				{
					base.GetComponent<Renderer>().materials[j].SetColor(this.namedcolorvalue.ToString(), this.colors[j, 2]);
				}
			}
			else if (base.GetComponent<Light>())
			{
				base.GetComponent<Light>().color = this.colors[0, 2];
			}
			if (this._percentage == 1f)
			{
				if (base.GetComponent(typeof(GUITexture)))
				{
					base.GetComponent<GUITexture>().color = this.colors[0, 1];
				}
				else if (base.GetComponent(typeof(GUIText)))
				{
					base.GetComponent<GUIText>().material.color = this.colors[0, 1];
				}
				else if (base.GetComponent<Renderer>())
				{
					for (int k = 0; k < this.colors.GetLength(0); k++)
					{
						base.GetComponent<Renderer>().materials[k].SetColor(this.namedcolorvalue.ToString(), this.colors[k, 1]);
					}
				}
				else if (base.GetComponent<Light>())
				{
					base.GetComponent<Light>().color = this.colors[0, 1];
				}
			}
		}

		// Token: 0x06009EAD RID: 40621 RVA: 0x0040D3D0 File Offset: 0x0040B7D0
		private void ApplyMoveToPathTargets()
		{
			this.preUpdate = base.transform.position;
			float value = this.ease(0f, 1f, this._percentage);
			if (this.isLocal)
			{
				base.transform.localPosition = this.path.Interp(Mathf.Clamp(value, 0f, 1f));
			}
			else
			{
				base.transform.position = this.path.Interp(Mathf.Clamp(value, 0f, 1f));
			}
			if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
			{
				float num = (!this.tweenArguments.Contains("lookahead")) ? StudioTween.Defaults.lookAhead : ((float)this.tweenArguments["lookahead"]);
				float value2 = this.ease(0f, 1f, Mathf.Min(1f, this._percentage + num));
				this.tweenArguments["looktarget"] = this.path.Interp(Mathf.Clamp(value2, 0f, 1f));
			}
			this.postUpdate = base.transform.position;
			if (this.physics)
			{
				base.transform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06009EAE RID: 40622 RVA: 0x0040D560 File Offset: 0x0040B960
		private void ApplyMoveToTargets()
		{
			this.preUpdate = base.transform.position;
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			if (this.isLocal)
			{
				base.transform.localPosition = this.vector3s[2];
			}
			else
			{
				base.transform.position = this.vector3s[2];
			}
			if (this._percentage == 1f)
			{
				if (this.isLocal)
				{
					base.transform.localPosition = this.vector3s[1];
				}
				else
				{
					base.transform.position = this.vector3s[1];
				}
			}
			this.postUpdate = base.transform.position;
			if (this.physics)
			{
				base.transform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06009EAF RID: 40623 RVA: 0x0040D728 File Offset: 0x0040BB28
		private void ApplyMoveByTargets()
		{
			this.preUpdate = base.transform.position;
			Vector3 eulerAngles = default(Vector3);
			if (this.tweenArguments.Contains("looktarget"))
			{
				eulerAngles = base.transform.eulerAngles;
				base.transform.eulerAngles = this.vector3s[4];
			}
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			base.transform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			if (this.tweenArguments.Contains("looktarget"))
			{
				base.transform.eulerAngles = eulerAngles;
			}
			this.postUpdate = base.transform.position;
			if (this.physics)
			{
				base.transform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06009EB0 RID: 40624 RVA: 0x0040D910 File Offset: 0x0040BD10
		private void ApplyScaleToTargets()
		{
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			base.transform.localScale = this.vector3s[2];
			if (this._percentage == 1f)
			{
				base.transform.localScale = this.vector3s[1];
			}
		}

		// Token: 0x06009EB1 RID: 40625 RVA: 0x0040DA34 File Offset: 0x0040BE34
		private void ApplyLookToTargets()
		{
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			if (this.isLocal)
			{
				base.transform.localRotation = Quaternion.Euler(this.vector3s[2]);
			}
			else
			{
				base.transform.rotation = Quaternion.Euler(this.vector3s[2]);
			}
		}

		// Token: 0x06009EB2 RID: 40626 RVA: 0x0040DB60 File Offset: 0x0040BF60
		private void ApplyRotateToTargets()
		{
			this.preUpdate = base.transform.eulerAngles;
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			if (this.isLocal)
			{
				base.transform.localRotation = Quaternion.Euler(this.vector3s[2]);
			}
			else
			{
				base.transform.rotation = Quaternion.Euler(this.vector3s[2]);
			}
			if (this._percentage == 1f)
			{
				if (this.isLocal)
				{
					base.transform.localRotation = Quaternion.Euler(this.vector3s[1]);
				}
				else
				{
					base.transform.rotation = Quaternion.Euler(this.vector3s[1]);
				}
			}
			this.postUpdate = base.transform.eulerAngles;
			if (this.physics)
			{
				base.transform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06009EB3 RID: 40627 RVA: 0x0040DD44 File Offset: 0x0040C144
		private void ApplyRotateAddTargets()
		{
			this.preUpdate = base.transform.eulerAngles;
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this._percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this._percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this._percentage);
			base.transform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			this.postUpdate = base.transform.eulerAngles;
			if (this.physics)
			{
				base.transform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06009EB4 RID: 40628 RVA: 0x0040DECC File Offset: 0x0040C2CC
		private IEnumerator TweenDelay()
		{
			this.delayStarted = Time.time;
			yield return new WaitForSeconds(this.delay);
			if (this.wasPaused)
			{
				this.wasPaused = false;
				this.TweenStart();
			}
			yield break;
		}

		// Token: 0x06009EB5 RID: 40629 RVA: 0x0040DEE8 File Offset: 0x0040C2E8
		private void TweenStart()
		{
			if (this.disposableUpdate != null)
			{
				this.disposableUpdate.Dispose();
				this.disposableUpdate = null;
			}
			if (this.disposableFixedUpdate != null)
			{
				this.disposableFixedUpdate.Dispose();
				this.disposableFixedUpdate = null;
			}
			if (this.disposableLateUpdate != null)
			{
				this.disposableLateUpdate.Dispose();
				this.disposableLateUpdate = null;
			}
			if (this.onStart != null)
			{
				this.onStart();
			}
			if (!this.loop)
			{
				this.ConflictCheck();
				this.GenerateTargets();
			}
			this.isRunning = true;
			if (!this.physics)
			{
				this.disposableUpdate = new SingleAssignmentDisposable();
				this.disposableUpdate.Disposable = (from _ in this.UpdateAsObservable()
				where this.isRunning
				select _).Subscribe(delegate(Unit _)
				{
					if (!this.reverse)
					{
						if (this._percentage < 1f)
						{
							this.TweenUpdate();
						}
						else
						{
							this.TweenComplete();
						}
					}
					else if (this._percentage > 0f)
					{
						this.TweenUpdate();
					}
					else
					{
						this.TweenComplete();
					}
				}).AddTo(this);
			}
			if (this.physics)
			{
				this.disposableFixedUpdate = new SingleAssignmentDisposable();
				this.disposableFixedUpdate.Disposable = (from _ in this.FixedUpdateAsObservable()
				where this.isRunning
				select _).Subscribe(delegate(Unit _)
				{
					if (!this.reverse)
					{
						if (this._percentage < 1f)
						{
							this.TweenUpdate();
						}
						else
						{
							this.TweenComplete();
						}
					}
					else if (this._percentage > 0f)
					{
						this.TweenUpdate();
					}
					else
					{
						this.TweenComplete();
					}
				}).AddTo(this);
			}
			if (this.type == "move")
			{
				this.disposableLateUpdate = new SingleAssignmentDisposable();
				this.disposableLateUpdate.Disposable = (from _ in this.LateUpdateAsObservable()
				where this.isRunning
				where this.tweenArguments.Contains("looktarget")
				select _).Subscribe(delegate(Unit _)
				{
					StudioTween.LookUpdate(base.gameObject, this.tweenArguments);
				}).AddTo(this);
			}
		}

		// Token: 0x06009EB6 RID: 40630 RVA: 0x0040E088 File Offset: 0x0040C488
		private IEnumerator TweenRestart()
		{
			if (this.delay > 0f)
			{
				this.delayStarted = Time.time;
				yield return new WaitForSeconds(this.delay);
			}
			this.loop = true;
			this.TweenStart();
			yield break;
		}

		// Token: 0x06009EB7 RID: 40631 RVA: 0x0040E0A3 File Offset: 0x0040C4A3
		private void TweenUpdate()
		{
			this.apply();
			if (this.onUpdate != null)
			{
				this.onUpdate(this._percentage);
			}
			this.UpdatePercentage();
		}

		// Token: 0x06009EB8 RID: 40632 RVA: 0x0040E0D4 File Offset: 0x0040C4D4
		private void TweenComplete()
		{
			this.isRunning = false;
			this._percentage = ((this._percentage <= 0.5f) ? 0f : 1f);
			this.apply();
			if (this.type == "value" && this.onUpdate != null)
			{
				this.onUpdate(this._percentage);
			}
			if (this.nowIndex + 1 < this._listArguments.Count)
			{
				this.Next();
				return;
			}
			if (this.loopType == StudioTween.LoopType.none)
			{
				this.Dispose();
			}
			else
			{
				this.TweenLoop();
			}
			if (this.onComplete != null)
			{
				this.onComplete();
			}
		}

		// Token: 0x06009EB9 RID: 40633 RVA: 0x0040E19C File Offset: 0x0040C59C
		private void TweenLoop()
		{
			StudioTween.LoopType loopType = this.loopType;
			if (loopType != StudioTween.LoopType.loop)
			{
				if (loopType == StudioTween.LoopType.pingPong)
				{
					this.reverse = !this.reverse;
					this.runningTime = 0f;
					base.StartCoroutine("TweenRestart");
				}
			}
			else
			{
				this.nowIndex = -1;
				this.Next();
			}
		}

		// Token: 0x06009EBA RID: 40634 RVA: 0x0040E200 File Offset: 0x0040C600
		public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
		{
			Rect result = new Rect(StudioTween.FloatUpdate(currentValue.x, targetValue.x, speed), StudioTween.FloatUpdate(currentValue.y, targetValue.y, speed), StudioTween.FloatUpdate(currentValue.width, targetValue.width, speed), StudioTween.FloatUpdate(currentValue.height, targetValue.height, speed));
			return result;
		}

		// Token: 0x06009EBB RID: 40635 RVA: 0x0040E268 File Offset: 0x0040C668
		public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
		{
			Vector3 a = targetValue - currentValue;
			currentValue += a * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06009EBC RID: 40636 RVA: 0x0040E298 File Offset: 0x0040C698
		public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
		{
			Vector2 a = targetValue - currentValue;
			currentValue += a * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06009EBD RID: 40637 RVA: 0x0040E2C8 File Offset: 0x0040C6C8
		public static float FloatUpdate(float currentValue, float targetValue, float speed)
		{
			float num = targetValue - currentValue;
			currentValue += num * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06009EBE RID: 40638 RVA: 0x0040E2E7 File Offset: 0x0040C6E7
		public static void FadeUpdate(GameObject target, Hashtable args)
		{
			args["a"] = args["alpha"];
			StudioTween.ColorUpdate(target, args);
		}

		// Token: 0x06009EBF RID: 40639 RVA: 0x0040E306 File Offset: 0x0040C706
		public static void FadeUpdate(GameObject target, float alpha, float time)
		{
			StudioTween.FadeUpdate(target, StudioTween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06009EC0 RID: 40640 RVA: 0x0040E33C File Offset: 0x0040C73C
		public static void ColorUpdate(GameObject target, Hashtable args)
		{
			StudioTween.CleanArgs(args);
			Color[] array = new Color[4];
			if (!args.Contains("includechildren") || (bool)args["includechildren"])
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.ColorUpdate(transform.gameObject, args);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = StudioTween.Defaults.updateTime;
			}
			if (target.GetComponent(typeof(GUITexture)))
			{
				array[0] = (array[1] = target.GetComponent<GUITexture>().color);
			}
			else if (target.GetComponent(typeof(GUIText)))
			{
				array[0] = (array[1] = target.GetComponent<GUIText>().material.color);
			}
			else if (target.GetComponent<Renderer>())
			{
				array[0] = (array[1] = target.GetComponent<Renderer>().material.color);
			}
			else if (target.GetComponent<Light>())
			{
				array[0] = (array[1] = target.GetComponent<Light>().color);
			}
			if (args.Contains("color"))
			{
				array[1] = (Color)args["color"];
			}
			else
			{
				if (args.Contains("r"))
				{
					array[1].r = (float)args["r"];
				}
				if (args.Contains("g"))
				{
					array[1].g = (float)args["g"];
				}
				if (args.Contains("b"))
				{
					array[1].b = (float)args["b"];
				}
				if (args.Contains("a"))
				{
					array[1].a = (float)args["a"];
				}
			}
			array[3].r = Mathf.SmoothDamp(array[0].r, array[1].r, ref array[2].r, num);
			array[3].g = Mathf.SmoothDamp(array[0].g, array[1].g, ref array[2].g, num);
			array[3].b = Mathf.SmoothDamp(array[0].b, array[1].b, ref array[2].b, num);
			array[3].a = Mathf.SmoothDamp(array[0].a, array[1].a, ref array[2].a, num);
			if (target.GetComponent(typeof(GUITexture)))
			{
				target.GetComponent<GUITexture>().color = array[3];
			}
			else if (target.GetComponent(typeof(GUIText)))
			{
				target.GetComponent<GUIText>().material.color = array[3];
			}
			else if (target.GetComponent<Renderer>())
			{
				target.GetComponent<Renderer>().material.color = array[3];
			}
			else if (target.GetComponent<Light>())
			{
				target.GetComponent<Light>().color = array[3];
			}
		}

		// Token: 0x06009EC1 RID: 40641 RVA: 0x0040E7A0 File Offset: 0x0040CBA0
		public static void ColorUpdate(GameObject target, Color color, float time)
		{
			StudioTween.ColorUpdate(target, StudioTween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06009EC2 RID: 40642 RVA: 0x0040E7D8 File Offset: 0x0040CBD8
		public static void AudioUpdate(GameObject target, Hashtable args)
		{
			StudioTween.CleanArgs(args);
			Vector2[] array = new Vector2[4];
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = StudioTween.Defaults.updateTime;
			}
			AudioSource audioSource;
			if (args.Contains("audiosource"))
			{
				audioSource = (AudioSource)args["audiosource"];
			}
			else
			{
				if (!target.GetComponent(typeof(AudioSource)))
				{
					return;
				}
				audioSource = target.GetComponent<AudioSource>();
			}
			array[0] = (array[1] = new Vector2(audioSource.volume, audioSource.pitch));
			if (args.Contains("volume"))
			{
				array[1].x = (float)args["volume"];
			}
			if (args.Contains("pitch"))
			{
				array[1].y = (float)args["pitch"];
			}
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			audioSource.volume = array[3].x;
			audioSource.pitch = array[3].y;
		}

		// Token: 0x06009EC3 RID: 40643 RVA: 0x0040E988 File Offset: 0x0040CD88
		public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
		{
			StudioTween.AudioUpdate(target, StudioTween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06009EC4 RID: 40644 RVA: 0x0040E9DC File Offset: 0x0040CDDC
		public static void RotateUpdate(GameObject target, Hashtable args)
		{
			StudioTween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			Vector3 eulerAngles = target.transform.eulerAngles;
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = StudioTween.Defaults.updateTime;
			}
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = StudioTween.Defaults.isLocal;
			}
			if (flag)
			{
				array[0] = target.transform.localEulerAngles;
			}
			else
			{
				array[0] = target.transform.eulerAngles;
			}
			if (args.Contains("rotation"))
			{
				if (args["rotation"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["rotation"];
					array[1] = transform.eulerAngles;
				}
				else if (args["rotation"].GetType() == typeof(Vector3))
				{
					array[1] = (Vector3)args["rotation"];
				}
			}
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
			if (flag)
			{
				target.transform.localEulerAngles = array[3];
			}
			else
			{
				target.transform.eulerAngles = array[3];
			}
			if (target.GetComponent<Rigidbody>() != null)
			{
				Vector3 eulerAngles2 = target.transform.eulerAngles;
				target.transform.eulerAngles = eulerAngles;
				target.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(eulerAngles2));
			}
		}

		// Token: 0x06009EC5 RID: 40645 RVA: 0x0040EC51 File Offset: 0x0040D051
		public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
		{
			StudioTween.RotateUpdate(target, StudioTween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06009EC6 RID: 40646 RVA: 0x0040EC88 File Offset: 0x0040D088
		public static void ScaleUpdate(GameObject target, Hashtable args)
		{
			StudioTween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = StudioTween.Defaults.updateTime;
			}
			array[0] = (array[1] = target.transform.localScale);
			if (args.Contains("scale"))
			{
				if (args["scale"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["scale"];
					array[1] = transform.localScale;
				}
				else if (args["scale"].GetType() == typeof(Vector3))
				{
					array[1] = (Vector3)args["scale"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					array[1].x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					array[1].y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					array[1].z = (float)args["z"];
				}
			}
			array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
			target.transform.localScale = array[3];
		}

		// Token: 0x06009EC7 RID: 40647 RVA: 0x0040EEDB File Offset: 0x0040D2DB
		public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
		{
			StudioTween.ScaleUpdate(target, StudioTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06009EC8 RID: 40648 RVA: 0x0040EF10 File Offset: 0x0040D310
		public static void MoveUpdate(GameObject target, Hashtable args)
		{
			StudioTween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			Vector3 position = target.transform.position;
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = StudioTween.Defaults.updateTime;
			}
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = StudioTween.Defaults.isLocal;
			}
			if (flag)
			{
				array[0] = (array[1] = target.transform.localPosition);
			}
			else
			{
				array[0] = (array[1] = target.transform.position);
			}
			if (args.Contains("position"))
			{
				if (args["position"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["position"];
					array[1] = transform.position;
				}
				else if (args["position"].GetType() == typeof(Vector3))
				{
					array[1] = (Vector3)args["position"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					array[1].x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					array[1].y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					array[1].z = (float)args["z"];
				}
			}
			array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
			if (args.Contains("orienttopath") && (bool)args["orienttopath"])
			{
				args["looktarget"] = array[3];
			}
			if (args.Contains("looktarget"))
			{
				Tween.LookUpdate(target, args);
			}
			if (flag)
			{
				target.transform.localPosition = array[3];
			}
			else
			{
				target.transform.position = array[3];
			}
			if (target.GetComponent<Rigidbody>() != null)
			{
				Vector3 position2 = target.transform.position;
				target.transform.position = position;
				target.GetComponent<Rigidbody>().MovePosition(position2);
			}
		}

		// Token: 0x06009EC9 RID: 40649 RVA: 0x0040F283 File Offset: 0x0040D683
		public static void MoveUpdate(GameObject target, Vector3 position, float time)
		{
			StudioTween.MoveUpdate(target, StudioTween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06009ECA RID: 40650 RVA: 0x0040F2B8 File Offset: 0x0040D6B8
		public static void LookUpdate(GameObject target, Hashtable args)
		{
			StudioTween.CleanArgs(args);
			Vector3[] array = new Vector3[5];
			float num;
			if (args.Contains("looktime"))
			{
				num = (float)args["looktime"];
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else if (args.Contains("time"))
			{
				num = (float)args["time"] * 0.15f;
				num *= StudioTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = StudioTween.Defaults.updateTime;
			}
			array[0] = target.transform.eulerAngles;
			if (args.Contains("looktarget"))
			{
				if (args["looktarget"].GetType() == typeof(Transform))
				{
					Transform transform = target.transform;
					Transform target2 = (Transform)args["looktarget"];
					Vector3? vector = (Vector3?)args["up"];
					transform.LookAt(target2, (vector == null) ? StudioTween.Defaults.up : vector.Value);
				}
				else if (args["looktarget"].GetType() == typeof(Vector3))
				{
					Transform transform2 = target.transform;
					Vector3 worldPosition = (Vector3)args["looktarget"];
					Vector3? vector2 = (Vector3?)args["up"];
					transform2.LookAt(worldPosition, (vector2 == null) ? StudioTween.Defaults.up : vector2.Value);
				}
				array[1] = target.transform.eulerAngles;
				target.transform.eulerAngles = array[0];
				array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
				array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
				array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
				target.transform.eulerAngles = array[3];
				if (args.Contains("axis"))
				{
					array[4] = target.transform.eulerAngles;
					string text = (string)args["axis"];
					if (text != null)
					{
						if (!(text == "x"))
						{
							if (!(text == "y"))
							{
								if (text == "z")
								{
									array[4].x = array[0].x;
									array[4].y = array[0].y;
								}
							}
							else
							{
								array[4].x = array[0].x;
								array[4].z = array[0].z;
							}
						}
						else
						{
							array[4].y = array[0].y;
							array[4].z = array[0].z;
						}
					}
					target.transform.eulerAngles = array[4];
				}
				return;
			}
		}

		// Token: 0x06009ECB RID: 40651 RVA: 0x0040F65C File Offset: 0x0040DA5C
		public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
		{
			StudioTween.LookUpdate(target, StudioTween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06009ECC RID: 40652 RVA: 0x0040F694 File Offset: 0x0040DA94
		public static float PathLength(Vector3[] path)
		{
			float num = 0f;
			Vector3[] pts = StudioTween.PathControlPointGenerator(path);
			Vector3 a = StudioTween.Interp(pts, 0f);
			int num2 = path.Length * 20;
			for (int i = 1; i <= num2; i++)
			{
				float t = (float)i / (float)num2;
				Vector3 vector = StudioTween.Interp(pts, t);
				num += Vector3.Distance(a, vector);
				a = vector;
			}
			return num;
		}

		// Token: 0x06009ECD RID: 40653 RVA: 0x0040F6F8 File Offset: 0x0040DAF8
		public static Texture2D CameraTexture(Color color)
		{
			Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
			Color[] array = new Color[Screen.width * Screen.height];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = color;
			}
			texture2D.SetPixels(array);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06009ECE RID: 40654 RVA: 0x0040F757 File Offset: 0x0040DB57
		public static void PutOnPath(GameObject target, Vector3[] path, float percent)
		{
			target.transform.position = StudioTween.Interp(StudioTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06009ECF RID: 40655 RVA: 0x0040F770 File Offset: 0x0040DB70
		public static void PutOnPath(Transform target, Vector3[] path, float percent)
		{
			target.position = StudioTween.Interp(StudioTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06009ED0 RID: 40656 RVA: 0x0040F784 File Offset: 0x0040DB84
		public static void PutOnPath(GameObject target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.transform.position = StudioTween.Interp(StudioTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06009ED1 RID: 40657 RVA: 0x0040F7DC File Offset: 0x0040DBDC
		public static void PutOnPath(Transform target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.position = StudioTween.Interp(StudioTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06009ED2 RID: 40658 RVA: 0x0040F82C File Offset: 0x0040DC2C
		public static Vector3 PointOnPath(Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			return StudioTween.Interp(StudioTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06009ED3 RID: 40659 RVA: 0x0040F876 File Offset: 0x0040DC76
		public static void DrawLine(Vector3[] line)
		{
			if (line.Length > 0)
			{
				StudioTween.DrawLineHelper(line, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009ED4 RID: 40660 RVA: 0x0040F891 File Offset: 0x0040DC91
		public static void DrawLine(Vector3[] line, Color color)
		{
			if (line.Length > 0)
			{
				StudioTween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06009ED5 RID: 40661 RVA: 0x0040F8A8 File Offset: 0x0040DCA8
		public static void DrawLine(Transform[] line)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				StudioTween.DrawLineHelper(array, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009ED6 RID: 40662 RVA: 0x0040F900 File Offset: 0x0040DD00
		public static void DrawLine(Transform[] line, Color color)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				StudioTween.DrawLineHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009ED7 RID: 40663 RVA: 0x0040F953 File Offset: 0x0040DD53
		public static void DrawLineGizmos(Vector3[] line)
		{
			if (line.Length > 0)
			{
				StudioTween.DrawLineHelper(line, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009ED8 RID: 40664 RVA: 0x0040F96E File Offset: 0x0040DD6E
		public static void DrawLineGizmos(Vector3[] line, Color color)
		{
			if (line.Length > 0)
			{
				StudioTween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06009ED9 RID: 40665 RVA: 0x0040F988 File Offset: 0x0040DD88
		public static void DrawLineGizmos(Transform[] line)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				StudioTween.DrawLineHelper(array, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009EDA RID: 40666 RVA: 0x0040F9E0 File Offset: 0x0040DDE0
		public static void DrawLineGizmos(Transform[] line, Color color)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				StudioTween.DrawLineHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009EDB RID: 40667 RVA: 0x0040FA33 File Offset: 0x0040DE33
		public static void DrawLineHandles(Vector3[] line)
		{
			if (line.Length > 0)
			{
				StudioTween.DrawLineHelper(line, StudioTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009EDC RID: 40668 RVA: 0x0040FA4E File Offset: 0x0040DE4E
		public static void DrawLineHandles(Vector3[] line, Color color)
		{
			if (line.Length > 0)
			{
				StudioTween.DrawLineHelper(line, color, "handles");
			}
		}

		// Token: 0x06009EDD RID: 40669 RVA: 0x0040FA68 File Offset: 0x0040DE68
		public static void DrawLineHandles(Transform[] line)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				StudioTween.DrawLineHelper(array, StudioTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009EDE RID: 40670 RVA: 0x0040FAC0 File Offset: 0x0040DEC0
		public static void DrawLineHandles(Transform[] line, Color color)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				StudioTween.DrawLineHelper(array, color, "handles");
			}
		}

		// Token: 0x06009EDF RID: 40671 RVA: 0x0040FB13 File Offset: 0x0040DF13
		public static Vector3 PointOnPath(Vector3[] path, float percent)
		{
			return StudioTween.Interp(StudioTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06009EE0 RID: 40672 RVA: 0x0040FB21 File Offset: 0x0040DF21
		public static void DrawPath(Vector3[] path)
		{
			if (path.Length > 0)
			{
				StudioTween.DrawPathHelper(path, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009EE1 RID: 40673 RVA: 0x0040FB3C File Offset: 0x0040DF3C
		public static void DrawPath(Vector3[] path, Color color)
		{
			if (path.Length > 0)
			{
				StudioTween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06009EE2 RID: 40674 RVA: 0x0040FB54 File Offset: 0x0040DF54
		public static void DrawPath(Transform[] path)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				StudioTween.DrawPathHelper(array, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009EE3 RID: 40675 RVA: 0x0040FBAC File Offset: 0x0040DFAC
		public static void DrawPath(Transform[] path, Color color)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				StudioTween.DrawPathHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009EE4 RID: 40676 RVA: 0x0040FBFF File Offset: 0x0040DFFF
		public static void DrawPathGizmos(Vector3[] path)
		{
			if (path.Length > 0)
			{
				StudioTween.DrawPathHelper(path, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009EE5 RID: 40677 RVA: 0x0040FC1A File Offset: 0x0040E01A
		public static void DrawPathGizmos(Vector3[] path, Color color)
		{
			if (path.Length > 0)
			{
				StudioTween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06009EE6 RID: 40678 RVA: 0x0040FC34 File Offset: 0x0040E034
		public static void DrawPathGizmos(Transform[] path)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				StudioTween.DrawPathHelper(array, StudioTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009EE7 RID: 40679 RVA: 0x0040FC8C File Offset: 0x0040E08C
		public static void DrawPathGizmos(Transform[] path, Color color)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				StudioTween.DrawPathHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009EE8 RID: 40680 RVA: 0x0040FCDF File Offset: 0x0040E0DF
		public static void DrawPathHandles(Vector3[] path)
		{
			if (path.Length > 0)
			{
				StudioTween.DrawPathHelper(path, StudioTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009EE9 RID: 40681 RVA: 0x0040FCFA File Offset: 0x0040E0FA
		public static void DrawPathHandles(Vector3[] path, Color color)
		{
			if (path.Length > 0)
			{
				StudioTween.DrawPathHelper(path, color, "handles");
			}
		}

		// Token: 0x06009EEA RID: 40682 RVA: 0x0040FD14 File Offset: 0x0040E114
		public static void DrawPathHandles(Transform[] path)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				StudioTween.DrawPathHelper(array, StudioTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009EEB RID: 40683 RVA: 0x0040FD6C File Offset: 0x0040E16C
		public static void DrawPathHandles(Transform[] path, Color color)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				StudioTween.DrawPathHelper(array, color, "handles");
			}
		}

		// Token: 0x06009EEC RID: 40684 RVA: 0x0040FDC0 File Offset: 0x0040E1C0
		public static void CameraFadeDepth(int depth)
		{
			if (StudioTween.cameraFade)
			{
				StudioTween.cameraFade.transform.position = new Vector3(StudioTween.cameraFade.transform.position.x, StudioTween.cameraFade.transform.position.y, (float)depth);
			}
		}

		// Token: 0x06009EED RID: 40685 RVA: 0x0040FE20 File Offset: 0x0040E220
		public static void CameraFadeDestroy()
		{
			if (StudioTween.cameraFade)
			{
				UnityEngine.Object.Destroy(StudioTween.cameraFade);
			}
		}

		// Token: 0x06009EEE RID: 40686 RVA: 0x0040FE3B File Offset: 0x0040E23B
		public static void CameraFadeSwap(Texture2D texture)
		{
			if (StudioTween.cameraFade)
			{
				StudioTween.cameraFade.GetComponent<GUITexture>().texture = texture;
			}
		}

		// Token: 0x06009EEF RID: 40687 RVA: 0x0040FE5C File Offset: 0x0040E25C
		public static GameObject CameraFadeAdd(Texture2D texture, int depth)
		{
			if (StudioTween.cameraFade)
			{
				return null;
			}
			StudioTween.cameraFade = new GameObject("iTween Camera Fade");
			StudioTween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)depth);
			StudioTween.cameraFade.AddComponent<GUITexture>();
			StudioTween.cameraFade.GetComponent<GUITexture>().texture = texture;
			StudioTween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
			return StudioTween.cameraFade;
		}

		// Token: 0x06009EF0 RID: 40688 RVA: 0x0040FEF4 File Offset: 0x0040E2F4
		public static GameObject CameraFadeAdd(Texture2D texture)
		{
			if (StudioTween.cameraFade)
			{
				return null;
			}
			StudioTween.cameraFade = new GameObject("iTween Camera Fade");
			StudioTween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)StudioTween.Defaults.cameraFadeDepth);
			StudioTween.cameraFade.AddComponent<GUITexture>();
			StudioTween.cameraFade.GetComponent<GUITexture>().texture = texture;
			StudioTween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
			return StudioTween.cameraFade;
		}

		// Token: 0x06009EF1 RID: 40689 RVA: 0x0040FF90 File Offset: 0x0040E390
		public static GameObject CameraFadeAdd()
		{
			if (StudioTween.cameraFade)
			{
				return null;
			}
			StudioTween.cameraFade = new GameObject("iTween Camera Fade");
			StudioTween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)StudioTween.Defaults.cameraFadeDepth);
			StudioTween.cameraFade.AddComponent<GUITexture>();
			StudioTween.cameraFade.GetComponent<GUITexture>().texture = StudioTween.CameraTexture(Color.black);
			StudioTween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
			return StudioTween.cameraFade;
		}

		// Token: 0x06009EF2 RID: 40690 RVA: 0x00410034 File Offset: 0x0040E434
		public static void Resume(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				tween.enabled = true;
			}
		}

		// Token: 0x06009EF3 RID: 40691 RVA: 0x00410078 File Offset: 0x0040E478
		public static void Resume(GameObject target, bool includechildren)
		{
			StudioTween.Resume(target);
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.Resume(transform.gameObject, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009EF4 RID: 40692 RVA: 0x004100F0 File Offset: 0x0040E4F0
		public static void Resume(GameObject target, string type)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					tween.enabled = true;
				}
			}
		}

		// Token: 0x06009EF5 RID: 40693 RVA: 0x00410170 File Offset: 0x0040E570
		public static void Resume(GameObject target, string type, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					tween.enabled = true;
				}
			}
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.Resume(transform.gameObject, type, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009EF6 RID: 40694 RVA: 0x0041025C File Offset: 0x0040E65C
		public static void Resume()
		{
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject target = (GameObject)hashtable["target"];
				StudioTween.Resume(target);
			}
		}

		// Token: 0x06009EF7 RID: 40695 RVA: 0x004102AC File Offset: 0x0040E6AC
		public static void Resume(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				StudioTween.Resume((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06009EF8 RID: 40696 RVA: 0x00410338 File Offset: 0x0040E738
		public static void Pause(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				if (studioTween.delay > 0f)
				{
					studioTween.delay -= Time.time - studioTween.delayStarted;
					studioTween.StopCoroutine("TweenDelay");
				}
				studioTween.isPaused = true;
				studioTween.enabled = false;
			}
		}

		// Token: 0x06009EF9 RID: 40697 RVA: 0x004103B8 File Offset: 0x0040E7B8
		public static void Pause(GameObject target, bool includechildren)
		{
			StudioTween.Pause(target);
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.Pause(transform.gameObject, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009EFA RID: 40698 RVA: 0x00410430 File Offset: 0x0040E830
		public static void Pause(GameObject target, string type)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				string text = studioTween.type + studioTween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					if (studioTween.delay > 0f)
					{
						studioTween.delay -= Time.time - studioTween.delayStarted;
						studioTween.StopCoroutine("TweenDelay");
					}
					studioTween.isPaused = true;
					studioTween.enabled = false;
				}
			}
		}

		// Token: 0x06009EFB RID: 40699 RVA: 0x004104EC File Offset: 0x0040E8EC
		public static void Pause(GameObject target, string type, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				string text = studioTween.type + studioTween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					if (studioTween.delay > 0f)
					{
						studioTween.delay -= Time.time - studioTween.delayStarted;
						studioTween.StopCoroutine("TweenDelay");
					}
					studioTween.isPaused = true;
					studioTween.enabled = false;
				}
			}
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.Pause(transform.gameObject, type, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009EFC RID: 40700 RVA: 0x00410614 File Offset: 0x0040EA14
		public static void Pause()
		{
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject target = (GameObject)hashtable["target"];
				StudioTween.Pause(target);
			}
		}

		// Token: 0x06009EFD RID: 40701 RVA: 0x00410664 File Offset: 0x0040EA64
		public static void Pause(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				StudioTween.Pause((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06009EFE RID: 40702 RVA: 0x004106F0 File Offset: 0x0040EAF0
		public static void Stop()
		{
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject target = (GameObject)hashtable["target"];
				StudioTween.Stop(target);
			}
			StudioTween.tweens.Clear();
		}

		// Token: 0x06009EFF RID: 40703 RVA: 0x0041074C File Offset: 0x0040EB4C
		public static void Stop(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				StudioTween.Stop((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06009F00 RID: 40704 RVA: 0x004107D8 File Offset: 0x0040EBD8
		public static void StopByName(string name)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				StudioTween.StopByName((GameObject)arrayList[j], name);
			}
		}

		// Token: 0x06009F01 RID: 40705 RVA: 0x00410864 File Offset: 0x0040EC64
		public static void Stop(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				studioTween.Dispose();
			}
		}

		// Token: 0x06009F02 RID: 40706 RVA: 0x004108A8 File Offset: 0x0040ECA8
		public static void Stop(GameObject target, bool includechildren)
		{
			StudioTween.Stop(target);
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.Stop(transform.gameObject, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009F03 RID: 40707 RVA: 0x00410920 File Offset: 0x0040ED20
		public static void Stop(GameObject target, string type)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				string text = studioTween.type + studioTween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					studioTween.Dispose();
				}
			}
		}

		// Token: 0x06009F04 RID: 40708 RVA: 0x004109A0 File Offset: 0x0040EDA0
		public static void StopByName(GameObject target, string name)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				if (studioTween._name == name)
				{
					studioTween.Dispose();
				}
			}
		}

		// Token: 0x06009F05 RID: 40709 RVA: 0x004109F4 File Offset: 0x0040EDF4
		public static void Stop(GameObject target, string type, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				string text = studioTween.type + studioTween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					studioTween.Dispose();
				}
			}
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.Stop(transform.gameObject, type, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009F06 RID: 40710 RVA: 0x00410AE0 File Offset: 0x0040EEE0
		public static void StopByName(GameObject target, string name, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				if (studioTween._name == name)
				{
					studioTween.Dispose();
				}
			}
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						StudioTween.StopByName(transform.gameObject, name, true);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x06009F07 RID: 40711 RVA: 0x00410BA0 File Offset: 0x0040EFA0
		public static Hashtable Hash(params object[] args)
		{
			Hashtable hashtable = new Hashtable(args.Length / 2);
			if (args.Length % 2 != 0)
			{
				return null;
			}
			for (int i = 0; i < args.Length - 1; i += 2)
			{
				hashtable.Add(args[i], args[i + 1]);
			}
			return hashtable;
		}

		// Token: 0x06009F08 RID: 40712 RVA: 0x00410BE9 File Offset: 0x0040EFE9
		private void OnEnable()
		{
			if (this.isPaused)
			{
				this.isPaused = false;
				if (this.delay > 0f)
				{
					this.wasPaused = true;
					this.ResumeDelay();
				}
			}
		}

		// Token: 0x06009F09 RID: 40713 RVA: 0x00410C1C File Offset: 0x0040F01C
		private static void DrawLineHelper(Vector3[] line, Color color, string method)
		{
			Gizmos.color = color;
			for (int i = 0; i < line.Length - 1; i++)
			{
				if (method == "gizmos")
				{
					Gizmos.DrawLine(line[i], line[i + 1]);
				}
				else if (method == "handles")
				{
				}
			}
		}

		// Token: 0x06009F0A RID: 40714 RVA: 0x00410C8C File Offset: 0x0040F08C
		private static void DrawPathHelper(Vector3[] path, Color color, string method)
		{
			Vector3[] pts = StudioTween.PathControlPointGenerator(path);
			Vector3 to = StudioTween.Interp(pts, 0f);
			Gizmos.color = color;
			int num = path.Length * 20;
			for (int i = 1; i <= num; i++)
			{
				float t = (float)i / (float)num;
				Vector3 vector = StudioTween.Interp(pts, t);
				if (method == "gizmos")
				{
					Gizmos.DrawLine(vector, to);
				}
				else if (method == "handles")
				{
				}
				to = vector;
			}
		}

		// Token: 0x06009F0B RID: 40715 RVA: 0x00410D0C File Offset: 0x0040F10C
		private static Vector3[] PathControlPointGenerator(Vector3[] path)
		{
			int num = 2;
			Vector3[] array = new Vector3[path.Length + num];
			Array.Copy(path, 0, array, 1, path.Length);
			array[0] = array[1] + (array[1] - array[2]);
			array[array.Length - 1] = array[array.Length - 2] + (array[array.Length - 2] - array[array.Length - 3]);
			if (array[1] == array[array.Length - 2])
			{
				Vector3[] array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
				array2[0] = array2[array2.Length - 3];
				array2[array2.Length - 1] = array2[2];
				array = new Vector3[array2.Length];
				Array.Copy(array2, array, array2.Length);
			}
			return array;
		}

		// Token: 0x06009F0C RID: 40716 RVA: 0x00410E40 File Offset: 0x0040F240
		private static Vector3 Interp(Vector3[] pts, float t)
		{
			int num = pts.Length - 3;
			int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
			float num3 = t * (float)num - (float)num2;
			Vector3 a = pts[num2];
			Vector3 a2 = pts[num2 + 1];
			Vector3 vector = pts[num2 + 2];
			Vector3 b = pts[num2 + 3];
			return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
		}

		// Token: 0x06009F0D RID: 40717 RVA: 0x00410F58 File Offset: 0x0040F358
		private static StudioTween Launch(GameObject target, Hashtable args)
		{
			if (!args.Contains("id"))
			{
				args["id"] = StudioTween.GenerateID();
			}
			if (!args.Contains("target"))
			{
				args["target"] = target;
			}
			StudioTween studioTween = target.AddComponent<StudioTween>();
			studioTween.listArguments.Add(args);
			studioTween.RetrieveArgs(args);
			studioTween.Play(true);
			return studioTween;
		}

		// Token: 0x06009F0E RID: 40718 RVA: 0x00410FC4 File Offset: 0x0040F3C4
		private static Hashtable CleanArgs(Hashtable args)
		{
			Hashtable hashtable = new Hashtable(args.Count);
			Hashtable hashtable2 = new Hashtable(args.Count);
			IDictionaryEnumerator enumerator = args.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					hashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (dictionaryEntry2.Value.GetType() == typeof(int))
					{
						int num = (int)dictionaryEntry2.Value;
						float num2 = (float)num;
						args[dictionaryEntry2.Key] = num2;
					}
					if (dictionaryEntry2.Value.GetType() == typeof(double))
					{
						double num3 = (double)dictionaryEntry2.Value;
						float num4 = (float)num3;
						args[dictionaryEntry2.Key] = num4;
					}
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			IDictionaryEnumerator enumerator3 = args.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object obj3 = enumerator3.Current;
					DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
					hashtable2.Add(dictionaryEntry3.Key.ToString().ToLower(), dictionaryEntry3.Value);
				}
			}
			finally
			{
				IDisposable disposable3;
				if ((disposable3 = (enumerator3 as IDisposable)) != null)
				{
					disposable3.Dispose();
				}
			}
			args = hashtable2;
			return args;
		}

		// Token: 0x06009F0F RID: 40719 RVA: 0x0041119C File Offset: 0x0040F59C
		private static string GenerateID()
		{
			int num = 15;
			char[] array = new char[]
			{
				'a',
				'b',
				'c',
				'd',
				'e',
				'f',
				'g',
				'h',
				'i',
				'j',
				'k',
				'l',
				'm',
				'n',
				'o',
				'p',
				'q',
				'r',
				's',
				't',
				'u',
				'v',
				'w',
				'x',
				'y',
				'z',
				'A',
				'B',
				'C',
				'D',
				'E',
				'F',
				'G',
				'H',
				'I',
				'J',
				'K',
				'L',
				'M',
				'N',
				'O',
				'P',
				'Q',
				'R',
				'S',
				'T',
				'U',
				'V',
				'W',
				'X',
				'Y',
				'Z',
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8'
			};
			int max = array.Length - 1;
			string text = string.Empty;
			for (int i = 0; i < num; i++)
			{
				text += array[(int)Mathf.Floor((float)UnityEngine.Random.Range(0, max))];
			}
			return text;
		}

		// Token: 0x06009F10 RID: 40720 RVA: 0x00411200 File Offset: 0x0040F600
		private void RetrieveArgs(Hashtable _item)
		{
			this.tweenArguments = _item;
			this.id = (string)this.tweenArguments["id"];
			this.type = (string)this.tweenArguments["type"];
			this._name = (string)this.tweenArguments["name"];
			this.method = (string)this.tweenArguments["method"];
			this.time = ((!this.tweenArguments.Contains("time")) ? StudioTween.Defaults.time : ((float)this.tweenArguments["time"]));
			if (base.GetComponent<Rigidbody>() != null)
			{
				this.physics = true;
			}
			this.delay = ((!this.tweenArguments.Contains("delay")) ? StudioTween.Defaults.delay : ((float)this.tweenArguments["delay"]));
			if (this.tweenArguments.Contains("namedcolorvalue"))
			{
				if (this.tweenArguments["namedcolorvalue"].GetType() == typeof(StudioTween.NamedValueColor))
				{
					this.namedcolorvalue = (StudioTween.NamedValueColor)this.tweenArguments["namedcolorvalue"];
				}
				else
				{
					try
					{
						this.namedcolorvalue = (StudioTween.NamedValueColor)Enum.Parse(typeof(StudioTween.NamedValueColor), (string)this.tweenArguments["namedcolorvalue"], true);
					}
					catch
					{
						this.namedcolorvalue = StudioTween.NamedValueColor._Color;
					}
				}
			}
			else
			{
				this.namedcolorvalue = StudioTween.Defaults.namedColorValue;
			}
			if (this.tweenArguments.Contains("easetype"))
			{
				if (this.tweenArguments["easetype"].GetType() == typeof(StudioTween.EaseType))
				{
					this.easeType = (StudioTween.EaseType)this.tweenArguments["easetype"];
				}
				else
				{
					try
					{
						this.easeType = (StudioTween.EaseType)Enum.Parse(typeof(StudioTween.EaseType), (string)this.tweenArguments["easetype"], true);
					}
					catch
					{
						this.easeType = StudioTween.Defaults.easeType;
					}
				}
			}
			else
			{
				this.easeType = StudioTween.Defaults.easeType;
			}
			if (this.tweenArguments.Contains("space"))
			{
				if (this.tweenArguments["space"].GetType() == typeof(Space))
				{
					this.space = (Space)this.tweenArguments["space"];
				}
				else
				{
					try
					{
						this.space = (Space)Enum.Parse(typeof(Space), (string)this.tweenArguments["space"], true);
					}
					catch
					{
						this.space = StudioTween.Defaults.space;
					}
				}
			}
			else
			{
				this.space = StudioTween.Defaults.space;
			}
			this.isLocal = ((!this.tweenArguments.Contains("islocal")) ? StudioTween.Defaults.isLocal : ((bool)this.tweenArguments["islocal"]));
			this.useRealTime = ((!this.tweenArguments.Contains("ignoretimescale")) ? StudioTween.Defaults.useRealTime : ((bool)this.tweenArguments["ignoretimescale"]));
			this.GetEasingFunction();
		}

		// Token: 0x06009F11 RID: 40721 RVA: 0x004115C8 File Offset: 0x0040F9C8
		private void GetEasingFunction()
		{
			switch (this.easeType)
			{
			case StudioTween.EaseType.easeInQuad:
				this.ease = new StudioTween.EasingFunction(this.easeInQuad);
				break;
			case StudioTween.EaseType.easeOutQuad:
				this.ease = new StudioTween.EasingFunction(this.easeOutQuad);
				break;
			case StudioTween.EaseType.easeInOutQuad:
				this.ease = new StudioTween.EasingFunction(this.easeInOutQuad);
				break;
			case StudioTween.EaseType.easeInCubic:
				this.ease = new StudioTween.EasingFunction(this.easeInCubic);
				break;
			case StudioTween.EaseType.easeOutCubic:
				this.ease = new StudioTween.EasingFunction(this.easeOutCubic);
				break;
			case StudioTween.EaseType.easeInOutCubic:
				this.ease = new StudioTween.EasingFunction(this.easeInOutCubic);
				break;
			case StudioTween.EaseType.easeInQuart:
				this.ease = new StudioTween.EasingFunction(this.easeInQuart);
				break;
			case StudioTween.EaseType.easeOutQuart:
				this.ease = new StudioTween.EasingFunction(this.easeOutQuart);
				break;
			case StudioTween.EaseType.easeInOutQuart:
				this.ease = new StudioTween.EasingFunction(this.easeInOutQuart);
				break;
			case StudioTween.EaseType.easeInQuint:
				this.ease = new StudioTween.EasingFunction(this.easeInQuint);
				break;
			case StudioTween.EaseType.easeOutQuint:
				this.ease = new StudioTween.EasingFunction(this.easeOutQuint);
				break;
			case StudioTween.EaseType.easeInOutQuint:
				this.ease = new StudioTween.EasingFunction(this.easeInOutQuint);
				break;
			case StudioTween.EaseType.easeInSine:
				this.ease = new StudioTween.EasingFunction(this.easeInSine);
				break;
			case StudioTween.EaseType.easeOutSine:
				this.ease = new StudioTween.EasingFunction(this.easeOutSine);
				break;
			case StudioTween.EaseType.easeInOutSine:
				this.ease = new StudioTween.EasingFunction(this.easeInOutSine);
				break;
			case StudioTween.EaseType.easeInExpo:
				this.ease = new StudioTween.EasingFunction(this.easeInExpo);
				break;
			case StudioTween.EaseType.easeOutExpo:
				this.ease = new StudioTween.EasingFunction(this.easeOutExpo);
				break;
			case StudioTween.EaseType.easeInOutExpo:
				this.ease = new StudioTween.EasingFunction(this.easeInOutExpo);
				break;
			case StudioTween.EaseType.easeInCirc:
				this.ease = new StudioTween.EasingFunction(this.easeInCirc);
				break;
			case StudioTween.EaseType.easeOutCirc:
				this.ease = new StudioTween.EasingFunction(this.easeOutCirc);
				break;
			case StudioTween.EaseType.easeInOutCirc:
				this.ease = new StudioTween.EasingFunction(this.easeInOutCirc);
				break;
			case StudioTween.EaseType.linear:
				this.ease = new StudioTween.EasingFunction(this.linear);
				break;
			case StudioTween.EaseType.spring:
				this.ease = new StudioTween.EasingFunction(this.spring);
				break;
			case StudioTween.EaseType.easeInBounce:
				this.ease = new StudioTween.EasingFunction(this.easeInBounce);
				break;
			case StudioTween.EaseType.easeOutBounce:
				this.ease = new StudioTween.EasingFunction(this.easeOutBounce);
				break;
			case StudioTween.EaseType.easeInOutBounce:
				this.ease = new StudioTween.EasingFunction(this.easeInOutBounce);
				break;
			case StudioTween.EaseType.easeInBack:
				this.ease = new StudioTween.EasingFunction(this.easeInBack);
				break;
			case StudioTween.EaseType.easeOutBack:
				this.ease = new StudioTween.EasingFunction(this.easeOutBack);
				break;
			case StudioTween.EaseType.easeInOutBack:
				this.ease = new StudioTween.EasingFunction(this.easeInOutBack);
				break;
			case StudioTween.EaseType.easeInElastic:
				this.ease = new StudioTween.EasingFunction(this.easeInElastic);
				break;
			case StudioTween.EaseType.easeOutElastic:
				this.ease = new StudioTween.EasingFunction(this.easeOutElastic);
				break;
			case StudioTween.EaseType.easeInOutElastic:
				this.ease = new StudioTween.EasingFunction(this.easeInOutElastic);
				break;
			}
		}

		// Token: 0x06009F12 RID: 40722 RVA: 0x00411948 File Offset: 0x0040FD48
		private void UpdatePercentage()
		{
			this.runningTime += ((!this.useRealTime) ? Time.deltaTime : (Time.realtimeSinceStartup - this.lastRealTime));
			this._percentage = ((!this.reverse) ? (this.runningTime / this.time) : (1f - this.runningTime / this.time));
			this.lastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06009F13 RID: 40723 RVA: 0x004119C4 File Offset: 0x0040FDC4
		private void Dispose()
		{
			for (int i = 0; i < StudioTween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)StudioTween.tweens[i];
				if ((string)hashtable["id"] == this.id)
				{
					StudioTween.tweens.RemoveAt(i);
					break;
				}
			}
			if (this.disposableUpdate != null)
			{
				this.disposableUpdate.Dispose();
				this.disposableUpdate = null;
			}
			if (this.disposableFixedUpdate != null)
			{
				this.disposableFixedUpdate.Dispose();
				this.disposableFixedUpdate = null;
			}
			if (this.disposableLateUpdate != null)
			{
				this.disposableLateUpdate.Dispose();
				this.disposableLateUpdate = null;
			}
			UnityEngine.Object.DestroyImmediate(this);
		}

		// Token: 0x06009F14 RID: 40724 RVA: 0x00411A8C File Offset: 0x0040FE8C
		private void ConflictCheck()
		{
			Component[] components = base.GetComponents(typeof(StudioTween));
			foreach (StudioTween studioTween in components)
			{
				if (studioTween.type == "value")
				{
					return;
				}
				if (studioTween.isRunning && studioTween.type == this.type)
				{
					if (studioTween.method != this.method)
					{
						return;
					}
					if (studioTween.tweenArguments.Count != this.tweenArguments.Count)
					{
						studioTween.Dispose();
						return;
					}
					IDictionaryEnumerator enumerator = this.tweenArguments.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							if (!studioTween.tweenArguments.Contains(dictionaryEntry.Key))
							{
								studioTween.Dispose();
								return;
							}
							if (!studioTween.tweenArguments[dictionaryEntry.Key].Equals(this.tweenArguments[dictionaryEntry.Key]) && (string)dictionaryEntry.Key != "id")
							{
								studioTween.Dispose();
								return;
							}
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
					this.Dispose();
				}
			}
		}

		// Token: 0x06009F15 RID: 40725 RVA: 0x00411C10 File Offset: 0x00410010
		private void ResumeDelay()
		{
			base.StartCoroutine("TweenDelay");
		}

		// Token: 0x06009F16 RID: 40726 RVA: 0x00411C20 File Offset: 0x00410020
		private void Next()
		{
			this._percentage = 0f;
			this.runningTime = 0f;
			this.nowIndex++;
			this.RetrieveArgs(this._listArguments[this.nowIndex]);
			this.Play(false);
		}

		// Token: 0x06009F17 RID: 40727 RVA: 0x00411C70 File Offset: 0x00410070
		public void Play(bool _checkDelay = true)
		{
			if (_checkDelay && this.delay > 0f)
			{
				Observable.FromCoroutine(new Func<IEnumerator>(this.TweenStartCoroutine), false).Subscribe<Unit>().AddTo(this);
			}
			else
			{
				this.lastRealTime = Time.realtimeSinceStartup;
				this.TweenStart();
				this.apply();
			}
		}

		// Token: 0x06009F18 RID: 40728 RVA: 0x00411CD4 File Offset: 0x004100D4
		private IEnumerator TweenStartCoroutine()
		{
			this.lastRealTime = Time.realtimeSinceStartup;
			if (this.delay > 0f)
			{
				yield return base.StartCoroutine("TweenDelay");
			}
			this.TweenStart();
			yield break;
		}

		// Token: 0x06009F19 RID: 40729 RVA: 0x00411CF0 File Offset: 0x004100F0
		public void MoveTo(Hashtable args)
		{
			args = StudioTween.CleanArgs(args);
			if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["position"];
				args["position"] = transform.position;
				args["rotation"] = transform.eulerAngles;
				args["scale"] = transform.localScale;
			}
			args["type"] = "move";
			args["method"] = "to";
			if (!args.Contains("id"))
			{
				args["id"] = StudioTween.GenerateID();
			}
			if (!args.Contains("target"))
			{
				args["target"] = base.gameObject;
			}
			this._listArguments.Add(args);
		}

		// Token: 0x06009F1A RID: 40730 RVA: 0x00411DF9 File Offset: 0x004101F9
		private float linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		// Token: 0x06009F1B RID: 40731 RVA: 0x00411E04 File Offset: 0x00410204
		private float clerp(float start, float end, float value)
		{
			float num = 0f;
			float num2 = 360f;
			float num3 = Mathf.Abs((num2 - num) / 2f);
			float result;
			if (end - start < -num3)
			{
				float num4 = (num2 - start + end) * value;
				result = start + num4;
			}
			else if (end - start > num3)
			{
				float num4 = -(num2 - end + start) * value;
				result = start + num4;
			}
			else
			{
				result = start + (end - start) * value;
			}
			return result;
		}

		// Token: 0x06009F1C RID: 40732 RVA: 0x00411E7C File Offset: 0x0041027C
		private float spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		// Token: 0x06009F1D RID: 40733 RVA: 0x00411EE0 File Offset: 0x004102E0
		private float easeInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		// Token: 0x06009F1E RID: 40734 RVA: 0x00411EEE File Offset: 0x004102EE
		private float easeOutQuad(float start, float end, float value)
		{
			end -= start;
			return -end * value * (value - 2f) + start;
		}

		// Token: 0x06009F1F RID: 40735 RVA: 0x00411F04 File Offset: 0x00410304
		private float easeInOutQuad(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value + start;
			}
			value -= 1f;
			return -end / 2f * (value * (value - 2f) - 1f) + start;
		}

		// Token: 0x06009F20 RID: 40736 RVA: 0x00411F5B File Offset: 0x0041035B
		private float easeInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		// Token: 0x06009F21 RID: 40737 RVA: 0x00411F6B File Offset: 0x0041036B
		private float easeOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		// Token: 0x06009F22 RID: 40738 RVA: 0x00411F8C File Offset: 0x0041038C
		private float easeInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value * value + start;
			}
			value -= 2f;
			return end / 2f * (value * value * value + 2f) + start;
		}

		// Token: 0x06009F23 RID: 40739 RVA: 0x00411FE0 File Offset: 0x004103E0
		private float easeInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		// Token: 0x06009F24 RID: 40740 RVA: 0x00411FF2 File Offset: 0x004103F2
		private float easeOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * (value * value * value * value - 1f) + start;
		}

		// Token: 0x06009F25 RID: 40741 RVA: 0x00412014 File Offset: 0x00410414
		private float easeInOutQuart(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value * value * value + start;
			}
			value -= 2f;
			return -end / 2f * (value * value * value * value - 2f) + start;
		}

		// Token: 0x06009F26 RID: 40742 RVA: 0x0041206D File Offset: 0x0041046D
		private float easeInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		// Token: 0x06009F27 RID: 40743 RVA: 0x00412081 File Offset: 0x00410481
		private float easeOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		// Token: 0x06009F28 RID: 40744 RVA: 0x004120A4 File Offset: 0x004104A4
		private float easeInOutQuint(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value * value * value * value + start;
			}
			value -= 2f;
			return end / 2f * (value * value * value * value * value + 2f) + start;
		}

		// Token: 0x06009F29 RID: 40745 RVA: 0x00412100 File Offset: 0x00410500
		private float easeInSine(float start, float end, float value)
		{
			end -= start;
			return -end * Mathf.Cos(value / 1f * 1.5707964f) + end + start;
		}

		// Token: 0x06009F2A RID: 40746 RVA: 0x00412120 File Offset: 0x00410520
		private float easeOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value / 1f * 1.5707964f) + start;
		}

		// Token: 0x06009F2B RID: 40747 RVA: 0x0041213D File Offset: 0x0041053D
		private float easeInOutSine(float start, float end, float value)
		{
			end -= start;
			return -end / 2f * (Mathf.Cos(3.1415927f * value / 1f) - 1f) + start;
		}

		// Token: 0x06009F2C RID: 40748 RVA: 0x00412167 File Offset: 0x00410567
		private float easeInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
		}

		// Token: 0x06009F2D RID: 40749 RVA: 0x0041218F File Offset: 0x0041058F
		private float easeOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * value / 1f) + 1f) + start;
		}

		// Token: 0x06009F2E RID: 40750 RVA: 0x004121B8 File Offset: 0x004105B8
		private float easeInOutExpo(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
			}
			value -= 1f;
			return end / 2f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
		}

		// Token: 0x06009F2F RID: 40751 RVA: 0x0041222B File Offset: 0x0041062B
		private float easeInCirc(float start, float end, float value)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		// Token: 0x06009F30 RID: 40752 RVA: 0x0041224B File Offset: 0x0041064B
		private float easeOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		// Token: 0x06009F31 RID: 40753 RVA: 0x00412270 File Offset: 0x00410670
		private float easeInOutCirc(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return -end / 2f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
			}
			value -= 2f;
			return end / 2f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
		}

		// Token: 0x06009F32 RID: 40754 RVA: 0x004122E0 File Offset: 0x004106E0
		private float easeInBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return end - this.easeOutBounce(0f, end, num - value) + start;
		}

		// Token: 0x06009F33 RID: 40755 RVA: 0x0041230C File Offset: 0x0041070C
		private float easeOutBounce(float start, float end, float value)
		{
			value /= 1f;
			end -= start;
			if (value < 0.36363637f)
			{
				return end * (7.5625f * value * value) + start;
			}
			if (value < 0.72727275f)
			{
				value -= 0.54545456f;
				return end * (7.5625f * value * value + 0.75f) + start;
			}
			if ((double)value < 0.9090909090909091)
			{
				value -= 0.8181818f;
				return end * (7.5625f * value * value + 0.9375f) + start;
			}
			value -= 0.95454544f;
			return end * (7.5625f * value * value + 0.984375f) + start;
		}

		// Token: 0x06009F34 RID: 40756 RVA: 0x004123B4 File Offset: 0x004107B4
		private float easeInOutBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			if (value < num / 2f)
			{
				return this.easeInBounce(0f, end, value * 2f) * 0.5f + start;
			}
			return this.easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
		}

		// Token: 0x06009F35 RID: 40757 RVA: 0x0041241C File Offset: 0x0041081C
		private float easeInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		// Token: 0x06009F36 RID: 40758 RVA: 0x00412450 File Offset: 0x00410850
		private float easeOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value = value / 1f - 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		// Token: 0x06009F37 RID: 40759 RVA: 0x00412490 File Offset: 0x00410890
		private float easeInOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return end / 2f * (value * value * ((num + 1f) * value - num)) + start;
			}
			value -= 2f;
			num *= 1.525f;
			return end / 2f * (value * value * ((num + 1f) * value + num) + 2f) + start;
		}

		// Token: 0x06009F38 RID: 40760 RVA: 0x00412510 File Offset: 0x00410910
		private float punch(float amplitude, float value)
		{
			if (value == 0f)
			{
				return 0f;
			}
			if (value == 1f)
			{
				return 0f;
			}
			float num = 0.3f;
			float num2 = num / 6.2831855f * Mathf.Asin(0f);
			return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num2) * 6.2831855f / num);
		}

		// Token: 0x06009F39 RID: 40761 RVA: 0x00412588 File Offset: 0x00410988
		private float easeInElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return -(num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
		}

		// Token: 0x06009F3A RID: 40762 RVA: 0x00412640 File Offset: 0x00410A40
		private float easeOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) + end + start;
		}

		// Token: 0x06009F3B RID: 40763 RVA: 0x004126F0 File Offset: 0x00410AF0
		private float easeInOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num / 2f) == 2f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			if (value < 1f)
			{
				return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
			}
			return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) * 0.5f + end + start;
		}

		// Token: 0x04007DE4 RID: 32228
		public static ArrayList tweens = new ArrayList();

		// Token: 0x04007DE5 RID: 32229
		private static GameObject cameraFade;

		// Token: 0x04007DE6 RID: 32230
		public string id;

		// Token: 0x04007DE7 RID: 32231
		public string type;

		// Token: 0x04007DE8 RID: 32232
		public string method;

		// Token: 0x04007DE9 RID: 32233
		public StudioTween.EaseType easeType;

		// Token: 0x04007DEA RID: 32234
		public float time;

		// Token: 0x04007DEB RID: 32235
		public float delay;

		// Token: 0x04007DEC RID: 32236
		public StudioTween.LoopType loopType;

		// Token: 0x04007DED RID: 32237
		public bool isRunning;

		// Token: 0x04007DEE RID: 32238
		public bool isPaused;

		// Token: 0x04007DEF RID: 32239
		public string _name;

		// Token: 0x04007DF0 RID: 32240
		private float runningTime;

		// Token: 0x04007DF1 RID: 32241
		private float _percentage;

		// Token: 0x04007DF2 RID: 32242
		private float delayStarted;

		// Token: 0x04007DF3 RID: 32243
		private bool kinematic;

		// Token: 0x04007DF4 RID: 32244
		private bool isLocal;

		// Token: 0x04007DF5 RID: 32245
		private bool loop;

		// Token: 0x04007DF6 RID: 32246
		private bool reverse;

		// Token: 0x04007DF7 RID: 32247
		private bool wasPaused;

		// Token: 0x04007DF8 RID: 32248
		private bool physics;

		// Token: 0x04007DF9 RID: 32249
		private Hashtable tweenArguments;

		// Token: 0x04007DFA RID: 32250
		private Space space;

		// Token: 0x04007DFB RID: 32251
		private StudioTween.EasingFunction ease;

		// Token: 0x04007DFC RID: 32252
		private StudioTween.ApplyTween apply;

		// Token: 0x04007DFD RID: 32253
		private AudioSource audioSource;

		// Token: 0x04007DFE RID: 32254
		private Vector3[] vector3s;

		// Token: 0x04007DFF RID: 32255
		private Vector2[] vector2s;

		// Token: 0x04007E00 RID: 32256
		private Color[,] colors;

		// Token: 0x04007E01 RID: 32257
		private float[] floats;

		// Token: 0x04007E02 RID: 32258
		private Rect[] rects;

		// Token: 0x04007E03 RID: 32259
		private StudioTween.CRSpline path;

		// Token: 0x04007E04 RID: 32260
		private Vector3 preUpdate;

		// Token: 0x04007E05 RID: 32261
		private Vector3 postUpdate;

		// Token: 0x04007E06 RID: 32262
		private StudioTween.NamedValueColor namedcolorvalue;

		// Token: 0x04007E07 RID: 32263
		private float lastRealTime;

		// Token: 0x04007E08 RID: 32264
		private bool useRealTime;

		// Token: 0x04007E09 RID: 32265
		private List<Hashtable> _listArguments = new List<Hashtable>();

		// Token: 0x04007E0A RID: 32266
		private int nowIndex;

		// Token: 0x04007E0B RID: 32267
		public Action onStart;

		// Token: 0x04007E0C RID: 32268
		public Action<float> onUpdate;

		// Token: 0x04007E0D RID: 32269
		public StudioTween.CompleteFunction onComplete;

		// Token: 0x04007E0E RID: 32270
		private SingleAssignmentDisposable disposableUpdate;

		// Token: 0x04007E0F RID: 32271
		private SingleAssignmentDisposable disposableFixedUpdate;

		// Token: 0x04007E10 RID: 32272
		private SingleAssignmentDisposable disposableLateUpdate;

		// Token: 0x020012B3 RID: 4787
		// (Invoke) Token: 0x06009F46 RID: 40774
		private delegate float EasingFunction(float start, float end, float value);

		// Token: 0x020012B4 RID: 4788
		// (Invoke) Token: 0x06009F4A RID: 40778
		private delegate void ApplyTween();

		// Token: 0x020012B5 RID: 4789
		public enum EaseType
		{
			// Token: 0x04007E13 RID: 32275
			easeInQuad,
			// Token: 0x04007E14 RID: 32276
			easeOutQuad,
			// Token: 0x04007E15 RID: 32277
			easeInOutQuad,
			// Token: 0x04007E16 RID: 32278
			easeInCubic,
			// Token: 0x04007E17 RID: 32279
			easeOutCubic,
			// Token: 0x04007E18 RID: 32280
			easeInOutCubic,
			// Token: 0x04007E19 RID: 32281
			easeInQuart,
			// Token: 0x04007E1A RID: 32282
			easeOutQuart,
			// Token: 0x04007E1B RID: 32283
			easeInOutQuart,
			// Token: 0x04007E1C RID: 32284
			easeInQuint,
			// Token: 0x04007E1D RID: 32285
			easeOutQuint,
			// Token: 0x04007E1E RID: 32286
			easeInOutQuint,
			// Token: 0x04007E1F RID: 32287
			easeInSine,
			// Token: 0x04007E20 RID: 32288
			easeOutSine,
			// Token: 0x04007E21 RID: 32289
			easeInOutSine,
			// Token: 0x04007E22 RID: 32290
			easeInExpo,
			// Token: 0x04007E23 RID: 32291
			easeOutExpo,
			// Token: 0x04007E24 RID: 32292
			easeInOutExpo,
			// Token: 0x04007E25 RID: 32293
			easeInCirc,
			// Token: 0x04007E26 RID: 32294
			easeOutCirc,
			// Token: 0x04007E27 RID: 32295
			easeInOutCirc,
			// Token: 0x04007E28 RID: 32296
			linear,
			// Token: 0x04007E29 RID: 32297
			spring,
			// Token: 0x04007E2A RID: 32298
			easeInBounce,
			// Token: 0x04007E2B RID: 32299
			easeOutBounce,
			// Token: 0x04007E2C RID: 32300
			easeInOutBounce,
			// Token: 0x04007E2D RID: 32301
			easeInBack,
			// Token: 0x04007E2E RID: 32302
			easeOutBack,
			// Token: 0x04007E2F RID: 32303
			easeInOutBack,
			// Token: 0x04007E30 RID: 32304
			easeInElastic,
			// Token: 0x04007E31 RID: 32305
			easeOutElastic,
			// Token: 0x04007E32 RID: 32306
			easeInOutElastic
		}

		// Token: 0x020012B6 RID: 4790
		public enum LoopType
		{
			// Token: 0x04007E34 RID: 32308
			none,
			// Token: 0x04007E35 RID: 32309
			loop,
			// Token: 0x04007E36 RID: 32310
			pingPong
		}

		// Token: 0x020012B7 RID: 4791
		public enum NamedValueColor
		{
			// Token: 0x04007E38 RID: 32312
			_Color,
			// Token: 0x04007E39 RID: 32313
			_SpecColor,
			// Token: 0x04007E3A RID: 32314
			_Emission,
			// Token: 0x04007E3B RID: 32315
			_ReflectColor
		}

		// Token: 0x020012B8 RID: 4792
		// (Invoke) Token: 0x06009F4E RID: 40782
		public delegate bool CompleteFunction();

		// Token: 0x020012B9 RID: 4793
		public static class Defaults
		{
			// Token: 0x04007E3C RID: 32316
			public static float time = 1f;

			// Token: 0x04007E3D RID: 32317
			public static float delay = 0f;

			// Token: 0x04007E3E RID: 32318
			public static StudioTween.NamedValueColor namedColorValue = StudioTween.NamedValueColor._Color;

			// Token: 0x04007E3F RID: 32319
			public static StudioTween.LoopType loopType = StudioTween.LoopType.none;

			// Token: 0x04007E40 RID: 32320
			public static StudioTween.EaseType easeType = StudioTween.EaseType.easeOutExpo;

			// Token: 0x04007E41 RID: 32321
			public static float lookSpeed = 3f;

			// Token: 0x04007E42 RID: 32322
			public static bool isLocal = false;

			// Token: 0x04007E43 RID: 32323
			public static Space space = Space.Self;

			// Token: 0x04007E44 RID: 32324
			public static bool orientToPath = false;

			// Token: 0x04007E45 RID: 32325
			public static Color color = Color.white;

			// Token: 0x04007E46 RID: 32326
			public static float updateTimePercentage = 0.05f;

			// Token: 0x04007E47 RID: 32327
			public static float updateTime = 1f * StudioTween.Defaults.updateTimePercentage;

			// Token: 0x04007E48 RID: 32328
			public static int cameraFadeDepth = 999999;

			// Token: 0x04007E49 RID: 32329
			public static float lookAhead = 0.05f;

			// Token: 0x04007E4A RID: 32330
			public static bool useRealTime = false;

			// Token: 0x04007E4B RID: 32331
			public static Vector3 up = Vector3.up;
		}

		// Token: 0x020012BA RID: 4794
		private class CRSpline
		{
			// Token: 0x06009F52 RID: 40786 RVA: 0x004129A0 File Offset: 0x00410DA0
			public CRSpline(params Vector3[] pts)
			{
				this.pts = new Vector3[pts.Length];
				Array.Copy(pts, this.pts, pts.Length);
			}

			// Token: 0x06009F53 RID: 40787 RVA: 0x004129C8 File Offset: 0x00410DC8
			public Vector3 Interp(float t)
			{
				int num = this.pts.Length - 3;
				int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
				float num3 = t * (float)num - (float)num2;
				Vector3 a = this.pts[num2];
				Vector3 a2 = this.pts[num2 + 1];
				Vector3 vector = this.pts[num2 + 2];
				Vector3 b = this.pts[num2 + 3];
				return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
			}

			// Token: 0x04007E4C RID: 32332
			public Vector3[] pts;
		}
	}
}
