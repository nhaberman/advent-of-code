$InputData = Get-Content -Path ".\2023\day-10\input-10.txt"

# create a map based on directions, to make it easier to travel later
$Map = @()
foreach ($inputLine in $InputData) {
    $MapLine = @()
    foreach ($inputCharacter in $inputLine.ToCharArray()) {
        $MapTile = switch ($inputCharacter) {
            '|' { 'NS' }
            '-' { 'EW' }
            'L' { 'NE' }
            'J' { 'NW' }
            '7' { 'SW' }
            'F' { 'SE' }
            'S' { 'S' }
            Default { '' }
        }
        $MapLine += $MapTile
    }
    $Map += , $MapLine
}

# save some useful map attributes
$MapXsize = $Map[0].Length
$MapYsize = $Map.Length

# find coordinates for S
$StartingXCoordinate = 0
$StartingYCoordinate = 0
for ($y = 0; $y -lt $MapYsize; $y++) {
    for ($x = 0; $x -lt $MapXsize; $x++) {
        if ($Map[$y][$x] -eq 'S') {
            $StartingXCoordinate = $x
            $StartingYCoordinate = $y
        }
    }
}
"S coordinates:  ($($StartingXCoordinate),$($StartingYCoordinate))"


# determine the direction for S
$StartDirection = ''
# check the E side
if ($StartingXCoordinate + 1 -lt $MapXsize) {
    $EastValue = $Map[$StartingYCoordinate][$StartingXCoordinate + 1]
    if ($EastValue.Contains('W')) {
        $StartDirection += 'E'
    }
}
# check the W side
if ($StartingXCoordinate - 1 -gt 0) {
    $WestValue = $Map[$StartingYCoordinate][$StartingXCoordinate - 1]
    if ($WestValue.Contains('E')) {
        $StartDirection += 'W'
    }
}
# check the N side
if ($StartingYCoordinate -1 -gt 0) {
    $NorthValue = $Map[$StartingYCoordinate - 1][$StartingXCoordinate]
    if ($NorthValue.Contains('S')) {
        $StartDirection += 'N'
    }
}
# check the S side
if ($StartingYCoordinate + 1 -lt $MapYsize) {
    $SouthValue = $Map[$StartingYCoordinate + 1][$StartingXCoordinate]
    if ($SouthValue.Contains('N')) {
        $StartDirection += 'S'
    }
}

"S direction:  " + $StartDirection

# travel along the map until returning to the starting point, and track the steps
$StepCount = 0
$xCoordinate = $StartingXCoordinate
$yCoordinate = $StartingYCoordinate
$DirectionToTake = $StartDirection[0]
do {
    # follow the next direction
    switch ($DirectionToTake) {
        'N' {
            $NextTile = $Map[--$yCoordinate][$xCoordinate]
            $DirectionToTake = $NextTile.Replace('S', '')
            break
        }
        'E' {
            $NextTile = $Map[$yCoordinate][++$xCoordinate]
            $DirectionToTake = $NextTile.Replace('W', '')
            break
        }
        'S' {
            $NextTile = $Map[++$yCoordinate][$xCoordinate]
            $DirectionToTake = $NextTile.Replace('N', '')
            break
        }
        'W' {
            $NextTile = $Map[$yCoordinate][--$xCoordinate]
            $DirectionToTake = $NextTile.Replace('E', '')
            break
        }
    }

    $StepCount++
} until ($xCoordinate -eq $StartingXCoordinate -and $yCoordinate -eq $StartingYCoordinate)
"Total Step Count is:  " + $StepCount


# the distance to the furthest point is half the step count
$FurthestDistance = $StepCount / 2

"Answer is:  " + $FurthestDistance

# Answers:
# 1360000000+ -> (stopped, wrong input)
# 18023 -> CORRECT

<#
S coordinates:  (10,61)
S direction:  WN
Total Step Count is:  13514
Answer is:  6757
#>