. '.\2023\day-8\lcm-function.ps1'

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

$TargetNodeEnd = "Z"
$CurrentNodeEnd = "A"

# find all starting nodes
$StartingNodes = $Nodes | Where-Object { $_.EndsWith($CurrentNodeEnd) }

<#
For each node, I've determined the path only ends in 'Z' on the final step of the
instructions, and always on the same node (e.g. SBA always ends up at SSZ).

Also, starting with the Z node, it takes the exact same number of cycles to repeat 
and return to the Z node (e.g. SSZ to SSZ takes the same number of cycles as SBA to SSZ)

Therefore, we need to calculate the number of cycles through the instructions to get to
the Z node for each starting A node, find the least common multiple of these numbers,
and multiply them by the number of steps in the instructions to get the total step count.
#>

<#  # testing code to prove points 1 and 2:
# examine each node
$CurrentNode = $StartingNodes[5]
$ResultsAfterInstructions = @()
$Iterations = 1000
$EndingInTarget = @()

# collect test data
for ($i = 0; $i -lt $Iterations; $i++) {
    
    # follow every instruction once and store the result nodes
    for ($j = 0; $j -lt $Instructions.Length; $j++) {
        $Instruction = $Instructions[$j % $Instructions.Length]
        $CurrentNodeIndex = $Nodes.IndexOf($CurrentNode)

        if ($Instruction -eq 'L') {
            $CurrentNode = $LeftElements[$CurrentNodeIndex]
        } elseif ($Instruction -eq 'R') {
            $CurrentNode = $RightElements[$CurrentNodeIndex]
        }

        # track if any pass ends in target ending
        if ($CurrentNode.EndsWith($TargetNodeEnd)) {
            $EndingInTarget += "Instruction Pass:  $i, Step:  $j, Node:  $CurrentNode"
        }
    }

    $ResultsAfterInstructions += $CurrentNode
}

$EndingInTarget
$ResultsAfterInstructions
#>

# for each node, follow the instructions until reaching an end node, and track the cycle counts
$CycleNumbers = @()

foreach ($startingNode in $StartingNodes) {
    
    $CycleCount = 0
    $CurrentNode = $startingNode

    do {
        # perform one cycle through the instructions
        for ($j = 0; $j -lt $Instructions.Length; $j++) {
            $Instruction = $Instructions[$j % $Instructions.Length]
            $CurrentNodeIndex = $Nodes.IndexOf($CurrentNode)

            if ($Instruction -eq 'L') {
                $CurrentNode = $LeftElements[$CurrentNodeIndex]
            } elseif ($Instruction -eq 'R') {
                $CurrentNode = $RightElements[$CurrentNodeIndex]
            }
        }
    
        # increment the cycle count
        $CycleCount++
    } until ($CurrentNode.EndsWith($TargetNodeEnd))

    $CycleNumbers += $CycleCount
}

# get number of steps in the instructions
$InstructionCount = $Instructions.Length

# calculate the LCM of the cycle counts
$Multiples = $CycleNumbers
do {
    $NewMultiples = @()

    for ($i = 0; $i -lt $Multiples.Count; $i += 2) {
        # check if only one more multiple to process
        if ($Multiples.Count -eq $i + 1) {
            # add the final multiple to the results since there are no more to pair it with
            $NewMultiples += $Multiples[$i]
        }
        # if two or more multiples left, process them
        else {
            # find the LCM of the next two multiples
            $NewMultiple = Get-LeastCommonMultiple -FirstNumber $Multiples[$i] -SecondNumber $Multiples[$i + 1]
            $NewMultiples += $NewMultiple
        }
    }

    $Multiples = $NewMultiples
} until ($Multiples.Length -eq 1)

$LCM = $Multiples[0]

# find the answer
$Answer = $LCM * $InstructionCount

"Instruction Count:  $InstructionCount"
for ($nodeNumber = 0; $nodeNumber -lt $StartingNodes.Count; $nodeNumber++) {
    "Cycle Count for node '$($StartingNodes[$nodeNumber])':  $($CycleNumbers[$nodeNumber])"
}
"LCM is:  $LCM"

"Answer is:  " + $Answer

<# Output:
Instruction Count:  269
Cycle Count for node 'BFA':  73
Cycle Count for node 'AAA':  67
Cycle Count for node 'DFA':  79
Cycle Count for node 'XFA':  61
Cycle Count for node 'QJA':  43
Cycle Count for node 'SBA':  53
LCM is:  53715412391
Answer is:  14449445933179
#>

# Answers:
# 14449445933179 -> CORRECT