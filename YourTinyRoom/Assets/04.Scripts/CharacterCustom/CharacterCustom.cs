using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;

public class CharacterCustom : MonoBehaviour
{

	// 캐릭터 스킨
	[SpineSkin] public string baseSkin = "skin-base";


	//배열 쓰기.  here we use arrays of strings to be able to cycle between them easily.
	[SpineSkin] public string[] hairSkins = { "hair_front/hairfront1", "hair_front/hairfront2", "hair_front/hairfront3" };
	public int activeHairIndex = 0;
	[SpineSkin] public string[] eyesSkins = { "pupil/pupil1", "pupil/pupil2 cat", "pupil/pupil3 none" };
	public int activeEyesIndex = 0;
	[SpineSkin] public string[] eyelashSkins = { "eyelash/eyelash high", "eyelash/eyelash normal", "eyelash/eyelash low" };
	public int activeEyelashIndex = 0;
	[SpineSkin] public string[] clothSkins = { "clothes/suit", "clothes/clothes" };
	public int activeClothIndex = 0;

	// 장비 스킨
	public enum ItemType
	{
		Tail,
		HairBack
	}
	[SpineSkin] public string TailSkin = "";
	[SpineSkin] public string HairBackSkin = "";

	[SerializeField]
	SkeletonAnimation skeletonAnimation;
	[SerializeField]
	Skeleton skeleton;
	[SerializeField]
	Skin characterSkin;

	// for repacking the skin to a new atlas texture
	public Material runtimeMaterial;
	public Texture2D runtimeAtlas;

	ColorChange colorChange;
	public MakeColor lightColor;
	public MakeColor darkColor;
	void Awake()
	{
		skeletonAnimation = this.GetComponent<SkeletonAnimation>();
		colorChange = this.GetComponent<ColorChange>();
	}


	public void LoadSkinData(int a, int b, int c, int d, string str, string str2)
    {
		SelectHairSkin(a);
		SelectEyesSkin(b);
		SelectEyelashSkin(c);
		SelectClothSkin(d);

		Equip(str, ItemType.Tail);
		Equip(str2, ItemType.HairBack);
	}


	void Start()
	{
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}
	public void SelectHairSkin(int num)
	{
		activeHairIndex = num;
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

	public void SelectEyesSkin(int num)
	{
		activeEyesIndex = num;
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
	public void SelectEyelashSkin(int num)
	{
		activeEyelashIndex = num;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}
	public void NextEyelashSkin()
	{
		activeEyelashIndex = (activeEyelashIndex + 1) % eyelashSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void PrevEyelashSkin()
	{
		activeEyelashIndex = (activeEyelashIndex + eyelashSkins.Length - 1) % eyelashSkins.Length;
		UpdateCharacterSkin();
		UpdateCombinedSkin();
	}

	public void SelectClothSkin(int num)
	{
		activeClothIndex = num;
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

		switch (itemType)
		{
			case ItemType.Tail:
				TailSkin = itemSkin;
				break;
			case ItemType.HairBack:
				HairBackSkin = itemSkin;
				break;		
			default:
				break;
		}
		UpdateCombinedSkin();
		colorChange.RepeatUpdateColor();
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
		var skeleton = skeletonAnimation.Skeleton;
		var skeletonData = skeleton.Data;
		characterSkin = new Skin("character-base");
		// Note that the result Skin returned by calls to skeletonData.FindSkin()
		// could be cached once in Start() instead of searching for the same skin
		// every time. For demonstration purposes we keep it simple here.
		characterSkin.AddSkin(skeletonData.FindSkin(baseSkin));
		characterSkin.AddSkin(skeletonData.FindSkin(eyesSkins[activeEyesIndex]));
		characterSkin.AddSkin(skeletonData.FindSkin(eyelashSkins[activeEyelashIndex]));
		characterSkin.AddSkin(skeletonData.FindSkin(hairSkins[activeHairIndex]));
		characterSkin.AddSkin(skeletonData.FindSkin(clothSkins[activeClothIndex]));

	}

	void AddEquipmentSkinsTo(Skin combinedSkin)
	{
		var skeleton = skeletonAnimation.Skeleton;
		var skeletonData = skeleton.Data;
		if (!string.IsNullOrEmpty(TailSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(TailSkin));
		if (!string.IsNullOrEmpty(HairBackSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(HairBackSkin));
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
