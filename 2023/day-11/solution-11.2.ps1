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


# get the coordinates of galaxies from the un-expanded universe
$GalaxyCoordinates = @()

for ($row = 0; $row -lt $UniverseLength; $row++) {
    for ($col = 0; $col -lt $UniverseWidth; $col++) {
        if ($InputData[$row][$col] -eq '#') {
            $Coordinates = @($row, $col)
            $GalaxyCoordinates += , $Coordinates
        }
    }
}

"Galaxies found:  " + $GalaxyCoordinates.Count

# find the shortest path between all the galaxies, assuming the universe is expanded
$ShortestPath = 0
$EmptyExpansionFactor = 1000000
"Expansion Factor:  " + $EmptyExpansionFactor

for ($i = 0; $i -lt $GalaxyCoordinates.Count; $i++) {
    for ($j = $i + 1; $j -lt $GalaxyCoordinates.Count; $j++) {
        $GalaxyA = $GalaxyCoordinates[$i]
        $GalaxyB = $GalaxyCoordinates[$j]

        # find the number of empty rows between the two galaxies
        $EmptyRows = 0
        foreach ($EmptyRowIndex in $EmptyRowIndices) {
            if (($GalaxyA[0] -lt $EmptyRowIndex -and $GalaxyB[0] -gt $EmptyRowIndex) -or ($GalaxyB[0] -lt $EmptyRowIndex -and $GalaxyA[0] -gt $EmptyRowIndex)) {
                $EmptyRows++
            }
        }

        # find the number of empty columns between the two galaxies
        $EmptyColumns = 0
        foreach ($EmptyColumnIndex in $EmptyColumnIndices) {
            if (($GalaxyA[1] -lt $EmptyColumnIndex -and $GalaxyB[1] -gt $EmptyColumnIndex) -or ($GalaxyB[1] -lt $EmptyColumnIndex -and $GalaxyA[1] -gt $EmptyColumnIndex)) {
                $EmptyColumns++
            }
        }

        # calculate the horizontal distance
        $xDistance = [Math]::Abs($GalaxyA[0] - $GalaxyB[0]) + $EmptyExpansionFactor * $EmptyRows - $EmptyRows
        $yDistance = [Math]::Abs($GalaxyA[1] - $GalaxyB[1]) + $EmptyExpansionFactor * $EmptyColumns - $EmptyColumns

        $ShortestDistance = $xDistance + $yDistance
        "Distance from ($($GalaxyA[0]),$($GalaxyA[1])) to ($($GalaxyB[0]),$($GalaxyB[1])) is: $ShortestDistance" | Out-Null
        $ShortestPath += $ShortestDistance
    }
}

"Answer is:  " + $ShortestPath

# Answers:
# 82000210 -> X (too low, submitted demo data)
# 00000000 -> CORRECT

<#
Empty Rows:  34 37 55 60 82
Empty Cols:  27 35 58 80 93
Galaxies found:  455
Expansion Factor:  1000000
Answer is:  447073334102
#>