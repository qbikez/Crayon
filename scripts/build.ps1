$projs = Get-ChildItem -path "$PSScriptRoot/.." -Recurse -Filter "project.json"

. "$PSScriptRoot/functions/global.ps1"

$global = parse-globaljson

$projs | % {
    $path = $_.FullName
    $path = split-path -Parent $path
    push-location
    try {
        cd $path
        dnu build
    } 
    finally {
        pop-location
    }
}