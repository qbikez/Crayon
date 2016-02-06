function covertto-pshashtable($obj) {
    return $obj.psobject.properties | % { $ht = @{} } { $ht[$_.Name] = $_.Value } { $ht }
}

function find-globaljson($path = $null) {
    if ($path -ne $null) {
        if (Test-Path $path) {
            $i = gi $path
            if (!$i.psiscontainer) {
                return $path
            }
        }
    }
    else {
        $path = "."
    }

    $path = (gi $path).FullName
    do {
        $p = join-path $path "global.json"
        if (test-path $p) { return $p }
        $path = split-path -Parent $path
    } 
    while (![string]::IsNullOrEmpty($path))
}

function parse-globaljson($path = $null) {
    $path = find-globaljson $path

    $global = get-content $path | out-string | ConvertFrom-Json
    
    if ($global.sdk -ne $null) {
        $p = covertto-pshashtable $global.sdk
        & dnvm install @p
        & dnvm use @p
    }

    return $global
}



