. '.\2023\day-14\day-14-functions.ps1'

$InputData = Get-Content -Path ".\2023\day-14\input-14-demo.txt"

$Platform = @()
foreach ($line in $InputData) {
    $Platform += $line.ToString()
}

# get some useful attributes
$PlatformLength = $Platform.Count
$PlatformWidth = $Platform[0].Length

"The Platform is $PlatformLength rocks long and $PlatformWidth rocks wide."

# spin the platform for a defined number of cycles
$CycleCount = 1000000000
"Tilting the platform for $CycleCount cycles."

for ($i = 0; $i -lt $CycleCount; $i++) {
    Invoke-NorthSpinCycle -Platform ([ref]$Platform)
    Invoke-WestSpinCycle -Platform ([ref]$Platform)
    Invoke-SouthSpinCycle -Platform ([ref]$Platform)
    Invoke-EastSpinCycle -Platform ([ref]$Platform)

    # periodically log progress
    if ($i % 1000 -eq 999) {
        "Completed $($i + 1) tilting cycles."
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

    #"There are $RockCount round rocks in row $($row + 1)."

    # multiply the count by the distance from the end to get the answer
    $Answer += ($PlatformLength - $row) * $RockCount
}

"Answer is:  " + $Answer