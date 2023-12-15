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
:startingSearch for ($y = 0; $y -lt $MapYsize; $y++) {
    for ($x = 0; $x -lt $MapXsize; $x++) {
        if ($Map[$y][$x] -eq 'S') {
            $StartingXCoordinate = $x
            $StartingYCoordinate = $y
            break startingSearch
        }
    }
}
"S coordinates:  ($($StartingXCoordinate),$($StartingYCoordinate))"

# determine the direction for S
$StartDirection = ''
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
"S direction:  " + $StartDirection

# build an array mirroring the map, to track which tiles are on the path
$TileIsPathMap = @()
for ($i = 0; $i -lt $MapYsize; $i++) {
    $newArray = @()
    for ($j = 0; $j -lt $MapXsize; $j++) {
        # for now just set everything to false, path tiles will be toggled true later
        $newArray += $false
    }
    $TileIsPathMap += , $newArray
}

# travel along the map until returning to the starting point, and track the path
$StepCount = 0
$xCoordinate = $StartingXCoordinate
$yCoordinate = $StartingYCoordinate
$DirectionToTake = $StartDirection[0]
do {
    # follow the next direction
    switch ($DirectionToTake) {
        'N' {
            # move to the next tile
            $NextTile = $Map[--$yCoordinate][$xCoordinate]
            # remove the entry direction to determine the next direction
            $DirectionToTake = $NextTile.Replace('S', '')
            break
        }
        'E' {
            # move to the next tile
            $NextTile = $Map[$yCoordinate][++$xCoordinate]
            # remove the entry direction to determine the next direction
            $DirectionToTake = $NextTile.Replace('W', '')
            break
        }
        'S' {
            # move to the next tile
            $NextTile = $Map[++$yCoordinate][$xCoordinate]
            # remove the entry direction to determine the next direction
            $DirectionToTake = $NextTile.Replace('N', '')
            break
        }
        'W' {
            # move to the next tile
            $NextTile = $Map[$yCoordinate][--$xCoordinate]
            # remove the entry direction to determine the next direction
            $DirectionToTake = $NextTile.Replace('E', '')
            break
        }
    }

    # save the current coordinate as part of the path
    $TileIsPathMap[$yCoordinate][$xCoordinate] = $true

    $StepCount++
} until ($xCoordinate -eq $StartingXCoordinate -and $yCoordinate -eq $StartingYCoordinate)
"Total Path Step Count is:  " + $StepCount

# now replace the starting point with its direction
$Map[$StartingYCoordinate][$StartingXCoordinate] = $StartDirection

<#  How to find tiles "in" the path enclosure
- Check each path line
- Look for "toggle points" -> where the line crosses in and out of the path enclosure
    - These points toggle whether to count non-path tiles as in or out of the enclosure
    - Toggle points are NS tiles in the path, and certain combinations of other tiles in the path that
      create "break chains" where the path crosses the line
        - Break chains are a tile ending in E, followed by any number (0..*) of EW tiles, which are then
          finished with a tile ending in W that has the opposite other direction from the starting tile
            - e.g. NE-EW-SW and SE-NW are break chains, SE-EW-EW-SW and NE-EW...EW-NW are not
- Count non-path tiles that occur when the count of toggle points in a line is odd
#>

# loop through the map, count all tiles "in" the path enclosure
$TilesInPathEnclosure = 0

for ($y = 0; $y -lt $MapYsize; $y++) {
    $TogglePointCount = 0
    $BreakChainStartDirection = ''

    # check each tile in the line
    for ($x = 0; $x -lt $MapXsize; $x++) {
        # check if the tile is part of the path
        if ($TileIsPathMap[$y][$x]) {
            $Tile = $Map[$y][$x]

            # check if the tile is a NS toggle point
            if ($Tile -eq 'NS') {
                # all NS tiles are toggle points
                $TogglePointCount++
                "Counted a NS toggle point at line $y, position $x" | Out-Null
            }
            # check if the tile starts a break chain
            elseif ($Tile -in ('NE', 'SE')) {
                # track the starting direction of the break chain
                $BreakChainStartDirection = $Tile[0]
                "Counted a possible break chain start at line $y, position $x" | Out-Null
            }
            # check if the tile ends a break chain
            elseif ($Tile -in ('NW', 'SW')) {
                "Counted a possible break chain end at line $y, position $x" | Out-Null
                # check if the tile's ending direction makes this a valid break chain
                if ($Tile[0] -ne $BreakChainStartDirection) {
                    # count this break chain as a toggle point
                    $TogglePointCount++
                    "Confirmed a break chain toggle point at line $y, position $x" | Out-Null
                }

                # this always ends the possible chain, clear the start direction
                $BreakChainStartDirection = ''
            }
        }
        # if not a path tile, only count it if we are currently "in" the path enclosure
        # -> we are "in" the enclosure if an odd number of toggle points have been counted on the current line
        elseif ($TogglePointCount % 2 -eq 1) {
            $TilesInPathEnclosure++
            "Counted a tile as ""in"" the path enclosure at line $y, position $x" | Out-Null
        }
    }
}

"Answer is:  " + $TilesInPathEnclosure

# Answers:
# 523 -> CORRECT

<#
S coordinates:  (10,61)
S direction:  NW
Total Path Step Count is:  13514
Answer is:  523
#>