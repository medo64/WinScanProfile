#!/bin/bash

SOLUTION_FILE="WinScanProfile.sln"
EXECUTABLE_FILES="WinScanProfile.exe WinScanProfile.pdb"
DIST_FILES="Make.sh CONTRIBUTING.md ICON.png LICENSE.md README.md lib/ src/"

if [ -t 1 ]; then
    ANSI_RESET="$(tput sgr0)"
    ANSI_UNDERLINE="$(tput smul)"
    ANSI_RED="$(tput setaf 1)$(tput bold)"
    ANSI_YELLOW="$(tput setaf 3)$(tput bold)"
    ANSI_CYAN="$(tput setaf 6)$(tput bold)"
    ANSI_WHITE="$(tput setaf 7)$(tput bold)"
fi

while getopts ":h" OPT; do
    case $OPT in
        h)
            echo
            echo    "  SYNOPSIS"
            echo -e "  $(basename "$0") [${ANSI_UNDERLINE}operation${ANSI_RESET}]"
            echo
            echo -e "    ${ANSI_UNDERLINE}operation${ANSI_RESET}"
            echo    "    Operation to perform."
            echo
            echo    "  DESCRIPTION"
            echo    "  Make script compatible with both Windows and Linux."
            echo
            echo    "  SAMPLES"
            echo    "  $(basename "$0")"
            echo    "  $(basename "$0") dist"
            echo
            exit 0
        ;;

        \?) echo "${ANSI_RED}Invalid option: -$OPTARG!${ANSI_RESET}" >&2 ; exit 1 ;;
        :)  echo "${ANSI_RED}Option -$OPTARG requires an argument!${ANSI_RESET}" >&2 ; exit 1 ;;
    esac
done


trap "exit 255" SIGHUP SIGINT SIGQUIT SIGPIPE SIGTERM
trap "echo -n \"$ANSI_RESET\"" EXIT

BASE_DIRECTORY="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"


TOOL_VISUALSTUDIO="/c/Program Files (x86)/Microsoft Visual Studio/2019/Community/Common7/IDE/devenv.exe"
if [[ ! -e "$TOOL_VISUALSTUDIO" ]]; then
    echo "${ANSI_RED}Cannot find Visual Studio!${ANSI_RESET}" >&2
    exit 1
fi


function clean() {
    rm -r "$BASE_DIRECTORY/bin/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/build/" 2>/dev/null
    find "$BASE_DIRECTORY/src/" -name "bin" -type d -exec rm -rf {} \;
    find "$BASE_DIRECTORY/src/" -name "obj" -type d -exec rm -rf {} \;
    return 0
}

function distclean() {
    rm -r "$BASE_DIRECTORY/dist/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/target/" 2>/dev/null
    find "$BASE_DIRECTORY/src/" -name ".vs" -type d -exec rm -rf {} \;
    find "$BASE_DIRECTORY/src/" -name "*.csproj.user" -delete
    return 0
}

function dist() {
    DIST_DIRECTORY="$BASE_DIRECTORY/build/dist/$PACKAGE_ID-$PACKAGE_VERSION"
    DIST_FILE=
    rm -r "$DIST_DIRECTORY/" 2>/dev/null
    mkdir -p "$DIST_DIRECTORY/"
    for DIRECTORY in $DIST_FILES; do
        cp -r "$BASE_DIRECTORY/$DIRECTORY" "$DIST_DIRECTORY/"
    done
    find "$DIST_DIRECTORY/src/" -name ".vs" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_DIRECTORY/src/" -name "bin" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_DIRECTORY/obj/" -name "bin" -type d -exec rm -rf {} \; 2>/dev/null
    tar -cz -C "$BASE_DIRECTORY/build/dist/" \
        --owner=0 --group=0 \
        -f "$DIST_DIRECTORY.tar.gz" \
        "$PACKAGE_ID-$PACKAGE_VERSION/" || return 1
    mkdir -p "$BASE_DIRECTORY/dist/"
    mv "$DIST_DIRECTORY.tar.gz" "$BASE_DIRECTORY/dist/" || return 1
    echo "${ANSI_CYAN}Output at 'dist/$PACKAGE_ID-$PACKAGE_VERSION.tar.gz'${ANSI_RESET}"
    return 0
}

function debug() {
    mkdir -p "$BASE_DIRECTORY/bin/"
    mkdir -p "$BASE_DIRECTORY/build/debug/"
    echo "Compiling (debug)..."
    "$TOOL_VISUALSTUDIO" -build "Debug" "$BASE_DIRECTORY/src/$SOLUTION_FILE" || return 1
    for FILE in $EXECUTABLE_FILES; do
        cp "$BASE_DIRECTORY/build/release/$FILE" "$BASE_DIRECTORY/bin/" || return 1
    done
    echo "${ANSI_CYAN}Output in 'bin/'${ANSI_RESET}"
}

function release() {
    if [[ `shell git status -s 2>/dev/null | wc -l` -gt 0 ]]; then
        echo "${ANSI_YELLOW}Uncommited changes present.${ANSI_RESET}" >&2
    fi
    mkdir -p "$BASE_DIRECTORY/bin/"
    mkdir -p "$BASE_DIRECTORY/build/release/"
    echo "Compiling (release)..."
    "$TOOL_VISUALSTUDIO" -build "Release" "$BASE_DIRECTORY/src/$SOLUTION_FILE" || return 1
    for FILE in $EXECUTABLE_FILES; do
        cp "$BASE_DIRECTORY/build/release/$FILE" "$BASE_DIRECTORY/bin/" || return 1
    done
    echo "${ANSI_CYAN}Output in 'bin/'${ANSI_RESET}"
}


PACKAGE_ID=WinScanProfile
PACKAGE_VERSION=`cat "$BASE_DIRECTORY/src/WinScanProfile/WinScanProfile.csproj" | grep "<Version>" | sed 's^</\?Version>^^g' | xargs`



while [ $# -gt 0 ]; do
    OPERATION="$1"
    case "$OPERATION" in
        all)        clean || break ;;
        clean)      clean || break ;;
        distclean)  clean || distclean || break ;;
        dist)       dist || break ;;
        debug)      debug || break ;;
        release)    release || break ;;

        *)  echo "${ANSI_RED}Unknown operation '$OPERATION'!${ANSI_RESET}" >&2 ; exit 1 ;;
    esac

    shift
done

if [[ "$1" != "" ]]; then
    echo "${ANSI_RED}Error performing '$OPERATION' operation!${ANSI_RESET}" >&2
    exit 1
fi
