using System;

// Token: 0x020011BF RID: 4543
public class KK_Lib
{
	// Token: 0x06009500 RID: 38144 RVA: 0x003D6EBC File Offset: 0x003D52BC
	public static AssetBundleLoadAssetOperation LoadFile<Type>(string _assetBundleName, string _assetName, string _manifest = "")
	{
		if (!AssetBundleCheck.IsFile(_assetBundleName, _assetName))
		{
			string text = "読み込みエラー\r\nassetBundleName：" + _assetBundleName + "\tassetName：" + _assetName;
			return null;
		}
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(_assetBundleName, _assetName, typeof(Type), (!_manifest.IsNullOrEmpty()) ? _manifest : null);
		if (assetBundleLoadAssetOperation == null)
		{
			return null;
		}
		return assetBundleLoadAssetOperation;
	}

	// Token: 0x06009501 RID: 38145 RVA: 0x003D6F16 File Offset: 0x003D5316
	public static bool Range(int _value, int _min, int _max)
	{
		return _min <= _value && _value <= _max;
	}

	// Token: 0x06009502 RID: 38146 RVA: 0x003D6F2D File Offset: 0x003D532D
	public static bool Range(float _value, float _min, float _max)
	{
		return _min <= _value && _value <= _max;
	}

	// Token: 0x06009503 RID: 38147 RVA: 0x003D6F44 File Offset: 0x003D5344
	public static float GCD(float _a, float _b)
	{
		if (_a == 0f || _b == 0f)
		{
			return 0f;
		}
		while (_a != _b)
		{
			if (_a > _b)
			{
				_a -= _b;
			}
			else
			{
				_b -= _a;
			}
		}
		return _a;
	}

	// Token: 0x06009504 RID: 38148 RVA: 0x003D6F90 File Offset: 0x003D5390
	public static float LCM(float _a, float _b)
	{
		return (_a != 0f && _b != 0f) ? (_a / KK_Lib.GCD(_a, _b) * _b) : 0f;
	}

	// Token: 0x06009505 RID: 38149 RVA: 0x003D6FC0 File Offset: 0x003D53C0
	public static void Ratio(ref float _outA, ref float _outB, float _a, float _b)
	{
		float num = KK_Lib.GCD(_a, _b);
		_outA = _a / num;
		_outB = _b / num;
	}

	// Token: 0x06009506 RID: 38150 RVA: 0x003D6FE0 File Offset: 0x003D53E0
	public static int Search(int _value)
	{
		if (_value < 2)
		{
			return 0;
		}
		int num = 1;
		for (int i = 2; i < _value; i++)
		{
			int num2 = 0;
			for (int j = 2; j <= i; j++)
			{
				if (i % j <= 0)
				{
					num2++;
				}
			}
			if (num2 == 1)
			{
				num++;
			}
		}
		return num;
	}
}
