using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

// Token: 0x02000A81 RID: 2689
public class FeelHit
{
	// Token: 0x06004F7C RID: 20348 RVA: 0x001E9448 File Offset: 0x001E7848
	public void FeelHitInit(int _personality = 0)
	{
		int num = _personality;
		if (num == 90)
		{
			num = 5;
		}
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.DicLstHitInfo.TryGetValue(num, out this.lstHitInfo))
		{
			this.lstHitInfo = new List<FeelHit.FeelInfo>();
		}
		if (this.lstHitInfo.Count == 0)
		{
			return;
		}
		foreach (FeelHit.FeelInfo feelInfo in this.lstHitInfo)
		{
			feelInfo.CreateHit();
			feelInfo.InitTime();
		}
	}

	// Token: 0x06004F7D RID: 20349 RVA: 0x001E94F4 File Offset: 0x001E78F4
	public bool isHit(int _state, int _loop, float _power)
	{
		Vector2 hitArea = this.GetHitArea(_state, _loop);
		return GlobalMethod.RangeOn<float>(_power, hitArea.x, hitArea.y);
	}

	// Token: 0x06004F7E RID: 20350 RVA: 0x001E951E File Offset: 0x001E791E
	public Vector2 GetHitArea(int _state, int _loop)
	{
		if (_state >= this.lstHitInfo.Count)
		{
			return new Vector2(1.1f, 1.1f);
		}
		return this.lstHitInfo[_state].Get(_loop);
	}

	// Token: 0x06004F7F RID: 20351 RVA: 0x001E9553 File Offset: 0x001E7953
	public bool ChangeHit(int _state, int _loop)
	{
		return this.lstHitInfo[_state].ChangeHit(_loop);
	}

	// Token: 0x06004F80 RID: 20352 RVA: 0x001E9568 File Offset: 0x001E7968
	public void InitTime()
	{
		for (int i = 0; i < this.lstHitInfo.Count; i++)
		{
			this.lstHitInfo[i].InitTime();
		}
	}

	// Token: 0x04004890 RID: 18576
	private List<FeelHit.FeelInfo> lstHitInfo = new List<FeelHit.FeelInfo>();

	// Token: 0x02000A82 RID: 2690
	public struct FeelHitInfo
	{
		// Token: 0x04004891 RID: 18577
		public Vector2 area;

		// Token: 0x04004892 RID: 18578
		public float rate;
	}

	// Token: 0x02000A83 RID: 2691
	public class FeelInfo
	{
		// Token: 0x06004F82 RID: 20354 RVA: 0x001E95D8 File Offset: 0x001E79D8
		public void CreateHit()
		{
			foreach (FeelHit.FeelHitInfo feelHitInfo in this.lstHitArea)
			{
				this.tmpv = Vector2.zero;
				this.tmpv.x = (float)UnityEngine.Random.Range(5, 95 - (int)feelHitInfo.rate);
				this.tmpv.y = this.tmpv.x + feelHitInfo.rate;
				this.lstHit.Add(this.tmpv);
			}
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x001E9684 File Offset: 0x001E7A84
		public bool ChangeHit(int _state)
		{
			if (_state >= this.lstChangeTime.Count || _state >= this.lstChangeDeltaTime.Count || _state >= this.lstHitArea.Count || _state >= this.lstHit.Count)
			{
				return false;
			}
			List<float> list;
			(list = this.lstChangeDeltaTime)[_state] = list[_state] + Time.deltaTime;
			if (this.lstChangeDeltaTime[_state] >= this.lstChangeTime[_state])
			{
				this.lstChangeTime[_state] = UnityEngine.Random.Range(this.lstHitArea[_state].area.x, this.lstHitArea[_state].area.y);
				this.lstChangeDeltaTime[_state] = 0f;
				Vector2 value = this.lstHit[_state];
				value.x = (float)UnityEngine.Random.Range(5, 95 - (int)this.lstHitArea[_state].rate);
				value.y = value.x + this.lstHitArea[_state].rate;
				this.lstHit[_state] = value;
			}
			return true;
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x001E97D0 File Offset: 0x001E7BD0
		public void InitTime()
		{
			this.lstChangeTime.Clear();
			this.lstChangeDeltaTime.Clear();
			foreach (FeelHit.FeelHitInfo feelHitInfo in this.lstHitArea)
			{
				float item = UnityEngine.Random.Range(feelHitInfo.area.x, feelHitInfo.area.y);
				this.lstChangeTime.Add(item);
				this.lstChangeDeltaTime.Add(0f);
			}
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x001E9878 File Offset: 0x001E7C78
		public Vector2 Get(int _state)
		{
			if (_state >= this.lstHit.Count)
			{
				return new Vector2(1.1f, 1.1f);
			}
			return this.lstHit[_state] * 0.01f;
		}

		// Token: 0x04004893 RID: 18579
		public List<Vector2> lstHit = new List<Vector2>();

		// Token: 0x04004894 RID: 18580
		public List<FeelHit.FeelHitInfo> lstHitArea = new List<FeelHit.FeelHitInfo>();

		// Token: 0x04004895 RID: 18581
		public List<float> lstChangeTime = new List<float>();

		// Token: 0x04004896 RID: 18582
		public List<float> lstChangeDeltaTime = new List<float>();

		// Token: 0x04004897 RID: 18583
		private Vector2 tmpv;
	}
}
