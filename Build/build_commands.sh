#!/bin/sh

TARGET="Sesh-FT/Assets"

codeGen() {
    echo "Codegen..."

    xbuild /property:Configuration=Debug SeshFT.CodeGen/SeshFT.CodeGen.csproj /verbosity:minimal
    mono SeshFT.CodeGen/bin/Debug/SeshFT.CodeGen.exe

    echo "Codegen finished!"
}

build() {
    echo "Building..."

    xbuild /target:Clean /property:Configuration=Release AGBIC-2016.sln /verbosity:minimal 
    xbuild /property:Configuration=Release AGBIC-2016.sln /verbosity:minimal

    echo "Build finished"
}

update() {
    xbuild /property:Configuration=Release AGBIC-2016.sln /verbosity:minimal
}

runTests() {
    mono Deps/NSpec/NSpecRunner.exe SeshFT.Gameplay.Test/bin/Release/SeshFT.Gameplay.Test.dll
}

deploy() {
    rm -rf "$TARGET/Deps"
    mkdir "$TARGET/Deps"
    cp SeshFT.Unity/bin/Release/*.dll "$TARGET/Deps"
}