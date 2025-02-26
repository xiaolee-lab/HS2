using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001265 RID: 4709
	public class DrawLightLine : MonoBehaviour
	{
		// Token: 0x06009BD7 RID: 39895 RVA: 0x003FBCBC File Offset: 0x003FA0BC
		public void Add(Light _light)
		{
			this.dicLight.Add(_light, true);
		}

		// Token: 0x06009BD8 RID: 39896 RVA: 0x003FBCCB File Offset: 0x003FA0CB
		public void Remove(Light _light)
		{
			this.dicLight.Remove(_light);
		}

		// Token: 0x06009BD9 RID: 39897 RVA: 0x003FBCDA File Offset: 0x003FA0DA
		public void Clear()
		{
			this.dicLight.Clear();
		}

		// Token: 0x06009BDA RID: 39898 RVA: 0x003FBCE7 File Offset: 0x003FA0E7
		public void SetEnable(Light _light, bool _value)
		{
			if (!this.dicLight.ContainsKey(_light))
			{
				return;
			}
			this.dicLight[_light] = _value;
		}

		// Token: 0x06009BDB RID: 39899 RVA: 0x003FBD08 File Offset: 0x003FA108
		private void Start()
		{
			LightLine.shader = this.m_Shader;
		}

		// Token: 0x06009BDC RID: 39900 RVA: 0x003FBD18 File Offset: 0x003FA118
		public void OnPostRender()
		{
			if (this.dicLight.Count <= 0)
			{
				return;
			}
			foreach (KeyValuePair<Light, bool> keyValuePair in this.dicLight)
			{
				if (keyValuePair.Value)
				{
					LightLine.DrawLine(keyValuePair.Key);
				}
			}
		}

		// Token: 0x04007C31 RID: 31793
		[SerializeField]
		private Shader m_Shader;

		// Token: 0x04007C32 RID: 31794
		private Dictionary<Light, bool> dicLight = new Dictionary<Light, bool>();
	}
}
