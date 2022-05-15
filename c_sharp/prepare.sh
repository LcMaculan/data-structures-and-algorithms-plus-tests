#!/bin/bash

mkdir _libs
(cd _libs; curl https://api.nuget.org/downloads/nuget.exe -o nuget.exe)
(cd _libs; mono nuget.exe update -self)
(cd _libs; mono --runtime=v4.0 nuget.exe install nunit.console)
(cd _libs; mono --runtime=v4.0 nuget.exe install NUnit)