. "$PSScriptRoot/functions/global.ps1"

#install dnvm
&{$Branch='dev';iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.ps1'))}

#dnvm use
$global = parse-globaljson

dnu restore

