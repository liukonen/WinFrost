# Note
Due to the recent bug found in Chrome, CVE-2019-5786, I recomend not using the CEF or Master branch until CEFSharp can get its latest version up to at least the current verison. I will not be providing updates on the Nuget packages in my programs reliably, so it is up to you to keep your applications up to date.
https://www.forbes.com/sites/daveywinder/2019/03/07/google-confirms-serious-chrome-security-problem-heres-how-to-fix-it/#7a5fb7e72002

# WinFrost
Inspired by former projects like Mozilla Prisim, and the current functionality found in Peppermint Linux, This is a Small Site Specific Browser application built on CEFSharp (main branch and CEFSharp Branch) (built on Google Chromium)  or, GeckoFX branch build on the GeckoFX browser (Mozilla's Engine). This gives the user a site specific browser link the user can use for web browsing.

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
