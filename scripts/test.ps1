. "$PSScriptRoot/functions/global.ps1"

$global = parse-globaljson

push-location
try {
    cd "$psscriptroot/../Crayons.test"
    dnx test
} finally {
    pop-location
}