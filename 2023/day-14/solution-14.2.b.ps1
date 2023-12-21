. '.\2023\day-14\day-14-functions.ps1'

$InputData = Get-Content -Path ".\2023\day-14\input-14.txt"

$Platform = @()
foreach ($line in $InputData) {
    $Platform += $line.ToString()
}

# get some useful attributes
$PlatformLength = $Platform.Count
$PlatformWidth = $Platform[0].Length

"The Platform is $PlatformLength rocks long and $PlatformWidth rocks wide."

# tilt the platform for a defined number of cycles (until a pattern emerges)
$CycleCount = 1000000000
"Tilting the platform for $CycleCount cycles."
$PlatformHistory = @()
$FirstMatchingPlatform = 0
$SecondMatchingPlatform = 0

for ($i = 1; $i -le $CycleCount; $i++) {
    Invoke-NorthSpinCycle -Platform ([ref]$Platform)
    Invoke-WestSpinCycle -Platform ([ref]$Platform)
    Invoke-SouthSpinCycle -Platform ([ref]$Platform)
    Invoke-EastSpinCycle -Platform ([ref]$Platform)

    # periodically log progress
    if ($i % 10 -eq 0) {
        "Completed $i tilting cycles."
    }

    # test the current platform against the history to see if any pattern has yet emerged
    $TestResult = Test-PlatformsIfEqual -Platforms $PlatformHistory -PlatformToTest $Platform
    if ($TestResult -ne -1) {
        "Found a recurring platform after $i cycles."
        $FirstMatchingPlatform = $TestResult + 1
        $SecondMatchingPlatform = $i

        "The platforms after cycle $FirstMatchingPlatform and $SecondMatchingPlatform are matches."
        break
    }
    else {
        # save the current platform arrangement in the history and continue
        $CurrentPlatform = @() + $Platform
        $PlatformHistory += , $CurrentPlatform
    }
}

# determine the number of additional cycles needed to orient the platform at the final position
[int]$CyclePeriod = $SecondMatchingPlatform - $FirstMatchingPlatform
[int]$CyclesAfterRecurrenceStarts = $CycleCount - $FirstMatchingPlatform
[int]$RemainingFullCycles = $CyclesAfterRecurrenceStarts / $CyclePeriod
[int]$AdditionalCyclesNeeded = $CyclesAfterRecurrenceStarts - $CyclePeriod * $RemainingFullCycles
"Cycle the platform $AdditionalCyclesNeeded more times to get the final position."

# tilt the platform again until we reach a cycle that will match the final cycle
for ($i = 1; $i -le $AdditionalCyclesNeeded; $i++) {
    Invoke-NorthSpinCycle -Platform ([ref]$Platform)
    Invoke-WestSpinCycle -Platform ([ref]$Platform)
    Invoke-SouthSpinCycle -Platform ([ref]$Platform)
    Invoke-EastSpinCycle -Platform ([ref]$Platform)

    # periodically log progress
    if ($i % 10 -eq 0) {
        "Completed $i additional tilting cycles."
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

    # multiply the count by the distance from the end to get the answer for the row
    $Answer += ($PlatformLength - $row) * $RockCount
}

"Answer is:  " + $Answer

# Answers:
# 88680 -> CORRECT

<#
The Platform is 100 rocks long and 100 rocks wide.
Tilting the platform for 1000000000 cycles.
Completed 10 tilting cycles.
Completed 20 tilting cycles.
Completed 30 tilting cycles.
Completed 40 tilting cycles.
Completed 50 tilting cycles.
Completed 60 tilting cycles.
Completed 70 tilting cycles.
Completed 80 tilting cycles.
Completed 90 tilting cycles.
Completed 100 tilting cycles.
Completed 110 tilting cycles.
Completed 120 tilting cycles.
Completed 130 tilting cycles.
Completed 140 tilting cycles.
Completed 150 tilting cycles.
Found a recurring platform after 150 cycles.
The platforms after cycle 108 and 150 are matches.
Cycle the platform 10 more times to get the final position.
Completed 10 additional tilting cycles.
Answer is:  88680
#>