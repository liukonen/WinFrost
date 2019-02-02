# WinFrost
Inspired by former projects like Mozilla Prisim, and the current functionality found in Peppermint Linux, This is a Small Site Specific Browser application built on CEFSharp (main branch and CEFSharp Branch) (built on Google Chromium)  or, GeckoFX branch build on the GeckoFX browser (Mozilla's Engine). This gives the user a site specific browser link the user can use for web browsing.

# Compile Files
    Winfrost.sln (must use either x86 or x64 compile options, AnyCpu will not work for either project) 

# How to run
    the program without parameters will rendor a settings form, where you can enter the URL of the file, attempt to auto pull page names and favicons, and generate shortcuts of the application onto your desktop. It also gives settings on global pameters for both the application as well as menu items for whatever browser type (chromium or Gecko) you compiled.
    the program passed in with one command will rendor the browser form dynamicly linked at time of start up.
    

#Ideas / Techmology used
* Managed Extensibility Framework
* Common Interfaces
* Simple http request / response
* configuration management
* nuget

