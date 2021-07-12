# Donkey Kong ASCII
A long time ago in a galaxy far, far away...
I taught an introduction to object oriented programming college class. Since it was an introductory class most of the code we wrote was pretty basic, including a lot of console, non-GUI, non-web apps. I wanted to demonstrate to my students that even with fairly simple code and limited user interfaces it is still possible to write code that does interesting things. So as an example (which eventually got turned into a coding exam) I created a simplified ASCII version of Donkey Kong.

The goal of this activity is to attempt to modify the Donkey Kong ASCII code that was originally written in 2007.

## Level 0
The first step is to get the code copied to your local machine, get it compiling, and make sure that the code still works. 

To copy the code to your local machine you can `git clone` the repository. Or if you don't have git installed (or prefer it for some other reason), Github allows you to download all the code as a zip file that you can then extract on your local machine. Use whichever method you prefer to get the code on your local machine.

The next part of level 0 is to get the code compiling. The code was originally written in Visual Studio 2005, targeting .NET Framework 2.0, both of which are Windows only. That is problematic in our modern, multi-platform world. Some initial work has been done to make the code work with .NET 5 (which is cross platform), but more work probably remains. To get started, [download](https://dotnet.microsoft.com/download) and install .NET 5. If you would like to use an earlier .NET Core version, you can probably modify the code to support that.

The final part of level 0 is to actually run the code. In a terminal, change to the directory where you put your local copy of the code (specifically the directory where the `DonkeyKong.csproj` file is located). In that directory running `dotnet run` should start the app.

Depending on your terminal and which font it uses, it's possible that some of the special characters used may not display correctly. If the player or Donkey Kong don't show up, try changing which characters are used when displaying them. The code to do that is in [Display.cs](Display.cs) on lines [64](Display.cs#L64) and [75](Display.cs#L75). This has been reported as a problem on MacOS.

## Level 1
Now that you've finished level 0, we'll start getting more familiar with the code. This and the remaining levels are much less prescriptive than level 0. Each level has a list of suggested activities to accomplish before moving to the next level. If you want to complete all suggested activities before moving to the next level, that's great. But if you want to move faster that's okay too. If there are other activities that are not in the list of suggested activities that you want to do before moving to the next level, feel free to do so.

The goal of this level is to get more familiar with the application and code, as well as start making the code a little easier to work in. Here are some possible level 1 activities:
* Beat a level or two - Can you beat all the existing levels?
* Make a new level - You can use one of the existing level files as a template, or there is also a blank level file if that's easier to start with.
* Clean up comments - This code was originally written for people who had very little coding experience, so it included a lot of comments to help them. Are those comments useful to you? Are they accurate? Delete any comments you don't find useful.
* Minor refactorings - Parts of the code could probably benefit from some minor refactorings. Use your IDE's built in refactoring tools to do things like rename variables and methods and to extract methods to make the code more to your liking.

## Level 2
Now that you're more familiar with the code the time has come to start making it your own and preparing for more significant changes.

Here are some possible level 2 activities:
* Use newer C# features - This code was written when C# 2 was the new hotness. A lot has changed since then. Update the code to use some newer C# language features. Some possible improvements could be to use autoproperties, private setters on properties, and expression bodies. There are probably other newer language feature that could also make the code more concise and more readable.
* Major refactorings - This could be a great time to do some more significant refactorings. Are there any other methods you'd like to extract that you haven't yet? Is there anywhere you'd like to remove or combine classes? Are there any new classes you'd like to extract from the existing code? Do you need to move files around or restructure the project in any way?
* Add unit tests. This code base currently has no tests. Having even a few automated tests can be a great way to refactor code with more confidence. There are a few classes that will be fairly trivial to add tests to. There are others that will be more challenging. Decide how much automated test converage you feel like you need before you proceeed to the next level.

## Level 3
At this point you should feel fairly comfortable navigating and modifying the code. It's time to start adding new features.

Here are some possible level 3 activities:
* Add new player abilities - Some examples include jumping, falling, or shooting some kind of projectile.
* Add new game elements - Some examples include adding powerups, new types of enemies, or allies.
* Add a score and keep track of the high score.
* Add some other feature that you think the game could benefit from.

## Level 4
At this point you should be familiar with the code and comfortable adding features. Now it's time to go crazy with much more significant changes to the code.

Here are some possible level 4 activities:
* Add procedurally generated levels.
* Convert the game to be web-based.
* Convert the game to use actual graphics (Winforms, WCF, Unity, etc.).
* Make the game multiplayer.
* Make it so the player can modify the map and those changes persist.
