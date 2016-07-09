#!/bin/sh
xbuild SeshFT.Gameplay.Test/SeshFT.Gameplay.Test.csproj /property:Configuration=Debug /verbosity:minimal
if [ $? = 0 ]
then
	mono Deps/NSpec/NSpecRunner.exe SeshFT.Gameplay.Test/bin/Debug/SeshFT.Gameplay.Test.dll
else
	echo "ERROR: Could not compile!"
	exit 1
fi