using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AIChara;
using AIProject;
using Cinemachine;
using FBSAssist;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

// Token: 0x02000A8E RID: 2702
public class GlobalMethod
{
	// Token: 0x06004F99 RID: 20377 RVA: 0x001E99CC File Offset: 0x001E7DCC
	public static void setCameraMoveFlag(VirtualCameraController _ctrl, bool _bPlay)
	{
		if (_ctrl == null)
		{
			return;
		}
		_ctrl.NoCtrlCondition = (() => !_bPlay);
	}

	// Token: 0x06004F9A RID: 20378 RVA: 0x001E9A08 File Offset: 0x001E7E08
	public static bool IsCameraMoveFlag(VirtualCameraController _ctrl)
	{
		if (_ctrl == null)
		{
			return false;
		}
		VirtualCameraController.NoCtrlFunc noCtrlCondition = _ctrl.NoCtrlCondition;
		bool flag = true;
		if (noCtrlCondition != null)
		{
			flag = noCtrlCondition();
		}
		return !flag;
	}

	// Token: 0x06004F9B RID: 20379 RVA: 0x001E9A3D File Offset: 0x001E7E3D
	public static bool IsCameraActionFlag(CameraControl_Ver2 _ctrl)
	{
		return !(_ctrl == null) && _ctrl.isControlNow;
	}

	// Token: 0x06004F9C RID: 20380 RVA: 0x001E9A53 File Offset: 0x001E7E53
	public static void setCameraBase(VirtualCameraController _ctrl, Transform _transTarget)
	{
		if (_ctrl == null)
		{
			return;
		}
		_ctrl.Follow.position = _transTarget.position;
		_ctrl.Follow.rotation = _transTarget.rotation;
	}

	// Token: 0x06004F9D RID: 20381 RVA: 0x001E9A84 File Offset: 0x001E7E84
	public static void setCameraBase(VirtualCameraController _ctrl, Vector3 _pos, Vector3 _rot)
	{
		if (_ctrl == null)
		{
			return;
		}
		_ctrl.Follow.position = _pos;
		_ctrl.Follow.rotation = Quaternion.Euler(_rot);
	}

	// Token: 0x06004F9E RID: 20382 RVA: 0x001E9AB0 File Offset: 0x001E7EB0
	public static void CameraKeyCtrl(VirtualCameraController _ctrl, ChaControl[] _Females)
	{
		if (_Females == null)
		{
			return;
		}
		if (_ctrl == null)
		{
			return;
		}
		if (!UnityEngine.Input.GetKey(KeyCode.LeftShift) && !UnityEngine.Input.GetKey(KeyCode.RightShift))
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
			{
				GameObject gameObject = _Females[0].objBodyBone.transform.FindLoop("cf_J_Head");
				if (gameObject == null)
				{
					return;
				}
				_ctrl.TargetPos = _ctrl.Follow.InverseTransformPoint(gameObject.transform.position);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.W))
			{
				GameObject gameObject2 = _Females[0].objBodyBone.transform.FindLoop("cf_J_Mune00");
				if (gameObject2 == null)
				{
					return;
				}
				_ctrl.TargetPos = _ctrl.Follow.InverseTransformPoint(gameObject2.transform.position);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
			{
				GameObject gameObject3 = _Females[0].objBodyBone.transform.FindLoop("cf_J_Kokan");
				if (gameObject3 == null)
				{
					return;
				}
				_ctrl.TargetPos = _ctrl.Follow.InverseTransformPoint(gameObject3.transform.position);
			}
		}
		else if (_Females[1] != null && _Females[1].objBodyBone)
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
			{
				GameObject gameObject4 = _Females[1].objBodyBone.transform.FindLoop("cf_J_Head");
				if (gameObject4 == null)
				{
					return;
				}
				_ctrl.TargetPos = _ctrl.Follow.InverseTransformPoint(gameObject4.transform.position);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.W))
			{
				GameObject gameObject5 = _Females[1].objBodyBone.transform.FindLoop("cf_J_Mune00");
				if (gameObject5 == null)
				{
					return;
				}
				_ctrl.TargetPos = _ctrl.Follow.InverseTransformPoint(gameObject5.transform.position);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
			{
				GameObject gameObject6 = _Females[1].objBodyBone.transform.FindLoop("cf_J_Kokan");
				if (gameObject6 == null)
				{
					return;
				}
				_ctrl.TargetPos = _ctrl.Follow.InverseTransformPoint(gameObject6.transform.position);
			}
		}
	}

	// Token: 0x06004F9F RID: 20383 RVA: 0x001E9CFC File Offset: 0x001E80FC
	public static void saveCamera(VirtualCameraController _ctrl, string _strAssetPath, string _strfile)
	{
		if (_ctrl == null)
		{
			return;
		}
		_ctrl.CameraDataSave(_strAssetPath, _strfile);
	}

	// Token: 0x06004FA0 RID: 20384 RVA: 0x001E9D14 File Offset: 0x001E8114
	public static void saveCamera(CinemachineVirtualCamera _ctrl, string _strAssetPath, string _strfile)
	{
		if (_ctrl == null)
		{
			return;
		}
		Vector3 localPosition = _ctrl.LookAt.localPosition;
		Vector3 vector = _ctrl.transform.localPosition - _ctrl.LookAt.localPosition;
		FileData fileData = new FileData(string.Empty);
		string path = fileData.Create(_strAssetPath) + _strfile + ".txt";
		using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8")))
		{
			streamWriter.Write(localPosition.x);
			streamWriter.Write('\n');
			streamWriter.Write(localPosition.y);
			streamWriter.Write('\n');
			streamWriter.Write(localPosition.z);
			streamWriter.Write('\n');
			streamWriter.Write(vector.x);
			streamWriter.Write('\n');
			streamWriter.Write(vector.y);
			streamWriter.Write('\n');
			streamWriter.Write(vector.z);
			streamWriter.Write('\n');
			streamWriter.Write(_ctrl.m_Lens.FieldOfView);
			streamWriter.Write('\n');
		}
	}

	// Token: 0x06004FA1 RID: 20385 RVA: 0x001E9E50 File Offset: 0x001E8250
	public static void loadCamera(VirtualCameraController _ctrl, string _assetbundleFolder, string _strfile, bool _isDirect = false)
	{
		if (_ctrl == null)
		{
			return;
		}
		_ctrl.CameraDataLoad(_assetbundleFolder, _strfile, _isDirect);
	}

	// Token: 0x06004FA2 RID: 20386 RVA: 0x001E9E6C File Offset: 0x001E826C
	public static void loadCamera(CinemachineVirtualCamera _ctrl, string _assetbundleFolder, string _strfile, bool _isDirect = false)
	{
		if (_ctrl == null)
		{
			return;
		}
		string text = string.Empty;
		if (!_isDirect)
		{
			text = GlobalMethod.LoadAllListText(_assetbundleFolder, _strfile, null);
		}
		else
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(_assetbundleFolder, _strfile, false, string.Empty);
			AssetBundleManager.UnloadAssetBundle(_assetbundleFolder, true, null, false);
			if (textAsset)
			{
				text = textAsset.text;
			}
		}
		if (text == string.Empty)
		{
			GlobalMethod.DebugLog("cameraファイル読み込めません", 1);
			return;
		}
		string[][] array;
		GlobalMethod.GetListString(text, out array);
		Vector3 vector;
		vector.x = float.Parse(array[0][0]);
		vector.y = float.Parse(array[1][0]);
		vector.z = float.Parse(array[2][0]);
		Vector3 a;
		a.x = float.Parse(array[3][0]);
		a.y = float.Parse(array[4][0]);
		a.z = float.Parse(array[5][0]);
		_ctrl.LookAt.localPosition = vector;
		_ctrl.transform.localPosition = a + vector;
		float fieldOfView = 0f;
		if (float.TryParse(array[6][0], out fieldOfView))
		{
			_ctrl.m_Lens.FieldOfView = fieldOfView;
		}
	}

	// Token: 0x06004FA3 RID: 20387 RVA: 0x001E9F97 File Offset: 0x001E8397
	public static void loadResetCamera(VirtualCameraController _ctrl, string _assetbundleFolder, string _strfile, bool _isDirect = false)
	{
		if (_ctrl == null)
		{
			return;
		}
		_ctrl.CameraResetDataLoad(_assetbundleFolder, _strfile, _isDirect);
	}

	// Token: 0x06004FA4 RID: 20388 RVA: 0x001E9FB0 File Offset: 0x001E83B0
	public static void DebugLog(string _str, int _state = 0)
	{
	}

	// Token: 0x06004FA5 RID: 20389 RVA: 0x001E9FB4 File Offset: 0x001E83B4
	public static void SetAllClothState(ChaControl _female, bool _isUpper, int _state, bool _isForce = false)
	{
		if (_female == null)
		{
			return;
		}
		if (_state < 0)
		{
			_state = 0;
		}
		List<int> list = new List<int>();
		if (_isUpper)
		{
			list.Add(0);
			list.Add(2);
		}
		else
		{
			list.Add(1);
			list.Add(3);
			list.Add(5);
		}
		foreach (int num in list)
		{
			if (_female.IsClothesStateKind(num) && ((int)_female.fileStatus.clothesState[num] < _state || _isForce))
			{
				_female.SetClothesState(num, (byte)_state, true);
			}
		}
	}

	// Token: 0x06004FA6 RID: 20390 RVA: 0x001EA080 File Offset: 0x001E8480
	public static int ValLoop(int valNow, int valMax)
	{
		return (valMax <= 1) ? 0 : ((valNow % valMax + valMax) % valMax);
	}

	// Token: 0x06004FA7 RID: 20391 RVA: 0x001EA096 File Offset: 0x001E8496
	public static int ValLoopEX(int valNow, int valMin, int valMax)
	{
		return GlobalMethod.ValLoop(valNow - valMin, valMax - valMin) + valMin;
	}

	// Token: 0x06004FA8 RID: 20392 RVA: 0x001EA0A8 File Offset: 0x001E84A8
	public static void GetListString(string text, out string[][] data)
	{
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		int num = array.Length;
		if (num != 0 && array[num - 1].Trim() == string.Empty)
		{
			num--;
		}
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				'\t'
			});
			num2 = Mathf.Max(num2, array2.Length);
		}
		data = new string[num][];
		for (int j = 0; j < num; j++)
		{
			data[j] = new string[num2];
			string[] array3 = array[j].Split(new char[]
			{
				'\t'
			});
			for (int k = 0; k < array3.Length; k++)
			{
				array3[k] = array3[k].Replace("\r", string.Empty).Replace("\n", string.Empty);
				if (k >= num2)
				{
					break;
				}
				data[j][k] = array3[k];
			}
		}
	}

	// Token: 0x06004FA9 RID: 20393 RVA: 0x001EA1BC File Offset: 0x001E85BC
	public static int GetIntTryParse(string _text, int _init = 0)
	{
		int result = 0;
		if (int.TryParse(_text, out result))
		{
			return result;
		}
		return _init;
	}

	// Token: 0x06004FAA RID: 20394 RVA: 0x001EA1DB File Offset: 0x001E85DB
	public static bool RangeOn<T>(T valNow, T valMin, T valMax) where T : IComparable
	{
		return valNow.CompareTo(valMax) <= 0 && valNow.CompareTo(valMin) >= 0;
	}

	// Token: 0x06004FAB RID: 20395 RVA: 0x001EA212 File Offset: 0x001E8612
	public static bool RangeOff<T>(T valNow, T valMin, T valMax) where T : IComparable
	{
		return valNow.CompareTo(valMax) < 0 && valNow.CompareTo(valMin) > 0;
	}

	// Token: 0x06004FAC RID: 20396 RVA: 0x001EA248 File Offset: 0x001E8648
	public static string LoadAllListText(string _assetbundleFolder, string _strLoadFile, List<string> _OmitFolderName = null)
	{
		StringBuilder stringBuilder = new StringBuilder();
		GlobalMethod.lstABName.Clear();
		GlobalMethod.lstABName = GlobalMethod.GetAssetBundleNameListFromPath(_assetbundleFolder, false);
		GlobalMethod.lstABName.Sort();
		for (int i = 0; i < GlobalMethod.lstABName.Count; i++)
		{
			string text = Path.GetFileNameWithoutExtension(GlobalMethod.lstABName[i]);
			int num = -1;
			if (!int.TryParse(text, out num))
			{
				num = -1;
			}
			if (num < 50 || Game.isAdd50)
			{
				text = YS_Assist.GetStringRight(text, 2);
				if (_OmitFolderName == null || !_OmitFolderName.Contains(text))
				{
					string[] allAssetName = AssetBundleCheck.GetAllAssetName(GlobalMethod.lstABName[i], false, null, false);
					bool flag = false;
					for (int j = 0; j < allAssetName.Length; j++)
					{
						if (allAssetName[j].Compare(_strLoadFile, true))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						GlobalMethod.DebugLog(string.Concat(new string[]
						{
							"[",
							GlobalMethod.lstABName[i],
							"][",
							_strLoadFile,
							"]は見つかりません"
						}), 1);
					}
					else
					{
						TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(GlobalMethod.lstABName[i], _strLoadFile, false, string.Empty);
						AssetBundleManager.UnloadAssetBundle(GlobalMethod.lstABName[i], true, null, false);
						if (!(textAsset == null))
						{
							stringBuilder.Append(textAsset.text);
						}
					}
				}
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06004FAD RID: 20397 RVA: 0x001EA3D8 File Offset: 0x001E87D8
	public static string LoadAllListText(List<string> _lstAssetBundleNames, string _strLoadFile)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < _lstAssetBundleNames.Count; i++)
		{
			int num = -1;
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_lstAssetBundleNames[i]);
			if (!int.TryParse(fileNameWithoutExtension, out num))
			{
				num = -1;
			}
			if (num < 50 || Game.isAdd50)
			{
				string[] allAssetName = AssetBundleCheck.GetAllAssetName(_lstAssetBundleNames[i], false, null, false);
				bool flag = false;
				for (int j = 0; j < allAssetName.Length; j++)
				{
					if (allAssetName[j].Compare(_strLoadFile, true))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GlobalMethod.DebugLog(string.Concat(new string[]
					{
						"[",
						_lstAssetBundleNames[i],
						"][",
						_strLoadFile,
						"]は見つかりません"
					}), 1);
				}
				else
				{
					TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(_lstAssetBundleNames[i], _strLoadFile, false, string.Empty);
					AssetBundleManager.UnloadAssetBundle(_lstAssetBundleNames[i], true, null, false);
					if (!(textAsset == null))
					{
						stringBuilder.Append(textAsset.text);
					}
				}
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06004FAE RID: 20398 RVA: 0x001EA510 File Offset: 0x001E8910
	public static List<string> LoadAllListTextFromList(string _assetbundleFolder, string _strLoadFile, ref List<string> lst, List<string> _OmitFolderName = null)
	{
		GlobalMethod.lstABName.Clear();
		GlobalMethod.lstABName = GlobalMethod.GetAssetBundleNameListFromPath(_assetbundleFolder, false);
		GlobalMethod.lstABName.Sort();
		for (int i = 0; i < GlobalMethod.lstABName.Count; i++)
		{
			string text = Path.GetFileNameWithoutExtension(GlobalMethod.lstABName[i]);
			text = YS_Assist.GetStringRight(text, 2);
			if (_OmitFolderName == null || !_OmitFolderName.Contains(text))
			{
				if (GlobalMethod.AssetFileExist(GlobalMethod.lstABName[i], _strLoadFile, string.Empty))
				{
					TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(GlobalMethod.lstABName[i], _strLoadFile, false, string.Empty);
					AssetBundleManager.UnloadAssetBundle(GlobalMethod.lstABName[i], true, null, false);
					if (!(textAsset == null))
					{
						lst.Add(textAsset.text);
					}
				}
			}
		}
		return lst;
	}

	// Token: 0x06004FAF RID: 20399 RVA: 0x001EA5F8 File Offset: 0x001E89F8
	public static List<ExcelData.Param> LoadExcelData(string _strAssetPath, string _strFileName, int sCell, int sRow, int eCell, int eRow, bool _isWarning = true)
	{
		if (!GlobalMethod.AssetFileExist(_strAssetPath, _strFileName, string.Empty))
		{
			if (_isWarning)
			{
				GlobalMethod.DebugLog(string.Concat(new string[]
				{
					"excel : [",
					_strAssetPath,
					"][",
					_strFileName,
					"]は見つかりません"
				}), 1);
			}
			return null;
		}
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(_strAssetPath, _strFileName, typeof(ExcelData), null);
		AssetBundleManager.UnloadAssetBundle(_strAssetPath, true, null, false);
		if (assetBundleLoadAssetOperation.IsEmpty())
		{
			if (_isWarning)
			{
				GlobalMethod.DebugLog(string.Concat(new string[]
				{
					"excel : [",
					_strFileName,
					"]は[",
					_strAssetPath,
					"]の中に入っていません"
				}), 1);
			}
			return null;
		}
		GlobalMethod.excelData = assetBundleLoadAssetOperation.GetAsset<ExcelData>();
		GlobalMethod.cell.Clear();
		foreach (ExcelData.Param param in GlobalMethod.excelData.list)
		{
			GlobalMethod.cell.Add(param.list[sCell]);
		}
		GlobalMethod.row.Clear();
		foreach (string item in GlobalMethod.excelData.list[sRow].list)
		{
			GlobalMethod.row.Add(item);
		}
		List<string> list = GlobalMethod.cell;
		List<string> list2 = GlobalMethod.row;
		ExcelData.Specify specify = new ExcelData.Specify(eCell, eRow);
		ExcelData.Specify specify2 = new ExcelData.Specify(list.Count, list2.Count);
		GlobalMethod.excelParams.Clear();
		if ((ulong)specify.cell > (ulong)((long)specify2.cell) || (ulong)specify.row > (ulong)((long)specify2.row))
		{
			return null;
		}
		if (specify.cell < GlobalMethod.excelData.list.Count)
		{
			int num = specify.cell;
			while (num < GlobalMethod.excelData.list.Count && num <= specify2.cell)
			{
				ExcelData.Param param2 = new ExcelData.Param();
				if (specify.row < GlobalMethod.excelData.list[num].list.Count)
				{
					param2.list = new List<string>();
					int num2 = specify.row;
					while (num2 < GlobalMethod.excelData.list[num].list.Count && num2 <= specify2.row)
					{
						param2.list.Add(GlobalMethod.excelData.list[num].list[num2]);
						num2++;
					}
				}
				GlobalMethod.excelParams.Add(param2);
				num++;
			}
		}
		return GlobalMethod.excelParams;
	}

	// Token: 0x06004FB0 RID: 20400 RVA: 0x001EA908 File Offset: 0x001E8D08
	public static List<ExcelData.Param> LoadExcelDataAlFindlFile(string _strAssetPath, string _strFileName, int sCell, int sRow, int eCell, int eRow, List<string> _OmitFolderName = null, bool _isWarning = true)
	{
		GlobalMethod.lstABName.Clear();
		GlobalMethod.lstABName = GlobalMethod.GetAssetBundleNameListFromPath(_strAssetPath, false);
		GlobalMethod.lstABName.Sort();
		for (int i = 0; i < GlobalMethod.lstABName.Count; i++)
		{
			GlobalMethod.strNo.Clear();
			GlobalMethod.strNo.Append(Path.GetFileNameWithoutExtension(GlobalMethod.lstABName[i]));
			GlobalMethod.strNo.Replace(GlobalMethod.strNo.ToString(), YS_Assist.GetStringRight(GlobalMethod.strNo.ToString(), 2));
			if (_OmitFolderName == null || !_OmitFolderName.Contains(GlobalMethod.strNo.ToString()))
			{
				string[] allAssetName = AssetBundleCheck.GetAllAssetName(GlobalMethod.lstABName[i], false, null, false);
				bool flag = false;
				for (int j = 0; j < allAssetName.Length; j++)
				{
					if (allAssetName[j].Compare(_strFileName, true))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GlobalMethod.DebugLog(string.Concat(new string[]
					{
						"[",
						GlobalMethod.lstABName[i],
						"][",
						_strFileName,
						"]は見つかりません"
					}), 1);
				}
				else
				{
					List<ExcelData.Param> list = GlobalMethod.LoadExcelData(GlobalMethod.lstABName[i], _strFileName, sCell, sRow, eCell, eRow, _isWarning);
					if (list != null)
					{
						return list;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06004FB1 RID: 20401 RVA: 0x001EAA70 File Offset: 0x001E8E70
	public static T LoadAllFolderInOneFile<T>(string _findFolder, string _strLoadFile, List<string> _OmitFolderName = null) where T : UnityEngine.Object
	{
		GlobalMethod.lstABName.Clear();
		GlobalMethod.lstABName = GlobalMethod.GetAssetBundleNameListFromPath(_findFolder, false);
		GlobalMethod.lstABName.Sort();
		for (int i = 0; i < GlobalMethod.lstABName.Count; i++)
		{
			string text = Path.GetFileNameWithoutExtension(GlobalMethod.lstABName[i]);
			text = YS_Assist.GetStringRight(text, 2);
			if (_OmitFolderName == null || !_OmitFolderName.Contains(text))
			{
				if (GlobalMethod.AssetFileExist(GlobalMethod.lstABName[i].ToString(), _strLoadFile, string.Empty))
				{
					T result = CommonLib.LoadAsset<T>(GlobalMethod.lstABName[i], _strLoadFile, false, string.Empty);
					AssetBundleManager.UnloadAssetBundle(GlobalMethod.lstABName[i], true, null, false);
					return result;
				}
			}
		}
		return (T)((object)null);
	}

	// Token: 0x06004FB2 RID: 20402 RVA: 0x001EAB44 File Offset: 0x001E8F44
	public static List<T> LoadAllFolder<T>(string _findFolder, string _strLoadFile, List<string> _OmitFolderName = null) where T : UnityEngine.Object
	{
		List<T> list = new List<T>();
		GlobalMethod.lstABName.Clear();
		GlobalMethod.lstABName = GlobalMethod.GetAssetBundleNameListFromPath(_findFolder, false);
		GlobalMethod.lstABName.Sort();
		for (int i = 0; i < GlobalMethod.lstABName.Count; i++)
		{
			string text = Path.GetFileNameWithoutExtension(GlobalMethod.lstABName[i]);
			int num = -1;
			if (!int.TryParse(text, out num))
			{
				num = -1;
			}
			if (num < 50 || Game.isAdd50)
			{
				text = YS_Assist.GetStringRight(text, 2);
				if (_OmitFolderName == null || !_OmitFolderName.Contains(text))
				{
					string[] allAssetName = AssetBundleCheck.GetAllAssetName(GlobalMethod.lstABName[i], false, null, false);
					bool flag = false;
					for (int j = 0; j < allAssetName.Length; j++)
					{
						if (allAssetName[j].Compare(_strLoadFile, true))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						GlobalMethod.DebugLog(string.Concat(new string[]
						{
							"[",
							GlobalMethod.lstABName[i],
							"][",
							_strLoadFile,
							"]は見つかりません"
						}), 1);
					}
					else
					{
						T t = CommonLib.LoadAsset<T>(GlobalMethod.lstABName[i], _strLoadFile, false, string.Empty);
						AssetBundleManager.UnloadAssetBundle(GlobalMethod.lstABName[i], true, null, false);
						if (t)
						{
							list.Add(t);
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06004FB3 RID: 20403 RVA: 0x001EACC8 File Offset: 0x001E90C8
	public static bool CheckFlagsArray(bool[] flags, int _check = 0)
	{
		if (flags.Length == 0)
		{
			return false;
		}
		bool flag = _check == 0;
		foreach (bool flag2 in flags)
		{
			if ((_check != 0) ? flag2 : (!flag2))
			{
				return !flag;
			}
		}
		return flag;
	}

	// Token: 0x06004FB4 RID: 20404 RVA: 0x001EAD1C File Offset: 0x001E911C
	public static List<string> GetAssetBundleNameListFromPath(string path, bool subdirCheck = false)
	{
		List<string> result = new List<string>();
		if (!AssetBundleCheck.IsSimulation)
		{
			string path2 = AssetBundleManager.BaseDownloadingURL + path;
			if (subdirCheck)
			{
				List<string> list = new List<string>();
				CommonLib.GetAllFiles(path2, "*.unity3d", list);
				result = (from s in list
				select s.Replace(AssetBundleManager.BaseDownloadingURL, string.Empty)).ToList<string>();
			}
			else
			{
				if (!Directory.Exists(path2))
				{
					return result;
				}
				result = (from s in Directory.GetFiles(path2, "*.unity3d")
				select s.Replace(AssetBundleManager.BaseDownloadingURL, string.Empty)).ToList<string>();
			}
		}
		return result;
	}

	// Token: 0x06004FB5 RID: 20405 RVA: 0x001EADD4 File Offset: 0x001E91D4
	public static bool StartsWith(string a, string b)
	{
		int length = a.Length;
		int length2 = b.Length;
		int num = 0;
		int num2 = 0;
		while (num < length && num2 < length2 && a[num] == b[num2])
		{
			num++;
			num2++;
		}
		return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
	}

	// Token: 0x06004FB6 RID: 20406 RVA: 0x001EAE44 File Offset: 0x001E9244
	public static bool StartsWith2(string check, string template)
	{
		int length = check.Length;
		int length2 = template.Length;
		int num = 0;
		int num2 = 0;
		while (num < length && num2 < length2 && check[num] == template[num2])
		{
			num++;
			num2++;
		}
		return num2 == length2 && length >= length2;
	}

	// Token: 0x06004FB7 RID: 20407 RVA: 0x001EAEA4 File Offset: 0x001E92A4
	public static bool AssetFileExist(string path, string targetName, string manifest = "")
	{
		bool result = false;
		if (path.IsNullOrEmpty())
		{
			return result;
		}
		int num = -1;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		if (!int.TryParse(fileNameWithoutExtension, out num))
		{
			num = -1;
		}
		if (num >= 50 && !Game.isAdd50)
		{
			return result;
		}
		if (!AssetBundleCheck.IsFile(path, targetName))
		{
			return result;
		}
		string[] allAssetName = AssetBundleCheck.GetAllAssetName(path, false, manifest, false);
		for (int i = 0; i < allAssetName.Length; i++)
		{
			if (allAssetName[i].Compare(targetName, true))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x0400489A RID: 18586
	private static ExcelData excelData;

	// Token: 0x0400489B RID: 18587
	private static List<string> cell = new List<string>();

	// Token: 0x0400489C RID: 18588
	private static List<string> row = new List<string>();

	// Token: 0x0400489D RID: 18589
	private static List<ExcelData.Param> excelParams = new List<ExcelData.Param>();

	// Token: 0x0400489E RID: 18590
	private static List<string> lstABName = new List<string>();

	// Token: 0x0400489F RID: 18591
	private static StringBuilder strNo = new StringBuilder();

	// Token: 0x02000A8F RID: 2703
	public class FloatBlend
	{
		// Token: 0x06004FBC RID: 20412 RVA: 0x001EAFA9 File Offset: 0x001E93A9
		public bool Start(float _min, float _max, float _timeBlend = 0.15f)
		{
			this.tpc.SetProgressTime(_timeBlend);
			this.tpc.Start();
			this.min = _min;
			this.max = _max;
			this.blend = true;
			return true;
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x001EAFD8 File Offset: 0x001E93D8
		public bool Proc(ref float _ans)
		{
			if (!this.blend)
			{
				return false;
			}
			float num = this.tpc.Calculate();
			_ans = Mathf.Lerp(this.min, this.max, num);
			if (num >= 1f)
			{
				this.blend = false;
			}
			return true;
		}

		// Token: 0x040048A2 RID: 18594
		private bool blend;

		// Token: 0x040048A3 RID: 18595
		private float min;

		// Token: 0x040048A4 RID: 18596
		private float max;

		// Token: 0x040048A5 RID: 18597
		private TimeProgressCtrl tpc = new TimeProgressCtrl(0.15f);
	}
}
