# Note
This depends on updates from 3rd party plugins (engines) and may not have the latest and greatest browsing experience. Meaning there may be zero-days that have not been patched. Use at your own risk!

# WinFrost
Inspired by former projects like Mozilla Prisim, and the current functionality found in Peppermint Linux, This is a Small Site Specific Browser application built on CEFSharp (main branch and CEFSharp Branch) (built on Google Chromium)  This gives the user a site-specific browser link the user can use for web browsing.

# Compile Files
    Winfrost.sln (must use either x86 or x64 compile options, AnyCpu will not work for either project) 

# How to run
    the program without parameters will rendor a settings form, where you can enter the URL of the file, attempt to auto pull page names and favicons, and generate shortcuts of the application onto your desktop. It also gives settings on global pameters for both the application as well as menu items for whatever browser type (chromium or Gecko) you compiled.
    the program passed in with one command will rendor the browser form dynamicly linked at time of start up.
    

# Ideas / Technology used
* Managed Extensibility Framework
* Common Interfaces
* Simple http request / response
* configuration management
* nuget
* parse html file
* GeckFX .net controls
* CEFSharp .net controls

# Screenshots
![settings](https://user-images.githubusercontent.com/28105142/52165916-7c2cb780-26cc-11e9-886c-56c229213255.png)
![form1](https://user-images.githubusercontent.com/28105142/52165913-7b942100-26cc-11e9-967a-da82310c8e82.png)
![form12](https://user-images.githubusercontent.com/28105142/52165914-7b942100-26cc-11e9-8534-f068947103e6.png)
