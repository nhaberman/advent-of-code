$InputData = Get-Content -Path ".\day-2\input-2.txt"

$RedThreshold = 12
$GreenThreshold = 13
$BlueThreshold = 14

$Answer = 0

$InputData | ForEach-Object {
    # get game number
    $GameNumber = [int]($_ -split ':')[0].replace("Game ", "")
    
    # get game results
    $GameResults = ($_ -split ':')[1] -split ';'

    # test each result to see if they pass
    $DidPass = $true
    foreach ($GameResult in $GameResults) {
        # get the cube counts
        if($GameResult -match 'red') {
            [int]$RedCount = (Select-String -InputObject $GameResult -Pattern "(\d+)(?= red)").matches.groups[0].value

            $DidPass = $DidPass -and ($RedCount -le $RedThreshold)
        }

        if($GameResult -match 'green') {
            [int]$GreenCount = (Select-String -InputObject $GameResult -Pattern "(\d+)(?= green)").matches.groups[0].value

            $DidPass = $DidPass -and ($GreenCount -le $GreenThreshold)
        }

        if($GameResult -match 'blue') {
            [int]$BlueCount = (Select-String -InputObject $GameResult -Pattern "(\d+)(?= blue)").matches.groups[0].value

            $DidPass = $DidPass -and ($BlueCount -le $BlueThreshold)
        }
    }

    # if all results passed, save the game number
    if ($DidPass) {
        $Answer += $GameNumber
    }
}

"Answer is:  " + $Answer