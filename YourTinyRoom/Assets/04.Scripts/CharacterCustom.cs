using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;

public class CharacterCustom : MonoBehaviour
{

	// 캐릭터 스킨
	[SpineSkin] public string suitSkin = "suit";
	[SpineSkin] public string underwearSkin = "underwear";

	//배열 쓰기.  here we use arrays of strings to be able to cycle between them easily.
	[SpineSkin] public string[] hairSkins = { "hair/brown", "hair/grey" };
	public int activeHairIndex = 0;
	[SpineSkin] public string[] eyesSkins = { "eyes/brown", "eyes/grey" };
	public int activeEyesIndex = 0;
	[SpineSkin] public string[] clothSkins = { "suit", "underwear" };
	public int activeClothIndex = 0;

	// 옷
	public enum ItemType
	{
		Cloth,
		Pants
	}
	[SpineSkin] public string clothesSkin = "clothes/suit";
	[SpineSkin] public string pantsSkin = "legs/pants-jeans";

	[SerializeField]
	SkeletonAnimation skeletonAnimation;
	[SerializeField]
	Skeleton skeleton;
	[SerializeField]
	Skin characterSkin;

	// for repacking the skin to a new atlas texture
	public Material runtimeMaterial;
	public Texture2D runtimeAtlas;

	void Awake()
	{
		skeletonAnimation = this.GetComponent<SkeletonAnimation>();
	}

	void Start()
	{
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void NextHairSkin()
	{
		activeHairIndex = (activeHairIndex + 1) % hairSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void PrevHairSkin()
	{
		activeHairIndex = (activeHairIndex + hairSkins.Length - 1) % hairSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void NextEyesSkin()
	{
		activeEyesIndex = (activeEyesIndex + 1) % eyesSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void PrevEyesSkin()
	{
		activeEyesIndex = (activeEyesIndex + eyesSkins.Length - 1) % eyesSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void NextClothSkin()
	{
		activeClothIndex = (activeClothIndex + 1) % clothSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void PrevClothSkin()
	{
		activeClothIndex = (activeClothIndex + clothSkins.Length - 1) % clothSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void Equip(string itemSkin, ItemType itemType)
	{
		
				clothesSkin = itemSkin;
			
		UpdateCombinedSkin();
	}

	public void OptimizeSkin()
	{
		// Create a repacked skin.
		var previousSkin = skeletonAnimation.Skeleton.Skin;
		// Note: materials and textures returned by GetRepackedSkin() behave like 'new Texture2D()' and need to be destroyed
		if (runtimeMaterial)
			Destroy(runtimeMaterial);
		if (runtimeAtlas)
			Destroy(runtimeAtlas);
		Skin repackedSkin = previousSkin.GetRepackedSkin("Repacked skin", skeletonAnimation.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial, out runtimeMaterial, out runtimeAtlas);
		previousSkin.Clear();

		// Use the repacked skin.
		skeletonAnimation.Skeleton.Skin = repackedSkin;
		skeletonAnimation.Skeleton.SetSlotsToSetupPose();
		skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);

		// `GetRepackedSkin()` and each call to `GetRemappedClone()` with parameter `premultiplyAlpha` set to `true`
		// cache necessarily created Texture copies which can be cleared by calling AtlasUtilities.ClearCache().
		// You can optionally clear the textures cache after multiple repack operations.
		// Just be aware that while this cleanup frees up memory, it is also a costly operation
		// and will likely cause a spike in the framerate.
		AtlasUtilities.ClearCache();
		Resources.UnloadUnusedAssets();
	}

	void UpdateCharacterSkin()
	{
		//var skeleton = skeletonAnimation.Skeleton;
		var skeletonData = skeleton.Data;
		characterSkin = new Skin("character-base");
		// Note that the result Skin returned by calls to skeletonData.FindSkin()
		// could be cached once in Start() instead of searching for the same skin
		// every time. For demonstration purposes we keep it simple here.
		characterSkin.AddSkin(skeletonData.FindSkin(suitSkin));
		characterSkin.AddSkin(skeletonData.FindSkin(underwearSkin));
		characterSkin.AddSkin(skeletonData.FindSkin(eyesSkins[activeEyesIndex]));
		characterSkin.AddSkin(skeletonData.FindSkin(hairSkins[activeHairIndex]));
		characterSkin.AddSkin(skeletonData.FindSkin(clothSkins[activeClothIndex]));
	}

	void AddEquipmentSkinsTo(Skin combinedSkin)
	{
		var skeleton = skeletonAnimation.Skeleton;
		var skeletonData = skeleton.Data;
		combinedSkin.AddSkin(skeletonData.FindSkin(clothesSkin));
		combinedSkin.AddSkin(skeletonData.FindSkin(pantsSkin));
	}

	void UpdateCombinedSkin()
	{
		var skeleton = skeletonAnimation.Skeleton;
		var resultCombinedSkin = new Skin("character-combined");

		resultCombinedSkin.AddSkin(characterSkin);
		AddEquipmentSkinsTo(resultCombinedSkin);

		skeleton.SetSkin(resultCombinedSkin);
		skeleton.SetSlotsToSetupPose();
	}
}
