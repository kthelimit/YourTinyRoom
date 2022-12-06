using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class CharacterCustomButton : MonoBehaviour
{


		public SkeletonDataAsset skeletonDataAsset;
		public CharacterCustom skinsSystem;

		[SpineSkin(dataField: "skeletonDataAsset")] public string itemSkin;
		public CharacterCustom.ItemType itemType;

		void Start()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(
				delegate { skinsSystem.Equip(itemSkin, itemType); }
			);
		}
	
}
