function Invoke-NorthSpinCycle ([ref]$Platform) {

    # get some useful attributes
    $PlatformLength = $Platform.Value.Count
    $PlatformWidth = $Platform.Value[0].Length
        
    # loop through each column starting from the bottom and roll rocks north
    for ($col = 0; $col -lt $PlatformWidth; $col++) {
        $RollingRocks = 0

        for ($row = $PlatformLength - 1; $row -ge 0; $row--) {
            # take action depending on what is in the space
            $Rock = $Platform.Value[$row][$col]

            switch ($Rock) {
                'O' { 
                    # rounded rock, track the rock and clear the space
                    $RollingRocks++
                    $Platform.Value[$row] = $Platform.Value[$row].Remove($col, 1).Insert($col, '.')
                    break
                }
                '#' { 
                    # cube-shaped rock, unload any accumulated rocks below it
                    for ($i = 0; $i -lt $RollingRocks; $i++) {
                        $Platform.Value[$row + 1 + $i] = $Platform.Value[$row + 1 + $i].Remove($col, 1).Insert($col, 'O')
                    }

                    # clear the accumulated rock counter
                    $RollingRocks = 0
                    break
                }
                '.' { 
                    # empty space, do nothing
                    break
                }
                Default {
                    break
                }
            }
        }

        # if any remaining rocks, unload them at the beginning of the column
        for ($i = 0; $i -lt $RollingRocks; $i++) {
            $Platform.Value[$i] = $Platform.Value[$i].Remove($col, 1).Insert($col, 'O')
        }
    }
}

function Invoke-SouthSpinCycle ([ref]$Platform) {

    # get some useful attributes
    $PlatformLength = $Platform.Value.Count
    $PlatformWidth = $Platform.Value[0].Length
        
    # loop through each column starting from the top and roll rocks south
    for ($col = 0; $col -lt $PlatformWidth; $col++) {
        $RollingRocks = 0

        for ($row = 0; $row -lt $PlatformLength; $row++) {
            # take action depending on what is in the space
            $Rock = $Platform.Value[$row][$col]

            switch ($Rock) {
                'O' { 
                    # rounded rock, track the rock and clear the space
                    $RollingRocks++
                    $Platform.Value[$row] = $Platform.Value[$row].Remove($col, 1).Insert($col, '.')
                    break
                }
                '#' { 
                    # cube-shaped rock, unload any accumulated rocks above it
                    for ($i = 0; $i -lt $RollingRocks; $i++) {
                        $Platform.Value[$row - 1 - $i] = $Platform.Value[$row - 1 - $i].Remove($col, 1).Insert($col, 'O')
                    }

                    # clear the accumulated rock counter
                    $RollingRocks = 0
                    break
                }
                '.' { 
                    # empty space, do nothing
                    break
                }
                Default {
                    break
                }
            }
        }

        # if any remaining rocks, unload them at the end of the column
        for ($i = 0; $i -lt $RollingRocks; $i++) {
            $Platform.Value[$PlatformLength - 1 - $i] = $Platform.Value[$PlatformLength - 1 - $i].Remove($col, 1).Insert($col, 'O')
        }
    }
}

function Invoke-EastSpinCycle ([ref]$Platform) {

    # get some useful attributes
    $PlatformLength = $Platform.Value.Count
    $PlatformWidth = $Platform.Value[0].Length
        
    # loop through each row starting from the left and roll rocks east
    for ($row = 0; $row -lt $PlatformLength; $row++) {
        $RollingRocks = 0

        for ($col = 0; $col -lt $PlatformWidth; $col++) {
            # take action depending on what is in the space
            $Rock = $Platform.Value[$row][$col]

            switch ($Rock) {
                'O' { 
                    # rounded rock, track the rock and clear the space
                    $RollingRocks++
                    $Platform.Value[$row] = $Platform.Value[$row].Remove($col, 1).Insert($col, '.')
                    break
                }
                '#' { 
                    # cube-shaped rock, unload any accumulated rocks to the left of it
                    for ($i = 0; $i -lt $RollingRocks; $i++) {
                        $Platform.Value[$row] = $Platform.Value[$row].Remove($col - 1 - $i, 1).Insert($col - 1 - $i, 'O')
                    }

                    # clear the accumulated rock counter
                    $RollingRocks = 0
                    break
                }
                '.' { 
                    # empty space, do nothing
                    break
                }
                Default {
                    break
                }
            }
        }

        # if any remaining rocks, unload them at the end of the row
        for ($i = 0; $i -lt $RollingRocks; $i++) {
            $Platform.Value[$row] = $Platform.Value[$row].Remove($PlatformWidth - 1 - $i, 1).Insert($PlatformWidth - 1 - $i, 'O')
        }
    }
}

function Invoke-WestSpinCycle ([ref]$Platform) {

    # get some useful attributes
    $PlatformLength = $Platform.Value.Count
    $PlatformWidth = $Platform.Value[0].Length
        
    # loop through each row starting from the right and roll rocks west
    for ($row = 0; $row -lt $PlatformLength; $row++) {
        $RollingRocks = 0

        for ($col = $PlatformWidth - 1; $col -ge 0; $col--) {
            # take action depending on what is in the space
            $Rock = $Platform.Value[$row][$col]

            switch ($Rock) {
                'O' { 
                    # rounded rock, track the rock and clear the space
                    $RollingRocks++
                    $Platform.Value[$row] = $Platform.Value[$row].Remove($col, 1).Insert($col, '.')
                    break
                }
                '#' { 
                    # cube-shaped rock, unload any accumulated rocks to the right of it
                    for ($i = 0; $i -lt $RollingRocks; $i++) {
                        $Platform.Value[$row] = $Platform.Value[$row].Remove($col + 1 + $i, 1).Insert($col + 1 + $i, 'O')
                    }

                    # clear the accumulated rock counter
                    $RollingRocks = 0
                    break
                }
                '.' { 
                    # empty space, do nothing
                    break
                }
                Default {
                    break
                }
            }
        }

        # if any remaining rocks, unload them at the beginning of the row
        for ($i = 0; $i -lt $RollingRocks; $i++) {
            $Platform.Value[$row] = $Platform.Value[$row].Remove($i, 1).Insert($i, 'O')
        }
    }
}

function Test-PlatformsIfEqual ($Platforms, $PlatformToTest) {
    # test the platform against all other platforms in the history
    for ($i = 0; $i -lt $Platforms.Count; $i++) {
        if (Test-PlatformIfEqual -Platform1 $PlatformToTest -Platform2 $Platforms[$i]) {
            # return the index of the matching platform
            return $i
        }
    }

    # if no matches found, return dummy results
    return -1
}

function Test-PlatformIfEqual ($Platform1, $Platform2) {
    # test rocks at each position
    for ($i = 0; $i -lt $Platform1.Count; $i++) {
        for ($j = 0; $j -lt $Platform1[$i].Length; $j++) {
            if ($Platform1[$i][$j] -ne $Platform2[$i][$j]) {
                return $false
            }
        }
    }

    # if all positions matched, then return true
    return $true
}