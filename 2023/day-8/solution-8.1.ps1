$InputData = Get-Content -Path ".\2023\day-8\input-8.txt"

# get the left/right instructions
$Instructions = $InputData[0]

# get the network
$Nodes = @()
$LeftElements = @()
$RightElements = @()
$RegexPattern = "(?<node>\w{3}) = \((?<left>\w{3}), (?<right>\w{3})\)"
$InputData | ForEach-Object {
    $NetworkLineMatches = (Select-String -InputObject $_ -Pattern $RegexPattern).Matches

    if ($NetworkLineMatches) {
        $Nodes += $NetworkLineMatches.Groups[1].Value
        $LeftElements += $NetworkLineMatches.Groups[2].Value
        $RightElements += $NetworkLineMatches.Groups[3].Value
    }
}

# traverse the network until reaching ZZZ
$StepCount = 0
$TargetNode = "ZZZ"
$CurrentNode = "AAA"

while ($CurrentNode -ne $TargetNode) {
    $Instruction = $Instructions[$StepCount % $Instructions.Length]
    $CurrentNodeIndex = $Nodes.IndexOf($CurrentNode)

    if ($Instruction -eq 'L') {
        $CurrentNode = $LeftElements[$CurrentNodeIndex]
    } elseif ($Instruction -eq 'R') {
        $CurrentNode = $RightElements[$CurrentNodeIndex]
    }

    # increment the step count
    $StepCount++
}

"Answer is:  " + $StepCount

# Answers:
# 1360000000+ -> (stopped, wrong input)
# 18023 -> CORRECT
#>