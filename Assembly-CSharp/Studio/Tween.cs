using System;
using System.Collections;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012BB RID: 4795
	public class Tween : MonoBehaviour
	{
		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06009F55 RID: 40789 RVA: 0x00412D59 File Offset: 0x00411159
		public float percentage
		{
			get
			{
				return this._percentage;
			}
		}

		// Token: 0x06009F56 RID: 40790 RVA: 0x00412D61 File Offset: 0x00411161
		public static void Init(GameObject target)
		{
			Tween.MoveBy(target, Vector3.zero, 0f);
		}

		// Token: 0x06009F57 RID: 40791 RVA: 0x00412D74 File Offset: 0x00411174
		public static void CameraFadeFrom(float amount, float time)
		{
			if (Tween.cameraFade)
			{
				Tween.CameraFadeFrom(Tween.Hash(new object[]
				{
					"amount",
					amount,
					"time",
					time
				}));
			}
		}

		// Token: 0x06009F58 RID: 40792 RVA: 0x00412DC7 File Offset: 0x004111C7
		public static void CameraFadeFrom(Hashtable args)
		{
			if (Tween.cameraFade)
			{
				Tween.ColorFrom(Tween.cameraFade, args);
			}
		}

		// Token: 0x06009F59 RID: 40793 RVA: 0x00412DE8 File Offset: 0x004111E8
		public static void CameraFadeTo(float amount, float time)
		{
			if (Tween.cameraFade)
			{
				Tween.CameraFadeTo(Tween.Hash(new object[]
				{
					"amount",
					amount,
					"time",
					time
				}));
			}
		}

		// Token: 0x06009F5A RID: 40794 RVA: 0x00412E3B File Offset: 0x0041123B
		public static void CameraFadeTo(Hashtable args)
		{
			if (Tween.cameraFade)
			{
				Tween.ColorTo(Tween.cameraFade, args);
			}
		}

		// Token: 0x06009F5B RID: 40795 RVA: 0x00412E5C File Offset: 0x0041125C
		public static void ValueTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			if (!args.Contains("onupdate") || !args.Contains("from") || !args.Contains("to"))
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
				args.Add("easetype", Tween.EaseType.linear);
			}
			Tween.Launch(target, args);
		}

		// Token: 0x06009F5C RID: 40796 RVA: 0x00412FFA File Offset: 0x004113FA
		public static void FadeFrom(GameObject target, float alpha, float time)
		{
			Tween.FadeFrom(target, Tween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06009F5D RID: 40797 RVA: 0x0041302F File Offset: 0x0041142F
		public static void FadeFrom(GameObject target, Hashtable args)
		{
			Tween.ColorFrom(target, args);
		}

		// Token: 0x06009F5E RID: 40798 RVA: 0x00413038 File Offset: 0x00411438
		public static void FadeTo(GameObject target, float alpha, float time)
		{
			Tween.FadeTo(target, Tween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06009F5F RID: 40799 RVA: 0x0041306D File Offset: 0x0041146D
		public static void FadeTo(GameObject target, Hashtable args)
		{
			Tween.ColorTo(target, args);
		}

		// Token: 0x06009F60 RID: 40800 RVA: 0x00413076 File Offset: 0x00411476
		public static void ColorFrom(GameObject target, Color color, float time)
		{
			Tween.ColorFrom(target, Tween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06009F61 RID: 40801 RVA: 0x004130AC File Offset: 0x004114AC
		public static void ColorFrom(GameObject target, Hashtable args)
		{
			Color color = default(Color);
			Color color2 = default(Color);
			args = Tween.CleanArgs(args);
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
						Tween.ColorFrom(transform.gameObject, hashtable);
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
				args.Add("easetype", Tween.EaseType.linear);
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
			Tween.Launch(target, args);
		}

		// Token: 0x06009F62 RID: 40802 RVA: 0x00413440 File Offset: 0x00411840
		public static void ColorTo(GameObject target, Color color, float time)
		{
			Tween.ColorTo(target, Tween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06009F63 RID: 40803 RVA: 0x00413478 File Offset: 0x00411878
		public static void ColorTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
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
						Tween.ColorTo(transform.gameObject, hashtable);
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
				args.Add("easetype", Tween.EaseType.linear);
			}
			args["type"] = "color";
			args["method"] = "to";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F64 RID: 40804 RVA: 0x00413578 File Offset: 0x00411978
		public static void AudioFrom(GameObject target, float volume, float pitch, float time)
		{
			Tween.AudioFrom(target, Tween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06009F65 RID: 40805 RVA: 0x004135CC File Offset: 0x004119CC
		public static void AudioFrom(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
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
			Vector2 vector;
			Vector2 vector2;
			vector.x = (vector2.x = audioSource.volume);
			vector.y = (vector2.y = audioSource.pitch);
			if (args.Contains("volume"))
			{
				vector2.x = (float)args["volume"];
			}
			if (args.Contains("pitch"))
			{
				vector2.y = (float)args["pitch"];
			}
			audioSource.volume = vector2.x;
			audioSource.pitch = vector2.y;
			args["volume"] = vector.x;
			args["pitch"] = vector.y;
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", Tween.EaseType.linear);
			}
			args["type"] = "audio";
			args["method"] = "to";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F66 RID: 40806 RVA: 0x0041373C File Offset: 0x00411B3C
		public static void AudioTo(GameObject target, float volume, float pitch, float time)
		{
			Tween.AudioTo(target, Tween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06009F67 RID: 40807 RVA: 0x00413790 File Offset: 0x00411B90
		public static void AudioTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", Tween.EaseType.linear);
			}
			args["type"] = "audio";
			args["method"] = "to";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F68 RID: 40808 RVA: 0x004137EF File Offset: 0x00411BEF
		public static void Stab(GameObject target, AudioClip audioclip, float delay)
		{
			Tween.Stab(target, Tween.Hash(new object[]
			{
				"audioclip",
				audioclip,
				"delay",
				delay
			}));
		}

		// Token: 0x06009F69 RID: 40809 RVA: 0x0041381F File Offset: 0x00411C1F
		public static void Stab(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "stab";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F6A RID: 40810 RVA: 0x00413841 File Offset: 0x00411C41
		public static void LookFrom(GameObject target, Vector3 looktarget, float time)
		{
			Tween.LookFrom(target, Tween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06009F6B RID: 40811 RVA: 0x00413878 File Offset: 0x00411C78
		public static void LookFrom(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			Vector3 eulerAngles = target.transform.eulerAngles;
			if (args["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = target.transform;
				Transform target2 = (Transform)args["looktarget"];
				Vector3? vector = (Vector3?)args["up"];
				transform.LookAt(target2, (vector == null) ? Tween.Defaults.up : vector.Value);
			}
			else if (args["looktarget"].GetType() == typeof(Vector3))
			{
				Transform transform2 = target.transform;
				Vector3 worldPosition = (Vector3)args["looktarget"];
				Vector3? vector2 = (Vector3?)args["up"];
				transform2.LookAt(worldPosition, (vector2 == null) ? Tween.Defaults.up : vector2.Value);
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
			Tween.Launch(target, args);
		}

		// Token: 0x06009F6C RID: 40812 RVA: 0x00413A8D File Offset: 0x00411E8D
		public static void LookTo(GameObject target, Vector3 looktarget, float time)
		{
			Tween.LookTo(target, Tween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06009F6D RID: 40813 RVA: 0x00413AC4 File Offset: 0x00411EC4
		public static void LookTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["looktarget"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			}
			args["type"] = "look";
			args["method"] = "to";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F6E RID: 40814 RVA: 0x00413BC7 File Offset: 0x00411FC7
		public static void MoveTo(GameObject target, Vector3 position, float time)
		{
			Tween.MoveTo(target, Tween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06009F6F RID: 40815 RVA: 0x00413C00 File Offset: 0x00412000
		public static Tween MoveTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["position"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "move";
			args["method"] = "to";
			return Tween.Launch(target, args);
		}

		// Token: 0x06009F70 RID: 40816 RVA: 0x00413D44 File Offset: 0x00412144
		public static void MoveFrom(GameObject target, Vector3 position, float time)
		{
			Tween.MoveFrom(target, Tween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06009F71 RID: 40817 RVA: 0x00413D7C File Offset: 0x0041217C
		public static void MoveFrom(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = Tween.Defaults.isLocal;
			}
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
			Tween.Launch(target, args);
		}

		// Token: 0x06009F72 RID: 40818 RVA: 0x004140F8 File Offset: 0x004124F8
		public static void MoveAdd(GameObject target, Vector3 amount, float time)
		{
			Tween.MoveAdd(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F73 RID: 40819 RVA: 0x0041412D File Offset: 0x0041252D
		public static void MoveAdd(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "add";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F74 RID: 40820 RVA: 0x0041415F File Offset: 0x0041255F
		public static void MoveBy(GameObject target, Vector3 amount, float time)
		{
			Tween.MoveBy(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F75 RID: 40821 RVA: 0x00414194 File Offset: 0x00412594
		public static void MoveBy(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "by";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F76 RID: 40822 RVA: 0x004141C6 File Offset: 0x004125C6
		public static void ScaleTo(GameObject target, Vector3 scale, float time)
		{
			Tween.ScaleTo(target, Tween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06009F77 RID: 40823 RVA: 0x004141FC File Offset: 0x004125FC
		public static void ScaleTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["scale"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "scale";
			args["method"] = "to";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F78 RID: 40824 RVA: 0x00414341 File Offset: 0x00412741
		public static void ScaleFrom(GameObject target, Vector3 scale, float time)
		{
			Tween.ScaleFrom(target, Tween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06009F79 RID: 40825 RVA: 0x00414378 File Offset: 0x00412778
		public static void ScaleFrom(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
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
			Tween.Launch(target, args);
		}

		// Token: 0x06009F7A RID: 40826 RVA: 0x004144E0 File Offset: 0x004128E0
		public static void ScaleAdd(GameObject target, Vector3 amount, float time)
		{
			Tween.ScaleAdd(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F7B RID: 40827 RVA: 0x00414515 File Offset: 0x00412915
		public static void ScaleAdd(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "add";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F7C RID: 40828 RVA: 0x00414547 File Offset: 0x00412947
		public static void ScaleBy(GameObject target, Vector3 amount, float time)
		{
			Tween.ScaleBy(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F7D RID: 40829 RVA: 0x0041457C File Offset: 0x0041297C
		public static void ScaleBy(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "by";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F7E RID: 40830 RVA: 0x004145AE File Offset: 0x004129AE
		public static void RotateTo(GameObject target, Vector3 rotation, float time)
		{
			Tween.RotateTo(target, Tween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06009F7F RID: 40831 RVA: 0x004145E4 File Offset: 0x004129E4
		public static void RotateTo(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["rotation"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "rotate";
			args["method"] = "to";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F80 RID: 40832 RVA: 0x00414729 File Offset: 0x00412B29
		public static void RotateFrom(GameObject target, Vector3 rotation, float time)
		{
			Tween.RotateFrom(target, Tween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06009F81 RID: 40833 RVA: 0x00414760 File Offset: 0x00412B60
		public static void RotateFrom(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = Tween.Defaults.isLocal;
			}
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
			Tween.Launch(target, args);
		}

		// Token: 0x06009F82 RID: 40834 RVA: 0x00414924 File Offset: 0x00412D24
		public static void RotateAdd(GameObject target, Vector3 amount, float time)
		{
			Tween.RotateAdd(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F83 RID: 40835 RVA: 0x00414959 File Offset: 0x00412D59
		public static void RotateAdd(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "add";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F84 RID: 40836 RVA: 0x0041498B File Offset: 0x00412D8B
		public static void RotateBy(GameObject target, Vector3 amount, float time)
		{
			Tween.RotateBy(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F85 RID: 40837 RVA: 0x004149C0 File Offset: 0x00412DC0
		public static void RotateBy(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "by";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F86 RID: 40838 RVA: 0x004149F2 File Offset: 0x00412DF2
		public static void ShakePosition(GameObject target, Vector3 amount, float time)
		{
			Tween.ShakePosition(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F87 RID: 40839 RVA: 0x00414A27 File Offset: 0x00412E27
		public static void ShakePosition(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "position";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F88 RID: 40840 RVA: 0x00414A59 File Offset: 0x00412E59
		public static void ShakeScale(GameObject target, Vector3 amount, float time)
		{
			Tween.ShakeScale(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F89 RID: 40841 RVA: 0x00414A8E File Offset: 0x00412E8E
		public static void ShakeScale(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "scale";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F8A RID: 40842 RVA: 0x00414AC0 File Offset: 0x00412EC0
		public static void ShakeRotation(GameObject target, Vector3 amount, float time)
		{
			Tween.ShakeRotation(target, Tween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06009F8B RID: 40843 RVA: 0x00414AF5 File Offset: 0x00412EF5
		public static void ShakeRotation(GameObject target, Hashtable args)
		{
			args = Tween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "rotation";
			Tween.Launch(target, args);
		}

		// Token: 0x06009F8C RID: 40844 RVA: 0x00414B28 File Offset: 0x00412F28
		private void GenerateTargets()
		{
			string text = this.type;
			switch (text)
			{
			case "value":
			{
				string text2 = this.method;
				if (text2 != null)
				{
					if (!(text2 == "float"))
					{
						if (!(text2 == "vector2"))
						{
							if (!(text2 == "vector3"))
							{
								if (!(text2 == "color"))
								{
									if (text2 == "rect")
									{
										this.GenerateRectTargets();
										this.apply = new Tween.ApplyTween(this.ApplyRectTargets);
									}
								}
								else
								{
									this.GenerateColorTargets();
									this.apply = new Tween.ApplyTween(this.ApplyColorTargets);
								}
							}
							else
							{
								this.GenerateVector3Targets();
								this.apply = new Tween.ApplyTween(this.ApplyVector3Targets);
							}
						}
						else
						{
							this.GenerateVector2Targets();
							this.apply = new Tween.ApplyTween(this.ApplyVector2Targets);
						}
					}
					else
					{
						this.GenerateFloatTargets();
						this.apply = new Tween.ApplyTween(this.ApplyFloatTargets);
					}
				}
				break;
			}
			case "color":
			{
				string text3 = this.method;
				if (text3 != null)
				{
					if (text3 == "to")
					{
						this.GenerateColorToTargets();
						this.apply = new Tween.ApplyTween(this.ApplyColorToTargets);
					}
				}
				break;
			}
			case "audio":
			{
				string text4 = this.method;
				if (text4 != null)
				{
					if (text4 == "to")
					{
						this.GenerateAudioToTargets();
						this.apply = new Tween.ApplyTween(this.ApplyAudioToTargets);
					}
				}
				break;
			}
			case "move":
			{
				string text5 = this.method;
				if (text5 != null)
				{
					if (!(text5 == "to"))
					{
						if (text5 == "by" || text5 == "add")
						{
							this.GenerateMoveByTargets();
							this.apply = new Tween.ApplyTween(this.ApplyMoveByTargets);
						}
					}
					else if (this.tweenArguments.Contains("path"))
					{
						this.GenerateMoveToPathTargets();
						this.apply = new Tween.ApplyTween(this.ApplyMoveToPathTargets);
					}
					else
					{
						this.GenerateMoveToTargets();
						this.apply = new Tween.ApplyTween(this.ApplyMoveToTargets);
					}
				}
				break;
			}
			case "scale":
			{
				string text6 = this.method;
				if (text6 != null)
				{
					if (!(text6 == "to"))
					{
						if (!(text6 == "by"))
						{
							if (text6 == "add")
							{
								this.GenerateScaleAddTargets();
								this.apply = new Tween.ApplyTween(this.ApplyScaleToTargets);
							}
						}
						else
						{
							this.GenerateScaleByTargets();
							this.apply = new Tween.ApplyTween(this.ApplyScaleToTargets);
						}
					}
					else
					{
						this.GenerateScaleToTargets();
						this.apply = new Tween.ApplyTween(this.ApplyScaleToTargets);
					}
				}
				break;
			}
			case "rotate":
			{
				string text7 = this.method;
				if (text7 != null)
				{
					if (!(text7 == "to"))
					{
						if (!(text7 == "add"))
						{
							if (text7 == "by")
							{
								this.GenerateRotateByTargets();
								this.apply = new Tween.ApplyTween(this.ApplyRotateAddTargets);
							}
						}
						else
						{
							this.GenerateRotateAddTargets();
							this.apply = new Tween.ApplyTween(this.ApplyRotateAddTargets);
						}
					}
					else
					{
						this.GenerateRotateToTargets();
						this.apply = new Tween.ApplyTween(this.ApplyRotateToTargets);
					}
				}
				break;
			}
			case "shake":
			{
				string text8 = this.method;
				if (text8 != null)
				{
					if (!(text8 == "position"))
					{
						if (!(text8 == "scale"))
						{
							if (text8 == "rotation")
							{
								this.GenerateShakeRotationTargets();
								this.apply = new Tween.ApplyTween(this.ApplyShakeRotationTargets);
							}
						}
						else
						{
							this.GenerateShakeScaleTargets();
							this.apply = new Tween.ApplyTween(this.ApplyShakeScaleTargets);
						}
					}
					else
					{
						this.GenerateShakePositionTargets();
						this.apply = new Tween.ApplyTween(this.ApplyShakePositionTargets);
					}
				}
				break;
			}
			case "punch":
			{
				string text9 = this.method;
				if (text9 != null)
				{
					if (!(text9 == "position"))
					{
						if (!(text9 == "rotation"))
						{
							if (text9 == "scale")
							{
								this.GeneratePunchScaleTargets();
								this.apply = new Tween.ApplyTween(this.ApplyPunchScaleTargets);
							}
						}
						else
						{
							this.GeneratePunchRotationTargets();
							this.apply = new Tween.ApplyTween(this.ApplyPunchRotationTargets);
						}
					}
					else
					{
						this.GeneratePunchPositionTargets();
						this.apply = new Tween.ApplyTween(this.ApplyPunchPositionTargets);
					}
				}
				break;
			}
			case "look":
			{
				string text10 = this.method;
				if (text10 != null)
				{
					if (text10 == "to")
					{
						this.GenerateLookToTargets();
						this.apply = new Tween.ApplyTween(this.ApplyLookToTargets);
					}
				}
				break;
			}
			case "stab":
				this.GenerateStabTargets();
				this.apply = new Tween.ApplyTween(this.ApplyStabTargets);
				break;
			}
		}

		// Token: 0x06009F8D RID: 40845 RVA: 0x00415148 File Offset: 0x00413548
		private void GenerateRectTargets()
		{
			this.rects = new Rect[3];
			this.rects[0] = (Rect)this.tweenArguments["from"];
			this.rects[1] = (Rect)this.tweenArguments["to"];
		}

		// Token: 0x06009F8E RID: 40846 RVA: 0x004151B0 File Offset: 0x004135B0
		private void GenerateColorTargets()
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (Color)this.tweenArguments["from"];
			this.colors[0, 1] = (Color)this.tweenArguments["to"];
		}

		// Token: 0x06009F8F RID: 40847 RVA: 0x00415218 File Offset: 0x00413618
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

		// Token: 0x06009F90 RID: 40848 RVA: 0x004152DC File Offset: 0x004136DC
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

		// Token: 0x06009F91 RID: 40849 RVA: 0x004153DC File Offset: 0x004137DC
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

		// Token: 0x06009F92 RID: 40850 RVA: 0x00415478 File Offset: 0x00413878
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

		// Token: 0x06009F93 RID: 40851 RVA: 0x004158F0 File Offset: 0x00413CF0
		private void GenerateAudioToTargets()
		{
			this.vector2s = new Vector2[3];
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
				this.Dispose();
			}
			this.vector2s[0] = (this.vector2s[1] = new Vector2(this.audioSource.volume, this.audioSource.pitch));
			if (this.tweenArguments.Contains("volume"))
			{
				this.vector2s[1].x = (float)this.tweenArguments["volume"];
			}
			if (this.tweenArguments.Contains("pitch"))
			{
				this.vector2s[1].y = (float)this.tweenArguments["pitch"];
			}
		}

		// Token: 0x06009F94 RID: 40852 RVA: 0x00415A28 File Offset: 0x00413E28
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

		// Token: 0x06009F95 RID: 40853 RVA: 0x00415B70 File Offset: 0x00413F70
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
					transform.LookAt(target, (vector == null) ? Tween.Defaults.up : vector.Value);
				}
				else if (this.tweenArguments["looktarget"].GetType() == typeof(Vector3))
				{
					Transform transform2 = base.transform;
					Vector3 worldPosition = (Vector3)this.tweenArguments["looktarget"];
					Vector3? vector2 = (Vector3?)this.tweenArguments["up"];
					transform2.LookAt(worldPosition, (vector2 == null) ? Tween.Defaults.up : vector2.Value);
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

		// Token: 0x06009F96 RID: 40854 RVA: 0x00415F38 File Offset: 0x00414338
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
				array2 = new Vector3[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array2[i] = array3[i].position;
				}
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
			this.path = new Tween.CRSpline(this.vector3s);
			if (this.tweenArguments.Contains("speed"))
			{
				float num2 = Tween.PathLength(this.vector3s);
				this.time = num2 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06009F97 RID: 40855 RVA: 0x00416288 File Offset: 0x00414688
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

		// Token: 0x06009F98 RID: 40856 RVA: 0x0041653C File Offset: 0x0041493C
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

		// Token: 0x06009F99 RID: 40857 RVA: 0x00416800 File Offset: 0x00414C00
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

		// Token: 0x06009F9A RID: 40858 RVA: 0x00416A20 File Offset: 0x00414E20
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

		// Token: 0x06009F9B RID: 40859 RVA: 0x00416BE4 File Offset: 0x00414FE4
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

		// Token: 0x06009F9C RID: 40860 RVA: 0x00416DA0 File Offset: 0x004151A0
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

		// Token: 0x06009F9D RID: 40861 RVA: 0x0041709C File Offset: 0x0041549C
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

		// Token: 0x06009F9E RID: 40862 RVA: 0x0041726C File Offset: 0x0041566C
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

		// Token: 0x06009F9F RID: 40863 RVA: 0x00417464 File Offset: 0x00415864
		private void GenerateShakePositionTargets()
		{
			this.vector3s = new Vector3[4];
			this.vector3s[3] = base.transform.eulerAngles;
			this.vector3s[0] = base.transform.position;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
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
		}

		// Token: 0x06009FA0 RID: 40864 RVA: 0x004175A8 File Offset: 0x004159A8
		private void GenerateShakeScaleTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = base.transform.localScale;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
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
		}

		// Token: 0x06009FA1 RID: 40865 RVA: 0x004176D0 File Offset: 0x00415AD0
		private void GenerateShakeRotationTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = base.transform.eulerAngles;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
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
		}

		// Token: 0x06009FA2 RID: 40866 RVA: 0x004177F8 File Offset: 0x00415BF8
		private void GeneratePunchPositionTargets()
		{
			this.vector3s = new Vector3[5];
			this.vector3s[4] = base.transform.eulerAngles;
			this.vector3s[0] = base.transform.position;
			this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
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
		}

		// Token: 0x06009FA3 RID: 40867 RVA: 0x00417964 File Offset: 0x00415D64
		private void GeneratePunchRotationTargets()
		{
			this.vector3s = new Vector3[4];
			this.vector3s[0] = base.transform.eulerAngles;
			this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
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
		}

		// Token: 0x06009FA4 RID: 40868 RVA: 0x00417AB4 File Offset: 0x00415EB4
		private void GeneratePunchScaleTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = base.transform.localScale;
			this.vector3s[1] = Vector3.zero;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
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
		}

		// Token: 0x06009FA5 RID: 40869 RVA: 0x00417BF0 File Offset: 0x00415FF0
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

		// Token: 0x06009FA6 RID: 40870 RVA: 0x00417D6C File Offset: 0x0041616C
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

		// Token: 0x06009FA7 RID: 40871 RVA: 0x00417EEC File Offset: 0x004162EC
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

		// Token: 0x06009FA8 RID: 40872 RVA: 0x00418024 File Offset: 0x00416424
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

		// Token: 0x06009FA9 RID: 40873 RVA: 0x00418118 File Offset: 0x00416518
		private void ApplyFloatTargets()
		{
			this.floats[2] = this.ease(this.floats[0], this.floats[1], this._percentage);
			this.tweenArguments["onupdateparams"] = this.floats[2];
			if (this._percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.floats[1];
			}
		}

		// Token: 0x06009FAA RID: 40874 RVA: 0x00418198 File Offset: 0x00416598
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

		// Token: 0x06009FAB RID: 40875 RVA: 0x004184E8 File Offset: 0x004168E8
		private void ApplyAudioToTargets()
		{
			this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this._percentage);
			this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this._percentage);
			this.audioSource.volume = this.vector2s[2].x;
			this.audioSource.pitch = this.vector2s[2].y;
			if (this._percentage == 1f)
			{
				this.audioSource.volume = this.vector2s[1].x;
				this.audioSource.pitch = this.vector2s[1].y;
			}
		}

		// Token: 0x06009FAC RID: 40876 RVA: 0x004185FD File Offset: 0x004169FD
		private void ApplyStabTargets()
		{
		}

		// Token: 0x06009FAD RID: 40877 RVA: 0x00418600 File Offset: 0x00416A00
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
				float num;
				if (this.tweenArguments.Contains("lookahead"))
				{
					num = (float)this.tweenArguments["lookahead"];
				}
				else
				{
					num = Tween.Defaults.lookAhead;
				}
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

		// Token: 0x06009FAE RID: 40878 RVA: 0x00418794 File Offset: 0x00416B94
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

		// Token: 0x06009FAF RID: 40879 RVA: 0x0041895C File Offset: 0x00416D5C
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

		// Token: 0x06009FB0 RID: 40880 RVA: 0x00418B44 File Offset: 0x00416F44
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

		// Token: 0x06009FB1 RID: 40881 RVA: 0x00418C68 File Offset: 0x00417068
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

		// Token: 0x06009FB2 RID: 40882 RVA: 0x00418D94 File Offset: 0x00417194
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

		// Token: 0x06009FB3 RID: 40883 RVA: 0x00418F78 File Offset: 0x00417378
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

		// Token: 0x06009FB4 RID: 40884 RVA: 0x00419100 File Offset: 0x00417500
		private void ApplyShakePositionTargets()
		{
			this.preUpdate = base.transform.position;
			Vector3 eulerAngles = default(Vector3);
			if (this.tweenArguments.Contains("looktarget"))
			{
				eulerAngles = base.transform.eulerAngles;
				base.transform.eulerAngles = this.vector3s[3];
			}
			if (this._percentage == 0f)
			{
				base.transform.Translate(this.vector3s[1], this.space);
			}
			base.transform.position = this.vector3s[0];
			float num = 1f - this._percentage;
			this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
			this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
			this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
			base.transform.Translate(this.vector3s[2], this.space);
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

		// Token: 0x06009FB5 RID: 40885 RVA: 0x004192F8 File Offset: 0x004176F8
		private void ApplyShakeScaleTargets()
		{
			if (this._percentage == 0f)
			{
				base.transform.localScale = this.vector3s[1];
			}
			base.transform.localScale = this.vector3s[0];
			float num = 1f - this._percentage;
			this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
			this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
			this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
			base.transform.localScale += this.vector3s[2];
		}

		// Token: 0x06009FB6 RID: 40886 RVA: 0x00419438 File Offset: 0x00417838
		private void ApplyShakeRotationTargets()
		{
			this.preUpdate = base.transform.eulerAngles;
			if (this._percentage == 0f)
			{
				base.transform.Rotate(this.vector3s[1], this.space);
			}
			base.transform.eulerAngles = this.vector3s[0];
			float num = 1f - this._percentage;
			this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
			this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
			this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
			base.transform.Rotate(this.vector3s[2], this.space);
			this.postUpdate = base.transform.eulerAngles;
			if (this.physics)
			{
				base.transform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06009FB7 RID: 40887 RVA: 0x004195D0 File Offset: 0x004179D0
		private void ApplyPunchPositionTargets()
		{
			this.preUpdate = base.transform.position;
			Vector3 eulerAngles = default(Vector3);
			if (this.tweenArguments.Contains("looktarget"))
			{
				eulerAngles = base.transform.eulerAngles;
				base.transform.eulerAngles = this.vector3s[4];
			}
			if (this.vector3s[1].x > 0f)
			{
				this.vector3s[2].x = this.punch(this.vector3s[1].x, this._percentage);
			}
			else if (this.vector3s[1].x < 0f)
			{
				this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this._percentage);
			}
			if (this.vector3s[1].y > 0f)
			{
				this.vector3s[2].y = this.punch(this.vector3s[1].y, this._percentage);
			}
			else if (this.vector3s[1].y < 0f)
			{
				this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this._percentage);
			}
			if (this.vector3s[1].z > 0f)
			{
				this.vector3s[2].z = this.punch(this.vector3s[1].z, this._percentage);
			}
			else if (this.vector3s[1].z < 0f)
			{
				this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this._percentage);
			}
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

		// Token: 0x06009FB8 RID: 40888 RVA: 0x004198C4 File Offset: 0x00417CC4
		private void ApplyPunchRotationTargets()
		{
			this.preUpdate = base.transform.eulerAngles;
			if (this.vector3s[1].x > 0f)
			{
				this.vector3s[2].x = this.punch(this.vector3s[1].x, this._percentage);
			}
			else if (this.vector3s[1].x < 0f)
			{
				this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this._percentage);
			}
			if (this.vector3s[1].y > 0f)
			{
				this.vector3s[2].y = this.punch(this.vector3s[1].y, this._percentage);
			}
			else if (this.vector3s[1].y < 0f)
			{
				this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this._percentage);
			}
			if (this.vector3s[1].z > 0f)
			{
				this.vector3s[2].z = this.punch(this.vector3s[1].z, this._percentage);
			}
			else if (this.vector3s[1].z < 0f)
			{
				this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this._percentage);
			}
			base.transform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			this.postUpdate = base.transform.eulerAngles;
			if (this.physics)
			{
				base.transform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06009FB9 RID: 40889 RVA: 0x00419B58 File Offset: 0x00417F58
		private void ApplyPunchScaleTargets()
		{
			if (this.vector3s[1].x > 0f)
			{
				this.vector3s[2].x = this.punch(this.vector3s[1].x, this._percentage);
			}
			else if (this.vector3s[1].x < 0f)
			{
				this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this._percentage);
			}
			if (this.vector3s[1].y > 0f)
			{
				this.vector3s[2].y = this.punch(this.vector3s[1].y, this._percentage);
			}
			else if (this.vector3s[1].y < 0f)
			{
				this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this._percentage);
			}
			if (this.vector3s[1].z > 0f)
			{
				this.vector3s[2].z = this.punch(this.vector3s[1].z, this._percentage);
			}
			else if (this.vector3s[1].z < 0f)
			{
				this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this._percentage);
			}
			base.transform.localScale = this.vector3s[0] + this.vector3s[2];
		}

		// Token: 0x06009FBA RID: 40890 RVA: 0x00419D70 File Offset: 0x00418170
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

		// Token: 0x06009FBB RID: 40891 RVA: 0x00419D8C File Offset: 0x0041818C
		private void TweenStart()
		{
			this.CallBack("onstart");
			if (!this.loop)
			{
				this.ConflictCheck();
				this.GenerateTargets();
			}
			if (this.type == "stab")
			{
				this.audioSource.PlayOneShot(this.audioSource.clip);
			}
			if (this.type == "move" || this.type == "scale" || this.type == "rotate" || this.type == "punch" || this.type == "shake" || this.type == "curve" || this.type == "look")
			{
				this.EnableKinematic();
			}
			this.isRunning = true;
		}

		// Token: 0x06009FBC RID: 40892 RVA: 0x00419E88 File Offset: 0x00418288
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

		// Token: 0x06009FBD RID: 40893 RVA: 0x00419EA3 File Offset: 0x004182A3
		private void TweenUpdate()
		{
			this.apply();
			this.CallBack("onupdate");
			if (this.onUpdate != null)
			{
				this.onUpdate(this._percentage);
			}
			this.UpdatePercentage();
		}

		// Token: 0x06009FBE RID: 40894 RVA: 0x00419EE0 File Offset: 0x004182E0
		private void TweenComplete()
		{
			this.isRunning = false;
			if (this._percentage > 0.5f)
			{
				this._percentage = 1f;
			}
			else
			{
				this._percentage = 0f;
			}
			this.apply();
			if (this.type == "value")
			{
				this.CallBack("onupdate");
				if (this.onUpdate != null)
				{
					this.onUpdate(this._percentage);
				}
			}
			if (this.loopType == Tween.LoopType.none)
			{
				this.Dispose();
			}
			else
			{
				this.TweenLoop();
			}
			this.CallBack("oncomplete");
			if (this.onComplete != null)
			{
				this.onComplete();
			}
		}

		// Token: 0x06009FBF RID: 40895 RVA: 0x00419FA4 File Offset: 0x004183A4
		private void TweenLoop()
		{
			this.DisableKinematic();
			Tween.LoopType loopType = this.loopType;
			if (loopType != Tween.LoopType.loop)
			{
				if (loopType == Tween.LoopType.pingPong)
				{
					this.reverse = !this.reverse;
					this.runningTime = 0f;
					base.StartCoroutine("TweenRestart");
				}
			}
			else
			{
				this._percentage = 0f;
				this.runningTime = 0f;
				this.apply();
				base.StartCoroutine("TweenRestart");
			}
		}

		// Token: 0x06009FC0 RID: 40896 RVA: 0x0041A030 File Offset: 0x00418430
		public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
		{
			Rect result = new Rect(Tween.FloatUpdate(currentValue.x, targetValue.x, speed), Tween.FloatUpdate(currentValue.y, targetValue.y, speed), Tween.FloatUpdate(currentValue.width, targetValue.width, speed), Tween.FloatUpdate(currentValue.height, targetValue.height, speed));
			return result;
		}

		// Token: 0x06009FC1 RID: 40897 RVA: 0x0041A098 File Offset: 0x00418498
		public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
		{
			Vector3 a = targetValue - currentValue;
			currentValue += a * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06009FC2 RID: 40898 RVA: 0x0041A0C8 File Offset: 0x004184C8
		public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
		{
			Vector2 a = targetValue - currentValue;
			currentValue += a * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06009FC3 RID: 40899 RVA: 0x0041A0F8 File Offset: 0x004184F8
		public static float FloatUpdate(float currentValue, float targetValue, float speed)
		{
			float num = targetValue - currentValue;
			currentValue += num * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06009FC4 RID: 40900 RVA: 0x0041A117 File Offset: 0x00418517
		public static void FadeUpdate(GameObject target, Hashtable args)
		{
			args["a"] = args["alpha"];
			Tween.ColorUpdate(target, args);
		}

		// Token: 0x06009FC5 RID: 40901 RVA: 0x0041A136 File Offset: 0x00418536
		public static void FadeUpdate(GameObject target, float alpha, float time)
		{
			Tween.FadeUpdate(target, Tween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06009FC6 RID: 40902 RVA: 0x0041A16C File Offset: 0x0041856C
		public static void ColorUpdate(GameObject target, Hashtable args)
		{
			Tween.CleanArgs(args);
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
						Tween.ColorUpdate(transform.gameObject, args);
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
				num *= Tween.Defaults.updateTimePercentage;
			}
			else
			{
				num = Tween.Defaults.updateTime;
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

		// Token: 0x06009FC7 RID: 40903 RVA: 0x0041A5D0 File Offset: 0x004189D0
		public static void ColorUpdate(GameObject target, Color color, float time)
		{
			Tween.ColorUpdate(target, Tween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06009FC8 RID: 40904 RVA: 0x0041A608 File Offset: 0x00418A08
		public static void AudioUpdate(GameObject target, Hashtable args)
		{
			Tween.CleanArgs(args);
			Vector2[] array = new Vector2[4];
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= Tween.Defaults.updateTimePercentage;
			}
			else
			{
				num = Tween.Defaults.updateTime;
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

		// Token: 0x06009FC9 RID: 40905 RVA: 0x0041A7B8 File Offset: 0x00418BB8
		public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
		{
			Tween.AudioUpdate(target, Tween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06009FCA RID: 40906 RVA: 0x0041A80C File Offset: 0x00418C0C
		public static void RotateUpdate(GameObject target, Hashtable args)
		{
			Tween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			Vector3 eulerAngles = target.transform.eulerAngles;
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= Tween.Defaults.updateTimePercentage;
			}
			else
			{
				num = Tween.Defaults.updateTime;
			}
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = Tween.Defaults.isLocal;
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

		// Token: 0x06009FCB RID: 40907 RVA: 0x0041AA81 File Offset: 0x00418E81
		public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
		{
			Tween.RotateUpdate(target, Tween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06009FCC RID: 40908 RVA: 0x0041AAB8 File Offset: 0x00418EB8
		public static void ScaleUpdate(GameObject target, Hashtable args)
		{
			Tween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= Tween.Defaults.updateTimePercentage;
			}
			else
			{
				num = Tween.Defaults.updateTime;
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

		// Token: 0x06009FCD RID: 40909 RVA: 0x0041AD0B File Offset: 0x0041910B
		public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
		{
			Tween.ScaleUpdate(target, Tween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06009FCE RID: 40910 RVA: 0x0041AD40 File Offset: 0x00419140
		public static void MoveUpdate(GameObject target, Hashtable args)
		{
			Tween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			Vector3 position = target.transform.position;
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= Tween.Defaults.updateTimePercentage;
			}
			else
			{
				num = Tween.Defaults.updateTime;
			}
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = Tween.Defaults.isLocal;
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

		// Token: 0x06009FCF RID: 40911 RVA: 0x0041B0B3 File Offset: 0x004194B3
		public static void MoveUpdate(GameObject target, Vector3 position, float time)
		{
			Tween.MoveUpdate(target, Tween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06009FD0 RID: 40912 RVA: 0x0041B0E8 File Offset: 0x004194E8
		public static void LookUpdate(GameObject target, Hashtable args)
		{
			Tween.CleanArgs(args);
			Vector3[] array = new Vector3[5];
			float num;
			if (args.Contains("looktime"))
			{
				num = (float)args["looktime"];
				num *= Tween.Defaults.updateTimePercentage;
			}
			else if (args.Contains("time"))
			{
				num = (float)args["time"] * 0.15f;
				num *= Tween.Defaults.updateTimePercentage;
			}
			else
			{
				num = Tween.Defaults.updateTime;
			}
			array[0] = target.transform.eulerAngles;
			if (args.Contains("looktarget"))
			{
				if (args["looktarget"].GetType() == typeof(Transform))
				{
					Transform transform = target.transform;
					Transform target2 = (Transform)args["looktarget"];
					Vector3? vector = (Vector3?)args["up"];
					transform.LookAt(target2, (vector == null) ? Tween.Defaults.up : vector.Value);
				}
				else if (args["looktarget"].GetType() == typeof(Vector3))
				{
					Transform transform2 = target.transform;
					Vector3 worldPosition = (Vector3)args["looktarget"];
					Vector3? vector2 = (Vector3?)args["up"];
					transform2.LookAt(worldPosition, (vector2 == null) ? Tween.Defaults.up : vector2.Value);
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

		// Token: 0x06009FD1 RID: 40913 RVA: 0x0041B48C File Offset: 0x0041988C
		public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
		{
			Tween.LookUpdate(target, Tween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06009FD2 RID: 40914 RVA: 0x0041B4C4 File Offset: 0x004198C4
		public static float PathLength(Transform[] path)
		{
			Vector3[] array = new Vector3[path.Length];
			float num = 0f;
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			Vector3[] pts = Tween.PathControlPointGenerator(array);
			Vector3 a = Tween.Interp(pts, 0f);
			int num2 = path.Length * 20;
			for (int j = 1; j <= num2; j++)
			{
				float t = (float)j / (float)num2;
				Vector3 vector = Tween.Interp(pts, t);
				num += Vector3.Distance(a, vector);
				a = vector;
			}
			return num;
		}

		// Token: 0x06009FD3 RID: 40915 RVA: 0x0041B560 File Offset: 0x00419960
		public static float PathLength(Vector3[] path)
		{
			float num = 0f;
			Vector3[] pts = Tween.PathControlPointGenerator(path);
			Vector3 a = Tween.Interp(pts, 0f);
			int num2 = path.Length * 20;
			for (int i = 1; i <= num2; i++)
			{
				float t = (float)i / (float)num2;
				Vector3 vector = Tween.Interp(pts, t);
				num += Vector3.Distance(a, vector);
				a = vector;
			}
			return num;
		}

		// Token: 0x06009FD4 RID: 40916 RVA: 0x0041B5C4 File Offset: 0x004199C4
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

		// Token: 0x06009FD5 RID: 40917 RVA: 0x0041B623 File Offset: 0x00419A23
		public static void PutOnPath(GameObject target, Vector3[] path, float percent)
		{
			target.transform.position = Tween.Interp(Tween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06009FD6 RID: 40918 RVA: 0x0041B63C File Offset: 0x00419A3C
		public static void PutOnPath(Transform target, Vector3[] path, float percent)
		{
			target.position = Tween.Interp(Tween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06009FD7 RID: 40919 RVA: 0x0041B650 File Offset: 0x00419A50
		public static void PutOnPath(GameObject target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.transform.position = Tween.Interp(Tween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06009FD8 RID: 40920 RVA: 0x0041B6A8 File Offset: 0x00419AA8
		public static void PutOnPath(Transform target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.position = Tween.Interp(Tween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06009FD9 RID: 40921 RVA: 0x0041B6F8 File Offset: 0x00419AF8
		public static Vector3 PointOnPath(Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			return Tween.Interp(Tween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06009FDA RID: 40922 RVA: 0x0041B742 File Offset: 0x00419B42
		public static void DrawLine(Vector3[] line)
		{
			if (line.Length > 0)
			{
				Tween.DrawLineHelper(line, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FDB RID: 40923 RVA: 0x0041B75D File Offset: 0x00419B5D
		public static void DrawLine(Vector3[] line, Color color)
		{
			if (line.Length > 0)
			{
				Tween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06009FDC RID: 40924 RVA: 0x0041B774 File Offset: 0x00419B74
		public static void DrawLine(Transform[] line)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				Tween.DrawLineHelper(array, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FDD RID: 40925 RVA: 0x0041B7CC File Offset: 0x00419BCC
		public static void DrawLine(Transform[] line, Color color)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				Tween.DrawLineHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009FDE RID: 40926 RVA: 0x0041B81F File Offset: 0x00419C1F
		public static void DrawLineGizmos(Vector3[] line)
		{
			if (line.Length > 0)
			{
				Tween.DrawLineHelper(line, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FDF RID: 40927 RVA: 0x0041B83A File Offset: 0x00419C3A
		public static void DrawLineGizmos(Vector3[] line, Color color)
		{
			if (line.Length > 0)
			{
				Tween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06009FE0 RID: 40928 RVA: 0x0041B854 File Offset: 0x00419C54
		public static void DrawLineGizmos(Transform[] line)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				Tween.DrawLineHelper(array, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FE1 RID: 40929 RVA: 0x0041B8AC File Offset: 0x00419CAC
		public static void DrawLineGizmos(Transform[] line, Color color)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				Tween.DrawLineHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009FE2 RID: 40930 RVA: 0x0041B8FF File Offset: 0x00419CFF
		public static void DrawLineHandles(Vector3[] line)
		{
			if (line.Length > 0)
			{
				Tween.DrawLineHelper(line, Tween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009FE3 RID: 40931 RVA: 0x0041B91A File Offset: 0x00419D1A
		public static void DrawLineHandles(Vector3[] line, Color color)
		{
			if (line.Length > 0)
			{
				Tween.DrawLineHelper(line, color, "handles");
			}
		}

		// Token: 0x06009FE4 RID: 40932 RVA: 0x0041B934 File Offset: 0x00419D34
		public static void DrawLineHandles(Transform[] line)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				Tween.DrawLineHelper(array, Tween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009FE5 RID: 40933 RVA: 0x0041B98C File Offset: 0x00419D8C
		public static void DrawLineHandles(Transform[] line, Color color)
		{
			if (line.Length > 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				Tween.DrawLineHelper(array, color, "handles");
			}
		}

		// Token: 0x06009FE6 RID: 40934 RVA: 0x0041B9DF File Offset: 0x00419DDF
		public static Vector3 PointOnPath(Vector3[] path, float percent)
		{
			return Tween.Interp(Tween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06009FE7 RID: 40935 RVA: 0x0041B9ED File Offset: 0x00419DED
		public static void DrawPath(Vector3[] path)
		{
			if (path.Length > 0)
			{
				Tween.DrawPathHelper(path, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FE8 RID: 40936 RVA: 0x0041BA08 File Offset: 0x00419E08
		public static void DrawPath(Vector3[] path, Color color)
		{
			if (path.Length > 0)
			{
				Tween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06009FE9 RID: 40937 RVA: 0x0041BA20 File Offset: 0x00419E20
		public static void DrawPath(Transform[] path)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				Tween.DrawPathHelper(array, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FEA RID: 40938 RVA: 0x0041BA78 File Offset: 0x00419E78
		public static void DrawPath(Transform[] path, Color color)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				Tween.DrawPathHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009FEB RID: 40939 RVA: 0x0041BACB File Offset: 0x00419ECB
		public static void DrawPathGizmos(Vector3[] path)
		{
			if (path.Length > 0)
			{
				Tween.DrawPathHelper(path, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FEC RID: 40940 RVA: 0x0041BAE6 File Offset: 0x00419EE6
		public static void DrawPathGizmos(Vector3[] path, Color color)
		{
			if (path.Length > 0)
			{
				Tween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06009FED RID: 40941 RVA: 0x0041BB00 File Offset: 0x00419F00
		public static void DrawPathGizmos(Transform[] path)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				Tween.DrawPathHelper(array, Tween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06009FEE RID: 40942 RVA: 0x0041BB58 File Offset: 0x00419F58
		public static void DrawPathGizmos(Transform[] path, Color color)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				Tween.DrawPathHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06009FEF RID: 40943 RVA: 0x0041BBAB File Offset: 0x00419FAB
		public static void DrawPathHandles(Vector3[] path)
		{
			if (path.Length > 0)
			{
				Tween.DrawPathHelper(path, Tween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009FF0 RID: 40944 RVA: 0x0041BBC6 File Offset: 0x00419FC6
		public static void DrawPathHandles(Vector3[] path, Color color)
		{
			if (path.Length > 0)
			{
				Tween.DrawPathHelper(path, color, "handles");
			}
		}

		// Token: 0x06009FF1 RID: 40945 RVA: 0x0041BBE0 File Offset: 0x00419FE0
		public static void DrawPathHandles(Transform[] path)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				Tween.DrawPathHelper(array, Tween.Defaults.color, "handles");
			}
		}

		// Token: 0x06009FF2 RID: 40946 RVA: 0x0041BC38 File Offset: 0x0041A038
		public static void DrawPathHandles(Transform[] path, Color color)
		{
			if (path.Length > 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				Tween.DrawPathHelper(array, color, "handles");
			}
		}

		// Token: 0x06009FF3 RID: 40947 RVA: 0x0041BC8C File Offset: 0x0041A08C
		public static void CameraFadeDepth(int depth)
		{
			if (Tween.cameraFade)
			{
				Tween.cameraFade.transform.position = new Vector3(Tween.cameraFade.transform.position.x, Tween.cameraFade.transform.position.y, (float)depth);
			}
		}

		// Token: 0x06009FF4 RID: 40948 RVA: 0x0041BCEC File Offset: 0x0041A0EC
		public static void CameraFadeDestroy()
		{
			if (Tween.cameraFade)
			{
				UnityEngine.Object.Destroy(Tween.cameraFade);
			}
		}

		// Token: 0x06009FF5 RID: 40949 RVA: 0x0041BD07 File Offset: 0x0041A107
		public static void CameraFadeSwap(Texture2D texture)
		{
			if (Tween.cameraFade)
			{
				Tween.cameraFade.GetComponent<GUITexture>().texture = texture;
			}
		}

		// Token: 0x06009FF6 RID: 40950 RVA: 0x0041BD28 File Offset: 0x0041A128
		public static GameObject CameraFadeAdd(Texture2D texture, int depth)
		{
			if (Tween.cameraFade)
			{
				return null;
			}
			Tween.cameraFade = new GameObject("iTween Camera Fade");
			Tween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)depth);
			Tween.cameraFade.AddComponent<GUITexture>();
			Tween.cameraFade.GetComponent<GUITexture>().texture = texture;
			Tween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
			return Tween.cameraFade;
		}

		// Token: 0x06009FF7 RID: 40951 RVA: 0x0041BDC0 File Offset: 0x0041A1C0
		public static GameObject CameraFadeAdd(Texture2D texture)
		{
			if (Tween.cameraFade)
			{
				return null;
			}
			Tween.cameraFade = new GameObject("iTween Camera Fade");
			Tween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)Tween.Defaults.cameraFadeDepth);
			Tween.cameraFade.AddComponent<GUITexture>();
			Tween.cameraFade.GetComponent<GUITexture>().texture = texture;
			Tween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
			return Tween.cameraFade;
		}

		// Token: 0x06009FF8 RID: 40952 RVA: 0x0041BE5C File Offset: 0x0041A25C
		public static GameObject CameraFadeAdd()
		{
			if (Tween.cameraFade)
			{
				return null;
			}
			Tween.cameraFade = new GameObject("iTween Camera Fade");
			Tween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)Tween.Defaults.cameraFadeDepth);
			Tween.cameraFade.AddComponent<GUITexture>();
			Tween.cameraFade.GetComponent<GUITexture>().texture = Tween.CameraTexture(Color.black);
			Tween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
			return Tween.cameraFade;
		}

		// Token: 0x06009FF9 RID: 40953 RVA: 0x0041BF00 File Offset: 0x0041A300
		public static void Resume(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				tween.enabled = true;
			}
		}

		// Token: 0x06009FFA RID: 40954 RVA: 0x0041BF44 File Offset: 0x0041A344
		public static void Resume(GameObject target, bool includechildren)
		{
			Tween.Resume(target);
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						Tween.Resume(transform.gameObject, true);
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

		// Token: 0x06009FFB RID: 40955 RVA: 0x0041BFBC File Offset: 0x0041A3BC
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

		// Token: 0x06009FFC RID: 40956 RVA: 0x0041C03C File Offset: 0x0041A43C
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
						Tween.Resume(transform.gameObject, type, true);
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

		// Token: 0x06009FFD RID: 40957 RVA: 0x0041C128 File Offset: 0x0041A528
		public static void Resume()
		{
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject target = (GameObject)hashtable["target"];
				Tween.Resume(target);
			}
		}

		// Token: 0x06009FFE RID: 40958 RVA: 0x0041C178 File Offset: 0x0041A578
		public static void Resume(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				Tween.Resume((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06009FFF RID: 40959 RVA: 0x0041C204 File Offset: 0x0041A604
		public static void Pause(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				if (tween.delay > 0f)
				{
					tween.delay -= Time.time - tween.delayStarted;
					tween.StopCoroutine("TweenDelay");
				}
				tween.isPaused = true;
				tween.enabled = false;
			}
		}

		// Token: 0x0600A000 RID: 40960 RVA: 0x0041C284 File Offset: 0x0041A684
		public static void Pause(GameObject target, bool includechildren)
		{
			Tween.Pause(target);
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						Tween.Pause(transform.gameObject, true);
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

		// Token: 0x0600A001 RID: 40961 RVA: 0x0041C2FC File Offset: 0x0041A6FC
		public static void Pause(GameObject target, string type)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					if (tween.delay > 0f)
					{
						tween.delay -= Time.time - tween.delayStarted;
						tween.StopCoroutine("TweenDelay");
					}
					tween.isPaused = true;
					tween.enabled = false;
				}
			}
		}

		// Token: 0x0600A002 RID: 40962 RVA: 0x0041C3B8 File Offset: 0x0041A7B8
		public static void Pause(GameObject target, string type, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					if (tween.delay > 0f)
					{
						tween.delay -= Time.time - tween.delayStarted;
						tween.StopCoroutine("TweenDelay");
					}
					tween.isPaused = true;
					tween.enabled = false;
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
						Tween.Pause(transform.gameObject, type, true);
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

		// Token: 0x0600A003 RID: 40963 RVA: 0x0041C4E0 File Offset: 0x0041A8E0
		public static void Pause()
		{
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject target = (GameObject)hashtable["target"];
				Tween.Pause(target);
			}
		}

		// Token: 0x0600A004 RID: 40964 RVA: 0x0041C530 File Offset: 0x0041A930
		public static void Pause(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				Tween.Pause((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x0600A005 RID: 40965 RVA: 0x0041C5BB File Offset: 0x0041A9BB
		public static int Count()
		{
			return Tween.tweens.Count;
		}

		// Token: 0x0600A006 RID: 40966 RVA: 0x0041C5C8 File Offset: 0x0041A9C8
		public static int Count(string type)
		{
			int num = 0;
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				string text = (string)hashtable["type"] + (string)hashtable["method"];
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600A007 RID: 40967 RVA: 0x0041C654 File Offset: 0x0041AA54
		public static int Count(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			return components.Length;
		}

		// Token: 0x0600A008 RID: 40968 RVA: 0x0041C678 File Offset: 0x0041AA78
		public static int Count(GameObject target, string type)
		{
			int num = 0;
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600A009 RID: 40969 RVA: 0x0041C6FC File Offset: 0x0041AAFC
		public static void Stop()
		{
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject target = (GameObject)hashtable["target"];
				Tween.Stop(target);
			}
			Tween.tweens.Clear();
		}

		// Token: 0x0600A00A RID: 40970 RVA: 0x0041C758 File Offset: 0x0041AB58
		public static void Stop(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				Tween.Stop((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x0600A00B RID: 40971 RVA: 0x0041C7E4 File Offset: 0x0041ABE4
		public static void StopByName(string name)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				GameObject value = (GameObject)hashtable["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				Tween.StopByName((GameObject)arrayList[j], name);
			}
		}

		// Token: 0x0600A00C RID: 40972 RVA: 0x0041C870 File Offset: 0x0041AC70
		public static void Stop(GameObject target)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				tween.Dispose();
			}
		}

		// Token: 0x0600A00D RID: 40973 RVA: 0x0041C8B4 File Offset: 0x0041ACB4
		public static void Stop(GameObject target, bool includechildren)
		{
			Tween.Stop(target);
			if (includechildren)
			{
				IEnumerator enumerator = target.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						Tween.Stop(transform.gameObject, true);
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

		// Token: 0x0600A00E RID: 40974 RVA: 0x0041C92C File Offset: 0x0041AD2C
		public static void Stop(GameObject target, string type)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					tween.Dispose();
				}
			}
		}

		// Token: 0x0600A00F RID: 40975 RVA: 0x0041C9AC File Offset: 0x0041ADAC
		public static void StopByName(GameObject target, string name)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				if (tween._name == name)
				{
					tween.Dispose();
				}
			}
		}

		// Token: 0x0600A010 RID: 40976 RVA: 0x0041CA00 File Offset: 0x0041AE00
		public static void Stop(GameObject target, string type, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				string text = tween.type + tween.method;
				text = text.Substring(0, type.Length);
				if (text.ToLower() == type.ToLower())
				{
					tween.Dispose();
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
						Tween.Stop(transform.gameObject, type, true);
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

		// Token: 0x0600A011 RID: 40977 RVA: 0x0041CAEC File Offset: 0x0041AEEC
		public static void StopByName(GameObject target, string name, bool includechildren)
		{
			Component[] components = target.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				if (tween._name == name)
				{
					tween.Dispose();
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
						Tween.StopByName(transform.gameObject, name, true);
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

		// Token: 0x0600A012 RID: 40978 RVA: 0x0041CBAC File Offset: 0x0041AFAC
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

		// Token: 0x0600A013 RID: 40979 RVA: 0x0041CBF5 File Offset: 0x0041AFF5
		private void Awake()
		{
			this.RetrieveArgs();
			this.lastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0600A014 RID: 40980 RVA: 0x0041CC08 File Offset: 0x0041B008
		private IEnumerator Start()
		{
			if (this.delay > 0f)
			{
				yield return base.StartCoroutine("TweenDelay");
			}
			this.TweenStart();
			yield break;
		}

		// Token: 0x0600A015 RID: 40981 RVA: 0x0041CC24 File Offset: 0x0041B024
		private void Update()
		{
			if (this.isRunning && !this.physics)
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
			}
		}

		// Token: 0x0600A016 RID: 40982 RVA: 0x0041CC9C File Offset: 0x0041B09C
		private void FixedUpdate()
		{
			if (this.isRunning && this.physics)
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
			}
		}

		// Token: 0x0600A017 RID: 40983 RVA: 0x0041CD14 File Offset: 0x0041B114
		private void LateUpdate()
		{
			if (this.tweenArguments.Contains("looktarget") && this.isRunning && (this.type == "move" || this.type == "shake" || this.type == "punch"))
			{
				Tween.LookUpdate(base.gameObject, this.tweenArguments);
			}
		}

		// Token: 0x0600A018 RID: 40984 RVA: 0x0041CD94 File Offset: 0x0041B194
		private void OnEnable()
		{
			if (this.isRunning)
			{
				this.EnableKinematic();
			}
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

		// Token: 0x0600A019 RID: 40985 RVA: 0x0041CDE1 File Offset: 0x0041B1E1
		private void OnDisable()
		{
			this.DisableKinematic();
		}

		// Token: 0x0600A01A RID: 40986 RVA: 0x0041CDEC File Offset: 0x0041B1EC
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

		// Token: 0x0600A01B RID: 40987 RVA: 0x0041CE5C File Offset: 0x0041B25C
		private static void DrawPathHelper(Vector3[] path, Color color, string method)
		{
			Vector3[] pts = Tween.PathControlPointGenerator(path);
			Vector3 to = Tween.Interp(pts, 0f);
			Gizmos.color = color;
			int num = path.Length * 20;
			for (int i = 1; i <= num; i++)
			{
				float t = (float)i / (float)num;
				Vector3 vector = Tween.Interp(pts, t);
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

		// Token: 0x0600A01C RID: 40988 RVA: 0x0041CEDC File Offset: 0x0041B2DC
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

		// Token: 0x0600A01D RID: 40989 RVA: 0x0041D010 File Offset: 0x0041B410
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

		// Token: 0x0600A01E RID: 40990 RVA: 0x0041D128 File Offset: 0x0041B528
		private static Tween Launch(GameObject target, Hashtable args)
		{
			if (!args.Contains("id"))
			{
				args["id"] = Tween.GenerateID();
			}
			if (!args.Contains("target"))
			{
				args["target"] = target;
			}
			Tween.tweens.Insert(0, args);
			return target.AddComponent<Tween>();
		}

		// Token: 0x0600A01F RID: 40991 RVA: 0x0041D184 File Offset: 0x0041B584
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

		// Token: 0x0600A020 RID: 40992 RVA: 0x0041D35C File Offset: 0x0041B75C
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

		// Token: 0x0600A021 RID: 40993 RVA: 0x0041D3C0 File Offset: 0x0041B7C0
		private void RetrieveArgs()
		{
			IEnumerator enumerator = Tween.tweens.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Hashtable hashtable = (Hashtable)obj;
					if ((GameObject)hashtable["target"] == base.gameObject)
					{
						this.tweenArguments = hashtable;
						break;
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
			this.id = (string)this.tweenArguments["id"];
			this.type = (string)this.tweenArguments["type"];
			this._name = (string)this.tweenArguments["name"];
			this.method = (string)this.tweenArguments["method"];
			this.time = ((!this.tweenArguments.Contains("time")) ? Tween.Defaults.time : ((float)this.tweenArguments["time"]));
			if (base.GetComponent<Rigidbody>() != null)
			{
				this.physics = true;
			}
			this.delay = ((!this.tweenArguments.Contains("delay")) ? Tween.Defaults.delay : ((float)this.tweenArguments["delay"]));
			if (this.tweenArguments.Contains("namedcolorvalue"))
			{
				if (this.tweenArguments["namedcolorvalue"].GetType() == typeof(Tween.NamedValueColor))
				{
					this.namedcolorvalue = (Tween.NamedValueColor)this.tweenArguments["namedcolorvalue"];
				}
				else
				{
					try
					{
						this.namedcolorvalue = (Tween.NamedValueColor)Enum.Parse(typeof(Tween.NamedValueColor), (string)this.tweenArguments["namedcolorvalue"], true);
					}
					catch
					{
						this.namedcolorvalue = Tween.NamedValueColor._Color;
					}
				}
			}
			else
			{
				this.namedcolorvalue = Tween.Defaults.namedColorValue;
			}
			if (this.tweenArguments.Contains("looptype"))
			{
				if (this.tweenArguments["looptype"].GetType() == typeof(Tween.LoopType))
				{
					this.loopType = (Tween.LoopType)this.tweenArguments["looptype"];
				}
				else
				{
					try
					{
						this.loopType = (Tween.LoopType)Enum.Parse(typeof(Tween.LoopType), (string)this.tweenArguments["looptype"], true);
					}
					catch
					{
						this.loopType = Tween.LoopType.none;
					}
				}
			}
			else
			{
				this.loopType = Tween.LoopType.none;
			}
			if (this.tweenArguments.Contains("easetype"))
			{
				if (this.tweenArguments["easetype"].GetType() == typeof(Tween.EaseType))
				{
					this.easeType = (Tween.EaseType)this.tweenArguments["easetype"];
				}
				else
				{
					try
					{
						this.easeType = (Tween.EaseType)Enum.Parse(typeof(Tween.EaseType), (string)this.tweenArguments["easetype"], true);
					}
					catch
					{
						this.easeType = Tween.Defaults.easeType;
					}
				}
			}
			else
			{
				this.easeType = Tween.Defaults.easeType;
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
						this.space = Tween.Defaults.space;
					}
				}
			}
			else
			{
				this.space = Tween.Defaults.space;
			}
			this.isLocal = ((!this.tweenArguments.Contains("islocal")) ? Tween.Defaults.isLocal : ((bool)this.tweenArguments["islocal"]));
			this.useRealTime = ((!this.tweenArguments.Contains("ignoretimescale")) ? Tween.Defaults.useRealTime : ((bool)this.tweenArguments["ignoretimescale"]));
			this.GetEasingFunction();
		}

		// Token: 0x0600A022 RID: 40994 RVA: 0x0041D8B0 File Offset: 0x0041BCB0
		private void GetEasingFunction()
		{
			switch (this.easeType)
			{
			case Tween.EaseType.easeInQuad:
				this.ease = new Tween.EasingFunction(this.easeInQuad);
				break;
			case Tween.EaseType.easeOutQuad:
				this.ease = new Tween.EasingFunction(this.easeOutQuad);
				break;
			case Tween.EaseType.easeInOutQuad:
				this.ease = new Tween.EasingFunction(this.easeInOutQuad);
				break;
			case Tween.EaseType.easeInCubic:
				this.ease = new Tween.EasingFunction(this.easeInCubic);
				break;
			case Tween.EaseType.easeOutCubic:
				this.ease = new Tween.EasingFunction(this.easeOutCubic);
				break;
			case Tween.EaseType.easeInOutCubic:
				this.ease = new Tween.EasingFunction(this.easeInOutCubic);
				break;
			case Tween.EaseType.easeInQuart:
				this.ease = new Tween.EasingFunction(this.easeInQuart);
				break;
			case Tween.EaseType.easeOutQuart:
				this.ease = new Tween.EasingFunction(this.easeOutQuart);
				break;
			case Tween.EaseType.easeInOutQuart:
				this.ease = new Tween.EasingFunction(this.easeInOutQuart);
				break;
			case Tween.EaseType.easeInQuint:
				this.ease = new Tween.EasingFunction(this.easeInQuint);
				break;
			case Tween.EaseType.easeOutQuint:
				this.ease = new Tween.EasingFunction(this.easeOutQuint);
				break;
			case Tween.EaseType.easeInOutQuint:
				this.ease = new Tween.EasingFunction(this.easeInOutQuint);
				break;
			case Tween.EaseType.easeInSine:
				this.ease = new Tween.EasingFunction(this.easeInSine);
				break;
			case Tween.EaseType.easeOutSine:
				this.ease = new Tween.EasingFunction(this.easeOutSine);
				break;
			case Tween.EaseType.easeInOutSine:
				this.ease = new Tween.EasingFunction(this.easeInOutSine);
				break;
			case Tween.EaseType.easeInExpo:
				this.ease = new Tween.EasingFunction(this.easeInExpo);
				break;
			case Tween.EaseType.easeOutExpo:
				this.ease = new Tween.EasingFunction(this.easeOutExpo);
				break;
			case Tween.EaseType.easeInOutExpo:
				this.ease = new Tween.EasingFunction(this.easeInOutExpo);
				break;
			case Tween.EaseType.easeInCirc:
				this.ease = new Tween.EasingFunction(this.easeInCirc);
				break;
			case Tween.EaseType.easeOutCirc:
				this.ease = new Tween.EasingFunction(this.easeOutCirc);
				break;
			case Tween.EaseType.easeInOutCirc:
				this.ease = new Tween.EasingFunction(this.easeInOutCirc);
				break;
			case Tween.EaseType.linear:
				this.ease = new Tween.EasingFunction(this.linear);
				break;
			case Tween.EaseType.spring:
				this.ease = new Tween.EasingFunction(this.spring);
				break;
			case Tween.EaseType.easeInBounce:
				this.ease = new Tween.EasingFunction(this.easeInBounce);
				break;
			case Tween.EaseType.easeOutBounce:
				this.ease = new Tween.EasingFunction(this.easeOutBounce);
				break;
			case Tween.EaseType.easeInOutBounce:
				this.ease = new Tween.EasingFunction(this.easeInOutBounce);
				break;
			case Tween.EaseType.easeInBack:
				this.ease = new Tween.EasingFunction(this.easeInBack);
				break;
			case Tween.EaseType.easeOutBack:
				this.ease = new Tween.EasingFunction(this.easeOutBack);
				break;
			case Tween.EaseType.easeInOutBack:
				this.ease = new Tween.EasingFunction(this.easeInOutBack);
				break;
			case Tween.EaseType.easeInElastic:
				this.ease = new Tween.EasingFunction(this.easeInElastic);
				break;
			case Tween.EaseType.easeOutElastic:
				this.ease = new Tween.EasingFunction(this.easeOutElastic);
				break;
			case Tween.EaseType.easeInOutElastic:
				this.ease = new Tween.EasingFunction(this.easeInOutElastic);
				break;
			}
		}

		// Token: 0x0600A023 RID: 40995 RVA: 0x0041DC30 File Offset: 0x0041C030
		private void UpdatePercentage()
		{
			this.runningTime += ((!this.useRealTime) ? Time.deltaTime : (Time.realtimeSinceStartup - this.lastRealTime));
			this._percentage = ((!this.reverse) ? (this.runningTime / this.time) : (1f - this.runningTime / this.time));
			this.lastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0600A024 RID: 40996 RVA: 0x0041DCAC File Offset: 0x0041C0AC
		private void CallBack(string callbackType)
		{
			if (this.tweenArguments.Contains(callbackType) && !this.tweenArguments.Contains("ischild"))
			{
				GameObject gameObject = (!this.tweenArguments.Contains(callbackType + "target")) ? base.gameObject : ((GameObject)this.tweenArguments[callbackType + "target"]);
				if (this.tweenArguments[callbackType].GetType() == typeof(string))
				{
					gameObject.SendMessage((string)this.tweenArguments[callbackType], this.tweenArguments[callbackType + "params"], SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					UnityEngine.Object.Destroy(this);
				}
			}
		}

		// Token: 0x0600A025 RID: 40997 RVA: 0x0041DD80 File Offset: 0x0041C180
		private void Dispose()
		{
			for (int i = 0; i < Tween.tweens.Count; i++)
			{
				Hashtable hashtable = (Hashtable)Tween.tweens[i];
				if ((string)hashtable["id"] == this.id)
				{
					Tween.tweens.RemoveAt(i);
					break;
				}
			}
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x0600A026 RID: 40998 RVA: 0x0041DDF0 File Offset: 0x0041C1F0
		private void ConflictCheck()
		{
			Component[] components = base.GetComponents(typeof(Tween));
			foreach (Tween tween in components)
			{
				if (tween.type == "value")
				{
					return;
				}
				if (tween.isRunning && tween.type == this.type)
				{
					if (tween.method != this.method)
					{
						return;
					}
					if (tween.tweenArguments.Count != this.tweenArguments.Count)
					{
						tween.Dispose();
						return;
					}
					IDictionaryEnumerator enumerator = this.tweenArguments.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							if (!tween.tweenArguments.Contains(dictionaryEntry.Key))
							{
								tween.Dispose();
								return;
							}
							if (!tween.tweenArguments[dictionaryEntry.Key].Equals(this.tweenArguments[dictionaryEntry.Key]) && (string)dictionaryEntry.Key != "id")
							{
								tween.Dispose();
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

		// Token: 0x0600A027 RID: 40999 RVA: 0x0041DF74 File Offset: 0x0041C374
		private void EnableKinematic()
		{
		}

		// Token: 0x0600A028 RID: 41000 RVA: 0x0041DF76 File Offset: 0x0041C376
		private void DisableKinematic()
		{
		}

		// Token: 0x0600A029 RID: 41001 RVA: 0x0041DF78 File Offset: 0x0041C378
		private void ResumeDelay()
		{
			base.StartCoroutine("TweenDelay");
		}

		// Token: 0x0600A02A RID: 41002 RVA: 0x0041DF86 File Offset: 0x0041C386
		private float linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		// Token: 0x0600A02B RID: 41003 RVA: 0x0041DF90 File Offset: 0x0041C390
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

		// Token: 0x0600A02C RID: 41004 RVA: 0x0041E008 File Offset: 0x0041C408
		private float spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		// Token: 0x0600A02D RID: 41005 RVA: 0x0041E06C File Offset: 0x0041C46C
		private float easeInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		// Token: 0x0600A02E RID: 41006 RVA: 0x0041E07A File Offset: 0x0041C47A
		private float easeOutQuad(float start, float end, float value)
		{
			end -= start;
			return -end * value * (value - 2f) + start;
		}

		// Token: 0x0600A02F RID: 41007 RVA: 0x0041E090 File Offset: 0x0041C490
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

		// Token: 0x0600A030 RID: 41008 RVA: 0x0041E0E7 File Offset: 0x0041C4E7
		private float easeInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		// Token: 0x0600A031 RID: 41009 RVA: 0x0041E0F7 File Offset: 0x0041C4F7
		private float easeOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		// Token: 0x0600A032 RID: 41010 RVA: 0x0041E118 File Offset: 0x0041C518
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

		// Token: 0x0600A033 RID: 41011 RVA: 0x0041E16C File Offset: 0x0041C56C
		private float easeInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		// Token: 0x0600A034 RID: 41012 RVA: 0x0041E17E File Offset: 0x0041C57E
		private float easeOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * (value * value * value * value - 1f) + start;
		}

		// Token: 0x0600A035 RID: 41013 RVA: 0x0041E1A0 File Offset: 0x0041C5A0
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

		// Token: 0x0600A036 RID: 41014 RVA: 0x0041E1F9 File Offset: 0x0041C5F9
		private float easeInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		// Token: 0x0600A037 RID: 41015 RVA: 0x0041E20D File Offset: 0x0041C60D
		private float easeOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		// Token: 0x0600A038 RID: 41016 RVA: 0x0041E230 File Offset: 0x0041C630
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

		// Token: 0x0600A039 RID: 41017 RVA: 0x0041E28C File Offset: 0x0041C68C
		private float easeInSine(float start, float end, float value)
		{
			end -= start;
			return -end * Mathf.Cos(value / 1f * 1.5707964f) + end + start;
		}

		// Token: 0x0600A03A RID: 41018 RVA: 0x0041E2AC File Offset: 0x0041C6AC
		private float easeOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value / 1f * 1.5707964f) + start;
		}

		// Token: 0x0600A03B RID: 41019 RVA: 0x0041E2C9 File Offset: 0x0041C6C9
		private float easeInOutSine(float start, float end, float value)
		{
			end -= start;
			return -end / 2f * (Mathf.Cos(3.1415927f * value / 1f) - 1f) + start;
		}

		// Token: 0x0600A03C RID: 41020 RVA: 0x0041E2F3 File Offset: 0x0041C6F3
		private float easeInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
		}

		// Token: 0x0600A03D RID: 41021 RVA: 0x0041E31B File Offset: 0x0041C71B
		private float easeOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * value / 1f) + 1f) + start;
		}

		// Token: 0x0600A03E RID: 41022 RVA: 0x0041E344 File Offset: 0x0041C744
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

		// Token: 0x0600A03F RID: 41023 RVA: 0x0041E3B7 File Offset: 0x0041C7B7
		private float easeInCirc(float start, float end, float value)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		// Token: 0x0600A040 RID: 41024 RVA: 0x0041E3D7 File Offset: 0x0041C7D7
		private float easeOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		// Token: 0x0600A041 RID: 41025 RVA: 0x0041E3FC File Offset: 0x0041C7FC
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

		// Token: 0x0600A042 RID: 41026 RVA: 0x0041E46C File Offset: 0x0041C86C
		private float easeInBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return end - this.easeOutBounce(0f, end, num - value) + start;
		}

		// Token: 0x0600A043 RID: 41027 RVA: 0x0041E498 File Offset: 0x0041C898
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

		// Token: 0x0600A044 RID: 41028 RVA: 0x0041E540 File Offset: 0x0041C940
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

		// Token: 0x0600A045 RID: 41029 RVA: 0x0041E5A8 File Offset: 0x0041C9A8
		private float easeInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		// Token: 0x0600A046 RID: 41030 RVA: 0x0041E5DC File Offset: 0x0041C9DC
		private float easeOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value = value / 1f - 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		// Token: 0x0600A047 RID: 41031 RVA: 0x0041E61C File Offset: 0x0041CA1C
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

		// Token: 0x0600A048 RID: 41032 RVA: 0x0041E69C File Offset: 0x0041CA9C
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

		// Token: 0x0600A049 RID: 41033 RVA: 0x0041E714 File Offset: 0x0041CB14
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

		// Token: 0x0600A04A RID: 41034 RVA: 0x0041E7CC File Offset: 0x0041CBCC
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

		// Token: 0x0600A04B RID: 41035 RVA: 0x0041E87C File Offset: 0x0041CC7C
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

		// Token: 0x04007E4D RID: 32333
		public static ArrayList tweens = new ArrayList();

		// Token: 0x04007E4E RID: 32334
		private static GameObject cameraFade;

		// Token: 0x04007E4F RID: 32335
		public string id;

		// Token: 0x04007E50 RID: 32336
		public string type;

		// Token: 0x04007E51 RID: 32337
		public string method;

		// Token: 0x04007E52 RID: 32338
		public Tween.EaseType easeType;

		// Token: 0x04007E53 RID: 32339
		public float time;

		// Token: 0x04007E54 RID: 32340
		public float delay;

		// Token: 0x04007E55 RID: 32341
		public Tween.LoopType loopType;

		// Token: 0x04007E56 RID: 32342
		public bool isRunning;

		// Token: 0x04007E57 RID: 32343
		public bool isPaused;

		// Token: 0x04007E58 RID: 32344
		public string _name;

		// Token: 0x04007E59 RID: 32345
		private float runningTime;

		// Token: 0x04007E5A RID: 32346
		private float _percentage;

		// Token: 0x04007E5B RID: 32347
		private float delayStarted;

		// Token: 0x04007E5C RID: 32348
		private bool kinematic;

		// Token: 0x04007E5D RID: 32349
		private bool isLocal;

		// Token: 0x04007E5E RID: 32350
		private bool loop;

		// Token: 0x04007E5F RID: 32351
		private bool reverse;

		// Token: 0x04007E60 RID: 32352
		private bool wasPaused;

		// Token: 0x04007E61 RID: 32353
		private bool physics;

		// Token: 0x04007E62 RID: 32354
		private Hashtable tweenArguments;

		// Token: 0x04007E63 RID: 32355
		private Space space;

		// Token: 0x04007E64 RID: 32356
		private Tween.EasingFunction ease;

		// Token: 0x04007E65 RID: 32357
		private Tween.ApplyTween apply;

		// Token: 0x04007E66 RID: 32358
		private AudioSource audioSource;

		// Token: 0x04007E67 RID: 32359
		private Vector3[] vector3s;

		// Token: 0x04007E68 RID: 32360
		private Vector2[] vector2s;

		// Token: 0x04007E69 RID: 32361
		private Color[,] colors;

		// Token: 0x04007E6A RID: 32362
		private float[] floats;

		// Token: 0x04007E6B RID: 32363
		private Rect[] rects;

		// Token: 0x04007E6C RID: 32364
		private Tween.CRSpline path;

		// Token: 0x04007E6D RID: 32365
		private Vector3 preUpdate;

		// Token: 0x04007E6E RID: 32366
		private Vector3 postUpdate;

		// Token: 0x04007E6F RID: 32367
		private Tween.NamedValueColor namedcolorvalue;

		// Token: 0x04007E70 RID: 32368
		private float lastRealTime;

		// Token: 0x04007E71 RID: 32369
		private bool useRealTime;

		// Token: 0x04007E72 RID: 32370
		public Action<float> onUpdate;

		// Token: 0x04007E73 RID: 32371
		public Tween.CompleteFunction onComplete;

		// Token: 0x020012BC RID: 4796
		// (Invoke) Token: 0x0600A04E RID: 41038
		private delegate float EasingFunction(float start, float end, float value);

		// Token: 0x020012BD RID: 4797
		// (Invoke) Token: 0x0600A052 RID: 41042
		private delegate void ApplyTween();

		// Token: 0x020012BE RID: 4798
		public enum EaseType
		{
			// Token: 0x04007E76 RID: 32374
			easeInQuad,
			// Token: 0x04007E77 RID: 32375
			easeOutQuad,
			// Token: 0x04007E78 RID: 32376
			easeInOutQuad,
			// Token: 0x04007E79 RID: 32377
			easeInCubic,
			// Token: 0x04007E7A RID: 32378
			easeOutCubic,
			// Token: 0x04007E7B RID: 32379
			easeInOutCubic,
			// Token: 0x04007E7C RID: 32380
			easeInQuart,
			// Token: 0x04007E7D RID: 32381
			easeOutQuart,
			// Token: 0x04007E7E RID: 32382
			easeInOutQuart,
			// Token: 0x04007E7F RID: 32383
			easeInQuint,
			// Token: 0x04007E80 RID: 32384
			easeOutQuint,
			// Token: 0x04007E81 RID: 32385
			easeInOutQuint,
			// Token: 0x04007E82 RID: 32386
			easeInSine,
			// Token: 0x04007E83 RID: 32387
			easeOutSine,
			// Token: 0x04007E84 RID: 32388
			easeInOutSine,
			// Token: 0x04007E85 RID: 32389
			easeInExpo,
			// Token: 0x04007E86 RID: 32390
			easeOutExpo,
			// Token: 0x04007E87 RID: 32391
			easeInOutExpo,
			// Token: 0x04007E88 RID: 32392
			easeInCirc,
			// Token: 0x04007E89 RID: 32393
			easeOutCirc,
			// Token: 0x04007E8A RID: 32394
			easeInOutCirc,
			// Token: 0x04007E8B RID: 32395
			linear,
			// Token: 0x04007E8C RID: 32396
			spring,
			// Token: 0x04007E8D RID: 32397
			easeInBounce,
			// Token: 0x04007E8E RID: 32398
			easeOutBounce,
			// Token: 0x04007E8F RID: 32399
			easeInOutBounce,
			// Token: 0x04007E90 RID: 32400
			easeInBack,
			// Token: 0x04007E91 RID: 32401
			easeOutBack,
			// Token: 0x04007E92 RID: 32402
			easeInOutBack,
			// Token: 0x04007E93 RID: 32403
			easeInElastic,
			// Token: 0x04007E94 RID: 32404
			easeOutElastic,
			// Token: 0x04007E95 RID: 32405
			easeInOutElastic
		}

		// Token: 0x020012BF RID: 4799
		public enum LoopType
		{
			// Token: 0x04007E97 RID: 32407
			none,
			// Token: 0x04007E98 RID: 32408
			loop,
			// Token: 0x04007E99 RID: 32409
			pingPong
		}

		// Token: 0x020012C0 RID: 4800
		public enum NamedValueColor
		{
			// Token: 0x04007E9B RID: 32411
			_Color,
			// Token: 0x04007E9C RID: 32412
			_SpecColor,
			// Token: 0x04007E9D RID: 32413
			_Emission,
			// Token: 0x04007E9E RID: 32414
			_ReflectColor
		}

		// Token: 0x020012C1 RID: 4801
		// (Invoke) Token: 0x0600A056 RID: 41046
		public delegate bool CompleteFunction();

		// Token: 0x020012C2 RID: 4802
		public static class Defaults
		{
			// Token: 0x04007E9F RID: 32415
			public static float time = 1f;

			// Token: 0x04007EA0 RID: 32416
			public static float delay = 0f;

			// Token: 0x04007EA1 RID: 32417
			public static Tween.NamedValueColor namedColorValue = Tween.NamedValueColor._Color;

			// Token: 0x04007EA2 RID: 32418
			public static Tween.LoopType loopType = Tween.LoopType.none;

			// Token: 0x04007EA3 RID: 32419
			public static Tween.EaseType easeType = Tween.EaseType.easeOutExpo;

			// Token: 0x04007EA4 RID: 32420
			public static float lookSpeed = 3f;

			// Token: 0x04007EA5 RID: 32421
			public static bool isLocal = false;

			// Token: 0x04007EA6 RID: 32422
			public static Space space = Space.Self;

			// Token: 0x04007EA7 RID: 32423
			public static bool orientToPath = false;

			// Token: 0x04007EA8 RID: 32424
			public static Color color = Color.white;

			// Token: 0x04007EA9 RID: 32425
			public static float updateTimePercentage = 0.05f;

			// Token: 0x04007EAA RID: 32426
			public static float updateTime = 1f * Tween.Defaults.updateTimePercentage;

			// Token: 0x04007EAB RID: 32427
			public static int cameraFadeDepth = 999999;

			// Token: 0x04007EAC RID: 32428
			public static float lookAhead = 0.05f;

			// Token: 0x04007EAD RID: 32429
			public static bool useRealTime = false;

			// Token: 0x04007EAE RID: 32430
			public static Vector3 up = Vector3.up;
		}

		// Token: 0x020012C3 RID: 4803
		private class CRSpline
		{
			// Token: 0x0600A05A RID: 41050 RVA: 0x0041EA28 File Offset: 0x0041CE28
			public CRSpline(params Vector3[] pts)
			{
				this.pts = new Vector3[pts.Length];
				Array.Copy(pts, this.pts, pts.Length);
			}

			// Token: 0x0600A05B RID: 41051 RVA: 0x0041EA50 File Offset: 0x0041CE50
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

			// Token: 0x04007EAF RID: 32431
			public Vector3[] pts;
		}
	}
}
