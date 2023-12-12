$InputData = Get-Content -Path ".\day-4\input-4.txt"

$Answer = 0

$InputData | ForEach-Object {
    # split the input to get numbers
    $LineData = $_.Substring(10).Split('|')
    $WinningNumbers = -split $LineData[0]
    $MyNumbers = -split $LineData[1]
    
    # calculate the matches
    $WinningMatches = (Compare-Object $WinningNumbers $MyNumbers -PassThru -IncludeEqual -ExcludeDifferent).Count

    # if any matches, save the game number
    if ($WinningMatches -gt 0) {
        $Answer += [Math]::Pow(2, $WinningMatches - 1)
    }
}

"Answer is:  " + $Answer

# Answers:
# 28750 -> CORRECT