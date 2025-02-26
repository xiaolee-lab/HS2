using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02001047 RID: 4167
	public class ReverbSetting : MonoBehaviour
	{
		// Token: 0x06008C1F RID: 35871 RVA: 0x003AD318 File Offset: 0x003AB718
		public void LoadFromExcelData(ExcelData data)
		{
			if (data == null || data.MaxCell <= 1)
			{
				return;
			}
			List<ReverbSetting.ReverbInfo> list = ListPool<ReverbSetting.ReverbInfo>.Get();
			for (int i = 1; i < data.MaxCell; i++)
			{
				List<string> list2 = data.list[i].list;
				if (!list2.IsNullOrEmpty<string>())
				{
					int num = 0;
					string text = list2.GetElement(num++) ?? string.Empty;
					if (text == "end")
					{
						break;
					}
					float minDistance;
					if (float.TryParse(text, out minDistance))
					{
						string s = list2.GetElement(num++) ?? string.Empty;
						float maxDistance;
						if (float.TryParse(s, out maxDistance))
						{
							string s2 = list2.GetElement(num++) ?? string.Empty;
							int num2;
							if (int.TryParse(s2, out num2))
							{
								if (num2 < 0 || 27 < num2)
								{
									num2 = 1;
								}
								AudioReverbPreset audioReverbPreset = (AudioReverbPreset)num2;
								if (audioReverbPreset != AudioReverbPreset.User)
								{
									ReverbSetting.ReverbInfo item = new ReverbSetting.ReverbInfo
									{
										MinDistance = minDistance,
										MaxDistance = maxDistance,
										ReverbPreset = audioReverbPreset
									};
									list.Add(item);
								}
								else
								{
									string s3 = list2.GetElement(num++) ?? string.Empty;
									int room;
									if (!int.TryParse(s3, out room))
									{
										room = -1000;
									}
									string s4 = list2.GetElement(num++) ?? string.Empty;
									int roomHF;
									if (!int.TryParse(s4, out roomHF))
									{
										roomHF = -100;
									}
									string s5 = list2.GetElement(num++) ?? string.Empty;
									int roomLF;
									if (!int.TryParse(s5, out roomLF))
									{
										roomLF = 0;
									}
									string s6 = list2.GetElement(num++) ?? string.Empty;
									float decayTime;
									if (!float.TryParse(s6, out decayTime))
									{
										decayTime = 1.49f;
									}
									string s7 = list2.GetElement(num++) ?? string.Empty;
									float decayHFRatio;
									if (!float.TryParse(s7, out decayHFRatio))
									{
										decayHFRatio = 0.83f;
									}
									string s8 = list2.GetElement(num++) ?? string.Empty;
									int reflections;
									if (!int.TryParse(s8, out reflections))
									{
										reflections = -2602;
									}
									string s9 = list2.GetElement(num++) ?? string.Empty;
									float reflectionsDelay;
									if (!float.TryParse(s9, out reflectionsDelay))
									{
										reflectionsDelay = 0.007f;
									}
									string s10 = list2.GetElement(num++) ?? string.Empty;
									int reverb;
									if (!int.TryParse(s10, out reverb))
									{
										reverb = 200;
									}
									string s11 = list2.GetElement(num++) ?? string.Empty;
									float reverbDelay;
									if (!float.TryParse(s11, out reverbDelay))
									{
										reverbDelay = 0.011f;
									}
									string s12 = list2.GetElement(num++) ?? string.Empty;
									int hfreference;
									if (!int.TryParse(s12, out hfreference))
									{
										hfreference = 5000;
									}
									string s13 = list2.GetElement(num++) ?? string.Empty;
									int lfreference;
									if (!int.TryParse(s13, out lfreference))
									{
										lfreference = 250;
									}
									string s14 = list2.GetElement(num++) ?? string.Empty;
									float diffusion;
									if (!float.TryParse(s14, out diffusion))
									{
										diffusion = 100f;
									}
									string s15 = list2.GetElement(num++) ?? string.Empty;
									float density;
									if (!float.TryParse(s15, out density))
									{
										density = 100f;
									}
									ReverbSetting.ReverbInfo item2 = new ReverbSetting.ReverbInfo
									{
										MinDistance = minDistance,
										MaxDistance = maxDistance,
										ReverbPreset = audioReverbPreset,
										Room = room,
										RoomHF = roomHF,
										RoomLF = roomLF,
										DecayTime = decayTime,
										DecayHFRatio = decayHFRatio,
										Reflections = reflections,
										ReflectionsDelay = reflectionsDelay,
										Reverb = reverb,
										ReverbDelay = reverbDelay,
										HFReference = hfreference,
										LFReference = lfreference,
										Diffusion = diffusion,
										Density = density
									};
									list.Add(item2);
								}
							}
						}
					}
				}
			}
			List<AudioReverbZone> list3 = ListPool<AudioReverbZone>.Get();
			AudioReverbZone[] componentsInChildren = base.GetComponentsInChildren<AudioReverbZone>(true);
			list3.AddRange(componentsInChildren);
			int num3 = list.Count - list3.Count;
			int count = list3.Count;
			for (int j = 0; j < num3; j++)
			{
				Transform transform = new GameObject(string.Format("Reverb Zone {0:00}", count++)).transform;
				transform.SetParent(base.transform, false);
				list3.Add(transform.GetOrAddComponent<AudioReverbZone>());
			}
			for (int k = 0; k < list.Count; k++)
			{
				AudioReverbZone audioReverbZone = list3[k];
				ReverbSetting.ReverbInfo reverbInfo = list[k];
				audioReverbZone.minDistance = reverbInfo.MinDistance;
				audioReverbZone.maxDistance = reverbInfo.MaxDistance;
				audioReverbZone.reverbPreset = reverbInfo.ReverbPreset;
				if (reverbInfo.ReverbPreset == AudioReverbPreset.User)
				{
					audioReverbZone.room = reverbInfo.Room;
					audioReverbZone.roomHF = reverbInfo.RoomHF;
					audioReverbZone.roomLF = reverbInfo.RoomLF;
					audioReverbZone.decayTime = reverbInfo.DecayTime;
					audioReverbZone.decayHFRatio = reverbInfo.DecayHFRatio;
					audioReverbZone.reflections = reverbInfo.Reflections;
					audioReverbZone.reflectionsDelay = reverbInfo.ReflectionsDelay;
					audioReverbZone.reverb = reverbInfo.Reverb;
					audioReverbZone.reverbDelay = reverbInfo.ReverbDelay;
					audioReverbZone.HFReference = (float)reverbInfo.HFReference;
					audioReverbZone.LFReference = (float)reverbInfo.LFReference;
					audioReverbZone.diffusion = reverbInfo.Diffusion;
					audioReverbZone.density = reverbInfo.Density;
				}
			}
			ListPool<AudioReverbZone>.Release(list3);
			ListPool<ReverbSetting.ReverbInfo>.Release(list);
		}

		// Token: 0x02001048 RID: 4168
		public class ReverbInfo
		{
			// Token: 0x17001E98 RID: 7832
			// (get) Token: 0x06008C21 RID: 35873 RVA: 0x003AD9C9 File Offset: 0x003ABDC9
			// (set) Token: 0x06008C22 RID: 35874 RVA: 0x003AD9D1 File Offset: 0x003ABDD1
			public float MinDistance { get; set; } = 10f;

			// Token: 0x17001E99 RID: 7833
			// (get) Token: 0x06008C23 RID: 35875 RVA: 0x003AD9DA File Offset: 0x003ABDDA
			// (set) Token: 0x06008C24 RID: 35876 RVA: 0x003AD9E2 File Offset: 0x003ABDE2
			public float MaxDistance { get; set; } = 15f;

			// Token: 0x17001E9A RID: 7834
			// (get) Token: 0x06008C25 RID: 35877 RVA: 0x003AD9EB File Offset: 0x003ABDEB
			// (set) Token: 0x06008C26 RID: 35878 RVA: 0x003AD9F3 File Offset: 0x003ABDF3
			public AudioReverbPreset ReverbPreset { get; set; } = AudioReverbPreset.Generic;

			// Token: 0x17001E9B RID: 7835
			// (get) Token: 0x06008C27 RID: 35879 RVA: 0x003AD9FC File Offset: 0x003ABDFC
			// (set) Token: 0x06008C28 RID: 35880 RVA: 0x003ADA04 File Offset: 0x003ABE04
			public int Room { get; set; } = -1000;

			// Token: 0x17001E9C RID: 7836
			// (get) Token: 0x06008C29 RID: 35881 RVA: 0x003ADA0D File Offset: 0x003ABE0D
			// (set) Token: 0x06008C2A RID: 35882 RVA: 0x003ADA15 File Offset: 0x003ABE15
			public int RoomHF { get; set; } = -100;

			// Token: 0x17001E9D RID: 7837
			// (get) Token: 0x06008C2B RID: 35883 RVA: 0x003ADA1E File Offset: 0x003ABE1E
			// (set) Token: 0x06008C2C RID: 35884 RVA: 0x003ADA26 File Offset: 0x003ABE26
			public int RoomLF { get; set; }

			// Token: 0x17001E9E RID: 7838
			// (get) Token: 0x06008C2D RID: 35885 RVA: 0x003ADA2F File Offset: 0x003ABE2F
			// (set) Token: 0x06008C2E RID: 35886 RVA: 0x003ADA37 File Offset: 0x003ABE37
			public float DecayTime { get; set; } = 1.49f;

			// Token: 0x17001E9F RID: 7839
			// (get) Token: 0x06008C2F RID: 35887 RVA: 0x003ADA40 File Offset: 0x003ABE40
			// (set) Token: 0x06008C30 RID: 35888 RVA: 0x003ADA48 File Offset: 0x003ABE48
			public float DecayHFRatio { get; set; } = 0.83f;

			// Token: 0x17001EA0 RID: 7840
			// (get) Token: 0x06008C31 RID: 35889 RVA: 0x003ADA51 File Offset: 0x003ABE51
			// (set) Token: 0x06008C32 RID: 35890 RVA: 0x003ADA59 File Offset: 0x003ABE59
			public int Reflections { get; set; } = -2602;

			// Token: 0x17001EA1 RID: 7841
			// (get) Token: 0x06008C33 RID: 35891 RVA: 0x003ADA62 File Offset: 0x003ABE62
			// (set) Token: 0x06008C34 RID: 35892 RVA: 0x003ADA6A File Offset: 0x003ABE6A
			public float ReflectionsDelay { get; set; } = 0.007f;

			// Token: 0x17001EA2 RID: 7842
			// (get) Token: 0x06008C35 RID: 35893 RVA: 0x003ADA73 File Offset: 0x003ABE73
			// (set) Token: 0x06008C36 RID: 35894 RVA: 0x003ADA7B File Offset: 0x003ABE7B
			public int Reverb { get; set; } = 200;

			// Token: 0x17001EA3 RID: 7843
			// (get) Token: 0x06008C37 RID: 35895 RVA: 0x003ADA84 File Offset: 0x003ABE84
			// (set) Token: 0x06008C38 RID: 35896 RVA: 0x003ADA8C File Offset: 0x003ABE8C
			public float ReverbDelay { get; set; } = 0.011f;

			// Token: 0x17001EA4 RID: 7844
			// (get) Token: 0x06008C39 RID: 35897 RVA: 0x003ADA95 File Offset: 0x003ABE95
			// (set) Token: 0x06008C3A RID: 35898 RVA: 0x003ADA9D File Offset: 0x003ABE9D
			public int HFReference { get; set; } = 5000;

			// Token: 0x17001EA5 RID: 7845
			// (get) Token: 0x06008C3B RID: 35899 RVA: 0x003ADAA6 File Offset: 0x003ABEA6
			// (set) Token: 0x06008C3C RID: 35900 RVA: 0x003ADAAE File Offset: 0x003ABEAE
			public int LFReference { get; set; } = 250;

			// Token: 0x17001EA6 RID: 7846
			// (get) Token: 0x06008C3D RID: 35901 RVA: 0x003ADAB7 File Offset: 0x003ABEB7
			// (set) Token: 0x06008C3E RID: 35902 RVA: 0x003ADABF File Offset: 0x003ABEBF
			public float Diffusion { get; set; } = 100f;

			// Token: 0x17001EA7 RID: 7847
			// (get) Token: 0x06008C3F RID: 35903 RVA: 0x003ADAC8 File Offset: 0x003ABEC8
			// (set) Token: 0x06008C40 RID: 35904 RVA: 0x003ADAD0 File Offset: 0x003ABED0
			public float Density { get; set; } = 100f;
		}
	}
}
