. '.\2023\day-15\day-15-functions.ps1'

$InputData = Get-Content -Path ".\2023\day-15\input-15.txt"

#$InitializationSequence = @('HASH')
$InitializationSequence = $InputData.Split(',')

"Found $($InitializationSequence.Count) steps in the Initialization Sequence."

# run the algorithm on the sequence steps
$Answer = 0
foreach ($step in $InitializationSequence) {
    $HashResult = Invoke-HashAlgorithmOnString -InputString $step
    #"Step '$step' becomes $HashResult."
    $Answer += $HashResult
}

"Answer is:  " + $Answer

# Answers:
# 517965 -> CORRECT

<#
Found 4000 steps in the Initialization Sequence.
Answer is:  517965
#>