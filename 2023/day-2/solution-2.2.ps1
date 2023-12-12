$InputData = Get-Content -Path ".\day-2\input-2.txt"

$Answer = 0

$InputData | ForEach-Object {
    # get game results
    $GameResults = ($_ -split ':')[1] -split ';'

    # get minimum number of cubes
    $RedCubes = 0
    $GreenCubes = 0
    $BlueCubes = 0
    foreach ($GameResult in $GameResults) {
        # get the cube counts
        if($GameResult -match 'red') {
            [int]$RedCount = (Select-String -InputObject $GameResult -Pattern "(\d+)(?= red)").matches.groups[0].value

            if ($RedCount -gt $RedCubes) {
                $RedCubes = $RedCount
            }
        }

        if($GameResult -match 'green') {
            [int]$GreenCount = (Select-String -InputObject $GameResult -Pattern "(\d+)(?= green)").matches.groups[0].value

            if ($GreenCount -gt $GreenCubes) {
                $GreenCubes = $GreenCount
            }
        }

        if($GameResult -match 'blue') {
            [int]$BlueCount = (Select-String -InputObject $GameResult -Pattern "(\d+)(?= blue)").matches.groups[0].value

            if ($BlueCount -gt $BlueCubes) {
                $BlueCubes = $BlueCount
            }
        }
    }

    # calculate the power
    $Power = $RedCubes * $GreenCubes * $BlueCubes
    $Answer += $Power
}

"Answer is:  " + $Answer