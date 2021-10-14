# Bullb
Program to turn web apps into windows applications (Similar to Electron) created with Visual Studio C#, and CefSharp, has it's own "Scripting language" using .bullbscript and .bullbform files or by directly Running Code from the js console
# Download Bullb
Everything you need to get started apart from the web files is located in your Resources Folder.  
  
Builds:  
1.0: [Download](http://fumacrom.com/2SlBD)
# Settings
Your Settings.bulbsettings file is located in your resources folder, this has to be named Settings.bulbsettings, it also must use every line in order.  
  
Line 1: The name of the html file you want your program to boot on  
Line 2: The window title your bullb window will start with (If you have SiteTitleChanges set to true, this is practically useless.)  
Line 3: The favicon your window will boot with  
Line 4: If you want to save cache, cookies, and histories or not  
Line 5: The location you want the cache to store at relative to your local appdata  
Line 6: This is SiteTitleChanges, if you have htis to true, upon loading a new page it will update the window title to the html file's title  
Line 7 / 8: The height and width respectivelly your window will boot with  
Line 9: If or if not your app users will be able to right click and view the context menu  
Line 10: If or if not you want to prevent multiple instances of the application from running at once (V1.1 and above)
Line 11: If the line above is true, the location of the bullbForm (relative to the folder your exe is in by default) you want to run if you try to run another instance, if you don't want this, you can set this to anything else and it won't do anything, by default it's set to "none" (V1.1 and above)
# Bullb Script Files
There are two types of Bullbscript files, .bullbform, and .bullbscript,  bullbform uses the "scripting language" to easily program basic "Form Applications" to run. bullbscript files just run normal bullbscript in bunches so you dont have to log each individual one. If you try to run a bullbscript file, it will automatically detect if it is a bullbform or bullbscript based on the file name.  
  
Here's how to create a bullbform:
Setting a FormType:
Currently, there's only one formtype, and that is the messagebox,
so in your bullbform file you will need:
```sh
FormType,MessageBox
```
ParameterCount is how many paramaters you want in your form, for a MEssageBox the current maximum is two. If you have one, the message box will only display text, otherwise, it will also have a title.  
Param<number>,
This just lets you choose what you want for each paramater, starting at one, the name you will want to put down for the paramater is just Param and then which paramater, for example
```sh
Param1,Hello World!
```
If you log RunBullbScript for this new form, it should show a message box that says 'Hello World!'
# Docs
Documentation for bullbscript
# The Following Commands were introduced in Version 1.0
# OpenFile
OpenFile is very straight forward, log OpenFile, and then whatever process name you want, by default it will go from the directory the exe is running in, and you can use slashes to go into folders from there, or you can type the exact directory.  
Example: 
```sh
console.log('OpenFile,Program2.exe');
```
This should open Program2.exe in the same directory as your main exe file.
# Browser Control Commands
With these, you can do basic tasks with the browser, such as refresh, go back, or forward  
Example: Refreshes The Current Page  
```sh
console.log('BulbRefresh;');
```
Example: Goes Back one page
```sh
console.log('BulbBack;');
```
Example: Goes Forward one page
```sh
console.log('BulbForward;');
```
# CloseBulbApp
This commands closes the application
```sh
console.log('CloseBulbApp;');
```
# CloseProcessByName
Shuts down any windows process given it's name (Does not include file type such as .exe  
Example:
```sh
console.log('CloseProcessByName,Audacity');
```
Closes Audacity
# ChangeBulbFormTitle
This commands changes the form title, however if you have SiteTitleChanges enabled in your .bulbsettings file, it will reset when a new page is loaded
```sh
console.log('ChangeBulbFormTitle,New Title!!');
```
# Bulb Form Icon Commands
This command changes the form icon to a file located in your resources folder  
Example:
```sh
console.log('ChangeBulbFormIconFromResources,favicon2.ico');
```
This command changes the form icon to a file in a general location  
Example:
```sh
console.log('ChangeBulbFormIconByLoc,C:/favicon.ico');
```
Should change the form icon to a file named 'favicon' in your C drive
# SetResizable
This command will set if your program is able to be resized, if this is false, the maximize button will be disable and you wont be able to resize the program on the edges with your Cursor  
Example:
```sh
console.log('SetResizable,false');
```
Sets the program to not be resizable
# Show Minimize/Maximize boxes
Allows you to individually change if  the minimize and maximize boxes are enable or disabled, doesn't effect resizing  the window normally.  
Example:
```sh
console.log('ShowMinimizeBox,false');
```
Hides the minimize box
# RunBatchCmdAsAdmin
Runs a single batch command as an administrator
```sh
console.log('RunBatchCmdAsAdmin,echo Hello World');
```
# RunBullbScript
In v1.0, this only worked for Bullb Forms, as BullbScript FILES werent implemented yet. However this runs bullb script from a file, which for normal bullb script is useful for executing a bunch of scripts at once  
Example:
```sh
console.log('RunBullbScript,/Resources/form.bullbform');
```
# The Following Commands were introduced in Version 1.1
# ShowTitleBar
This command shows and hides the titlebar that cotnains the icon, title, and minimize, maximize, and close buttons  
Example:  
```sh
console.log('ShowTitleBar,false');
```
# Minimize / Maximize Bulb Apps
These commands allow you to maximize and minimize the application
Example:  
```sh
console.log('MaximizeBulbApp;');
//If you want to minimize, just changed Maximize to Minimize in the command
```
# SetWindowLocationX/Y
These commands allow you to change the location of the window on your screen
Example:  
```sh
console.log('SetWindowLocationX,69');
console.log('SetWindowLocationX,420');
```
# LoadUAPreset
Switches the UA based off of our Int to UA conversion.
Example:  
```sh
console.log('LoadUAPreset,2');
//Sets the ua to mobile safari
```
# LoadSpecifiedUAString
Switches the UA for people who want to do thigns such as more specific uas to control the OS, and version of the User Agent
Example:  
```sh
console.log('LoadSpecifiedUAString,Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36 OPR/76.0.4017.94 (Edition utorrent)');
```
