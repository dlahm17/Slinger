!! IMPORTANT !! Upgrading from 1.31 to 1.4:
I changed the way I'm using the substances, they are now a separate package and you can extract it and do different export if you want to. I generated default textures already, this is to avoid the various bug and upgrade issues with the substance plugin.
This also mean i had to make new materials, so if you upgrade from a version earlier than 1.4 you may lose some material links, I HIGHLY SUGGEST YOU MAKE A COPY OF YOUR PROJECT BEFORE THE UPGRADE.


At the date of my submission Unity does not allow to include standard assets in the package. 
In order to correctly view the demo scenes you will need to have some of the standard assets, those steps are not necessary if you just want to start using the assets.

1. Download the "Standard Assets for Unity 4.6" and import the following files/folders (NOTE: you can skip this step if you want to use your own day/night skybox, you will have to set it up correctly inside the "lighting" window):
	-Standard Assets\Environment\Skyboxes\Materials\SkyMoonShine.mat
	-Standard Assets\Environment\Skyboxes\Materials\SkySunny02.mat
	-Standard Assets\Environment\Skyboxes\Textures\MoonShine
	-Standard Assets\Environment\Skyboxes\Textures\Sunny02

2. Download the "Standard Assets" and import the following folders:
	-Standard Assets\Prototyping

3. Download and import the "Post Processing Stack" package.

All those package are available on the store inside "Unity Essentials".


4. Create a new "VolumetricLights" folder inside "Assets". Download "VolumetricLights" package from https://github.com/SlightlyMad/VolumetricLights
Drag and drop the following folders into your new "VolumetricLights" folder:
	-Resources
	-Scripts
	-Shaders
	-Textures

5. Go into "File" > "Build Settings..." > "Player Settings" and set "Color Space to "Linear".

6. Go to "Edit" > "Project Settings" > "Graphics" and change the rendering path to Deferred for each Tier.


IMPORTANT: close and reopen the project it should fixe some of the missing script and also reset some values correctly.

Finally build the lightmaps and you should be able to play the scenes with the "flycam".




EXTRA IMPORTANT: For the trees to show up correctly go to "Edit" > "Project Settings" > "Graphics": 
 1. change the Built_in shader of "Deferred" to "Custom shader" and pick "CTI_Internal-DeferredShading"
 2. change the Built_in shader of "Deferred Reflections" to "Custom shader" and pick "CTI_Internal-DeferredReflections"


Sorry for the inconveninence with the demo scenes, i would have prefered something ready to test as soon as you import but there is no other choices for the moment :(



Changelog:

v1.4
-added compatibility with Unity 2018
-substances are now a subpackage and all of them have been exported in 4k as tga textures
-removed dependency to Water4
-added a simple stylized water shader for demo scenes
-added stone_wall_3x3_04
-added roof_metal_deco_arrow_01, roof_metal_deco_arrow_02, roof_metal_deco_arrow_03, roof_metal_deco_arrow_04, roof_metal_deco_arrow_05
-added roof_metal_deco_bar_01, roof_metal_deco_bar_02, roof_metal_deco_bar_03, roof_metal_deco_bar_04, roof_metal_deco_bar_05, roof_metal_deco_bar_06
-added new shaders with POM and one with heightblend
-updated demo scenes, with the new assets and materials
-changed the demo scenes floors, making it with probuilder and vertex painting using the new shader with height blend
-Updated README


v1.31
-fixed UVs on "stone_wall_corner_3x3_02"
-updated for Unity 2017
-added step 5 and 6 in README
-added IMPORTANT notes in README

v1.3
-added pine trees with lods compatible with unity terrain and wind
-added forge chimney
-added a forge house prefab
-updated demo2 with some trees
-updated "prefabs list" scene with the new models
-added volumetric lighting to demo1 and demo2
-fixed wall_3x3_04 uv
-updated metal texture with a bit more details to it
-adjusted some roughtness values in stone textures
-added namespace for scripts


v1.2
-added modular well
-added bucket model
-added new circular wall type for towers
-added new large bended wood for towers
-added new roof for towers
-added new options in substances
-optimized a few models (polycount)
-fix some lighting issues in demo1 scenes and added some ivy
-added new tower example prefab
-removed holes on top of some roof
-update demo2 with animated house creation

v1.1
-fixed lod warnings
-added wind particle effect
-added another metal piece
-added 2 new "stone arch" models
-added new house prefab
-added new building blocks
-separated the window glasses from the frames

v1.0
-initial release