CloudServersSnapshot
================


This is a simple example Console App written in .NET using the Rackspace Openstack.net SDK to create snapshots of all servers that are currently in your Rackspace Cloud Account

Usage
----------

Download via git and change into subdir:
```PoSh
git clone https://github.com/nick-o/CloudServersSnapshot.git
cd CloudServersSnapshot
```
Compile via PowerShell:
```PoSh
#Get directory of .NET Runtime
$NETdir = [System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()
#Add Runtime dir to PATH environment variable
$env:Path += ";" + $NETdir

#Build using msbuild, default Configuration is Debug so specifying Release Configuration
#using the Rebuild target which runs a clean first
MSBuild.exe .\CloudServersSnapshot.sln /t:Rebuild /p:Configuration=Release
```

Ouput directory is bin/Release. Usage per the below:
```PoSh
.\CloudServersSnapshot.exe username api_key [region (US|UK)]
```


NOTE: Region must be one of US|UK. When omitted, default region (US) will be used

This will then take a snapshot of all the servers in your Rackspace Cloud Account and name them using the following pattern: \<Server Name\>_\<current year\>-\<current month\>-\<current day\>
