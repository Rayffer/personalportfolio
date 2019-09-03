# Soundscape manager

This project intends to give the user the capability of defining different soundscapes to recreate the ambience of a storm, a beach, a corridor or any other soundscape one can imagine.

## Sound Effects
The backbone of the application, a sound effect is an audio file that plays a sound of a fireplace, a conversation, a wind rustling leaves, anything, it is up to the user to provide the sound effects he or she wants to use.

The supportes audio formats are .mp3, .wav and .ogg (this one converts the files to mp3 and deletes the ogg file, so be careful about that).

The sound effects are displayed as a control in the application, which allows the user to define the volume, if the sound effect loops and the repetition interval of said sound. 

The volume can be controlled using the horizontal trackbar displayed in the control, each sound effect is controlled independently.
To enable looping the sound effect once it's done reproducing, tick the checkbox besides the repetition interval combobox.
To define the repetition interval, select a value from the combobox below the volume trackbar.

The user can search for a specific sound by its name, to do so, write the name or a part of it of the desired sound effect in the text to the right of the "Search a sound:" label.
The filter can be cleared by using the "Clear filter" button.

Once the desired parameters have been set, click on the arrow button to begin playing the sound effect.
To stop playing the sound effect, click on the square button that has appeared.
If looping is enabled, the stop button will be disabled and the only way to stop playing the sound effect is to untick the loop checkbox.

If many sound are reproducing at the same time or the user can't find a sound that is currently reproducing, clicking the "Stop sounds" button will stop the reproduction of all sounds.

If the sound effects are saved in a directory in the computer other than the default one, the user can navigate to that directory by using the "Browse" button beside the label where the directory is displayed

## Sound collections
A sound collection is a group of sounds that the user can define to ease playing a desired sound effect on the fly.
Selecting a sound collection will filter the displayed sound effects to show only those of the collection selected.

A sound effect can be included in a sound collection by double clicking on its name, doing so will change the color of the control's border to a red color.
Once the desired sound effects have been selected, the sound collection can be saved by clicking on the button "Save current sound colletion", a proper name should be given to the collection.

To select a sound collection, select a value of the combobox below the "Sound collections" label:

If the sound collections are saved in a directory in the computer other than the default one, the user can navigate to that directory by using the "Browse" button beside the label where the directory is displayed

## Soundscapes
A soundscape is a collection of sounds that when played, mimic the ambience of a fireplace inside a tavern or the mistery and dangers of a jungle at night for example.

When saving a soundscape, only the sounds which are currently playing will be saved with their current configuration.
Selecting a soundscape will inmediately load the setting to any sound effect that matches the name of those stored in the soundscape (if a sound is not present, the setting in the soundscape will be ignored), and start playing them.

If the soundscapes are saved in a directory in the computer other than the default one, the user can navigate to that directory by using the "Browse" button beside the label where the directory is displayed

### Default paths
The application has some default paths present in its app.config file, listed in the following:

* **AmbientSoundsLocation:** This setting controls the default directory in which to search sounds.
* **SoundscapesLocation:** This setting controls the default directory in which soundscapes are stored and loaded from.
* **SoundscapeCollectionLocation:** This setting controls the default directory in which sound collections are stored and loaded from.
* **SupportedAudioExtensions:** This setting controls which audio files the application searchs to try and reproduce, any audio formats other than the specified will be ignored and currently only mp3, wav and ogg files have been tested and are the only ones that should work.
