#!/bin/bash

NUNIT_LIB=$(ls _libs | grep NUnit. | head -1)                           # ex. NUnit.3.13.3
NUNIT_CONSOLE_RUNNER_LIB=$(ls _libs | grep NUnit.ConsoleRunner.)        # ex. NUnit.ConsoleRunner.3.15.0

export MONO_PATH=$(PWD)/_libs/$NUNIT_LIB/lib/net40
mcs $1/$1.cs -target:library -out:$1/$1.dll
mcs $1/test.cs -target:library \
  -r:_libs/$NUNIT_LIB/lib/net40/nunit.framework.dll \
  -r:$1/$1.dll \
  -out:file_to_execute.dll
mono --runtime=v4.0 _libs/$NUNIT_CONSOLE_RUNNER_LIB/tools/nunit3-console.exe file_to_execute.dll
rm *.log
rm *.xml
rm $1/$1.dll
rm file_to_execute.dll
