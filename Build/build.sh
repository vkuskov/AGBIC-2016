#!/bin/sh

source Build/build_commands.sh

codeGen
build
runTests
deploy