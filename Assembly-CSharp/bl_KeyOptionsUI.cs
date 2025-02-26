using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000620 RID: 1568
public class bl_KeyOptionsUI : MonoBehaviour
{
	// Token: 0x0600256C RID: 9580 RVA: 0x000D667F File Offset: 0x000D4A7F
	private void Start()
	{
		this.InstanceKeysUI();
		this.WaitKeyWindowUI.SetActive(false);
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x000D6694 File Offset: 0x000D4A94
	private void InstanceKeysUI()
	{
		List<bl_KeyInfo> list = new List<bl_KeyInfo>();
		list = bl_Input.Instance.AllKeys;
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.KeyOptionPrefab);
			gameObject.GetComponent<bl_KeyInfoUI>().Init(list[i], this);
			gameObject.transform.SetParent(this.KeyOptionPanel, false);
			gameObject.gameObject.name = list[i].Function;
			this.cacheKeysInfoUI.Add(gameObject.GetComponent<bl_KeyInfoUI>());
		}
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x000D6724 File Offset: 0x000D4B24
	private void ClearList()
	{
		foreach (bl_KeyInfoUI bl_KeyInfoUI in this.cacheKeysInfoUI)
		{
			UnityEngine.Object.Destroy(bl_KeyInfoUI.gameObject);
		}
		this.cacheKeysInfoUI.Clear();
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x000D6790 File Offset: 0x000D4B90
	private void Update()
	{
		if (this.WaitForKey && this.m_InterectableKey)
		{
			this.DetectKey();
		}
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x000D67B0 File Offset: 0x000D4BB0
	private void DetectKey()
	{
		IEnumerator enumerator = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				KeyCode keyCode = (KeyCode)obj;
				if (Input.GetKey(keyCode))
				{
					if (this.DetectIfKeyIsUse && bl_Input.Instance.isKeyUsed(keyCode) && keyCode != this.WaitFunctionKey.Key)
					{
						this.WaitKeyText.text = string.Format("KEY <b>'{0}'</b> IS ALREADY USE, \n PLEASE PRESS ANOTHER KEY FOR REPLACE <b>{1}</b>", keyCode.ToString().ToUpper(), this.WaitFunctionKey.Description.ToUpper());
					}
					else
					{
						this.KeyDetected(keyCode);
						this.WaitForKey = false;
					}
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
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x000D6898 File Offset: 0x000D4C98
	public void SetWaitKeyProcess(bl_KeyInfo info)
	{
		if (this.WaitForKey)
		{
			return;
		}
		this.WaitFunctionKey = info;
		this.WaitForKey = true;
		this.WaitKeyText.text = string.Format("PRESS A KEY FOR REPLACE <b>{0}</b>", info.Description.ToUpper());
		this.WaitKeyWindowUI.SetActive(true);
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x000D68EC File Offset: 0x000D4CEC
	private void KeyDetected(KeyCode KeyPressed)
	{
		if (this.WaitFunctionKey == null)
		{
			return;
		}
		if (bl_Input.Instance.SetKey(this.WaitFunctionKey.Function, KeyPressed))
		{
			this.ClearList();
			this.InstanceKeysUI();
			this.WaitFunctionKey = null;
			this.WaitKeyWindowUI.SetActive(false);
		}
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x000D693F File Offset: 0x000D4D3F
	public void CancelWait()
	{
		this.WaitForKey = false;
		this.WaitFunctionKey = null;
		this.WaitKeyWindowUI.SetActive(false);
		this.InteractableKey = true;
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x06002574 RID: 9588 RVA: 0x000D6962 File Offset: 0x000D4D62
	// (set) Token: 0x06002575 RID: 9589 RVA: 0x000D696A File Offset: 0x000D4D6A
	public bool InteractableKey
	{
		get
		{
			return this.m_InterectableKey;
		}
		set
		{
			this.m_InterectableKey = value;
		}
	}

	// Token: 0x0400252B RID: 9515
	[Header("Settings")]
	[SerializeField]
	private bool DetectIfKeyIsUse = true;

	// Token: 0x0400252C RID: 9516
	[Header("References")]
	[SerializeField]
	private GameObject KeyOptionPrefab;

	// Token: 0x0400252D RID: 9517
	[SerializeField]
	private Transform KeyOptionPanel;

	// Token: 0x0400252E RID: 9518
	[SerializeField]
	private GameObject WaitKeyWindowUI;

	// Token: 0x0400252F RID: 9519
	[SerializeField]
	private Text WaitKeyText;

	// Token: 0x04002530 RID: 9520
	private bool WaitForKey;

	// Token: 0x04002531 RID: 9521
	private bl_KeyInfo WaitFunctionKey;

	// Token: 0x04002532 RID: 9522
	private List<bl_KeyInfoUI> cacheKeysInfoUI = new List<bl_KeyInfoUI>();

	// Token: 0x04002533 RID: 9523
	private bool m_InterectableKey = true;
}
