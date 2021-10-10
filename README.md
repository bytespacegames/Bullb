# Bullb
Program to turn web apps into windows applications (Similar to Electron) created with Visual Studio C#, and CefSharp, has it's own "Scripting language" using .bullbscript and .bullbform files or by directly Running Code from the js console
# Docs
Documentation for bullbscript
# OpenFile
OpenFile is very straight forward, log OpenFile, and then whatever process name you want, by default it will go from the directory the exe is running in, and you can use slashes to go itno folders from ther,e or you can type the exact directory.
Example: 
```sh
console.log('OpenFile,Program2.exe');
```
This should open Program2.exe in the same directory as your main exe file.
# Browser Control Commands
With these, you can do basic tasks with the brwoser, such as refresh, go back, or forward
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
