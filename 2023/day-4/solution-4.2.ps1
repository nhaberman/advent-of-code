$InputData = Get-Content -Path ".\day-4\input-4.txt"

$ScratchcardCounts = New-Object int[] $InputData.Count

for ($i = 0; $i -lt $InputData.Count; $i++) {
    # split the input to get numbers
    $LineData = $InputData[$i].Substring(10).Split('|')
    $WinningNumbers = -split $LineData[0]
    $MyNumbers = -split $LineData[1]
    
    # calculate the matches
    $WinningMatches = (Compare-Object $WinningNumbers $MyNumbers -PassThru -IncludeEqual -ExcludeDifferent).Count

    # count the current scratchcard
    $ScratchcardCounts[$i]++

    # if any matches, increment the subsequent matches (times the number for the current scratchcard)
    for ($j = 1; $j -le $WinningMatches; $j++) {
        $ScratchcardCounts[$i + $j] += $ScratchcardCounts[$i]
    }
}

$Answer = ($ScratchcardCounts | Measure-Object -Sum).Sum

"Answer is:  " + $Answer

# Answers:
# 10212704 -> CORRECT