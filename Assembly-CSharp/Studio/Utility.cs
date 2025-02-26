using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AIProject;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001239 RID: 4665
	internal static class Utility
	{
		// Token: 0x06009975 RID: 39285 RVA: 0x003F3524 File Offset: 0x003F1924
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06009976 RID: 39286 RVA: 0x003F3583 File Offset: 0x003F1983
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06009977 RID: 39287 RVA: 0x003F35A8 File Offset: 0x003F19A8
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06009978 RID: 39288 RVA: 0x003F3601 File Offset: 0x003F1A01
		public static void SaveColor(BinaryWriter _writer, Color _color)
		{
			_writer.Write(_color.r);
			_writer.Write(_color.g);
			_writer.Write(_color.b);
			_writer.Write(_color.a);
		}

		// Token: 0x06009979 RID: 39289 RVA: 0x003F3638 File Offset: 0x003F1A38
		public static Color LoadColor(BinaryReader _reader)
		{
			Color result;
			result.r = _reader.ReadSingle();
			result.g = _reader.ReadSingle();
			result.b = _reader.ReadSingle();
			result.a = _reader.ReadSingle();
			return result;
		}

		// Token: 0x0600997A RID: 39290 RVA: 0x003F367A File Offset: 0x003F1A7A
		public static T LoadAsset<T>(string _bundle, string _file, string _manifest) where T : UnityEngine.Object
		{
			return CommonLib.LoadAsset<T>(_bundle, _file, true, _manifest);
		}

		// Token: 0x0600997B RID: 39291 RVA: 0x003F3688 File Offset: 0x003F1A88
		public static float StringToFloat(string _text)
		{
			float num = 0f;
			return (!float.TryParse(_text, out num)) ? 0f : num;
		}

		// Token: 0x0600997C RID: 39292 RVA: 0x003F36B4 File Offset: 0x003F1AB4
		public static string GetCurrentTime()
		{
			DateTime now = DateTime.Now;
			return string.Format("{0}_{1:00}{2:00}_{3:00}{4:00}_{5:00}_{6:000}", new object[]
			{
				now.Year,
				now.Month,
				now.Day,
				now.Hour,
				now.Minute,
				now.Second,
				now.Millisecond
			});
		}

		// Token: 0x0600997D RID: 39293 RVA: 0x003F3740 File Offset: 0x003F1B40
		public static Color ConvertColor(int _r, int _g, int _b)
		{
			Color color;
			return (!ColorUtility.TryParseHtmlString(string.Format("#{0:X2}{1:X2}{2:X2}", _r, _g, _b), out color)) ? Color.clear : color;
		}

		// Token: 0x0600997E RID: 39294 RVA: 0x003F3780 File Offset: 0x003F1B80
		public static void PlaySE(SoundPack.SystemSE _systemSE)
		{
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(_systemSE);
			}
		}

		// Token: 0x0600997F RID: 39295 RVA: 0x003F379C File Offset: 0x003F1B9C
		public static string GetManifest(string _bundlePath, string _file)
		{
			if (AssetBundleCheck.IsSimulation)
			{
				return string.Empty;
			}
			foreach (KeyValuePair<string, AssetBundleManager.BundlePack> keyValuePair in from v in AssetBundleManager.ManifestBundlePack
			where Regex.Match(v.Key, "studio(\\d*)").Success
			select v)
			{
				if (keyValuePair.Value.AssetBundleManifest.GetAllAssetBundles().Any((string _s) => _s == _bundlePath))
				{
					return keyValuePair.Key;
				}
			}
			return string.Empty;
		}
	}
}
