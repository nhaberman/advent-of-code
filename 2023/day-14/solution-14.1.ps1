$InputData = Get-Content -Path ".\2023\day-14\input-14.txt"

$Platform = @()
foreach ($line in $InputData) {
    $Platform += $line.ToString()
}

# get some useful attributes
$PlatformLength = $Platform.Count
$PlatformWidth = $Platform[0].Length

"The Platform is $PlatformLength rocks long and $PlatformWidth rocks wide."

# loop through each column starting from the bottom and roll rocks north
for ($col = 0; $col -lt $PlatformWidth; $col++) {
    $RollingRocks = 0

    for ($row = $PlatformLength - 1; $row -ge 0; $row--) {
        # take action depending on what is in the space
        $Rock = $Platform[$row][$col]

        switch ($Rock) {
            'O' { 
                # rounded rock, track the rock and clear the space
                $RollingRocks++
                $Platform[$row] = $Platform[$row].Remove($col, 1).Insert($col, '.')
                break
            }
            '#' { 
                # cube-shaped rock, unload any accumulated rocks below it
                for ($i = 0; $i -lt $RollingRocks; $i++) {
                    $Platform[$row + 1 + $i] = $Platform[$row + 1 + $i].Remove($col, 1).Insert($col, 'O')
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
        $Platform[$i] = $Platform[$i].Remove($col, 1).Insert($col, 'O')
    }
}

# calculate the answer
$Answer = 0

for ($row = 0; $row -lt $PlatformLength; $row++) {
    # count the number of round rocks ('O') in the row
    $RockCount = 0
    for ($col = 0; $col -lt $PlatformWidth; $col++) {
        if ($Platform[$row][$col] -eq 'O') {
            $RockCount++
        }
    }

    "There are $RockCount round rocks in row $($row + 1)."

    # multiply the count by the distance from the end to get the answer
    $Answer += ($PlatformLength - $row) * $RockCount
}

"Answer is:  " + $Answer

# Answers:
# 105249 -> CORRECT

<#
The Platform is 100 rocks long and 100 rocks wide.
There are 46 round rocks in row 1.
There are 35 round rocks in row 2.
There are 34 round rocks in row 3.
There are 15 round rocks in row 4.
There are 20 round rocks in row 5.
There are 16 round rocks in row 6.
There are 24 round rocks in row 7.
There are 17 round rocks in row 8.
There are 23 round rocks in row 9.
There are 22 round rocks in row 10.
There are 18 round rocks in row 11.
There are 19 round rocks in row 12.
There are 17 round rocks in row 13.
There are 25 round rocks in row 14.
There are 29 round rocks in row 15.
There are 24 round rocks in row 16.
There are 16 round rocks in row 17.
There are 18 round rocks in row 18.
There are 15 round rocks in row 19.
There are 15 round rocks in row 20.
There are 27 round rocks in row 21.
There are 22 round rocks in row 22.
There are 21 round rocks in row 23.
There are 21 round rocks in row 24.
There are 16 round rocks in row 25.
There are 23 round rocks in row 26.
There are 18 round rocks in row 27.
There are 21 round rocks in row 28.
There are 20 round rocks in row 29.
There are 22 round rocks in row 30.
There are 21 round rocks in row 31.
There are 20 round rocks in row 32.
There are 22 round rocks in row 33.
There are 25 round rocks in row 34.
There are 22 round rocks in row 35.
There are 20 round rocks in row 36.
There are 19 round rocks in row 37.
There are 20 round rocks in row 38.
There are 19 round rocks in row 39.
There are 22 round rocks in row 40.
There are 22 round rocks in row 41.
There are 18 round rocks in row 42.
There are 16 round rocks in row 43.
There are 22 round rocks in row 44.
There are 18 round rocks in row 45.
There are 18 round rocks in row 46.
There are 24 round rocks in row 47.
There are 19 round rocks in row 48.
There are 16 round rocks in row 49.
There are 12 round rocks in row 50.
There are 13 round rocks in row 51.
There are 26 round rocks in row 52.
There are 16 round rocks in row 53.
There are 16 round rocks in row 54.
There are 19 round rocks in row 55.
There are 18 round rocks in row 56.
There are 16 round rocks in row 57.
There are 11 round rocks in row 58.
There are 18 round rocks in row 59.
There are 17 round rocks in row 60.
There are 20 round rocks in row 61.
There are 19 round rocks in row 62.
There are 17 round rocks in row 63.
There are 19 round rocks in row 64.
There are 23 round rocks in row 65.
There are 27 round rocks in row 66.
There are 20 round rocks in row 67.
There are 27 round rocks in row 68.
There are 29 round rocks in row 69.
There are 27 round rocks in row 70.
There are 16 round rocks in row 71.
There are 17 round rocks in row 72.
There are 23 round rocks in row 73.
There are 22 round rocks in row 74.
There are 16 round rocks in row 75.
There are 10 round rocks in row 76.
There are 21 round rocks in row 77.
There are 14 round rocks in row 78.
There are 22 round rocks in row 79.
There are 17 round rocks in row 80.
There are 14 round rocks in row 81.
There are 22 round rocks in row 82.
There are 26 round rocks in row 83.
There are 20 round rocks in row 84.
There are 25 round rocks in row 85.
There are 22 round rocks in row 86.
There are 17 round rocks in row 87.
There are 17 round rocks in row 88.
There are 15 round rocks in row 89.
There are 21 round rocks in row 90.
There are 20 round rocks in row 91.
There are 14 round rocks in row 92.
There are 12 round rocks in row 93.
There are 15 round rocks in row 94.
There are 17 round rocks in row 95.
There are 11 round rocks in row 96.
There are 8 round rocks in row 97.
There are 7 round rocks in row 98.
There are 7 round rocks in row 99.
There are 6 round rocks in row 100.
Answer is:  105249
#>