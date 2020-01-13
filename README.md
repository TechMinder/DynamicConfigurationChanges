# DynamicConfigurationChanges
ASP.Net Core IOptions/IOptionsMonitor/IOptionsSnapshot example
#
There are many ways to update the configuration while application running (ASP.NET core). It allows you to update the settings configured in AppSetting.json file. However, the old settings do not to be updated on the disk.
That means the settings will be reloaded upon the Application restart.

# Push Configuration Through HTTP
You can push the via Post method. Make sure you have security around that if you are going to use this approach. Sometimes you want to replace
the settings which are defined in the file (AppSettings.Json). Therefore, the push method is ideal for on-demand configuration updates.

# Retrieve Configuration With Background Task
You may want to start your application, however, you want to retrieve settings from the external sources. You can utilize ASP.NET HostedService for running the background task.
You want to make sure to cancel the task once the settings are loaded in the memory.

# How Should I Handle Updated Settings inside My Objects(Singleton/Scoped)?
ASP.NET core has various mechanisms to handle the updates. The most common approach is IOptions, IOptionsMonitor, and IOptionsSnapshot. 
Note: only IOptionMonitor works with Singleton objects. You will see runtime exception when trying to access IOptionsSnapshot inside the singleton class.

I have implemented all the above  in the sample code. My goal was to share the idea through code. This is not a production-ready code and I cannot guarantee that this would work in all possible environments.
So, I would recommend copying the idea instead of the code.
