using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012A5 RID: 4773
	public class AssociateSound : MonoBehaviour
	{
		// Token: 0x06009DD4 RID: 40404 RVA: 0x00406C95 File Offset: 0x00405095
		private void Awake()
		{
			if (this.m_AudioSource != null)
			{
				this.m_AudioSource.outputAudioMixerGroup = Sound.Mixer.FindMatchingGroups("GameSE")[0];
			}
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04007D95 RID: 32149
		[SerializeField]
		private AudioSource m_AudioSource;
	}
}
