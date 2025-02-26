using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200063F RID: 1599
public class TestPageSwitch : MonoBehaviour
{
	// Token: 0x060025FE RID: 9726 RVA: 0x000D86E8 File Offset: 0x000D6AE8
	private void Start()
	{
		this.ShowPage();
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x000D86F0 File Offset: 0x000D6AF0
	public void ShiftPage(int offset)
	{
		this.currentPage += offset;
		if (this.currentPage >= base.transform.childCount)
		{
			this.currentPage = 0;
		}
		else if (this.currentPage < 0)
		{
			this.currentPage = base.transform.childCount - 1;
		}
		this.ShowPage();
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x000D8754 File Offset: 0x000D6B54
	private void ShowPage()
	{
		int num = 0;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(num == this.currentPage);
				num++;
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

	// Token: 0x040025B6 RID: 9654
	private int currentPage;
}
