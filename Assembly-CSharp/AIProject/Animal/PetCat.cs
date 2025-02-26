using System;
using System.Collections.Generic;
using AIProject.SaveData;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000BAF RID: 2991
	public class PetCat : WalkingPetAnimal
	{
		// Token: 0x060059D2 RID: 22994 RVA: 0x00267CA0 File Offset: 0x002660A0
		protected override void Initialize()
		{
			base.Initialize();
			if (this.bodyObject == null)
			{
				return;
			}
			GameObject gameObject = this.bodyObject.transform.FindLoop(this._materialTargetObjectName);
			if (gameObject == null)
			{
				return;
			}
			Renderer component = gameObject.GetComponent<Renderer>();
			if (component == null)
			{
				return;
			}
			Material material = component.material;
			if (material == null)
			{
				return;
			}
			if (!material.HasProperty(this._textureKeyName))
			{
				return;
			}
			AnimalData animalData = base.AnimalData;
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>>> textureTable = Singleton<Manager.Resources>.Instance.AnimalTable.TextureTable;
			if (textureTable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>>>())
			{
				return;
			}
			int animalTypeID = base.AnimalTypeID;
			Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>> dictionary;
			if (!textureTable.TryGetValue(animalTypeID, out dictionary) || dictionary.IsNullOrEmpty<int, UnityEx.ValueTuple<Texture2D, Color[]>>())
			{
				return;
			}
			if (animalData.First || !dictionary.ContainsKey(animalData.TextureID))
			{
				List<int> list = ListPool<int>.Get();
				list.AddRange(dictionary.Keys);
				animalData.TextureID = list.Rand<int>();
			}
			UnityEx.ValueTuple<Texture2D, Color[]> valueTuple = dictionary[animalData.TextureID];
			material.SetTexture(this._textureKeyName, valueTuple.Item1);
			Color[] item = valueTuple.Item2;
			if (item.IsNullOrEmpty<Color>() || this._colorKeyNames.IsNullOrEmpty<string>())
			{
				return;
			}
			int num = 0;
			while (num < item.Length && num < this._colorKeyNames.Length)
			{
				string name = this._colorKeyNames[num];
				if (material.HasProperty(name))
				{
					material.SetColor(name, item[num]);
				}
				num++;
			}
		}

		// Token: 0x04005221 RID: 21025
		[SerializeField]
		private string _materialTargetObjectName = string.Empty;

		// Token: 0x04005222 RID: 21026
		[SerializeField]
		private string _textureKeyName = string.Empty;

		// Token: 0x04005223 RID: 21027
		[SerializeField]
		private string[] _colorKeyNames = new string[4];
	}
}
