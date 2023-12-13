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

$StepCount = 0
$TargetNodeEnd = "Z"
$CurrentNodeEnd = "A"

# find all starting nodes
$CurrentNodes = $Nodes | Where-Object { $_.EndsWith($CurrentNodeEnd) }

# traverse the network until all nodes end in Z
$didAllPass = $false
while (!$didAllPass) {
    $didAllPass = $true
    $Instruction = $Instructions[$StepCount % $Instructions.Length]

    for ($i = 0; $i -lt $CurrentNodes.Count; $i++) {
        $CurrentNodeIndex = $Nodes.IndexOf($CurrentNodes[$i])

        if ($Instruction -eq 'L') {
            $CurrentNodes[$i] = $LeftElements[$CurrentNodeIndex]
        } elseif ($Instruction -eq 'R') {
            $CurrentNodes[$i] = $RightElements[$CurrentNodeIndex]
        }

        # check if the node does not pass
        if (!$CurrentNodes[$i].EndsWith($TargetNodeEnd)) {
            $didAllPass = $false
        }
        else {
            "On step $($StepCount + 1), pass $i ends in $TargetNodeEnd"
        }
    }

    # increment the step count
    $StepCount++
}

"Answer is:  " + $StepCount

# Answers:
# 1360000000+ -> (stopped, wrong input)
# will not compute for at least 10 years!!! (~6 hours for 4 billion tests)
