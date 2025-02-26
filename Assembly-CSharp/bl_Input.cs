using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200061E RID: 1566
public class bl_Input : ScriptableObject
{
	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x0600255C RID: 9564 RVA: 0x000D6313 File Offset: 0x000D4713
	public static bl_Input Instance
	{
		get
		{
			if (bl_Input.m_Instance == null)
			{
				bl_Input.m_Instance = (Resources.Load("InputManager", typeof(bl_Input)) as bl_Input);
			}
			return bl_Input.m_Instance;
		}
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x000D6348 File Offset: 0x000D4748
	public void InitInput()
	{
		for (int i = 0; i < this.AllKeys.Count; i++)
		{
			string key = string.Format("Key.{0}", this.AllKeys[i].Function);
			this.AllKeys[i].Key = (KeyCode)PlayerPrefs.GetInt(key, (int)this.AllKeys[i].Key);
		}
	}

	// Token: 0x0600255E RID: 9566 RVA: 0x000D63B5 File Offset: 0x000D47B5
	public static bool GetKeyDown(string function)
	{
		return Input.GetKeyDown(bl_Input.Instance.GetKeyCode(function));
	}

	// Token: 0x0600255F RID: 9567 RVA: 0x000D63C7 File Offset: 0x000D47C7
	public static bool GetKey(string function)
	{
		return Input.GetKey(bl_Input.Instance.GetKeyCode(function));
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x000D63D9 File Offset: 0x000D47D9
	public static bool GetKeyUp(string function)
	{
		return Input.GetKeyUp(bl_Input.Instance.GetKeyCode(function));
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x000D63EC File Offset: 0x000D47EC
	public bool SetKey(string function, KeyCode newKey)
	{
		for (int i = 0; i < this.AllKeys.Count; i++)
		{
			if (this.AllKeys[i].Function == function)
			{
				this.AllKeys[i].Key = newKey;
				string key = string.Format("Key.{0}", function);
				PlayerPrefs.SetInt(key, (int)newKey);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x000D645C File Offset: 0x000D485C
	public KeyCode GetKeyCode(string function)
	{
		for (int i = 0; i < this.AllKeys.Count; i++)
		{
			if (this.AllKeys[i].Function == function)
			{
				return this.AllKeys[i].Key;
			}
		}
		return KeyCode.None;
	}

	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x06002563 RID: 9571 RVA: 0x000D64B4 File Offset: 0x000D48B4
	public static float VerticalAxis
	{
		get
		{
			if (bl_Input.GetKey("Up") && !bl_Input.GetKey("Down"))
			{
				return 1f;
			}
			if (!bl_Input.GetKey("Up") && bl_Input.GetKey("Down"))
			{
				return -1f;
			}
			if (bl_Input.GetKey("Up") && bl_Input.GetKey("Down"))
			{
				return 0.5f;
			}
			return 0f;
		}
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06002564 RID: 9572 RVA: 0x000D6534 File Offset: 0x000D4934
	public static float HorizontalAxis
	{
		get
		{
			if (bl_Input.GetKey("Right") && !bl_Input.GetKey("Left"))
			{
				return 1f;
			}
			if (!bl_Input.GetKey("Right") && bl_Input.GetKey("Left"))
			{
				return -1f;
			}
			if (bl_Input.GetKey("Right") && bl_Input.GetKey("Left"))
			{
				return 0.5f;
			}
			return 0f;
		}
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x000D65B4 File Offset: 0x000D49B4
	public bool isKeyUsed(KeyCode newKey)
	{
		for (int i = 0; i < this.AllKeys.Count; i++)
		{
			if (this.AllKeys[i].Key == newKey)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04002527 RID: 9511
	public List<bl_KeyInfo> AllKeys = new List<bl_KeyInfo>();

	// Token: 0x04002528 RID: 9512
	private static bl_Input m_Instance;
}
