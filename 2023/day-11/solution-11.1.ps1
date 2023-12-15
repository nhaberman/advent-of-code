$InputData = Get-Content -Path ".\2023\day-11\input-11.txt"

# get some useful attributes
$UniverseLength = $InputData.Count
$UniverseWidth = $InputData[0].Length

# find all empty rows
$EmptyRowIndices = @()
for ($row = 0; $row -lt $UniverseLength; $row++) {
    $isEmpty = $true
    for ($col = 0; $col -lt $UniverseWidth; $col++) {
        if ($InputData[$row][$col] -ne '.') {
            $isEmpty = $false
            break
        }
    }

    if ($isEmpty) {
        $EmptyRowIndices += $row
    }
}

# find all empty columns
$EmptyColumnIndices = @()
for ($col = 0; $col -lt $UniverseWidth; $col++) {
    $isEmpty = $true
    for ($row = 0; $row -lt $UniverseLength; $row++) {
        if ($InputData[$row][$col] -ne '.') {
            $isEmpty = $false
            break
        }
    }

    if ($isEmpty) {
        $EmptyColumnIndices += $col
    }
}

"Empty Rows:  " + $EmptyRowIndices -join ','
"Empty Cols:  " + $EmptyColumnIndices -join ','

# expand the universe
$ExpandedUniverse = @()
for ($row = 0; $row -lt $UniverseWidth; $row++) {
    # for empty rows, add two new empty rows
    if ($row -in $EmptyRowIndices) {
        $ExpandedUniverse += '.' * ($UniverseWidth + $EmptyColumnIndices.Length)
        $ExpandedUniverse += '.' * ($UniverseWidth + $EmptyColumnIndices.Length)
    }
    # for all other rows, add the column values
    else {
        $newRow = ''

        for ($col = 0; $col -lt $UniverseLength; $col++) {
            # for empty columns, add two empty spaces
            if ($col -in $EmptyColumnIndices) {
                $newRow += '..'
            }
            # for all other columns, add the original value
            else {
                $newRow += $InputData[$row][$col]
            }
        }

        $ExpandedUniverse += $newRow
    }
}

# print the expanded universe
$ExpandedUniverse | Out-Null

# get the coordinates of galaxies from the universe
$ExpandedUniverseLength = $ExpandedUniverse.Count
$ExpandedUniverseWidth = $ExpandedUniverse[0].Length
$GalaxyCoordinates = @()

for ($row = 0; $row -lt $ExpandedUniverseLength; $row++) {
    for ($col = 0; $col -lt $ExpandedUniverseWidth; $col++) {
        if ($ExpandedUniverse[$row][$col] -eq '#') {
            $Coordinates = @($row, $col)
            $GalaxyCoordinates += , $Coordinates
        }
    }
}

"Galaxies found:  " + $GalaxyCoordinates.Count

# find the shortest path between all the galaxies
$ShortestPath = 0

for ($i = 0; $i -lt $GalaxyCoordinates.Count; $i++) {
    for ($j = $i + 1; $j -lt $GalaxyCoordinates.Count; $j++) {
        $GalaxyA = $GalaxyCoordinates[$i]
        $GalaxyB = $GalaxyCoordinates[$j]

        # calculate the horizontal distance
        $xDistance = [Math]::Abs($GalaxyA[0] - $GalaxyB[0])
        $yDistance = [Math]::Abs($GalaxyA[1] - $GalaxyB[1])

        $ShortestDistance = $xDistance + $yDistance
        "Distance from ($($GalaxyA[0]),$($GalaxyA[1])) to ($($GalaxyB[0]),$($GalaxyB[1])) is: $ShortestDistance" | Out-Null
        $ShortestPath += $ShortestDistance
    }
}

"Answer is:  " + $ShortestPath

# Answers:
# 10228230 -> CORRECT

<#
Empty Rows:  34 37 55 60 82
Empty Cols:  27 35 58 80 93
Galaxies found:  455
Answer is:  10228230
#>